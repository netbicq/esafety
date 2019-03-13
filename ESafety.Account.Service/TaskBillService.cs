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
    class TaskBillService : FlowBusinessService, ITaskBillService
    {
        private IUnitwork _work = null;
        private IRepository<Bll_TaskBill> _rpstb = null;
        private IRepository<Bll_TaskBillSubjects> _rpstbs = null;

        private Core.IFlow srvFlow = null;
        public TaskBillService(IUnitwork work,IFlow flow):base(work,flow)
        {
            _work = work;
            Unitwork = work;
            _rpstb = work.Repository<Bll_TaskBill>();
            _rpstbs = work.Repository<Bll_TaskBillSubjects>();

            srvFlow = flow;
            var flowser = srvFlow as FlowService;
            flowser.AppUser = AppUser;
            flowser.ACOptions = ACOptions;
        }
        /// <summary>
        /// 新建任务单据详情
        /// </summary>
        /// <param name="subjectBillEdit"></param>
        /// <returns></returns>
        public ActionResult<bool> EditTaskBillSubjects(TaskSubjectBillEdit subjectBillEdit)
        {
            try
            {

                var dbtbs = _rpstbs.GetModel(subjectBillEdit.ID);
                if (dbtbs==null)
                {
                    throw new Exception("未找到要处理的信息");
                }
                dbtbs = subjectBillEdit.CopyTo<Bll_TaskBillSubjects>(dbtbs);
                _rpstbs.Update(dbtbs);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 获取巡检任务模型
        /// </summary>
        /// <returns></returns>
        public ActionResult<TaskBillModelView> GetTaskBillModel(Guid id)
        {
            try
            {
                var dbtbs = _rpstbs.GetModel(id);
                if (dbtbs == null)
                {
                    throw new Exception("未找到提交的单据的具体详情");
                }
                var dbdanger = _work.Repository<Basic_Danger>().GetModel(dbtbs.DangerID);
                var dev = _work.Repository<Basic_Facilities>().GetModel(dbtbs.SubjectID);
                var post = _work.Repository<Basic_Post>().GetModel(dbtbs.SubjectID);
                var opr = _work.Repository<Basic_Opreation>().GetModel(dbtbs.SubjectID);
                var re = new TaskBillModelView
                {
                    BillID=dbtbs.BillID,
                    DangerID=dbtbs.DangerID,
                    DangerName=dbdanger.Name,
                    SubjectID=dbtbs.SubjectID,
                    SubName = dev != null ? dev.Name : post != null ? post.Name : opr != null ? opr.Name : default(string),
                    SubjectType =dbtbs.SubjectType,
                    TaskResult=dbtbs.TaskResult,
                    TaskResultMemo=dbtbs.TaskResultMemo,
                    TaskResultName= Command.GetItems(typeof(PublicEnum.EE_TaskResultType)).FirstOrDefault(q => q.Value == dbtbs.TaskResult).Caption,
                };
                return new ActionResult<TaskBillModelView>(re);
            }
            catch (Exception ex)
            {

                return new ActionResult<TaskBillModelView>(ex);
            }
        }

        /// <summary>
        /// 获取任务单据信息
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<TaskBillView>> GetTaskBillPage(PagerQuery<TaskBillQuery> para)
        {
            try
            {
                var dbtb = _rpstb.Queryable(q=>(q.PostID==para.Query.PostID||para.Query.PostID==Guid.Empty)&&(q.State==para.Query.TaskState||para.Query.TaskState==0)&&(q.BillCode.Contains(para.Query.Key)||q.BillCode==string.Empty)).ToList();
                var tbid = dbtb.Select(s => s.ID);

                var taskid = dbtb.Select(s => s.TaskID);
                var empids = dbtb.Select(p=>p.EmployeeID);

                var emps = _work.Repository<Core.Model.DB.Basic_Employee>().Queryable(p=>empids.Contains(p.ID));

                var dbts = _work.Repository<Bll_InspectTask>().Queryable(p=>taskid.Contains(p.ID));

                var dbtbs = _rpstbs.Queryable(p=>tbid.Contains(p.BillID)).ToList();

                var rev = from s in dbtb
                          let emp=emps.FirstOrDefault(p=>p.ID==s.EmployeeID)
                          let ts=dbts.FirstOrDefault(p=>p.ID==s.TaskID)
                          let tbs=dbtbs.Count()==0?0:dbtbs.FindAll(p=>p.BillID==s.ID).Max(s=>s.TroubleLevel)
                          select new TaskBillView
                          {
                              ID=s.ID,
                              BillCode=s.BillCode,
                              StartTime=s.StartTime,
                              TaskID=s.TaskID,
                              EmployeeID=s.EmployeeID,
                              DangerID=s.DangerID,
                              State=s.State,
                              EndTime=s.EndTime,
                              EmployeeName=emp.CNName,
                              PostID=s.PostID,
                              TaskName=ts.Name,
                              TaskResult=tbs==0?"":Command.GetItems(typeof(PublicEnum.EE_TroubleLevel)).FirstOrDefault(q => q.Value == tbs).Caption,
                          };
                var re = new Pager<TaskBillView>().GetCurrentPage(rev, para.PageSize, para.PageIndex);
                return new ActionResult<Pager<TaskBillView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<TaskBillView>>(ex);
            }
        }


        /// <summary>
        /// 根据主体ID获取任务详情
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<TaskSubjectBillView>> GetTaskBillSubjects(PagerQuery<TaskBillSubjectsQuery> para)
        {
            try
            {
                var dbtb = _rpstb.GetModel(para.Query.BillID);
                var dbtbs = _rpstbs.Queryable(p => p.BillID == para.Query.BillID && p.SubjectID == para.Query.SubjectID).ToList();

                var ids = dbtbs.Select(p => p.DangerID);

                var dbdanger = _work.Repository<Basic_Danger>().Queryable(p => ids.Contains(p.ID)).ToList();

                var dict = _work.Repository<Core.Model.DB.Basic_Dict>();

                var rev = from tbd in dbtbs
                          let  s=dbdanger.FirstOrDefault(p=>p.ID==tbd.DangerID)
                          select new TaskSubjectBillView
                          {
                              ID = tbd.ID,
                              DangerName = s.Name,
                              DangerID = s.ID,
                              BillID = dbtb.ID,
                              TaskResult = dbtb.State,
                              TaskResultName = Command.GetItems(typeof(PublicEnum.EE_TaskResultType)).FirstOrDefault(q => q.Value == dbtb.State).Caption,

                              TroubleLevel = tbd.TroubleLevel,
                              TroubleLevelName = tbd.TroubleLevel == 0 ? "" : Command.GetItems(typeof(PublicEnum.EE_TroubleLevel)).FirstOrDefault(q => q.Value == tbd.TroubleLevel).Caption,

                              TaskResultMemo = tbd.TaskResultMemo,

                              Eval_Method = tbd.Eval_Method,
                              MethodName = tbd.Eval_Method == 0 ? string.Empty : Command.GetItems(typeof(PublicEnum.EE_EvaluateMethod)).FirstOrDefault(q => q.Value == tbd.Eval_Method).Caption,

                              Eval_SGJG = tbd.Eval_SGJG,
                              SGJGDic = tbd.Eval_SGJG == Guid.Empty ? string.Empty : dict.GetModel(tbd.Eval_SGJG).DictName,

                              Eval_SGLX = tbd.Eval_SGLX,
                              SGLXDic = tbd.Eval_SGLX == Guid.Empty ? string.Empty : dict.GetModel(tbd.Eval_SGLX).DictName,

                              Eval_WHYS = tbd.Eval_WHYS,
                              WHYSDic = tbd.Eval_WHYS == Guid.Empty ? string.Empty : dict.GetModel(tbd.Eval_WHYS).DictName,

                              Eval_YXFW = tbd.Eval_YXFW,
                              YXFWDic = tbd.Eval_YXFW == Guid.Empty ? string.Empty : dict.GetModel(tbd.Eval_YXFW).DictName,
                              SubjectID = tbd.SubjectID,
                              SubjectType = tbd.SubjectType,
                              IsControl=tbd.IsControl
                          };
                var re = new Pager<TaskSubjectBillView>().GetCurrentPage(rev, para.PageSize, para.PageIndex);
                return new ActionResult<Pager<TaskSubjectBillView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<TaskSubjectBillView>>(ex);
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
                var businessmodel = _rpstb.GetModel(businessid);
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
                    BusinessType = PublicEnum.EE_BusinessType.TaskBill
                });
                if (flowcheck.state != 200)
                {
                    throw new Exception(flowcheck.msg);
                }
                if (flowcheck.data)
                {
                    businessmodel.State = (int)PublicEnum.BillFlowState.audited;
                    _rpstb.Update(businessmodel);
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
                var businessmodel = _rpstb.GetModel(taskid);
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
                    BusinessType = PublicEnum.EE_BusinessType.TaskBill
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

                   _rpstb.Update(businessmodel);
                    _work.Commit();
                }
                else
                {
                    //更新业务单据状态
                    businessmodel.State = (int)PublicEnum.BillFlowState.pending;
                   _rpstb.Update(businessmodel);

                    //写入审批流程起始任务
                    taskmodel.BusinessCode = businessmodel.BillCode;
                    taskmodel.BusinessDate = businessmodel.StartTime;

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
    }
}
