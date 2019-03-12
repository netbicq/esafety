using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
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
    public class TroubleCtrService : ServiceBase,ITroubleCtrService
    {
        private IUnitwork _work = null;
        private IRepository<Bll_TroubleControl> _rpstc = null;
        private IRepository<Bll_TroubleControlDetails> _rpstcd = null;
        private IRepository<Bll_TroubleControlFlows> _rpstcf = null;
        
        public TroubleCtrService(IUnitwork work)
        {
            _work = work;
            Unitwork = work;
            _rpstc = work.Repository<Bll_TroubleControl>();
            _rpstcd = work.Repository<Bll_TroubleControlDetails>();
            _rpstcf = work.Repository<Bll_TroubleControlFlows>();

        }
        /// <summary>
        /// 新建隐患管控
        /// </summary>
        /// <param name="ctrNew"></param>
        /// <returns></returns>
        public ActionResult<bool> AddTroubleCtr(TroubleCtrNew ctrNew)
        {
            try
            {
                var check = _rpstc.Any(p=>p.ControlName==ctrNew.ControlName);
                if (check)
                {
                    throw new Exception("该隐患管控已存在!");
                }
                var dbtc = ctrNew.MAPTO<Bll_TroubleControl>();
                var dbtcd = (from d in ctrNew.TroubleCtrDetails
                             select new Bll_TroubleControlDetails
                             {
                                 ID = Guid.NewGuid(),
                                 TroubleControlID=d.TroubleControlID,
                                 BillSubjectsID=d.BillSubjectsID
                             }
                           ).ToList();
                _rpstc.Add(dbtc);
                _rpstcd.Add(dbtcd);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        public ActionResult<bool> AddTroubleCtrFlow(TroubleCtrFlowNew flowNew)
        {
            throw new NotImplementedException();
        }

        public ActionResult<bool> ChangeState(TroubleCtrChangeState state)
        {
            throw new NotImplementedException();
        }

        public ActionResult<bool> DelTroubleCtr(Guid id)
        {
            throw new NotImplementedException();
        }

        public ActionResult<bool> EditTroubleCtr(TroubleCtrEdit ctrEdit)
        {
            throw new NotImplementedException();
        }

        public ActionResult<bool> EditTroubleCtrFlow(TroubleCtrFlowEdit flowEdit)
        {
            throw new NotImplementedException();
        }

        public ActionResult<TroubleCtrView> GetTroubleCtr(Guid id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 分页获取管控类容
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<TroubleCtrDetailView>> GetTroubleCtrDetails(PagerQuery<TroubleControlDetailQuery> para)
        {
            try
            {
                var dbtcds = _rpstcd.Queryable(p=>p.TroubleControlID==para.Query.TroubleControlID).ToList();
                var billids = dbtcds.Select(s => s.BillSubjectsID);
                var subs = _work.Repository<Bll_TaskBillSubjects>().Queryable(p => billids.Contains(p.ID)).ToList();

                var did = subs.Select(s => s.DangerID);
                var dangers = _work.Repository<Basic_Danger>().Queryable(p => did.Contains(p.ID)).ToList() ;
                var dic = _work.Repository<Core.Model.DB.Basic_Dict>();

                var subids = subs.Select(s => s.SubjectID);

                var devs = _work.Repository<Basic_Facilities>().Queryable(q => subids.Contains(q.ID)).ToList();
                var posts = _work.Repository<Basic_Post>().Queryable(q => subids.Contains(q.ID)).ToList();
                var opres = _work.Repository<Basic_Opreation>().Queryable(q => subids.Contains(q.ID)).ToList();

                var retcds = from s in subs
                             let d=dangers.FirstOrDefault(q=>q.ID==s.DangerID)
                             select new TroubleCtrDetailView
                             {
                                 ID=s.ID,
                                 DangerName=d.Name,
                                 MethodName=s.Eval_Method==0?"":Command.GetItems(typeof(PublicEnum.EE_EvaluateMethod)).FirstOrDefault(p=>p.Value==s.Eval_Method).Caption,
                                 TaskResultMemo=s.TaskResultMemo,
                                 
                                 
                             };



            }
            catch (Exception ex)
            {

                throw;
            }
            throw new NotImplementedException();

        }

        public ActionResult<IEnumerable<TroubleCtrFlowView>> GetTroubleCtrFlows(Guid id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 分页获取隐患管控项
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<TroubleCtrView>> GetTroubleCtrs(PagerQuery<TroubleCtrQuery> para)
        {
            try
            {
                var starttime = para.Query.StartDate;
                var endtime = para.Query.EndTime.AddDays(1);
                if (para.Query.IsHistory)
                {
                    var dbtc = _rpstc.Queryable(q=>
                    (q.CreateDate>=starttime&&q.CreateDate<endtime)
                    &&(q.TroubleLevel==para.Query.TroubleLevel||para.Query.TroubleLevel==0)
                    && (q.ControlName.Contains(para.Query.Key)||para.Query.Key==string.Empty)
                    &&q.State==(int)PublicEnum.EE_TroubleState.history).ToList();

                    var pempids = dbtc.Select(s => s.PrincipalID);
                    var pemps = _work.Repository<Core.Model.DB.Basic_Employee>().Queryable(q=>pempids.Contains(q.ID)).ToList();

                    var porgids = dbtc.Select(s => s.OrgID);
                    var porgs = _work.Repository<Core.Model.DB.Basic_Org>().Queryable(q => porgids.Contains(q.ID)).ToList();

                    var retc = from tc in dbtc
                               let pemp = pemps.FirstOrDefault(q => q.ID == tc.PrincipalID)
                               let porg=porgs.FirstOrDefault(q=>q.ID==tc.OrgID)
                               select new TroubleCtrView
                               {
                                   ID = tc.ID,
                                   State = tc.State,
                                   CreateDate = tc.CreateDate,
                                   ControlName = tc.ControlName,
                                   ControlDescription = tc.ControlDescription,
                                   PrincipalID = tc.PrincipalID,
                                   PrincipalName = pemp.CNName,
                                   OrgID=tc.OrgID,
                                   OrgName=porg.OrgName,
                                   FinishTime=tc.FinishTime,
                                   PrincipalTEL=tc.PrincipalTEL,
                                   TroubleLevel=tc.TroubleLevel,
                                   TroubleLevelDesc=Command.GetItems(typeof(PublicEnum.EE_TroubleLevel)).FirstOrDefault(p=>p.Value==tc.TroubleLevel).Caption,
                                   
                             };
                    var re = new Pager<TroubleCtrView>().GetCurrentPage(retc,para.PageSize,para.PageIndex);
                    return new ActionResult<Pager<TroubleCtrView>>(re);
                }
                else
                {
                        var dbtc = _rpstc.Queryable(q =>
                        (q.CreateDate >= starttime && q.CreateDate < endtime)
                        && (q.TroubleLevel == para.Query.TroubleLevel || para.Query.TroubleLevel == 0)
                        && (q.ControlName.Contains(para.Query.Key) || para.Query.Key == string.Empty)
                        && q.State != (int)PublicEnum.EE_TroubleState.history);

                    var pempids = dbtc.Select(s => s.PrincipalID);
                    var pemps = _work.Repository<Core.Model.DB.Basic_Employee>().Queryable(q => pempids.Contains(q.ID)).ToList();

                    var porgids = dbtc.Select(s => s.OrgID);
                    var porgs = _work.Repository<Core.Model.DB.Basic_Org>().Queryable(q => porgids.Contains(q.ID)).ToList();

                    var retc = from tc in dbtc
                               let pemp = pemps.FirstOrDefault(q => q.ID == tc.PrincipalID)
                               let porg = porgs.FirstOrDefault(q => q.ID == tc.OrgID)
                               select new TroubleCtrView
                               {
                                   ID = tc.ID,
                                   State = tc.State,
                                   CreateDate = tc.CreateDate,
                                   ControlName = tc.ControlName,
                                   ControlDescription = tc.ControlDescription,
                                   PrincipalID = tc.PrincipalID,
                                   PrincipalName = pemp.CNName,
                                   OrgID = tc.OrgID,
                                   OrgName = porg.OrgName,
                                   FinishTime = tc.FinishTime,
                                   PrincipalTEL = tc.PrincipalTEL,
                                   TroubleLevel = tc.TroubleLevel,
                                   TroubleLevelDesc = Command.GetItems(typeof(PublicEnum.EE_TroubleLevel)).FirstOrDefault(p => p.Value == tc.TroubleLevel).Caption,

                               };
                    var re = new Pager<TroubleCtrView>().GetCurrentPage(retc, para.PageSize, para.PageIndex);
                    return new ActionResult<Pager<TroubleCtrView>>(re);
                }
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<TroubleCtrView>>(ex);
            }
        }
    }
}
