using ESafety.Account.IService;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.DB.Account;
using ESafety.ORM;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Service
{
    /// <summary>
    /// 看板
    /// </summary>
    public class DashboardService : ServiceBase, IDashboard
    {
        private IRepository<Basic_DangerPoint> rpsDP = null;


        private IUnitwork work = null;
        public DashboardService(IUnitwork _work)
        {
            work = _work;
            Unitwork = _work;
            rpsDP = _work.Repository<Basic_DangerPoint>();

        }
        /// <summary>
        /// 获取风险等级的个数
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<DashDangerLevel>> GetDashDLevel()
        {
            try
            {

                var dls = work.Repository<Core.Model.DB.Basic_Dict>().Queryable(p => p.ParentID == OptionConst.DangerLevel);
                var ctrs = work.Repository<Bll_TroubleControl>().Queryable();
                var dps = rpsDP.Queryable();
                var dpss = from dp in dps
                           let dlvs = ctrs.Where(p => p.DangerPoint == dp.ID).Select(s => s.DangerLevel)
                           let lv = dls.Where(p => dlvs.Contains(p.ID)).OrderByDescending(s => s.LECD_DMinValue).FirstOrDefault()
                           let dplv = dls.FirstOrDefault(p => p.ID == dp.DangerLevel)
                           select new
                           {
                               DangerLevelID = lv.LECD_DMinValue > dplv.LECD_DMinValue ? lv.ID : dp.DangerLevel,
                           };
                var re = from dl in dls
                         let count = dpss.Where(p => p.DangerLevelID == dl.ID).Count()
                         select new DashDangerLevel
                         {
                             Level = dl.DictName,
                             Count = count,
                         };
                return new ActionResult<IEnumerable<DashDangerLevel>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<DashDangerLevel>>(ex);
            }

        }
        /// <summary>
        /// 获取管控项
        /// </summary>
        /// <returns></returns>
        public ActionResult<DTroubleCtrl> GetDctrl()
        {
            try
            {

                //所有风险点
                var dangerPoints = rpsDP.Queryable();
                //风险点所对应的所有管控中的检查单据
                var dpids = dangerPoints.Select(s => s.ID);
                var bills = work.Repository<Bll_TaskBill>().Queryable(p => dpids.Contains(p.DangerPointID));
                var billIDs = bills.Select(s => s.ID);
                var ctrs = work.Repository<Bll_TroubleControl>().Queryable(p => billIDs.Contains(p.BillID) && p.State < (int)PublicEnum.EE_TroubleState.over);

                var dbf = work.Repository<Bll_TroubleControlFlows>().Queryable();

                var subs = work.Repository<Bll_TroubleControlDetails>().Queryable();

                var checkResult=work.Repository<Bll_TaskBillSubjects>().Queryable();

                var dicts = work.Repository<Basic_Dict>().Queryable(p => p.ParentID == OptionConst.DangerLevel);
                var tlvs = work.Repository<Basic_Dict>().Queryable(p => p.ParentID == OptionConst.TroubleLevel);

                var dpre = work.Repository<Basic_DangerPointRelation>().Queryable();

                var retemp = from c in ctrs.ToList()
                             let bill = bills.FirstOrDefault(p => p.ID == c.BillID)
                             let lv = dicts.FirstOrDefault(p => p.ID == c.DangerLevel)
                             let tlv = tlvs.FirstOrDefault(p => p.ID == c.DangerLevel)
                             let dp = dangerPoints.FirstOrDefault(p => p.ID == bill.DangerPointID)
                             let subid = subs.FirstOrDefault(p => p.TroubleControlID == c.ID).BillSubjectsID
                             let checksub = checkResult.FirstOrDefault(p => p.ID == subid)
                             let sub=dpre.FirstOrDefault(p=>p.SubjectID==checksub.SubjectID)
                             orderby c.TroubleLevel descending
                             select new TroubleCtrl
                             {
                                 DLevel = lv.DictName,
                                 DangerName =sub.SubjectName,
                                 DangerPoint = dp.Name
                             };
                var re = new DTroubleCtrl
                {
                    Ctrls = retemp,
                    CtrledCount = ctrs.Where(p => dbf.Select(s => s.ControlID).Contains(p.ID)).Count(),
                    CtrlingCount = ctrs.Where(p => !dbf.Select(s => s.ControlID).Contains(p.ID)).Count()
                };
                return new ActionResult<DTroubleCtrl>(re);
            }
            catch (Exception ex)
            {

                return new ActionResult<DTroubleCtrl>(ex);
            }
        }

        /// <summary>
        /// 获取风险点位置
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<DangerPointLocation>> GetDPLocation()
        {
            try
            {
                var dps = rpsDP.Queryable();
                var re = from dp in dps
                         select new DangerPointLocation
                         {
                             DPName = dp.Name,
                             DPLocation = dp.Location
                         };
                return new ActionResult<IEnumerable<DangerPointLocation>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<DangerPointLocation>>(ex);
            }
        }
    }
}
