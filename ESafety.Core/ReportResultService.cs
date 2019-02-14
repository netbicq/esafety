using Dapper;
using ESafety.Core.Model;
using ESafety.Core.Model.DB.Platform;
using ESafety.Core.Model.ReportResult;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core
{
    /// <summary>
    /// 报表执行
    /// </summary>
    public class ReportResultService : ORM.ServiceBase, IReportResult
    {


        private ORM.IUnitwork _work = null;



        public ReportResultService(ORM.IUnitwork work)
        {
            _work = work;
            Unitwork = work;

        }
        /// <summary>
        /// 获取指定报表ID的执行参数信息及报表相关信息
        /// </summary>
        /// <param name="reportid"></param>
        /// <returns></returns>
        public virtual ActionResult<ExcuteReportView> GetExcuteReportInfo(Guid reportid)
        {
            //基于系统账套操作
            _work.SetUserDB(null);

            var repparas = _work.Repository< RPTParameter>().GetList(q => q.ReportID == reportid);
            var repinfo = _work.Repository< RPTInfo>().GetModel(q => q.ID == reportid);

            ExcuteReportView re = new ExcuteReportView();
            re.ReportInfo = repinfo;
            re.Parameters = (from rep in repparas

                             select new Parameter
                             {
                                 DataSource = null,
                                 ParameterCaption = rep.ParameterCaption,
                                 ParameterID = rep.ID,
                                 ParameterName = rep.ParameterName,
                                 ParameterType = (PublicEnum.ReportParameterType)rep.ParameterType
                             }).ToList();

            foreach (var p in re.Parameters)
            {
                if (p.ParameterType == PublicEnum.ReportParameterType.Combox)
                {
                    var pinfo = _work.Repository< RPTParameter>().GetModel(q => q.ID == p.ParameterID);
                    if (pinfo != null)
                    {
                        _work.SetUserDB(AppUser.UserDB);
                        SqlMapper.GridReader gr = _work.ExecProcedre("Exec " + pinfo.TypeListSource);

                        p.DataSource = gr.Read<ParameterListSource>();
                        _work.SetUserDB(null);
                    }
                }
            }

            return new ActionResult<ExcuteReportView>(re);

        }

        public virtual ActionResult<IEnumerable<RPTInfo>> GetReports(Guid accountid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 执行报表，并返回结果
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public virtual ActionResult<ReportResult> ReportResult(PagerQuery<ExcuteReprot> para)
        {
            //基于系统账套操作，对报表定义需要持久化
            _work.SetUserDB(null);

            var re = new ReportResult();

            //报表信息
            var reportinfo = _work.Repository< RPTInfo>().GetModel(q => q.ID == para.Query.ReprotID);
            if (reportinfo == null)
            {
                throw new Exception("未找到指定的报表定义");
            }
            re.ReportInfo = reportinfo;

            //报表列
            var repcols = _work.Repository< RPTColumn>().GetList(q => q.ReportID == para.Query.ReprotID);
            if (repcols == null)
            {
                throw new Exception("报表未定义任何列");
            }
            if (!repcols.Any())
            {
                throw new Exception("报表未定义任何列");
            }
            re.ReportColumns = (from col in repcols
                                orderby col.OrderIndex
                                select new ReportResultColumns
                                {
                                    ColumnCaption = col.ColumnCaption,
                                    ColumnName = col.ColumnName,
                                    Visiable = col.Visiable
                                }).ToList();
            //报表子表
            var reptables = _work.Repository< RPTChildrenTable>().GetList(q => q.ReportID == para.Query.ReprotID);
            var reptbcols = _work.Repository< RPTChildrenColumn>().GetList(q => q.ReportID == para.Query.ReprotID);

            re.ChildeTables = (from tb in reptables
                               let cols = reptbcols.Where(q => q.ChildeID == tb.ID)
                               select new ReportResultChildeTable
                               {
                                   TableColumns = null,
                                   TableInfo = tb,
                                   TableData = null
                               }).ToList();
            //子表列
            foreach (var tb in re.ChildeTables)
            {
                if (!reptbcols.Any(q => q.ChildeID == tb.TableInfo.ID))
                {
                    throw new Exception("子表:" + tb.TableInfo.ChildeCaption + " 未定义任何列");
                }
                tb.TableColumns = (from c in reptbcols.Where(q => q.ChildeID == tb.TableInfo.ID)
                                   select new ReportResultColumns
                                   {
                                       ColumnCaption = c.ColumnCaption,
                                       ColumnName = c.ColumnName,
                                       Visiable = c.Visiable
                                   }).ToList();
            }

            //报表参数

            //有参数和没参数分开处理
            _work.SetUserDB(AppUser.UserDB);

            SqlMapper.GridReader result = null;// _work.ExecProcedre("Exec " + reportinfo.DataSource);

            if (para.Query.Parameters.Any())
            {
                var reppara = (from p in para.Query.Parameters
                               select new ReportPara
                               {
                                   ParaName = p.ParameterName,
                                   ParaValue = p.ParameterValue,
                                   ReportID = reportinfo.DataSource
                               }).ToList();
                var outlist = new List<string>();
                string sql = "Exec " + reportinfo.DataSource + " ";
                foreach (var p in reppara)
                {
                    sql += " @" + p.ParaName + "=@" + p.ParaName + ",";
                }
                sql = sql.Substring(0, sql.Length - 1);

                var expara = Command.DynamicLinqSingle(reppara, new List<string> { "ReportID" }, "ParaName", out outlist);


                result = _work.ExecProcedre(sql, expara[0]);

            }
            else
            {
                result = _work.ExecProcedre("Exec " + reportinfo.DataSource);

            }
            var data = result.Read();
            foreach (var tb in re.ChildeTables)
            {
                tb.TableData = result.Read();
            }
            //导出
            string excel = "";
            if (para.ToExcel)
            {
                List<string> han = new List<string>();
                foreach (var item in re.ReportColumns)
                {
                    han.Add(item.ColumnCaption);
                }
                List<List<string>> obj = new List<List<string>>();
                foreach (var item in data)
                {
                    List<string> lai = new List<string>();
                    foreach (var ben in item)
                    {
                        if (ben.Value != null)
                        {
                            lai.Add(ben.Value.ToString());
                        }
                        else
                        {
                            lai.Add(string.Empty);
                        }
                    }
                    obj.Add(lai);
                }
                excel = Command.CreateExcel(obj, han, AppUser.OutPutPaht);
            }
            re.ReprotData = new Pager<dynamic>().GetCurrentPage(data, para.PageSize, para.PageIndex);
            re.ReprotData.ExcelResult = excel;


            return new ActionResult< ReportResult>(re);

        }
    }

    public class Table
    {
        public string Value { get; set; }
    }
    public class ReportPara
    {

        public string ParaName { get; set; }


        public object ParaValue { get; set; }

        public string ReportID { get; set; }
    }
} 
