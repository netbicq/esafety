using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core;
using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.DB.Account;
using ESafety.Core.Model.PARA;
using ESafety.ORM;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Service
{
    public class TroubleCtrService : FlowBusinessService, ITroubleCtrService
    {
        private IUnitwork _work = null;
        private IRepository<Bll_TroubleControl> _rpstc = null;
        private IRepository<Bll_TroubleControlDetails> _rpstcd = null;
        private IRepository<Bll_TroubleControlFlows> _rpstcf = null;

        private Core.IFlow srvFlow = null;

        public TroubleCtrService(IUnitwork work, IFlow flow) : base(work,flow)
        {
            _work = work;
            Unitwork = work;
            _rpstc = work.Repository<Bll_TroubleControl>();
            _rpstcd = work.Repository<Bll_TroubleControlDetails>();
            _rpstcf = work.Repository<Bll_TroubleControlFlows>();

            srvFlow = flow;
            var flowser = srvFlow as FlowService;
            flowser.AppUser = AppUser;
            flowser.ACOptions = ACOptions;

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
                dbtc.Code = Command.CreateCode();
                var dbtcd = (from d in ctrNew.BillSubjectsIDs
                             select new Bll_TroubleControlDetails
                             {
                                 ID = Guid.NewGuid(),
                                 TroubleControlID=dbtc.ID,
                                 BillSubjectsID=d
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
        /// <summary>
        /// 新建管控验收申请日志
        /// </summary>
        /// <param name="flowNew"></param>
        /// <returns></returns>
        public ActionResult<bool> AddTroubleCtrFlow(TroubleCtrFlowNew flowNew)
        {
            try
            {
                if (flowNew == null)
                {
                    throw new Exception("参数有误!");
                }
                var dbf = flowNew.MAPTO<Bll_TroubleControlFlows>();
                _rpstcf.Add(dbf);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 改变状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public ActionResult<bool> ChangeState(TroubleCtrChangeState state)
        {
            try
            {
                if (state == null)
                {
                    throw new Exception("参数有误");
                }
                var dbtask = _rpstc.GetModel(state.ID);
                if (dbtask == null)
                {
                    throw new Exception("任务不存在");
                }
                if (dbtask.State == (int)PublicEnum.EE_TroubleState.history||dbtask.State==0)
                {
                    throw new Exception("当前状态不允许");
                }
                if (dbtask.State == (int)state.State)
                {
                    throw new Exception("状态有误");
                }
                dbtask.State = (int)state.State;

                _rpstc.Update(dbtask);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 删除隐患管控
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelTroubleCtr(Guid id)
        {
            try
            {
                var tc = _rpstc.GetModel(id);
                if (tc == null)
                {
                    throw new Exception("未找到所需删除的隐患管控!");
                }
                if (tc.State != (int)PublicEnum.EE_TroubleState.pending||tc.State!=0)
                {
                    throw new Exception("隐患管控当前状态不允许删除!");
                }
                var check = _rpstcf.Any(p=>p.ControlID==id);
                if (check)
                {
                    throw new Exception("该隐患管控存在管控验收申请日志,无法删除!");
                }

                _rpstc.Delete(tc);
                _rpstcd.Delete(p=>p.TroubleControlID==id);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 获取隐患管控模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<TroubleCtrView> GetTroubleCtr(Guid id)
        {
            try
            {
                var dbtc = _rpstc.GetModel(id);

                var pemp = _work.Repository<Core.Model.DB.Basic_Employee>().GetModel(dbtc.PrincipalID);

                var porg = _work.Repository<Core.Model.DB.Basic_Org>().GetModel(dbtc.OrgID);


                var tcf = _rpstcf.GetModel(p => p.ControlID == id&&p.FlowResult==(int)PublicEnum.EE_FlowResult.Pass);
                
                var emps = _work.Repository<Core.Model.DB.Basic_Employee>().GetModel(p =>p.ID==tcf.FlowEmployeeID);

                var retc = new TroubleCtrView
                {
                    Code=dbtc.Code,
                    ID = dbtc.ID,
                    State = (PublicEnum.EE_TroubleState)dbtc.State,
                    StateName = dbtc.State == 0 ? "" : Command.GetItems(typeof(PublicEnum.EE_TroubleState)).FirstOrDefault(p => p.Value == dbtc.State).Caption,

                    CreateDate = dbtc.CreateDate,
                    ControlName = dbtc.ControlName,
                    ControlDescription = dbtc.ControlDescription,
                    PrincipalID = dbtc.PrincipalID,
                    PrincipalName = pemp.CNName,
                    OrgID = dbtc.OrgID,
                    OrgName = porg.OrgName,
                    FinishTime = dbtc.FinishTime,
                    PrincipalTEL = dbtc.PrincipalTEL,
                    TroubleLevel = dbtc.TroubleLevel,
                    TroubleLevelDesc = Command.GetItems(typeof(PublicEnum.EE_TroubleLevel)).FirstOrDefault(p => p.Value == dbtc.TroubleLevel).Caption,
                    
                    FlowEmp=emps==null?"":emps.CNName,
                    FlowTime=tcf?.FlowDate
                    
                    

                };

                return new ActionResult<TroubleCtrView>(retc);
            }
            catch (Exception ex)
            {
                return new ActionResult<TroubleCtrView>(ex);
            }
        }

        /// <summary>
        /// 获取隐患管控细节模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<TroubleCtrDetailView> GetTroubleCtrDetailModel(Guid id)
        {
            try
            {
                var dbtcd= _rpstcd.GetModel(id);

                var sub = _work.Repository<Bll_TaskBillSubjects>().GetModel(dbtcd.BillSubjectsID);

                var danger = _work.Repository<Basic_Danger>().GetModel(sub.DangerID);

                var dic = _work.Repository<Core.Model.DB.Basic_Dict>();


                var dev = _work.Repository<Basic_Facilities>().GetModel(sub.SubjectID);
                var post = _work.Repository<Basic_Post>().GetModel(sub.SubjectID);
                var opr = _work.Repository<Basic_Opreation>().GetModel(sub.SubjectID);

                var retcds =new TroubleCtrDetailView
                             {
                                 ID = sub.ID,
                                 DangerName = danger.Name,
                                 DangerID=danger.ID,
                                 MethodName = sub.Eval_Method == 0 ? "" : Command.GetItems(typeof(PublicEnum.EE_EvaluateMethod)).FirstOrDefault(p => p.Value == sub.Eval_Method).Caption,
                                 TaskResultMemo = sub.TaskResultMemo,
                                 SubjectName = dev != null ? dev.Name : post != null ? post.Name : opr != null ? opr.Name : default(string),
                                 SubjectTypeName = Command.GetItems(typeof(PublicEnum.EE_SubjectType)).FirstOrDefault(q => q.Value == sub.SubjectType).Caption,
                                 TaskResultName = Command.GetItems(typeof(PublicEnum.EE_TaskResultType)).FirstOrDefault(q => q.Value == sub.TaskResult).Caption,
                                 TroubleLevelName = Command.GetItems(typeof(PublicEnum.EE_TroubleLevel)).FirstOrDefault(q => q.Value == sub.TroubleLevel).Caption,
                                 SGJGDic = dic.GetModel(sub.Eval_SGJG).DictName,
                                 SGLXDic = dic.GetModel(sub.Eval_SGLX).DictName,
                                 WHYSDic = dic.GetModel(sub.Eval_WHYS).DictName,
                                 YXFWDic = dic.GetModel(sub.Eval_YXFW).DictName
                             };
                return new ActionResult<TroubleCtrDetailView>(retcds);
            }
            catch (Exception ex)
            {
                return new ActionResult<TroubleCtrDetailView>(ex);
            }
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
                             let dev = devs.FirstOrDefault(q => q.ID == s.SubjectID)
                             let ppst = posts.FirstOrDefault(q => q.ID == s.SubjectID)
                             let opr = opres.FirstOrDefault(q => q.ID == s.SubjectID)
                             select new TroubleCtrDetailView
                             {
                                 ID=s.ID,
                                 DangerName=d.Name,
                                 DangerID=d.ID,
                                 MethodName=s.Eval_Method==0?"":Command.GetItems(typeof(PublicEnum.EE_EvaluateMethod)).FirstOrDefault(p=>p.Value==s.Eval_Method).Caption,
                                 TaskResultMemo=s.TaskResultMemo,
                                 SubjectName = dev != null ? dev.Name : ppst != null ? ppst.Name : opr != null ? opr.Name : default(string),
                                 SubjectTypeName= Command.GetItems(typeof(PublicEnum.EE_SubjectType)).FirstOrDefault(q => q.Value == s.SubjectType).Caption,
                                 TaskResultName= Command.GetItems(typeof(PublicEnum.EE_TaskResultType)).FirstOrDefault(q => q.Value == s.TaskResult).Caption,
                                 TroubleLevelName= Command.GetItems(typeof(PublicEnum.EE_TroubleLevel)).FirstOrDefault(q => q.Value == s.TroubleLevel).Caption,
                                 SGJGDic=s.Eval_SGJG==Guid.Empty?"":dic.GetModel(s.Eval_SGJG).DictName,
                                 SGLXDic=s.Eval_SGLX==Guid.Empty?"":dic.GetModel(s.Eval_SGLX).DictName,
                                 WHYSDic=s.Eval_WHYS==Guid.Empty?"":dic.GetModel(s.Eval_WHYS).DictName,
                                 YXFWDic=s.Eval_YXFW==Guid.Empty?"":dic.GetModel(s.Eval_YXFW).DictName
                             };

                var re = new Pager<TroubleCtrDetailView>().GetCurrentPage(retcds,para.PageSize,para.PageIndex);
                return new ActionResult<Pager<TroubleCtrDetailView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<TroubleCtrDetailView>>(ex);
            }
            

        }
        /// <summary>
        /// 获取管控验收申请日志列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<TroubleCtrFlowView>> GetTroubleCtrFlows(Guid id)
        {
            try
            {
                var tcf = _rpstcf.Queryable(p => p.ControlID == id);
                var fempids = tcf.Select(s => s.FlowEmployeeID);

                var emps = _work.Repository<Core.Model.DB.Basic_Employee>().Queryable(p=>fempids.Contains(p.ID));

                var re = from f in tcf.ToList()
                         let emp=emps.FirstOrDefault(p=>p.ID==f.FlowEmployeeID)
                         select new TroubleCtrFlowView
                         {
                             ControlID=f.ControlID,
                             FlowDate=f.FlowDate,
                             FlowEmployeeID=f.FlowEmployeeID,
                             EmpName=emp.CNName,
                             FlowMemo=f.FlowMemo,
                             FlowResult=f.FlowResult,
                             FlowType=(PublicEnum.EE_BusinessType)f.FlowType,
                             FlowResultName=f.FlowResult==0?"":Command.GetItems(typeof(PublicEnum.EE_FlowResult)).FirstOrDefault(s=>s.Value==f.FlowResult).Caption,
                             FlowTypeName=Command.GetItems(typeof(PublicEnum.EE_BusinessType)).FirstOrDefault(s=>s.Value==f.FlowType).Caption
                         };
                return new ActionResult<IEnumerable<TroubleCtrFlowView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<TroubleCtrFlowView>>(ex);
            }
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
                DateTime? starttime = para.Query.StartDate;
                DateTime? endtime = para.Query.EndTime?.AddDays(1);
                if (para.Query.IsHistory)
                {
                    var dbtc = _rpstc.Queryable(q=>
                    ((q.CreateDate>=starttime&&q.CreateDate<endtime)||(starttime==null&&endtime==null))
                    &&(q.TroubleLevel==para.Query.TroubleLevel||para.Query.TroubleLevel==0)
                    && (q.ControlName.Contains(para.Query.Key)||para.Query.Key==string.Empty)
                    &&q.State==(int)PublicEnum.EE_TroubleState.history);

                    var pempids = dbtc.Select(s => s.PrincipalID);
                    var pemps = _work.Repository<Core.Model.DB.Basic_Employee>().Queryable(q=>pempids.Contains(q.ID));

                    var porgids = dbtc.Select(s => s.OrgID);
                    var porgs = _work.Repository<Core.Model.DB.Basic_Org>().Queryable(q => porgids.Contains(q.ID));

                    var tcids = dbtc.Select(s => s.ID);

                    var tcfs = _rpstcf.Queryable(p => tcids.Contains(p.ControlID) && p.FlowResult == (int)PublicEnum.EE_FlowResult.Pass);
                    var tcfempids = tcfs.Select(s => s.FlowEmployeeID);

                    var emps = _work.Repository<Core.Model.DB.Basic_Employee>().Queryable(p =>tcfempids.Contains(p.ID));


                    var retc = from tc in dbtc.ToList()
                               let pemp = pemps.FirstOrDefault(q => q.ID == tc.PrincipalID)
                               let porg = porgs.FirstOrDefault(q => q.ID == tc.OrgID)
                               let tcf = tcfs.FirstOrDefault(q => q.ControlID == tc.ID)
                               let emp = emps.FirstOrDefault(q => q.ID == tcf.FlowEmployeeID)
                               select new TroubleCtrView
                               {
                                   Code = tc.Code,
                                   ID = tc.ID,
                                   State = (PublicEnum.EE_TroubleState)tc.State,
                                   StateName = tc.State == 0 ? "" : Command.GetItems(typeof(PublicEnum.EE_TroubleState)).FirstOrDefault(p => p.Value == tc.State).Caption,

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

                                   FlowEmp =emp==null?"":emp.CNName,
                                   FlowTime=tcf?.FlowDate
                             };
                    var re = new Pager<TroubleCtrView>().GetCurrentPage(retc,para.PageSize,para.PageIndex);
                    return new ActionResult<Pager<TroubleCtrView>>(re);
                }
                else
                {
                        var dbtc = _rpstc.Queryable(q =>
                        ((q.CreateDate >= starttime && q.CreateDate < endtime)||(starttime ==null && endtime == null))
                        && (q.TroubleLevel == para.Query.TroubleLevel || para.Query.TroubleLevel == 0)
                        && (q.ControlName.Contains(para.Query.Key) || para.Query.Key == string.Empty)
                        && q.State != (int)PublicEnum.EE_TroubleState.history);

                    var pempids = dbtc.Select(s => s.PrincipalID);
                    var pemps = _work.Repository<Core.Model.DB.Basic_Employee>().Queryable(q => pempids.Contains(q.ID));

                    var porgids = dbtc.Select(s => s.OrgID);
                    var porgs = _work.Repository<Core.Model.DB.Basic_Org>().Queryable(q => porgids.Contains(q.ID));

                    var tcids = dbtc.Select(s => s.ID);

                    var tcfs = _rpstcf.Queryable(p => tcids.Contains(p.ControlID) && p.FlowResult == (int)PublicEnum.EE_FlowResult.Pass);
                    var tcfempids = tcfs.Select(s => s.FlowEmployeeID);

                    var emps = _work.Repository<Core.Model.DB.Basic_Employee>().Queryable(p => tcfempids.Contains(p.ID));

                    var retc = from tc in dbtc.ToList()
                               let pemp = pemps.FirstOrDefault(q => q.ID == tc.PrincipalID)
                               let porg = porgs.FirstOrDefault(q => q.ID == tc.OrgID)
                               let tcf = tcfs.FirstOrDefault(q => q.ControlID == tc.ID)
                               let emp = emps.FirstOrDefault(q => q.ID == tcf.FlowEmployeeID)
                               select new TroubleCtrView
                               {
                                   Code=tc.Code,
                                   ID = tc.ID,
                                   State = (PublicEnum.EE_TroubleState)tc.State,
                                   StateName=tc.State==0?"":Command.GetItems(typeof(PublicEnum.EE_TroubleState)).FirstOrDefault(p=>p.Value==tc.State).Caption,

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

                                   FlowEmp = emp == null ? "" : emp.CNName,
                                   FlowTime = tcf?.FlowDate
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


        /// <summary>
        /// 业务单据审核
        /// </summary>
        /// <param name="businessid"></param>
        /// <returns></returns>
        public override ActionResult<bool> Approve(Guid businessid)
        {
            try
            {
                var businessmodel = _rpstc.GetModel(businessid);
                if (businessmodel == null)
                {
                    throw new Exception("业务单据不存在");
                }
                if (businessmodel.State != (int)PublicEnum.BillFlowState.approved)
                {
                    throw new Exception("业务单据状态不支持");
                }

                //检查审批流程状态
                var flowcheck = base.BusinessAprove(new BusinessAprovePara
                {
                    BusinessID = businessid,
                    BusinessType = PublicEnum.EE_BusinessType.TroubleControl
                });
                if (flowcheck.state != 200)
                {
                    throw new Exception(flowcheck.msg);
                }
                if (flowcheck.data)
                {
                    businessmodel.State = (int)PublicEnum.EE_TroubleState.pending;
                    _rpstc.Update(businessmodel);
                    _work.Commit();
                    return new ActionResult<bool>(true);
                }
                else
                {
                    throw new Exception("审批结果检查未通过");
                }
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 发起业务审批
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public override ActionResult<bool> StartBillFlow(Guid taskid)
        {
            try
            {
                var businessmodel = _rpstc.GetModel(taskid);
                if (businessmodel == null)
                {
                    throw new Exception("任务单据不存在");
                }
                if (businessmodel.State >= (int)PublicEnum.BillFlowState.pending)
                {
                    throw new Exception("状态不支持");
                }

                var flowtask = base.StartFlow(new BusinessAprovePara
                {
                    BusinessID = businessmodel.ID,
                    BusinessType = PublicEnum.EE_BusinessType.TroubleControl
                });
                if (flowtask.state != 200)
                {
                    throw new Exception(flowtask.msg);
                }
                var taskmodel = flowtask.data;
                if (taskmodel == null)//未定义审批流程
                {
                    //没有流程，直接为审批通过
                    businessmodel.State = (int)PublicEnum.BillFlowState.approved;

                    _rpstc.Update(businessmodel);
                    _work.Commit();
                }
                else
                {
                    //更新业务单据状态
                    businessmodel.State = (int)PublicEnum.BillFlowState.pending;
                    _rpstc.Update(businessmodel);

                    //写入审批流程起始任务
                    taskmodel.BusinessCode = businessmodel.ControlName;
                    taskmodel.BusinessDate = businessmodel.CreateDate;

                    _work.Repository<Flow_Task>().Add(taskmodel);

                    _work.Commit();

                }
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 修改完成时间
        /// </summary>
        /// <param name="finishTime"></param>
        /// <returns></returns>
        public ActionResult<bool> DelayFinishTime(DelayFinishTime finishTime)
        {
            try
            {
                if (finishTime == null)
                {
                    throw new Exception("参数有误");
                }
                var dbtask = _rpstc.GetModel(finishTime.ID);
                if (dbtask == null)
                {
                    throw new Exception("任务不存在");
                }
                if (dbtask.State == (int)PublicEnum.EE_TroubleState.history || dbtask.State == (int)PublicEnum.EE_TroubleState.over)
                {
                    throw new Exception("当前状态无法延期!");
                }

                dbtask.FinishTime = finishTime.FinishTime;
                _rpstc.Update(dbtask);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 修改隐患等级
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public ActionResult<bool> ChangeLevel(ChangeLevel level)
        {
            try
            {
                if (level == null)
                {
                    throw new Exception("参数有误");
                }
                var dbtask = _rpstc.GetModel(level.ID);
                if (dbtask == null)
                {
                    throw new Exception("任务不存在");
                }
                dbtask.TroubleLevel= (int)level.TroubleLevel;
                _rpstc.Update(dbtask);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
    }
}
