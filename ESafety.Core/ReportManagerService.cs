using ESafety.Core.Model;
using ESafety.Core.Model.DB.Platform;
using ESafety.Core.Model.ReportManager;
using ESafety.ORM;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core
{
    public class ReportManagerService : ORM.ServiceBase,IReportManager
    {

        private IUnitwork _work = null;

        private IRepository< RPTInfo> _rpsreport = null;
        private IRepository< RPTColumn> _rpscolumn = null;
        private IRepository< RPTParameter> _rpsparameter = null;
        private IRepository< RPTChildrenTable> _rpstable = null;
        private IRepository< RPTChildrenColumn> _rpstablecolumn = null;
        private IRepository< RPTAccountScope> _rpsscope = null;


        public ReportManagerService(ORM.IUnitwork work)
        {
            _work = work;
            Unitwork = work;

            _rpscolumn = work.Repository< RPTColumn>();
            _rpsparameter = work.Repository< RPTParameter>();
            _rpsreport = work.Repository< RPTInfo>();
            _rpsscope = work.Repository< RPTAccountScope>();
            _rpstable = work.Repository< RPTChildrenTable>();
            _rpstablecolumn = work.Repository< RPTChildrenColumn>();

        }

        /// <summary>
        /// 新建报表列
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public ActionResult<bool> AddColumn(ReportColumnNew column)
        {
            var report = _rpsreport.GetModel(q => q.ID == column.ReportID);
            if (report == null)
            {
                throw new Exception("报表不存在");
            }
            if (_rpscolumn.Any(q => q.ColumnName == column.ColumnName && q.ReportID == column.ReportID))
            {
                throw new Exception("该报表已经存在列名：" + column.ColumnName);
            }


            var dbcolumn = new  RPTColumn();
            dbcolumn = column.MAPTO<RPTColumn>();
            
            _rpscolumn.Add(dbcolumn);
            _work.Commit();

            return new ActionResult<bool>(true);
        }

        /// <summary>
        /// 新建报表参数
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public ActionResult<bool> AddParameter(ParameterNew parameter)
        {
            var report = _rpsreport.GetModel(q => q.ID == parameter.ReportID);
            if (report == null)
            {
                throw new Exception("报表不存在");
            }
            if (_rpsparameter.Any(q => q.ParameterName == parameter.ParameterName && q.ReportID == parameter.ReportID))
            {
                throw new Exception("该报表已经存在参数：" + parameter.ParameterName);
            }
            if (parameter.ParameterType == PublicEnum.ReportParameterType.Combox &&
               string.IsNullOrEmpty(parameter.TypeListTypeSource))
            {
                throw new Exception("列表类型参数必须定义参数数据源");
            }


            var dbparameter =parameter.MAPTO< RPTParameter>();
             
            dbparameter.TypeListSource = parameter.TypeListTypeSource;
            //如果不是下拉框就赋值为空
            if (parameter.ParameterType != PublicEnum.ReportParameterType.Combox)
            {
                dbparameter.TypeListSource = "";
                dbparameter.TypeListValueType = 0;
            }
            _rpsparameter.Add(dbparameter);
            _work.Commit();

            return new ActionResult<bool>(true);


        }
        /// <summary>
        /// 新建报表
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public ActionResult<bool> AddReportInfo(ReportInfoNew report)
        {
            if (_rpsreport.Any(q => q.ReportName == report.ReportName))
            {
                throw new Exception("报表名称：" + report.ReportName + "已经存在");
            }
            var dbreport =report.MAPTO<RPTInfo>(); 
            dbreport.State = 1;
            dbreport.CreateMan = AppUser.UserProfile.CNName;
            _rpsreport.Add(dbreport);
            _work.Commit();

            return new ActionResult<bool>(true);

        }
        /// <summary>
        /// 新建子表
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public ActionResult<bool> AddTable(ChildeTableNew table)
        {
            var report = _rpsreport.GetModel(q => q.ID == table.ReportID);
            if (report == null)
            {
                throw new Exception("报表不存在");
            }
            if (_rpstable.Any(q => q.ReportID == table.ReportID && q.ChildeCaption == table.ChildeCaption))
            {
                throw new Exception("报表已经存在标题：" + table.ChildeCaption);
            }
            var dbtable =table.MAPTO<RPTChildrenTable>(); 

            _rpstable.Add(dbtable);

            _work.Commit();

            return new ActionResult<bool>(true);

        }
        /// <summary>
        /// 新建子表列
        /// </summary>
        /// <param name="tablecolumn"></param>
        /// <returns></returns>
        public ActionResult<bool> AddTableColumn(ChildeTableColumnNew tablecolumn)
        {
            var report = _rpsreport.GetModel(q => q.ID == tablecolumn.ReportID);
            if (report == null)
            {
                throw new Exception("报表不存在");
            }
            var table = _rpstable.GetModel(q => q.ID == tablecolumn.ChildeID);
            if (table == null)
            {
                throw new Exception("子表不存在");
            }
            if (_rpstablecolumn.Any(q => q.ID == tablecolumn.ChildeID && q.ColumnName == tablecolumn.ColumnName))
            {
                throw new Exception("子表已经存在列：" + tablecolumn.ColumnName);
            }

            var dbtablecolum =tablecolumn.MAPTO<RPTChildrenColumn>();
             
            _rpstablecolumn.Add(dbtablecolum);

            _work.Commit();

            return new ActionResult<bool>(true);


        }
        /// <summary>
        /// 删除报表列
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelColumn(Guid id)
        {
            var column = _rpscolumn.GetModel(q => q.ID == id);
            if (column == null)
            {
                throw new Exception("列不存在");
            }

            _rpscolumn.Delete(column);
            _work.Commit();

            return new ActionResult<bool>(true);

        }
        /// <summary>
        /// 删除报表参数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelParameter(Guid id)
        {
            var parameter = _rpsparameter.GetModel(q => q.ID == id);
            if (parameter == null)
            {
                throw new Exception("参数不存在");
            }

            _rpsparameter.Delete(parameter);
            _work.Commit();
            return new ActionResult<bool>(true);

        }
        /// <summary>
        /// 删除报表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelReportInfo(Guid id)
        {
            var report = _rpsreport.GetModel(q => q.ID == id);
            if (report == null)
            {
                throw new Exception("报表不存在");
            }
            if (_rpscolumn.Any(q => q.ReportID == id))
            {
                throw new Exception("报表存在列，请先删除列");
            }
            if (_rpsparameter.Any(q => q.ReportID == id))
            {
                throw new Exception("报表存在参数，请先删除参数");
            }
            if (_rpstable.Any(q => q.ReportID == id))
            {
                throw new Exception("报表存在子表，请先删除子表");
            }

            _rpsreport.Delete(report);

            _work.Commit();

            return new ActionResult<bool>(true);

        }

        public ActionResult<bool> DelReportScope(Guid scopeid)
        {
            var re = _work.Repository< RPTAccountScope>().Delete(q => q.ID == scopeid);
            _work.Commit();

            return new ActionResult<bool>(re > 0);
        }

        /// <summary>
        /// 删除报表子表
        /// </summary>
        /// <param name="tableid"></param>
        /// <returns></returns>
        public ActionResult<bool> DelTable(Guid tableid)
        {
            var table = _rpstable.GetModel(q => q.ID == tableid);

            if (table == null)
            {
                throw new Exception("子表不存在");
            }
            if (_rpstablecolumn.Any(q => q.ChildeID == tableid))
            {
                throw new Exception("子表存在列，请先删除子表列");
            }

            _rpstable.Delete(table);
            _work.Commit();
            return new ActionResult<bool>(true);


        }
        /// <summary>
        /// 删除报表子表列
        /// </summary>
        /// <param name="tablecolumnid"></param>
        /// <returns></returns>
        public ActionResult<bool> DelTableColumn(Guid tablecolumnid)
        {
            var tablecolum = _rpstablecolumn.GetModel(q => q.ID == tablecolumnid);
            if (tablecolum == null)
            {
                throw new Exception("子表列不存在");
            }

            _rpstablecolumn.Delete(tablecolum);
            _work.Commit();

            return new ActionResult<bool>(true);

        }
        /// <summary>
        /// 修改报表列
        /// </summary>
        /// <param name="updater"></param>
        /// <returns></returns>
        public ActionResult<bool> EditColumn(ReportColumnEdit updater)
        {
            var column = _rpscolumn.GetModel(q => q.ID == updater.ID);
            if (column == null)
            {
                throw new Exception("列不存在");
            }
            if (!_rpsreport.Any(q => q.ID == column.ReportID))
            {
                throw new Exception("报表不存在");
            }
            column = updater.MAPTO<RPTColumn>();

            _rpscolumn.Update(column);

            _work.Commit();
            return new ActionResult<bool>(true);

        }
        /// <summary>
        /// 修改报表参数
        /// </summary>
        /// <param name="updater"></param>
        /// <returns></returns>
        public ActionResult<bool> EditParameter(ParemeterEdit updater)
        {
            var meter = _rpsparameter.GetModel(q => q.ID == updater.ID);
            if (meter == null)
            {
                throw new Exception("报表参数不存在");
            }
            if (!_rpsreport.Any(q => q.ID == meter.ReportID))
            {
                throw new Exception("报表不存在");
            }
            meter = updater.MAPTO<RPTParameter>(); 

            meter.TypeListSource = updater.TypeListTypeSource;
            _rpsparameter.Update(meter);

            _work.Commit();
            return new ActionResult<bool>(true);
        }
        /// <summary>
        /// 修改报表
        /// </summary>
        /// <param name="updater"></param>
        /// <returns></returns>
        public ActionResult<bool> EditReportInfo(ReportInfoEdit updater)
        {
            var report = _rpsreport.GetModel(q => q.ID == updater.ID);
            if (report == null)
            {
                throw new Exception("报表不存在");
            }
            if (_rpsreport.Any(q => q.ReportName == updater.ReportName && q.ID != updater.ID))
            {
                throw new Exception("报表名称已经存在");
            }
            var dbreport = updater.MAPTO< RPTInfo>();
            
            _rpsreport.Update(report);

            _work.Commit();

            return new ActionResult<bool>(true);


        }
        /// <summary>
        /// 修改报表子表
        /// </summary>
        /// <param name="updater"></param>
        /// <returns></returns>
        public ActionResult<bool> EditTable(ChildeTableEdit updater)
        {
            var report = _rpsreport.GetModel(q => q.ID == updater.ReportID);
            if (report == null)
            {
                throw new Exception("报表不存在");
            }
            var dbtable = _rpstable.GetModel(q => q.ID == updater.ID);
            if (dbtable == null)
            {
                throw new Exception("子表不存在");
            }
            if (_rpstable.Any(q => q.ChildeCaption == updater.ChildeCaption && q.ID != updater.ID && q.ReportID == updater.ReportID))
            {
                throw new Exception("报表已经存在子表标题：" + updater.ChildeCaption);
            }
            dbtable = updater.MAPTO<RPTChildrenTable>();
             
            _rpstable.Update(dbtable);

            _work.Commit();

            return new ActionResult<bool>(true);

        }
        /// <summary>
        /// 修改报表子表列
        /// </summary>
        /// <param name="updater"></param>
        /// <returns></returns>
        public ActionResult<bool> EditTableColumn(ChildeTableColumnEdit updater)
        {
            var tcolumn = _rpstablecolumn.GetModel(q => q.ID == updater.ID);
            if (tcolumn == null)
            {
                throw new Exception("子表列不存在");
            }
            if (!_rpsreport.Any(q => q.ID == updater.ReportID))
            {
                throw new Exception("报表不存在");
            }
            if (_rpstablecolumn.Any(q => q.ID != updater.ID && q.ReportID == updater.ReportID && q.ColumnName == updater.ColumnName))
            {
                throw new Exception("子表列名：" + updater.ColumnName + "已经存在");
            }

            tcolumn = updater.MAPTO<RPTChildrenColumn>();
             
            _rpstablecolumn.Update(tcolumn);

            _work.Commit();

            return new ActionResult<bool>(true);


        }
        /// <summary>
        /// 获取指定报表ID的列集合
        /// </summary>
        /// <param name="reportid"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<ReportColumnListView>> GetColumnList(Guid reportid)
        {
            var column = _rpscolumn.GetList(q => q.ReportID == reportid);
            var re = from col in column
                     select new ReportColumnListView
                     {
                         ColumnInfo = col,
                         DataTypeStr =
                           (PublicEnum.ReportColumnDataType)col.DataType == PublicEnum.ReportColumnDataType.Bool ? "布尔" :
                           (PublicEnum.ReportColumnDataType)col.DataType == PublicEnum.ReportColumnDataType.Date ? "日期" :
                           (PublicEnum.ReportColumnDataType)col.DataType == PublicEnum.ReportColumnDataType.GUID ? "Guid" :
                           (PublicEnum.ReportColumnDataType)col.DataType == PublicEnum.ReportColumnDataType.Int ? "整数" :
                           (PublicEnum.ReportColumnDataType)col.DataType == PublicEnum.ReportColumnDataType.Num ? "数字" :
                           (PublicEnum.ReportColumnDataType)col.DataType == PublicEnum.ReportColumnDataType.Str ? "字符" : "未知"
                     };

            return new ActionResult<IEnumerable<ReportColumnListView>>(re);


        }
        /// <summary>
        /// 获取指定报表ID的参数集合
        /// </summary>
        /// <param name="reportid"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<ParameterList>> GetParameterList(Guid reportid)
        {
            var parameters = _rpsparameter.GetList(q => q.ReportID == reportid);
            var re = from pa in parameters
                     select new ParameterList
                     {
                         TypeListValueTypeStr =
                         (PublicEnum.ReportColumnDataType)pa.TypeListValueType == PublicEnum.ReportColumnDataType.Bool ? "布尔" :
                            (PublicEnum.ReportColumnDataType)pa.TypeListValueType == PublicEnum.ReportColumnDataType.Date ? "日期" :
                            (PublicEnum.ReportColumnDataType)pa.TypeListValueType == PublicEnum.ReportColumnDataType.GUID ? "Guid" :
                            (PublicEnum.ReportColumnDataType)pa.TypeListValueType == PublicEnum.ReportColumnDataType.Int ? "整数" :
                            (PublicEnum.ReportColumnDataType)pa.TypeListValueType == PublicEnum.ReportColumnDataType.Num ? "数字" :
                            (PublicEnum.ReportColumnDataType)pa.TypeListValueType == PublicEnum.ReportColumnDataType.Str ? "字符" : "未知",
                         ParameterInfo = pa,
                         ParameterTypeStr =
                           (PublicEnum.ReportParameterType)pa.ParameterType == PublicEnum.ReportParameterType.Bool ? "布尔" :
                            (PublicEnum.ReportParameterType)pa.ParameterType == PublicEnum.ReportParameterType.Date ? "日期" :
                            (PublicEnum.ReportParameterType)pa.ParameterType == PublicEnum.ReportParameterType.Combox ? "列表" :
                            (PublicEnum.ReportParameterType)pa.ParameterType == PublicEnum.ReportParameterType.Int ? "整数" :
                            (PublicEnum.ReportParameterType)pa.ParameterType == PublicEnum.ReportParameterType.Num ? "数字" :
                            (PublicEnum.ReportParameterType)pa.ParameterType == PublicEnum.ReportParameterType.Str ? "字符" : "未知"
                     };
            return new ActionResult<IEnumerable<ParameterList>>(re);
        }
        /// <summary>
        /// 获取报表集哈
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<ReportInfoListView>> ReportSelector()
        {
            var reports = _rpsreport.GetList();

            var re = from rp in reports
                     select new ReportInfoListView
                     {
                         ReportInfo = rp,
                         ScopTypeStr =
                           (PublicEnum.ReortScopeType)rp.ScopeType == PublicEnum.ReortScopeType.Global ? "所有账套" :
                           (PublicEnum.ReortScopeType)rp.ScopeType == PublicEnum.ReortScopeType.Range ? "指定账套" : "未知",
                         StateStr =
                            (PublicEnum.GenericState)rp.State == PublicEnum.GenericState.Cancel ? "停用" :
                            (PublicEnum.GenericState)rp.State == PublicEnum.GenericState .Normal ? "正常" : "未知"
                     };

            return new ActionResult<IEnumerable<ReportInfoListView>>(re);

        }
        /// <summary>
        /// 获取指定子表id的子表列集合
        /// </summary>
        /// <param name="tableid"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<ChildeTableColumnList>> GetTableColumnList(Guid tableid)
        {
            var tcolumns = _rpstablecolumn.GetList(q => q.ChildeID == tableid);
            var re = from col in tcolumns
                     select new ChildeTableColumnList
                     {
                         ChildeColumnInfo = col,
                         DataTypeStr = col.DataType == (int)PublicEnum.ReportColumnDataType.Bool ? "布尔" :
                         col.DataType == (int)PublicEnum.ReportColumnDataType.Date ? "日期" :
                         col.DataType == (int)PublicEnum.ReportColumnDataType.GUID ? "Guid类型" :
                         col.DataType == (int)PublicEnum.ReportColumnDataType.Int ? "速数" :
                         col.DataType == (int)PublicEnum.ReportColumnDataType.Num ? "数字" :
                         col.DataType == (int)PublicEnum.ReportColumnDataType.Str ? "字符" : "未知"
                     };
            return new ActionResult<IEnumerable<ChildeTableColumnList>>(re);

        }
        /// <summary>
        /// 获取指定报表id的子表集合
        /// </summary>
        /// <param name="reportid"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<ChildeTableList>> GetTableList(Guid reportid)
        {
            var tables = _rpstable.GetList(q => q.ReportID == reportid);
            var re = from tb in tables
                     select new ChildeTableList
                     {
                         ChildeTableInfo = tb
                     };

            return new ActionResult<IEnumerable<ChildeTableList>>(re);

        }
        /// <summary>
        /// 设置报表的作用域
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<bool> SetReportScope(SetReportScope para)
        {
            var s = _rpsscope.GetModel(q => q.ReportID == para.ReportID);
            if (_rpsscope.Any(q => q.AccountCode == para.AccountCodes && q.ReportID == para.ReportID))
            {
                throw new Exception("已经存在");
            }
            var dbreport = new RPTAccountScope();
            dbreport.ReportID = para.ReportID;
            dbreport.AccountCode = para.AccountCodes; 

            _rpsscope.Add(dbreport);
            _work.Commit();

            return new ActionResult<bool>(true);
        }
        /// <summary>
        /// 报表列表
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<ReportInfoListView>> GetReportList(PagerQuery<string> para)
        {
            var re = ReportSelector();

            var retmp = re.data.Where(q => q.ReportInfo.ReportName.Contains(para.KeyWord));

            var result = new Pager<ReportInfoListView>().GetCurrentPage(retmp, para.PageSize, para.PageIndex);

            return new ActionResult<Pager<ReportInfoListView>>(result);


        }
        /// <summary>
        /// 作用域列表
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<reptlist>> reptlist()
        {
            var re = from tb in _rpsscope.GetList()
                     select new reptlist
                     {
                         ReportID = tb.ReportID,
                         AccountCode = tb.AccountCode
                     };

            return new ActionResult<IEnumerable<reptlist>>(re);
        }
        //自定义报表状态
        public ActionResult<bool> StateSet(PublicEnum.GenericState state, Guid ID)
        {
            var em = _rpsreport.GetModel(q => q.ID == ID);
            if (em == null)
            {
                throw new Exception("报表不存在");
            }
            em.State = (int)state;
            _rpsreport.Update(em);
            _work.Commit();
            return new ActionResult<bool>(true);
        }
    }
}
