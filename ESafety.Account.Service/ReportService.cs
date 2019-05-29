using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
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
    public class ReportService : ServiceBase, IReport
    {
        private IRepository<Basic_DangerPoint> rpsDP = null;
        private IRepository<Basic_DangerPointRelation> rpsDPR = null;
        private IRepository<Basic_Employee> rpsEmp = null;

        private IRepository<Basic_Danger> rpsDanger = null;
        private IRepository<Basic_DangerRelation> rpsDR = null;
        private IRepository<Basic_Dict> rpsDict = null;
        private IRepository<Basic_Org> rpsOrg = null;
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
                             let lv = dlvs.FirstOrDefault(p=>p.ID==dp.DangerLevel)
                             where (dp.DangerLevel==query.Query.DLevel||query.Query.DLevel==Guid.Empty)&&(emp.CNName.Contains(query.Query.KeyWord)||dp.Name.Contains(query.Query.KeyWord)||query.Query.KeyWord==string.Empty)
                             select new DPReport
                             {
                                 Consequence = dp.Consequence,
                                 DangerPoint = dp.Name,
                                 EmergencyMeasure = dp.EmergencyMeasure,
                                 ControlMeasure = dp.ControlMeasure,
                                 Principal = emp.CNName,
                                 Tel = emp.Tel,
                                 WHYSDict=whyss,
                                 DLevel=lv.DictName
                             };
                var re = new Pager<DPReport>().GetCurrentPage(retemp, query.PageSize, query.PageIndex);
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

                             let dp=dps.FirstOrDefault(p=>p.ID==dpr.DangerPointID)
                             let emp = emps.FirstOrDefault(p => p.ID == dp.Principal)
                             let org = rpsOrg.GetModel(emp.OrgID)
                             let whysIds = JsonConvert.DeserializeObject<IEnumerable<Guid>>(dp.WXYSJson)
                             let whyss = rpsDict.Queryable(p => whysIds.Contains(p.ID)).Select(s => s.DictName)
                             let drs=rpsDR.Queryable(p=>p.SubjectID==dpr.SubjectID)
                             let dangers=rpsDanger.Queryable(p=>drs.Select(s=>s.DangerID).Contains(p.ID))
                             let lv=dlvs.OrderByDescending(o=>o.LECD_DMinValue).FirstOrDefault(p=>dangers.Select(s=>s.DangerLevel).Contains(p.ID))
                             //where (lv.ID == query.Query.DLevel || query.Query.DLevel == Guid.Empty) && (emp.CNName.Contains(query.Query.KeyWord) || dpr.SubjectName.Contains(query.Query.KeyWord) || query.Query.KeyWord == string.Empty)
                             select new DSReport
                             {
                                 DangerPoint=dp.Name,
                                 ControlMeasure=dp.ControlMeasure,
                                 WHYSDict=whyss,
                                 Principal=emp.CNName,
                                 Subject=dpr.SubjectName,
                                 Org=org.OrgName,
                                 DLevel=lv==null?"":lv.DictName
                             };
                var re = new Pager<DSReport>().GetCurrentPage(retemp, query.PageSize, query.PageIndex);
                return new ActionResult<Pager<DSReport>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<DSReport>>(ex);
            }
        }
    }
}
