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
    public class TroubleCtrService :ServiceBase, ITroubleCtrService
    {
        private IUnitwork _work = null;
        private IRepository<Bll_TroubleControl> _rpstc = null;
        private IRepository<Bll_TroubleControlDetails> _rpstcd = null;
        private IRepository<Bll_TroubleControlFlows> _rpstcf = null;

        //private Core.IFlow srvFlow = null;

        public TroubleCtrService(IUnitwork work, IFlow flow) /*: base(work,flow)*/
        {
            _work = work;
            Unitwork = work;
            _rpstc = work.Repository<Bll_TroubleControl>();
            _rpstcd = work.Repository<Bll_TroubleControlDetails>();
            _rpstcf = work.Repository<Bll_TroubleControlFlows>();

            //srvFlow = flow;
            //var flowser = srvFlow as FlowService;
            //flowser.AppUser = AppUser;
            //flowser.ACOptions = ACOptions;

        }
        ///// <summary>
        ///// 新建隐患管控
        ///// </summary>
        ///// <param name="ctrNew"></param>
        ///// <returns></returns>
        //public ActionResult<bool> AddTroubleCtr(TroubleCtrNew ctrNew)
        //{
        //    try
        //    {
        //        var bill= _work.Repository<Bll_TaskBill>().GetModel(ctrNew.BillID);
        //        if (bill==null)
        //        {
        //            throw new Exception("未找到该单据!");
        //        }
        //        else if (bill.State!=(int)PublicEnum.BillFlowState.normal)
        //        {//状态有待协商
        //            throw new Exception("当前单据状态不允许进行管控!");
        //        }

        //        var check = _rpstc.Any(p=>p.ControlName==ctrNew.ControlName);
        //        if (check)
        //        {
        //            throw new Exception("该隐患管控已存在!");
        //        }
        //        var dbtc = ctrNew.MAPTO<Bll_TroubleControl>();

        //        dbtc.Code = Command.CreateCode();
        //        dbtc.State = (int)PublicEnum.EE_TroubleState.pending;
        //        dbtc.CreateDate = DateTime.Now;
        //        var lv = _work.Repository<Bll_TaskBillSubjects>().Queryable(p => ctrNew.BillSubjectsIDs.Contains(p.ID) && p.BillID == ctrNew.BillID).ToList();
        //        dbtc.TroubleLevel = lv.Count()==0?0:(int)lv.Max(m=>m.TroubleLevel);
        //        var dbtcd = (from d in ctrNew.BillSubjectsIDs
        //                     select new Bll_TroubleControlDetails
        //                     {
        //                         ID = Guid.NewGuid(),
        //                         TroubleControlID=dbtc.ID,
        //                         BillSubjectsID=d
        //                     }
        //                   ).ToList();

        //        _rpstc.Add(dbtc);
        //        _rpstcd.Add(dbtcd);
        //        _work.Commit();
        //        return new ActionResult<bool>(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ActionResult<bool>(ex);
        //    }
        //}
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
                var tc = _rpstc.GetModel(flowNew.ControlID);
                var dbf = flowNew.MAPTO<Bll_TroubleControlFlows>();
                dbf.FlowEmployeeID = AppUser.EmployeeInfo.ID;
                if (dbf.FlowType==(int)PublicEnum.EE_TroubleFlowState.TroubleApply&&tc.State!=(int)PublicEnum.EE_TroubleState.pending)
                {
                    throw new Exception("当前状态不允许申请验收!");
                }
                if (dbf.FlowType == (int)PublicEnum.EE_TroubleFlowState.TroubleR && tc.State != (int)PublicEnum.EE_TroubleState.applying)
                {
                    throw new Exception("当前状态不允许验收!");
                }

                if (dbf.FlowType == (int)PublicEnum.EE_TroubleFlowState.TroubleApply)
                {
                    tc.State = (int)PublicEnum.EE_TroubleState.applying;
                }
                else
                {
                    if (dbf.FlowResult == 1)
                    {
                        tc.State = (int)PublicEnum.EE_TroubleState.over;
                    }
                    else if (dbf.FlowResult == 2)
                    {
                        tc.State = (int)PublicEnum.EE_TroubleState.pending;
                    }
                    else
                    {
                        throw new Exception("请选择真确的验收结果！");
                    }
                }
                _rpstc.Update(tc);
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
        /// 归档
        /// </summary>
        /// <param name="ctrID"></param>
        /// <returns></returns>
        public ActionResult<bool> Filed(Guid ctrID)
        {
            try
            {
                var dbtask = _rpstc.GetModel(ctrID);
                if (dbtask == null)
                {
                    throw new Exception("管控项不存在");
                }
                if (dbtask.State != (int)PublicEnum.EE_TroubleState.over)
                {
                    throw new Exception("当前状态不允许");
                }
                dbtask.State = (int)PublicEnum.EE_TroubleState.history;
                _rpstc.Update(dbtask);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        ///// <summary>
        ///// 删除隐患管控
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public ActionResult<bool> DelTroubleCtr(Guid id)
        //{
        //    try
        //    {
        //        var tc = _rpstc.GetModel(id);
        //        if (tc == null)
        //        {
        //            throw new Exception("未找到所需删除的隐患管控!");
        //        }
        //        if (tc.State != (int)PublicEnum.EE_TroubleState.pending)
        //        {
        //            throw new Exception("隐患管控当前状态不允许删除!");
        //        }
        //        var check = _rpstcf.Any(p=>p.ControlID==id);
        //        if (check)
        //        {
        //            throw new Exception("该隐患管控存在管控验收申请日志,无法删除!");
        //        }

        //        _rpstc.Delete(tc);
        //        _rpstcd.Delete(p=>p.TroubleControlID==id);
        //        _work.Commit();
        //        return new ActionResult<bool>(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ActionResult<bool>(ex);
        //    }
        //}

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

                var porg = _work.Repository<Core.Model.DB.Basic_Org>().GetModel(pemp.OrgID);


                var tcf = _rpstcf.GetModel(p => p.ControlID == id&&p.FlowResult==(int)PublicEnum.EE_FlowResult.Pass);
                
                var emps = _work.Repository<Core.Model.DB.Basic_Employee>().GetModel(p =>p.ID==tcf.FlowEmployeeID);

                var billempid = _work.Repository<Bll_TaskBill>().GetModel(dbtc.BillID).EmployeeID;
                var bemp= _work.Repository<Core.Model.DB.Basic_Employee>().GetModel(billempid);

                var retc = new TroubleCtrView
                {
                    Code=dbtc.Code,
                    CtrID = dbtc.ID,
                    State = (PublicEnum.EE_TroubleState)dbtc.State,
                    StateName =  Command.GetItems(typeof(PublicEnum.EE_TroubleState)).FirstOrDefault(p => p.Value == dbtc.State).Caption,

                    CreateDate = dbtc.CreateDate,
                    ControlDescription = dbtc.ControlDescription,
                    PrincipalID = dbtc.PrincipalID,
                    PrincipalName = pemp.CNName,
                    OrgID = pemp.OrgID,
                    OrgName = porg.OrgName,
                    FinishTime = dbtc.FinishTime,
                    TroubleLevel = dbtc.TroubleLevel,
                    TroubleLevelDesc = Command.GetItems(typeof(PublicEnum.EE_TroubleLevel)).FirstOrDefault(p => p.Value == dbtc.TroubleLevel).Caption,
                    
                    FlowEmp=emps==null?"":emps.CNName,
                    FlowTime=tcf?.FlowDate,
                    
                    BillEmpName= bemp.CNName
                    

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
                         orderby f.FlowDate
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
                    //管控项
                    var dbtc = _rpstc.Queryable(q=>
                    ((q.CreateDate>=starttime&&q.CreateDate<endtime)||(starttime==null&&endtime==null))
                    &&(q.TroubleLevel==para.Query.TroubleLevel||para.Query.TroubleLevel==0)
                    && (q.ControlName.Contains(para.Query.Key)||para.Query.Key==string.Empty)
                    &&q.State==(int)PublicEnum.EE_TroubleState.history);

                    //管控负责人
                    var pempids = dbtc.Select(s => s.PrincipalID);
                    var pemps = _work.Repository<Core.Model.DB.Basic_Employee>().Queryable(q=>pempids.Contains(q.ID));
                    //管控负责人部门
                    var porgids = pemps.Select(s => s.OrgID);
                    var porgs = _work.Repository<Core.Model.DB.Basic_Org>().Queryable(q => porgids.Contains(q.ID));

                    var tcids = dbtc.Select(s => s.ID);

                    //管控最终验收人验收人
                    var tcfs = _rpstcf.Queryable(p => tcids.Contains(p.ControlID) && p.FlowResult == (int)PublicEnum.EE_FlowResult.Pass);
                    var tcfempids = tcfs.Select(s => s.FlowEmployeeID);

                    var emps = _work.Repository<Core.Model.DB.Basic_Employee>().Queryable(p =>tcfempids.Contains(p.ID));

                 

                    var retc = from tc in dbtc.ToList()
                               let pemp = pemps.FirstOrDefault(q => q.ID == tc.PrincipalID)
                               let porg = porgs.FirstOrDefault(q => q.ID == pemp.OrgID)
                               let tcf = tcfs.FirstOrDefault(q => q.ControlID == tc.ID)
                               let emp = tcf == null ? null : emps.FirstOrDefault(q => q.ID == tcf.FlowEmployeeID)
                               let bempid= _work.Repository<Bll_TaskBill>().GetModel(tc.BillID).EmployeeID   //巡检人员
                               let bemp= _work.Repository<Core.Model.DB.Basic_Employee>().GetModel(bempid)
                               select new TroubleCtrView
                               {
                                   Code = tc.Code,
                                   CtrID = tc.ID,
                                   State = (PublicEnum.EE_TroubleState)tc.State,
                                   StateName =Command.GetItems(typeof(PublicEnum.EE_TroubleState)).FirstOrDefault(p => p.Value == tc.State).Caption,

                                   CreateDate = tc.CreateDate,
                                   ControlDescription = tc.ControlDescription,
                                   PrincipalID = tc.PrincipalID,
                                   PrincipalName = pemp.CNName,
                                   OrgID = pemp.OrgID,
                                   OrgName = porg.OrgName,
                                   FinishTime = tc.FinishTime,
                                   TroubleLevel = tc.TroubleLevel,
                                   TroubleLevelDesc = Command.GetItems(typeof(PublicEnum.EE_TroubleLevel)).FirstOrDefault(p => p.Value == tc.TroubleLevel).Caption,

                                   FlowEmp =emp==null?"":emp.CNName,
                                   FlowTime = tcf == null ? null : (DateTime?)tcf.FlowDate,

                                   BillEmpName=bemp==null?"":bemp.CNName

                                   

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

                    var porgids = pemps.Select(s => s.OrgID);
                    var porgs = _work.Repository<Core.Model.DB.Basic_Org>().Queryable(q => porgids.Contains(q.ID));

                    var tcids = dbtc.Select(s => s.ID);

                    var tcfs = _rpstcf.Queryable(p => tcids.Contains(p.ControlID) && p.FlowResult == (int)PublicEnum.EE_FlowResult.Pass).ToList();
                    var tcfempids = tcfs.Select(s => s.FlowEmployeeID);

                    var emps = _work.Repository<Core.Model.DB.Basic_Employee>().Queryable(p => tcfempids.Contains(p.ID));

                    var retc = from tc in dbtc.ToList()
                               let pemp = pemps.FirstOrDefault(q => q.ID == tc.PrincipalID)
                               let porg = porgs.FirstOrDefault(q => q.ID == pemp.OrgID)
                               let tcf = tcfs.FirstOrDefault(q => q.ControlID == tc.ID)
                               let emp = tcf == null ? null : emps.FirstOrDefault(q => q.ID == tcf.FlowEmployeeID)
                               let bempid = _work.Repository<Bll_TaskBill>().GetModel(tc.BillID).EmployeeID   //巡检人员
                               let bemp = _work.Repository<Core.Model.DB.Basic_Employee>().GetModel(bempid)
                               select new TroubleCtrView
                               {
                                   Code=tc.Code,
                                   CtrID = tc.ID,
                                   State = (PublicEnum.EE_TroubleState)tc.State,
                                   StateName=tc.State==0?"":Command.GetItems(typeof(PublicEnum.EE_TroubleState)).FirstOrDefault(p=>p.Value==tc.State).Caption,

                                   CreateDate = tc.CreateDate,
                                   ControlDescription = tc.ControlDescription,
                                   PrincipalID = tc.PrincipalID,
                                   PrincipalName = pemp.CNName,
                                   OrgID = pemp.OrgID,
                                   OrgName = porg.OrgName,
                                   FinishTime = tc.FinishTime,
                                   TroubleLevel = tc.TroubleLevel,
                                   TroubleLevelDesc =tc.TroubleLevel==0?"":Command.GetItems(typeof(PublicEnum.EE_TroubleLevel)).FirstOrDefault(p => p.Value == tc.TroubleLevel).Caption,

                                   FlowEmp = emp == null ? "" : emp.CNName,
                                   FlowTime = tcf==null?null:(DateTime?)tcf.FlowDate,
                                   BillEmpName = bemp == null ? "" : bemp.CNName
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


        ///// <summary>
        ///// 业务单据审核
        ///// </summary>
        ///// <param name="businessid"></param>
        ///// <returns></returns>
        //public override ActionResult<bool> Approve(Guid businessid)
        //{
        //    try
        //    {
        //        var businessmodel = _rpstc.GetModel(businessid);
        //        if (businessmodel == null)
        //        {
        //            throw new Exception("业务单据不存在");
        //        }
        //        if (businessmodel.State != (int)PublicEnum.BillFlowState.approved)
        //        {
        //            throw new Exception("业务单据状态不支持");
        //        }

        //        //检查审批流程状态
        //        var flowcheck = base.BusinessAprove(new BusinessAprovePara
        //        {
        //            BusinessID = businessid,
        //            BusinessType = PublicEnum.EE_BusinessType.TroubleControl
        //        });
        //        if (flowcheck.state != 200)
        //        {
        //            throw new Exception(flowcheck.msg);
        //        }
        //        if (flowcheck.data)
        //        {
        //            businessmodel.State = (int)PublicEnum.EE_TroubleState.pending;
        //            _rpstc.Update(businessmodel);
        //            _work.Commit();
        //            return new ActionResult<bool>(true);
        //        }
        //        else
        //        {
        //            throw new Exception("审批结果检查未通过");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ActionResult<bool>(ex);
        //    }
        //}
        ///// <summary>
        ///// 发起业务审批
        ///// </summary>
        ///// <param name="taskid"></param>
        ///// <returns></returns>
        //public override ActionResult<bool> StartBillFlow(Guid taskid)
        //{
        //    try
        //    {
        //        var businessmodel = _rpstc.GetModel(taskid);
        //        if (businessmodel == null)
        //        {
        //            throw new Exception("任务单据不存在");
        //        }
        //        if (businessmodel.State >= (int)PublicEnum.BillFlowState.pending)
        //        {
        //            throw new Exception("状态不支持");
        //        }

        //        var flowtask = base.StartFlow(new BusinessAprovePara
        //        {
        //            BusinessID = businessmodel.ID,
        //            BusinessType = PublicEnum.EE_BusinessType.TroubleControl
        //        });
        //        if (flowtask.state != 200)
        //        {
        //            throw new Exception(flowtask.msg);
        //        }
        //        var taskmodel = flowtask.data;
        //        if (taskmodel == null)//未定义审批流程
        //        {
        //            //没有流程，直接为审批通过
        //            businessmodel.State = (int)PublicEnum.BillFlowState.approved;

        //            _rpstc.Update(businessmodel);
        //            _work.Commit();
        //        }
        //        else
        //        {
        //            //更新业务单据状态
        //            businessmodel.State = (int)PublicEnum.BillFlowState.pending;
        //            _rpstc.Update(businessmodel);

        //            //写入审批流程起始任务
        //            taskmodel.BusinessCode = businessmodel.ControlName;
        //            taskmodel.BusinessDate = businessmodel.CreateDate;

        //            _work.Repository<Flow_Task>().Add(taskmodel);

        //            _work.Commit();

        //        }
        //        return new ActionResult<bool>(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ActionResult<bool>(ex);
        //    }
        //}

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
                var user = AppUser.EmployeeInfo;
                if (user.ID != dbtask.PrincipalID)
                {
                    throw new Exception("没有权限!");
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




        /// <summary>
        /// APP端获取隐患管控项
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<APPTroubleCtrView>> GetTroubleCtr()
        {
            try
            {
                var user = AppUser.EmployeeInfo;
                var ctrs = _rpstc.Queryable(p => p.PrincipalID == user.ID || p.AcceptorID == user.ID || p.ExecutorID == user.ID);
                var emps = _work.Repository<Basic_Employee>().Queryable();
                var re = from c in ctrs.ToList()
                         let aemp = emps.FirstOrDefault(p => p.ID == c.AcceptorID)
                         let eemp = emps.FirstOrDefault(p => p.ID == c.ExecutorID)
                         let pemp = emps.FirstOrDefault(p => p.ID == c.PrincipalID)
                         let tcd = _rpstcd.GetModel(q => q.TroubleControlID == c.ID)
                         let checkresult = _work.Repository<Bll_TaskBillSubjects>().GetModel(tcd.BillSubjectsID)
                         let bill = _work.Repository<Bll_TaskBill>().GetModel(checkresult.BillID)
                         let point = _work.Repository<Basic_DangerPoint>().GetModel(bill.DangerPointID)
                         let sub = _work.Repository<Basic_DangerPointRelation>().Queryable(p => p.SubjectID == checkresult.SubjectID).First()
                         let danger = _work.Repository<Basic_Danger>().GetModel(p => p.ID == checkresult.DangerID)
                         let lv = _work.Repository<Basic_Dict>().GetModel(c.DangerLevel)
                         select new APPTroubleCtrView
                         {
                             KeyID=c.ID,
                             Acceptor = aemp == null ? "" : aemp.CNName,
                             Executor = eemp == null ? "" : eemp.CNName,
                             Principal = pemp.CNName,
                             State = (PublicEnum.EE_TroubleState)c.State,
                             CDangerLevel = c.DangerLevel,
                             TroubleLevel = (PublicEnum.EE_TroubleLevel)c.TroubleLevel,
                             CtrTarget = c.ControlDescription,
                             EstimatedDate = c.FinishTime,
                             TroubleDetails = checkresult.TaskResultMemo,
                             DangerName = danger.Name,
                             SubName = sub.SubjectName,
                             CDangerLevelName = lv.DictName,
                             DangerPoint = point.Name,
                             Cuser = user.ID == c.PrincipalID ? 1 : user.ID == c.ExecutorID ? 2 : user.ID == c.AcceptorID ? 3 : 0
                         };
                return new ActionResult<IEnumerable<APPTroubleCtrView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<APPTroubleCtrView>>(ex);
            }
        }
        /// <summary>
        /// 调整风险等级
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public ActionResult<bool> ChangeDangerLevel(ChangeDangerLevel level)
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
                var user = AppUser.EmployeeInfo;
                if (user.ID!=dbtask.PrincipalID)
                {
                    throw new Exception("没有权限!");
                }
                dbtask.DangerLevel = level.DangerLevel;
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
        /// 处理管控项
        /// </summary>
        /// <param name="handleTrouble"></param>
        /// <returns></returns>
        public ActionResult<bool> HandleCtr(HandleTroubleCtr handleTrouble)
        {
            try
            {
                var user = AppUser.EmployeeInfo;
                var ctr = _rpstc.GetModel(handleTrouble.CtrID);
                if (user.ID != ctr.PrincipalID)
                {
                    throw new Exception("没有权限!");
                }
                if (handleTrouble.AcceptorID == Guid.Empty || handleTrouble.ExecutorID == Guid.Empty)
                {
                    throw new Exception("执行人或验收人不能为空!");
                }
                if (handleTrouble.FinishTime<DateTime.Now)
                {
                    throw new Exception("请填写正确的预估完成时间!");
                }
                ctr.AcceptorID = handleTrouble.AcceptorID;
                ctr.ExecutorID = handleTrouble.ExecutorID;
                ctr.FinishTime = handleTrouble.FinishTime;
                ctr.ControlDescription = handleTrouble.ControlDescription;
                _rpstc.Update(ctr);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 快速处理
        /// </summary>
        /// <param name="quickHandleTrouble"></param>
        /// <returns></returns>
        public ActionResult<bool> QuickHandleCtr(QuickHandleTroubleCtr quickHandleTrouble)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 转让责任人
        /// </summary>
        /// <param name="transferTrouble"></param>
        /// <returns></returns>
        public ActionResult<bool> TransferPrincipal(TransferTroublePrincipal transferTrouble)
        {
            try
            {
                var user = AppUser.EmployeeInfo;
                var ctr = _rpstc.GetModel(transferTrouble.CtrID);
                if (user.ID != ctr.PrincipalID)
                {
                    throw new Exception("没有权限!");
                }
                if (transferTrouble.PrincipalID == Guid.Empty)
                {
                    throw new Exception("请选择责任人！");
                }
                ctr.PrincipalID = transferTrouble.PrincipalID;
                _rpstc.Update(ctr);
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
