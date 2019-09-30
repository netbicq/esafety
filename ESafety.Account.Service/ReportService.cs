using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core;
using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.DB.Account;
using ESafety.ORM;
using ESafety.Unity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Service
{
    /// <summary>
    /// 报表
    /// </summary>
    public class ReportService : ServiceBase, IService.IReport
    {
        private IRepository<Basic_DangerPoint> rpsDP = null;
        private IRepository<Basic_DangerPointRelation> rpsDPR = null;
        private IRepository<Basic_Employee> rpsEmp = null;

        private IRepository<Basic_Danger> rpsDanger = null;
        private IRepository<Basic_DangerRelation> rpsDR = null;
        private IRepository<Basic_Dict> rpsDict = null;
        private IRepository<Basic_Org> rpsOrg = null;
        private IRepository<Basic_Post> rpsPost = null;
        private IRepository<Bll_AttachFile> rpsFile = null;


        private IRepository<Basic_Opreation> rpsOpreate = null;
        private IRepository<Basic_OpreationFlow> rpsOF = null;


        private IRepository<Bll_TroubleControl> rpsTCtr = null;
        private IRepository<Bll_TroubleControlDetails> rpsTCD = null;
        private IRepository<Bll_TroubleControlFlows> rpsTCF = null;
        private IRepository<Bll_InspectTaskSubject> rpsTS = null;

        private IRepository<Basic_DangerSafetyStandards> rpsDSS = null;
        private IRepository<Basic_SafetyStandard> rpsSS = null;

        private IRepository<Bll_TaskBillSubjects> rpsTBS = null;
        private IRepository<Bll_TaskBill> rpsTB = null;

        private IUnitwork work = null;

        public ReportService(IUnitwork _work)
        {
            work = _work;
            Unitwork = _work;
            rpsDP = _work.Repository<Basic_DangerPoint>();
            rpsEmp = _work.Repository<Basic_Employee>();
            rpsDict = _work.Repository<Basic_Dict>();
            rpsDPR = _work.Repository<Basic_DangerPointRelation>();
            rpsDanger = _work.Repository<Basic_Danger>();
            rpsOrg = _work.Repository<Basic_Org>();
            rpsDR = _work.Repository<Basic_DangerRelation>();
            rpsPost = _work.Repository<Basic_Post>();
            rpsFile = _work.Repository<Bll_AttachFile>();
            rpsOpreate = _work.Repository<Basic_Opreation>();
            rpsOF = _work.Repository<Basic_OpreationFlow>();

            rpsTCtr = _work.Repository<Bll_TroubleControl>();
            rpsTCD = _work.Repository<Bll_TroubleControlDetails>();
            rpsTCF = _work.Repository<Bll_TroubleControlFlows>();
            rpsTS = _work.Repository<Bll_InspectTaskSubject>();
            rpsTBS = _work.Repository<Bll_TaskBillSubjects>();
            rpsTB = _work.Repository<Bll_TaskBill>();
            rpsDSS = _work.Repository<Basic_DangerSafetyStandards>();
            rpsSS = _work.Repository<Basic_SafetyStandard>();

        }
        /// <summary>
        /// 隐患整改情况
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult<Pager<CtrReport>> GetCtrReport(PagerQuery<CtrReportQuery> query)
        {
            try
            {
                var ctrs = rpsTCtr.Queryable();
                var subs = rpsTCD.Queryable();
                var emps = rpsEmp.Queryable();
                var ctrfs = rpsTCF.Queryable();
                var dps = rpsDP.Queryable();
                var dprs = rpsDPR.Queryable();
                var checks = rpsTBS.Queryable();
                var bills = rpsTB.Queryable();
                var dangers = rpsDanger.Queryable();
                var TLevels = rpsDict.Queryable(p => p.ParentID == OptionConst.TroubleLevel);

                var retemp = from ctr in ctrs
                             let dp = dps.FirstOrDefault(p => p.ID == ctr.DangerPoint)
                             let tcd = subs.FirstOrDefault(p => p.TroubleControlID == ctr.ID)
                             let subid = checks.FirstOrDefault(p => p.ID == tcd.BillSubjectsID)
                             let sub = dprs.FirstOrDefault(p => p.SubjectID == subid.SubjectID)
                             let danger = dangers.FirstOrDefault(p => p.ID == subid.DangerID)
                             let bill = bills.FirstOrDefault(p => p.ID == ctr.BillID)
                             let bemp = emps.FirstOrDefault(p => p.ID == bill.EmployeeID)
                             let pemp = emps.FirstOrDefault(p => p.ID == ctr.PrincipalID)
                             let aemp = emps.FirstOrDefault(p => p.ID == ctr.AcceptorID)
                             let ctrf = ctrfs.OrderByDescending(o => o.FlowDate).FirstOrDefault(p => p.ControlID == ctr.ID && p.FlowType == (int)PublicEnum.EE_TroubleFlowState.TroubleR)
                             let tlv = TLevels.FirstOrDefault(p => p.ID == ctr.TroubleLevel)
                             orderby tlv.MinValue descending
                             select new CtrReport
                             {
                                 DangerPoint = dp.Name,
                                 Subject = sub.SubjectName,
                                 Danger = danger.Name,
                                 BEmp = bemp.CNName,
                                 CheckResult = subid.TaskResultMemo,
                                 Principal = pemp.CNName,
                                 CreateDate = ctr.CreateDate,
                                 CtrTarget = ctr.ControlDescription,
                                 Acceptor = aemp == null ? "" : aemp.CNName,
                                 IsAccepte = (PublicEnum.EE_TroubleState)ctr.State,
                                 AccepteDate = ctrf == null ? null : (DateTime?)ctrf.FlowDate,
                                 TLevel = tlv.DictName,
                             };


                string excel = "";
                if (query.ToExcel)
                {
                    var exc = from ctr in ctrs
                              let dp = dps.FirstOrDefault(p => p.ID == ctr.DangerPoint)
                              let tcd = subs.FirstOrDefault(p => p.TroubleControlID == ctr.ID)
                              let subid = checks.FirstOrDefault(p => p.ID == tcd.BillSubjectsID)
                              let sub = dprs.FirstOrDefault(p => p.SubjectID == subid.SubjectID)
                              let danger = dangers.FirstOrDefault(p => p.ID == subid.DangerID)
                              let bill = bills.FirstOrDefault(p => p.ID == ctr.BillID)
                              let bemp = emps.FirstOrDefault(p => p.ID == bill.EmployeeID)
                              let pemp = emps.FirstOrDefault(p => p.ID == ctr.PrincipalID)
                              let aemp = emps.FirstOrDefault(p => p.ID == ctr.AcceptorID)
                              let ctrf = ctrfs.OrderByDescending(o => o.FlowDate).FirstOrDefault(p => p.ControlID == ctr.ID && p.FlowType == (int)PublicEnum.EE_TroubleFlowState.TroubleR)
                              let tlv = TLevels.FirstOrDefault(p => p.ID == ctr.TroubleLevel)
                              orderby tlv.MinValue descending
                              select new
                              {
                                  风险点 = dp.Name,
                                  检查主体 = sub.SubjectName,
                                  风控项 = danger.Name,
                                  检查人 = bemp.CNName,
                                  检查情况 = subid.TaskResultMemo,
                                  隐患发现时间 = ctr.CreateDate,
                                  隐患等级 = tlv.DictName,
                                  整改目标 = ctr.ControlDescription,
                                  整改负责人 = pemp.CNName,
                                  验收时间 = ctrf == null ? null : (DateTime?)ctrf.FlowDate,
                                  验收情况 = (PublicEnum.EE_TroubleState)ctr.State,
                                  验收人 = aemp == null ? "" : aemp.CNName,

                              };
                    excel = Command.CreateExcel(exc.AsEnumerable(), AppUser.OutPutPaht);
                }

                var re = new Pager<CtrReport>().GetCurrentPage(retemp, query.PageSize, query.PageIndex);
                re.ExcelResult = excel;
                return new ActionResult<Pager<CtrReport>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<CtrReport>>(ex);
            }
        }

        /// <summary>
        /// 企业安全风险三清单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult<Pager<DPReport>> GetDPReport(PagerQuery<DPReportQuery> query)
        {
            try
            {
                var dps = rpsDP.Queryable();
                var empIds = dps.Select(s => s.Principal);
                var emps = rpsEmp.Queryable(p => empIds.Contains(p.ID));

                var dlvs = rpsDict.Queryable(p => p.ParentID == OptionConst.DangerLevel);

                var retemp = from dp in dps.ToList()
                             let emp = emps.FirstOrDefault(p => p.ID == dp.Principal)
                             let whysIds = JsonConvert.DeserializeObject<IEnumerable<Guid>>(dp.WXYSJson)
                             let whyss = rpsDict.Queryable(p => whysIds.Contains(p.ID)).Select(s => s.DictName)
                             let lv = dlvs.FirstOrDefault(p => p.ID == dp.DangerLevel)
                             where (dp.DangerLevel == query.Query.DLevel || query.Query.DLevel == Guid.Empty) && (emp.CNName.Contains(query.Query.KeyWord) || dp.Name.Contains(query.Query.KeyWord) || query.Query.KeyWord == string.Empty)
                             select new DPReport
                             {
                                 Consequence = dp.Consequence,
                                 DangerPoint = dp.Name,
                                 EmergencyMeasure = dp.EmergencyMeasure,
                                 ControlMeasure = dp.ControlMeasure,
                                 Principal = emp.CNName,
                                 Tel = emp.Tel,
                                 WHYSDict = whyss,
                                 DLevel = lv.DictName
                             };


                string excel = "";
                if (query.ToExcel)
                {
                    var exc = from dp in dps.ToList()
                              let emp = emps.FirstOrDefault(p => p.ID == dp.Principal)
                              let whysIds = JsonConvert.DeserializeObject<IEnumerable<Guid>>(dp.WXYSJson)
                              let whyss = rpsDict.Queryable(p => whysIds.Contains(p.ID)).Select(s => s.DictName)
                              let lv = dlvs.FirstOrDefault(p => p.ID == dp.DangerLevel)
                              where (dp.DangerLevel == query.Query.DLevel || query.Query.DLevel == Guid.Empty) && (emp.CNName.Contains(query.Query.KeyWord) || dp.Name.Contains(query.Query.KeyWord) || query.Query.KeyWord == string.Empty)
                              select new
                              {
                                  风险点 = dp.Name,
                                  风险因素 = string.Join("\n", whyss.ToArray()),
                                  后果 = dp.Consequence,
                                  风险等级 = lv.DictName,
                                  管控措施 = dp.ControlMeasure,
                                  应急处理措施 = dp.EmergencyMeasure,
                                  负责人 = emp.CNName,
                                  报告电话 = emp.Tel,


                              };
                    excel = Command.CreateExcel(exc.AsEnumerable(), AppUser.OutPutPaht);
                }

                var re = new Pager<DPReport>().GetCurrentPage(retemp, query.PageSize, query.PageIndex);
                re.ExcelResult = excel;
                return new ActionResult<Pager<DPReport>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<DPReport>>(ex);
            }
        }
        /// <summary>
        /// 企业安全风险分级管控表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult<Pager<DSReport>> GetDSReport(PagerQuery<DSReportQuery> query)
        {
            try
            {
                var dps = rpsDP.Queryable();
                var empIds = dps.Select(s => s.Principal);
                var emps = rpsEmp.Queryable(p => empIds.Contains(p.ID));
                var dlvs = rpsDict.Queryable(p => p.ParentID == OptionConst.DangerLevel);

                var dprs = rpsDPR.Queryable();

                var retemp = from dpr in dprs.ToList()
                             let dp = dps.FirstOrDefault(p => p.ID == dpr.DangerPointID)
                             let emp = emps.FirstOrDefault(p => p.ID == dp.Principal)
                             let org = rpsOrg.GetModel(p => p.ID == emp.OrgID)
                             let whysIds = JsonConvert.DeserializeObject<IEnumerable<Guid>>(dp.WXYSJson)
                             let whyss = rpsDict.Queryable(p => whysIds.Contains(p.ID)).Select(s => s.DictName)
                             let drs = rpsDR.Queryable(p => p.SubjectID == dpr.SubjectID)
                             let dangers = rpsDanger.Queryable(p => drs.Select(s => s.DangerID).Contains(p.ID))
                             let lv = dlvs.OrderByDescending(o => o.LECD_DMinValue).FirstOrDefault(p => dangers.Select(s => s.DangerLevel).Contains(p.ID))
                             where emp.CNName.Contains(query.Query.KeyWord) || query.Query.KeyWord == string.Empty
                             select new DSReport
                             {
                                 DangerPoint = dp.Name,
                                 ControlMeasure = dp.ControlMeasure,
                                 WHYSDict = whyss,
                                 Principal = emp.CNName,
                                 Subject = dpr.SubjectName,
                                 DLevel = lv == null ? "" : lv.DictName,
                                 Org = org.OrgName
                             };


                string excel = "";
                if (query.ToExcel)
                {
                    var exc = from dpr in dprs.ToList()
                              let dp = dps.FirstOrDefault(p => p.ID == dpr.DangerPointID)
                              let emp = emps.FirstOrDefault(p => p.ID == dp.Principal)
                              let org = rpsOrg.GetModel(p => p.ID == emp.OrgID)
                              let whysIds = JsonConvert.DeserializeObject<IEnumerable<Guid>>(dp.WXYSJson)
                              let whyss = rpsDict.Queryable(p => whysIds.Contains(p.ID)).Select(s => s.DictName)
                              let drs = rpsDR.Queryable(p => p.SubjectID == dpr.SubjectID)
                              let dangers = rpsDanger.Queryable(p => drs.Select(s => s.DangerID).Contains(p.ID))
                              let lv = dlvs.OrderByDescending(o => o.LECD_DMinValue).FirstOrDefault(p => dangers.Select(s => s.DangerLevel).Contains(p.ID))
                              where emp.CNName.Contains(query.Query.KeyWord) || query.Query.KeyWord == string.Empty
                              select new
                              {
                                  风险点 = dp.Name,
                                  检查主体 = dpr.SubjectName,
                                  风险等级 = lv == null ? "" : lv.DictName,
                                  危险有害因素 = string.Join("\n", whyss),
                                  管控措施 = dp.ControlMeasure,
                                  负责部门 = org.OrgName,
                                  负责人 = emp.CNName,
                              };
                    excel = Command.CreateExcel(exc.AsEnumerable(), AppUser.OutPutPaht);
                }

                var re = new Pager<DSReport>().GetCurrentPage(retemp, query.PageSize, query.PageIndex);
                re.ExcelResult = excel;
                return new ActionResult<Pager<DSReport>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<DSReport>>(ex);
            }
        }
        /// <summary>
        ///企业岗位作业内容清单				
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult<Pager<OpreateReport>> GetOpreateReport(PagerQuery<string> query)
        {

            try
            {
                var opreates = rpsOpreate.Queryable();
                var ofIds = opreates.Select(s => s.ID);
                var ofs = rpsOF.Queryable(p => ofIds.Contains(p.OpreationID));
                var retemp = from op in opreates
                             let of = ofs.Where(p => p.OpreationID == op.ID)
                             orderby op.Code descending
                             select new OpreateReport
                             {
                                 Target = op.Memo,
                                 OpreateName = op.Name,
                                 OpreateFlow = from o in of
                                               orderby o.PointIndex ascending
                                               select o.PointIndex + "、" + o.PointName
                             };

                string excel = "";
                if (query.ToExcel)
                {
                    var exc = from op in opreates.ToList()
                              let of = ofs.Where(p => p.OpreationID == op.ID)
                              orderby op.Code descending
                              select new
                              {
                                  作业名称 = op.Name,
                                 
                                  作业步骤 = string.Join("\n", from o in of
                                                           orderby o.PointIndex ascending
                                                           select o.PointIndex + "、" + o.PointName),
                                  作业目的 = op.Memo,
                              };

                    excel = Command.CreateExcel(exc.AsEnumerable(), AppUser.OutPutPaht);
                }
                var re = new Pager<OpreateReport>().GetCurrentPage(retemp, query.PageSize, query.PageIndex);
                re.ExcelResult = excel;
                return new ActionResult<Pager<OpreateReport>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<OpreateReport>>(ex);
            }
        }

        /// <summary>
        /// 岗位报表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult<Pager<PostReport>> GetPostReport(PagerQuery<string> query)
        {
            try
            {
                var posts = rpsPost.Queryable();
                var orgIds = posts.Select(s => s.Org);
                var orgs = rpsOrg.Queryable(p => orgIds.Contains(p.ID));
                var retemp = from p in posts.ToList()
                             let org = orgs.FirstOrDefault(q => q.ID == p.Org)
                             let hasFile = rpsFile.Any(q => q.BusinessID == p.ID)
                             orderby org.Level, p.Code
                             select new PostReport
                             {
                                 Org = org.OrgName,
                                 PostName = p.Name,
                                 MainTasks = p.MainTasks,
                                 Memo = hasFile ? "有附件" : ""
                             };
                string excel = "";
                if (query.ToExcel)
                {
                    var exc = from p in posts.ToList()
                              let org = orgs.FirstOrDefault(q => q.ID == p.Org)
                              let hasFile = rpsFile.Any(q => q.BusinessID == p.ID)
                              orderby org.Level, p.Code
                              select new
                              {
                                  组织或部门 = org.OrgName,
                                  岗位名称 = p.Name,
                                  主要任务 = p.MainTasks,
                                  备注 = hasFile ? "有附件" : ""
                              };

                    excel = Command.CreateExcel(exc.AsEnumerable(), AppUser.OutPutPaht);
                }
                var re = new Pager<PostReport>().GetCurrentPage(retemp, query.PageSize, query.PageIndex);
                re.ExcelResult = excel;
                return new ActionResult<Pager<PostReport>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<PostReport>>(ex);
            }
        }
        /// <summary>
        /// 设备设施风险分级控制清单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult<Pager<SubDReport>> GetSubDReport(PagerQuery<SubDReportQuery> query)
        {
            try
            {
                var dps = rpsDP.Queryable();
                var dpIds = dps.Select(s => s.ID);
                var orgs = rpsOrg.Queryable();
                var emps = rpsEmp.Queryable();
                var drs = rpsDR.Queryable();
                var dangers = rpsDanger.Queryable();

                var dprs = rpsDPR.Queryable(p => dpIds.Contains(p.DangerPointID));
                var DLevels = rpsDict.Queryable(p => p.ParentID == OptionConst.DangerLevel);

                var ss = rpsSS.Queryable();
                var ssds = rpsDSS.Queryable();

                var retemp = from dp in dps
                             let org = orgs.FirstOrDefault(p => p.ID == dp.OrgID)
                             let pemp = emps.FirstOrDefault(p => p.ID == dp.Principal)
                             let cdprs = dprs.Where(p => p.DangerPointID == dp.ID)
                             let dplv = DLevels.FirstOrDefault(p => p.ID == dp.DangerLevel)
                             orderby dplv.LECD_DMaxValue descending
                             select new SubDReport
                             {
                                 DangerPoint = dp.Name,
                                 Consequence = dp.Consequence,
                                 POrg = org.OrgName,
                                 Principal = pemp.CNName,
                                 DangerSubs = from dpr in cdprs
                                              let cdangers = dangers.Where(p => drs.Where(q => q.SubjectID == dpr.SubjectID).Select(s => s.DangerID).Contains(p.ID))
                                              orderby dpr.SubjectType
                                              select new DangerSub
                                              {
                                                  Sub = dpr.SubjectName,
                                                  SubType = dpr.SubjectType == (int)PublicEnum.EE_SubjectType.Device ? "设备设施"
                                                         : dpr.SubjectType == (int)PublicEnum.EE_SubjectType.Opreate ? "作业"
                                                         : dpr.SubjectType == (int)PublicEnum.EE_SubjectType.Post ? "岗位" : "",
                                                  Dangers = from d in cdangers
                                                            let dlv = DLevels.FirstOrDefault(p => p.ID == d.DangerLevel)
                                                            let css = ss.Where(p => ssds.Where(q => q.DangerID == d.ID).Select(s => s.SafetyStandardID).Contains(p.ID))
                                                            select new RDanger
                                                            {
                                                                DangerName = d.Name,
                                                                DLevel = dlv.DictName,
                                                                Standards = from cs in css
                                                                            select new Standard
                                                                            {
                                                                                Accident = cs.Accident,
                                                                                Controls = cs.Controls,
                                                                                Engineering = cs.Engineering,
                                                                                Individual = cs.Individual
                                                                            }
                                                            }

                                              }

                             };
                string excel = "";
                if (query.ToExcel)
                {
                    var sw = new List<object>();
                    var dpss = from dp in dps
                               let dplv = DLevels.FirstOrDefault(p => p.ID == dp.DangerLevel)
                               orderby dplv.LECD_DMaxValue descending
                               select dp;
                    foreach (var dp in dpss)
                    {
                        var cdprs = dprs.Where(p => p.DangerPointID == dp.ID);
                        var org = orgs.FirstOrDefault(p => p.ID == dp.OrgID);
                        var pemp = emps.FirstOrDefault(p => p.ID == dp.Principal);
                        foreach (var dpr in cdprs)
                        {
                            var cdangers = dangers.Where(p => drs.Where(q => q.SubjectID == dpr.SubjectID).Select(s => s.DangerID).Contains(p.ID));
                            foreach (var d in cdangers)
                            {
                                sw.Add(new
                                {
                                    风险点 = dp.Name,
                                    主体类型 = dpr.SubjectType == (int)PublicEnum.EE_SubjectType.Device ? "设备设施"
                                                             : dpr.SubjectType == (int)PublicEnum.EE_SubjectType.Opreate ? "作业"
                                                             : dpr.SubjectType == (int)PublicEnum.EE_SubjectType.Post ? "岗位" : "",
                                    主体名称 = dpr.SubjectName,
                                    风控项名称 = d.Name,
                                    风险等级 = DLevels.FirstOrDefault(p => p.ID == d.DangerLevel).DictName,
                                    工程技术 = string.Join("\n", ss.Where(p => ssds.Where(q => q.DangerID == d.ID).Select(s => s.SafetyStandardID).Contains(p.ID)).Select(s => s.Individual)),
                                    管理措施 = string.Join("\n", ss.Where(p => ssds.Where(q => q.DangerID == d.ID).Select(s => s.SafetyStandardID).Contains(p.ID)).Select(s => s.Controls)),
                                    个体防护措施 = string.Join("\n", ss.Where(p => ssds.Where(q => q.DangerID == d.ID).Select(s => s.SafetyStandardID).Contains(p.ID)).Select(s => s.Individual)),
                                    应急处置措施 = string.Join("\n", ss.Where(p => ssds.Where(q => q.DangerID == d.ID).Select(s => s.SafetyStandardID).Contains(p.ID)).Select(s => s.Accident)),
                                    不符合标准情况及后果 = dp.Consequence,
                                    责任单位 = org.OrgName,
                                    责任人 = pemp.CNName

                                });
                            }

                        }
                    }
                 excel = Command.CreateExcel(sw.AsEnumerable(), AppUser.OutPutPaht);
                }
               
                var re = new Pager<SubDReport>().GetCurrentPage(retemp, query.PageSize, query.PageIndex);
                re.ExcelResult = excel;
                return new ActionResult<Pager<SubDReport>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<SubDReport>>(ex);
            }
        }
    }
}
