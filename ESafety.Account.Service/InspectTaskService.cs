﻿using ESafety.Account.IService;
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

    /// <summary>
    /// 巡查任务
    /// </summary>
    public class InspectTaskService : FlowBusinessService, IInspectTask
    {

        private IUnitwork _work = null;

        private IRepository<Bll_InspectTask> rpstask = null;
        private IRepository<Bll_InspectTaskSubject> rpstaskSubject = null;

        private Core.IFlow srvFlow = null;
        private IAttachFile srvfile = null;

        public InspectTaskService(IUnitwork work, Core.IFlow flow,IAttachFile file) : base(work, flow)
        {
            _work = work;
            Unitwork = work;
            srvfile = file;
            rpstask = work.Repository<Bll_InspectTask>();
            rpstaskSubject = work.Repository<Bll_InspectTaskSubject>();
            var s = AppUser;
            srvFlow = flow;
            var flowser = srvFlow as FlowService;
            flowser.AppUser = AppUser;
            flowser.ACOptions = ACOptions;

        }
        /// <summary>
        /// 新建任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public ActionResult<bool> AddNew(InspectTaskNew task)
        {
            try
            {
                if (task == null)
                {
                    throw new Exception("参数有误");
                }

                var dbtask = task.MAPTO<Bll_InspectTask>();
                dbtask.State = (int)PublicEnum.BillFlowState.normal;
                dbtask.Code = Command.CreateCode();
                dbtask.CreateMan = AppUser.EmployeeInfo.CNName;
                var dbsubjects = from s in task.TaskSubjects
                                 select new Bll_InspectTaskSubject
                                 {
                                     ID = Guid.NewGuid(),
                                     InspectTaskID = dbtask.ID,
                                     SubjectID = s.SubjectID,
                                     SubjectType = (int)s.SubjectType
                                 };

                rpstask.Add(dbtask);
                rpstaskSubject.Add(dbsubjects);
                _work.Commit();
                return new ActionResult<bool>(true);


            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }



        /// <summary>
        /// 改变任务状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public ActionResult<bool> ChangeState(InspectTaskChangeState state)
        {
            try
            {
                if (state == null)
                {
                    throw new Exception("参数有误");
                }
                var dbtask = rpstask.GetModel(state.ID);
                if (dbtask == null)
                {
                    throw new Exception("任务不存在");
                }
                if (dbtask.State != (int)PublicEnum.BillFlowState.audited || dbtask.State != (int)PublicEnum.BillFlowState.cancel)
                {
                    throw new Exception("当前状态不允许");
                }
                if (dbtask.State == (int)state.State)
                {
                    throw new Exception("状态有误");
                }
                dbtask.State = (int)state.State;

                rpstask.Update(dbtask);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelTask(Guid id)
        {
            try
            {
                var dbtask = rpstask.GetModel(id);

                if (dbtask == null)
                {
                    throw new Exception("任务不存在");
                }
                if (dbtask.State != (int)PublicEnum.BillFlowState.cancel)
                {
                    throw new Exception("任务状态不允许");
                }

                rpstask.Delete(dbtask);
                _work.Commit();
                return new ActionResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 修改任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public ActionResult<bool> EditTask(InspectTaskEdit task)
        {
            try
            {
                var dbtask = rpstask.GetModel(task.ID);
                if (dbtask == null)
                {
                    throw new Exception("任务不存在");
                }
                if (dbtask.State >= (int)PublicEnum.BillFlowState.pending)
                {
                    throw new Exception("任务状态不允许");
                }
                dbtask = task.CopyTo<Bll_InspectTask>(dbtask);
                dbtask.State = (int)PublicEnum.BillFlowState.normal;

                //处理明细主体
                var dbsubjects = from s in task.TaskSubjects
                                 select new Bll_InspectTaskSubject
                                 {
                                     ID = Guid.NewGuid(),
                                     InspectTaskID = dbtask.ID,
                                     SubjectID = s.SubjectID,
                                     SubjectType = (int)s.SubjectType
                                 };

                rpstask.Update(dbtask);
                rpstaskSubject.Delete(q => q.InspectTaskID == dbtask.ID);
                rpstaskSubject.Add(dbsubjects);
                _work.Commit();
                return new ActionResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <param name="qurey"></param>
        /// <returns></returns>
        public ActionResult<Pager<InspectTaskView>> GetTasks(PagerQuery<InspectTaskQuery> qurey)
        {
            try
            {
                var retemp = rpstask.Queryable(q =>
               (q.DangerID == qurey.Query.DangerID || qurey.Query.DangerID == Guid.Empty)
               && (qurey.Query.PostID == q.ExecutePostID || qurey.Query.PostID == Guid.Empty)
               && (qurey.Query.State == q.State || qurey.Query.State == 0)
               && (q.Name.Contains(qurey.Query.Key) || q.TaskDescription.Contains(qurey.Query.Key) || qurey.Query.Key == "")
               &&q.TaskType==(int)PublicEnum.EE_InspectTaskType.Cycle);
                var empids = retemp.Select(s => s.EmployeeID);//职员id
                var dangids = retemp.Select(s => s.DangerID); //风险点id
                var postids = retemp.Select(s => s.ExecutePostID);//岗位id

                var dangers = _work.Repository<Basic_Danger>().Queryable(q => dangids.Contains(q.ID));
                var emps = _work.Repository<Core.Model.DB.Basic_Employee>().Queryable(q => empids.Contains(q.ID));
                var posts = _work.Repository<Basic_Post>().Queryable(q => postids.Contains(q.ID));

                var re = from t in retemp.ToList()
                         let dange = dangers.FirstOrDefault(q => q.ID == t.DangerID)
                         let employee = emps.FirstOrDefault(q => q.ID == t.EmployeeID)
                         let postinfo = posts.FirstOrDefault(q => q.ID == t.ExecutePostID)
                         select new InspectTaskView
                         {
                             Code = t.Code,
                             CreateDate = t.CreateDate,
                             CreateMan = t.CreateMan,
                             CycleDateType = t.CycleDateType,
                             CycleValue = t.CycleValue,
                             DangerID = t.DangerID,
                             DangerName = dange == null ? "" : dange.Name,
                             Name = t.Name,
                             ID = t.ID,
                             EmployeeID = t.EmployeeID.GetValueOrDefault(),
                             EmployeeName = employee == null ? "" : employee.CNName,
                             EndTime = t.EndTime,
                             ExecutePostID = t.ExecutePostID,
                             ExecutePostName = postinfo == null ? "" : postinfo.Name,
                             StartTime = t.StartTime,
                             State = t.State,
                             StateName = Command.GetItems(typeof(PublicEnum.BillFlowState)).FirstOrDefault(q => q.Value == t.State).Caption,
                             TaskDescription = t.TaskDescription,
                             TaskType = (PublicEnum.EE_InspectTaskType)t.TaskType
                         };

                var result = new Pager<InspectTaskView>().GetCurrentPage(re, qurey.PageSize, qurey.PageIndex);

                return new ActionResult<Pager<InspectTaskView>>(result);


            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<InspectTaskView>>(ex);
            }
        }

        /// <summary>
        /// 获取临时任务列表
        /// </summary>
        /// <param name="qurey"></param>
        /// <returns></returns>
        public ActionResult<Pager<InspectTempTaskView>> GetTempTasks(PagerQuery<InspectTaskQuery> qurey)
        {
            try
            {
                var retemp = rpstask.Queryable(q =>
               (q.DangerID == qurey.Query.DangerID || qurey.Query.DangerID == Guid.Empty)
               && (qurey.Query.PostID == q.ExecutePostID || qurey.Query.PostID == Guid.Empty)
               && (qurey.Query.State == q.State || qurey.Query.State == 0)
               && (q.Name.Contains(qurey.Query.Key) || q.TaskDescription.Contains(qurey.Query.Key) || qurey.Query.Key == "")
               && q.TaskType == (int)PublicEnum.EE_InspectTaskType.Temp);
                var empids = retemp.Select(s => s.EmployeeID);//职员id
                var dangids = retemp.Select(s => s.DangerID); //风险点id
                var postids = retemp.Select(s => s.ExecutePostID);//岗位id

                var dangers = _work.Repository<Basic_Danger>().Queryable(q => dangids.Contains(q.ID));
                var emps = _work.Repository<Core.Model.DB.Basic_Employee>().Queryable(q => empids.Contains(q.ID));
                var posts = _work.Repository<Basic_Post>().Queryable(q => postids.Contains(q.ID));

                var re = from t in retemp.ToList()
                         let dange = dangers.FirstOrDefault(q => q.ID == t.DangerID)
                         let employee = emps.FirstOrDefault(q => q.ID == t.EmployeeID)
                         let postinfo = posts.FirstOrDefault(q => q.ID == t.ExecutePostID)
                         select new InspectTempTaskView
                         {
                             Code = t.Code,
                             CreateDate = t.CreateDate,
                             CreateMan = t.CreateMan,
                             DangerID = t.DangerID,
                             DangerName = dange == null ? "" : dange.Name,
                             Name = t.Name,
                             ID = t.ID,
                             EmployeeID = t.EmployeeID.GetValueOrDefault(),
                             EmployeeName = employee == null ? "" : employee.CNName,
                             EndTime = t.EndTime,
                             ExecutePostID = t.ExecutePostID,
                             ExecutePostName = postinfo == null ? "" : postinfo.Name,
                             StartTime = t.StartTime,
                             State = t.State,
                             StateName = Command.GetItems(typeof(PublicEnum.BillFlowState)).FirstOrDefault(q => q.Value == t.State).Caption,
                             TaskDescription = t.TaskDescription,
                             TaskType = (PublicEnum.EE_InspectTaskType)t.TaskType
                         };

                var result = new Pager<InspectTempTaskView>().GetCurrentPage(re, qurey.PageSize, qurey.PageIndex);

                return new ActionResult<Pager<InspectTempTaskView>>(result);


            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<InspectTempTaskView>>(ex);
            }
        }

        /// <summary>
        /// 获取任务明细主体
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<InspectTaskSubjectView>> GetTaskSubjects(Guid taskid)
        {
            try
            {
                var retemp = rpstaskSubject.Queryable(q => q.InspectTaskID == taskid);
                var subids = retemp.Select(s => s.SubjectID);

                var rt = rpstask.GetModel(taskid);

                var devices = _work.Repository<Basic_Facilities>().Queryable(q => subids.Contains(q.ID)).ToList();
                var posts = _work.Repository<Basic_Post>().Queryable(q => subids.Contains(q.ID)).ToList();
                var opreats = _work.Repository<Basic_Opreation>().Queryable(q => subids.Contains(q.ID)).ToList();

                var danger = _work.Repository<Basic_Danger>().GetModel(rt.DangerID);
                var lv = _work.Repository<Basic_Dict>().GetModel(danger.DangerLevel);

                var re = from s in retemp.ToList()
                         let subinfo =
                         (PublicEnum.EE_SubjectType)s.SubjectType == PublicEnum.EE_SubjectType.Device ? devices.FirstOrDefault(q => q.ID == s.SubjectID).Name :
                         (PublicEnum.EE_SubjectType)s.SubjectType == PublicEnum.EE_SubjectType.Opreate ? opreats.FirstOrDefault(q => q.ID == s.SubjectID).Name :
                         (PublicEnum.EE_SubjectType)s.SubjectType == PublicEnum.EE_SubjectType.Post ? posts.FirstOrDefault(q => q.ID == s.SubjectID).Name : ""
                         select new InspectTaskSubjectView
                         {
                             ID = s.ID,
                             InspectTaskID = s.InspectTaskID,
                             SubjectID = s.SubjectID,
                             SubjectName = subinfo,
                             SubjectType = s.SubjectType,
                             SubjectTypeName = Command.GetItems(typeof(PublicEnum.EE_SubjectType)).FirstOrDefault(q => q.Value == s.SubjectType).Caption,
                             DangerLevel=lv==null?"":lv.DictName
                         };

                return new ActionResult<IEnumerable<InspectTaskSubjectView>>(re);

            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<InspectTaskSubjectView>>(ex);
            }
        }
        /// <summary>
        /// 业务单据审核
        /// </summary>
        /// <param name="businessid"></param>
        /// <returns></returns>
        public override ActionResult<bool> Approve( Guid businessid) 
        {
            try
            {
                var businessmodel = rpstask.GetModel(businessid);
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
                    BusinessType =businessmodel.TaskType==(int)PublicEnum.EE_InspectTaskType.Cycle?PublicEnum.EE_BusinessType.InspectTask:PublicEnum.EE_BusinessType.TempTask
                });
                if (flowcheck.state != 200)
                {
                    throw new Exception(flowcheck.msg);
                }
                if (flowcheck.data)
                {
                    businessmodel.State = (int)PublicEnum.BillFlowState.audited;
                    rpstask.Update(businessmodel);
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
                var businessmodel = rpstask.GetModel(taskid);
                if (businessmodel == null)
                {
                    throw new Exception("任务不存在");
                }
                if (businessmodel.State >= (int)PublicEnum.BillFlowState.pending)
                {
                    throw new Exception("状态不支持");
                }

                var flowtask = base.StartFlow(new BusinessAprovePara
                {
                    BusinessID = businessmodel.ID,
                    BusinessType = businessmodel.TaskType == (int)PublicEnum.EE_InspectTaskType.Cycle ? PublicEnum.EE_BusinessType.InspectTask : PublicEnum.EE_BusinessType.TempTask
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

                    rpstask.Update(businessmodel);
                    _work.Commit();
                }
                else
                {
                    //更新业务单据状态
                    businessmodel.State = (int)PublicEnum.BillFlowState.pending;
                    rpstask.Update(businessmodel);

                    //写入审批流程起始任务
                    taskmodel.BusinessCode = businessmodel.Code;
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
        /// 获取指定id的巡检任务模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<InspectTaskModelView> GetModel(Guid id)
        {
            try
            {
                var dbmodel = rpstask.GetModel(id);
                if (dbmodel == null)
                {
                    throw new Exception("任务未找到");
                }
                var remodel = dbmodel.MAPTO<InspectTaskModelView>();
                var dangmodel = _work.Repository<Basic_Danger>().GetModel(q => q.ID == remodel.DangerID);
                var emp = _work.Repository<Basic_Employee>().GetModel(q => q.ID == remodel.EmployeeID);
                var post = _work.Repository<Basic_Post>().GetModel(q => q.ID == remodel.ExecutePostID);

                remodel.DangerName = dangmodel == null ? default(string) : dangmodel.Name;
                remodel.EmployeeName = emp == null ? default(string) : emp.CNName;
                remodel.StateName = Command.GetItems(typeof(PublicEnum.BillFlowState)).FirstOrDefault(q => q.Value == remodel.State).Caption;
                remodel.ExecutePostName = post == null ? default(string) : post.Name;

                var subs = rpstaskSubject.Queryable(q => q.InspectTaskID == remodel.ID).ToList();
                var subids = subs.Select(s => s.SubjectID);

                var devs = _work.Repository<Basic_Facilities>().Queryable(q => subids.Contains(q.ID)).ToList();
                var posts = _work.Repository<Basic_Post>().Queryable(q => subids.Contains(q.ID)).ToList();
                var opres = _work.Repository<Basic_Opreation>().Queryable(q => subids.Contains(q.ID)).ToList();

                remodel.Subjects = from s in subs
                                   let dev = devs.FirstOrDefault(q => q.ID == s.SubjectID)
                                   let ppst = posts.FirstOrDefault(q => q.ID == s.SubjectID)
                                   let opr = opres.FirstOrDefault(q => q.ID == s.SubjectID)
                                   select new InspectTaskSubjectView
                                   {
                                       ID = s.ID,
                                       InspectTaskID = s.InspectTaskID,
                                       SubjectID = s.SubjectID,
                                       SubjectType = s.SubjectType,
                                       SubjectTypeName = Command.GetItems(typeof(PublicEnum.EE_SubjectType)).FirstOrDefault(q => q.Value == s.SubjectType).Caption,
                                       SubjectName =
                                          dev != null ? dev.Name : ppst != null ? ppst.Name : opr != null ? opr.Name : default(string)
                                   };
                return new ActionResult<InspectTaskModelView>(remodel);
            }
            catch (Exception ex)
            {
                return new ActionResult<InspectTaskModelView>(ex);
            }
        }
        /// <summary>
        /// 获取当前用户的任务列表
        /// 
        /// </summary>
        /// <param name="istimeout"></param>
        /// <returns></returns>
        private ActionResult<IEnumerable<InsepctTaskByEmployee>> GetTaskList(bool istimeout)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<InsepctTaskByEmployee>>(ex);
            }
        }
        /// <summary>
        /// 获取当前用户的任务列表
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<InsepctTaskByEmployee>> GetTaskListByEmployee()
        {
            try
            {
                var user = AppUser.EmployeeInfo;
                var userpost = _work.Repository<Basic_PostEmployees>().Queryable(p => p.EmployeeID == user.ID);
                var postids = userpost.Select(s => s.PostID);

                if (user == null)
                {
                    throw new Exception("还未配置该用户!");
                }
                var tasks = rpstask.Queryable(q =>( q.EmployeeID ==user.ID||postids.Contains(q.ExecutePostID))
                                                   &&q.State== (int)PublicEnum.BillFlowState.audited
                                                   &&q.TaskType==(int)PublicEnum.EE_InspectTaskType.Cycle
                                                   ).ToList();

                //风险点
                var dangerids = tasks.Select(s => s.DangerID).ToList();
                var dangers = _work.Repository<Basic_Danger>().Queryable(q=>dangerids.Contains(q.ID)).ToList();

                //任务单据
                var taskids = tasks.Select(s => s.ID);
                var bills = _work.Repository<Bll_TaskBill>().Queryable(q=>taskids.Contains(q.TaskID)).ToList();

                //任务主体
                var billids = bills.Select(s => s.ID);
                var billsubjects = _work.Repository<Bll_TaskBillSubjects>().Queryable(q=>billids.Contains(q.BillID)).ToList();

           
                var re = from t in tasks
                         let danger = dangers.FirstOrDefault(q => q.ID== t.DangerID)
                         let bill=bills.FirstOrDefault(q=>q.TaskID==t.ID)
                         let date=t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Year?t.CycleValue*365*24*60
                         : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Month ?t.CycleValue*30*24*60
                         : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Week ? t.CycleValue * 7 * 24 * 60
                         : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Day ? t.CycleValue * 24 * 60
                         : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Houre ? t.CycleValue * 60
                         : t.CycleValue
                         let billsub=bill==null?null: billsubjects.FirstOrDefault(q => q.BillID == bill.ID)
                         where bill==null|| billsub == null||(billsubjects.Where(q => q.BillID == bill.ID).Max(s => s.TaskTime)-DateTime.Now).TotalMinutes<date
                         select new InsepctTaskByEmployee
                         {
                             
                             TaskID = t.ID,
                             DangerID = t.DangerID,
                             DangerName = danger.Name,
                             Name = t.Name,
                             TaskTypeID = (PublicEnum.EE_InspectTaskType)t.TaskType,
                             TaskTypeName = Command.GetItems(typeof(PublicEnum.EE_InspectTaskType)).FirstOrDefault(p => p.Value == t.TaskType).Caption,
                             TaskDescription=t.TaskDescription,
                             //最后时间和超时时间
                             LastTime= billsub == null?"":billsubjects.Where(q => q.BillID == bill.ID).Max(s => s.TaskTime).ToString(),
                             TimeOutHours= 0,
                             CycleDateType=t.CycleDateType,
                             CycleValue=t.CycleValue,
                             CycleName = Command.GetItems(typeof(PublicEnum.EE_CycleDateType)).FirstOrDefault(p => p.Value == t.CycleDateType).Caption,
                         };

                return new ActionResult<IEnumerable<InsepctTaskByEmployee>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<InsepctTaskByEmployee>>(ex);
            }
        }
        /// <summary>
        /// 获取当前用户超时任务列表
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<InsepctTaskByEmployee>> GetTaskListByTimeOut()
        {
            try
            {
                var user = AppUser.EmployeeInfo;
                if (user == null)
                {
                    throw new Exception("还未配置该用户!");
                }
                var userpost = _work.Repository<Basic_PostEmployees>().Queryable(p => p.EmployeeID == user.ID);
                var postids = userpost.Select(s => s.PostID);

                var tasks = rpstask.Queryable(q => (q.EmployeeID == user.ID||postids.Contains(q.ExecutePostID))
                                                   && q.State == (int)PublicEnum.BillFlowState.audited
                                                   && q.State==(int)PublicEnum.EE_InspectTaskType.Cycle
                                                   ).ToList();

                //风险点
                var dangerids = tasks.Select(s => s.DangerID);
                var dangers = _work.Repository<Basic_Danger>().Queryable(q => dangerids.Contains(q.ID)).ToList();

                //任务单据
                var taskids = tasks.Select(s => s.ID);
                var bills = _work.Repository<Bll_TaskBill>().Queryable(q => taskids.Contains(q.TaskID)).ToList();

                //任务主体
                var billids = bills.Select(s => s.ID);
                var billsubjects = _work.Repository<Bll_TaskBillSubjects>().Queryable(q => billids.Contains(q.BillID)).ToList();


                var re = from t in tasks
                         let danger = dangers.FirstOrDefault(q => q.ID == t.DangerID)
                         let bill = bills.FirstOrDefault(q => q.TaskID == t.ID)
                         let date = t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Year ? t.CycleValue * 365 * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Month ? t.CycleValue * 30 * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Week ? t.CycleValue * 7 * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Day ? t.CycleValue * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Houre ? t.CycleValue * 60
                        : t.CycleValue
                         let billsub = bill == null ? null : billsubjects.FirstOrDefault(q => q.BillID == bill.ID)
                         where bill != null && billsub !=null&& (billsubjects.Where(q => q.BillID == bill.ID).Max(s => s.TaskTime) - DateTime.Now).TotalMinutes > date
                         select new InsepctTaskByEmployee
                         {

                             TaskID = t.ID,
                             DangerID = t.DangerID,
                             DangerName = danger.Name,
                             Name = t.Name,
                             TaskTypeID = (PublicEnum.EE_InspectTaskType)t.TaskType,
                             TaskTypeName = Command.GetItems(typeof(PublicEnum.EE_InspectTaskType)).FirstOrDefault(p => p.Value == t.TaskType).Caption,
                             TaskDescription = t.TaskDescription,
                             //最后时间和超时时间
                             LastTime = billsubjects.Where(q => q.BillID == bill.ID).Max(s => s.TaskTime).ToString(),
                             TimeOutHours = (int)(billsubjects.Where(q => q.BillID == bill.ID).Max(s => s.TaskTime) - DateTime.Now).TotalMinutes,
                             CycleDateType = t.CycleDateType,
                             CycleValue = t.CycleValue,
                             CycleName=Command.GetItems(typeof(PublicEnum.EE_CycleDateType)).FirstOrDefault(p=>p.Value==t.CycleDateType).Caption
                         };

                return new ActionResult<IEnumerable<InsepctTaskByEmployee>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<InsepctTaskByEmployee>>(ex);
            }
        }

        /// <summary>
        /// 获取当前用户超时任务列表
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<InsepctTempTaskByEmployee>> GetTempTaskListByEmployee()
        {
            try
            {
                var user = AppUser.EmployeeInfo;
                var userpost = _work.Repository<Basic_PostEmployees>().Queryable(p => p.EmployeeID == user.ID);
                var postids = userpost.Select(s => s.PostID);

                if (user == null)
                {
                    throw new Exception("还未配置该用户!");
                }
                var tasks = rpstask.Queryable(q => (q.EmployeeID == user.ID || postids.Contains(q.ExecutePostID))
                                                   && q.State == (int)PublicEnum.BillFlowState.audited
                                                   && q.TaskType == (int)PublicEnum.EE_InspectTaskType.Temp
                                                   && q.EndTime >DateTime.Now
                                                   ).ToList();

                //风险点
                var dangerids = tasks.Select(s => s.DangerID).ToList();
                var dangers = _work.Repository<Basic_Danger>().Queryable(q => dangerids.Contains(q.ID)).ToList();
                var re = from t in tasks
                         let danger = dangers.FirstOrDefault(q => q.ID == t.DangerID)
                         select new InsepctTempTaskByEmployee
                         {
                             TaskID = t.ID,
                             DangerID = t.DangerID,
                             DangerName = danger.Name,
                             Name = t.Name,
                             TaskTypeID = (PublicEnum.EE_InspectTaskType)t.TaskType,
                             TaskTypeName = Command.GetItems(typeof(PublicEnum.EE_InspectTaskType)).FirstOrDefault(p => p.Value == t.TaskType).Caption,
                             TaskDescription = t.TaskDescription,
                             StartTime=t.StartTime,
                             EndTime=t.EndTime
                         };

                return new ActionResult<IEnumerable<InsepctTempTaskByEmployee>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<InsepctTempTaskByEmployee>>(ex);
            }
        }
        /// <summary>
        /// 分配任务执行岗位与人员
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public ActionResult<bool> AllotTempTaskEmp(AllotTempTaskEmp emp)
        {
            try
            {
                if (emp.EmpID == null)
                {
                    throw new Exception("必须分配岗位与执行人");
                }
                var task = rpstask.GetModel(emp.TempTaskID);
                if (task == null)
                {
                    throw new Exception("未找到该任务!");
                }
                if (task.State != (int)PublicEnum.BillFlowState.audited)
                {
                    throw new Exception("单据状态不允许!");
                }
                task.EmployeeID = emp.EmpID;
                task.ExecutePostID = emp.PostID;

                Bll_TaskBill taskBill = new Bll_TaskBill
                {
                    BillCode = Command.CreateCode(),
                    DangerID = task.DangerID,
                    PostID = emp.PostID,
                    EmployeeID = emp.EmpID,
                    TaskID = emp.TempTaskID,
                    StartTime = DateTime.Now,
                    State = (int)PublicEnum.BillFlowState.wait,
                    EndTime = task.EndTime,
                    ID = Guid.NewGuid()
                };

                rpstask.Update(task);
                _work.Repository<Bll_TaskBill>().Add(taskBill);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 移动端新建临时任务
        /// </summary>
        /// <param name="temptask"></param>
        /// <returns></returns>
        public ActionResult<bool> AddTempTask(AddTempTask temptask)
        {
            try
            {
                if (temptask == null)
                {
                    throw new Exception("参数有误");
                }
                var dbtask = temptask.MAPTO<Bll_InspectTask>();
                dbtask.State = (int)PublicEnum.BillFlowState.normal;
                dbtask.Code = Command.CreateCode();
                dbtask.CreateMan = AppUser.EmployeeInfo.CNName;
                dbtask.TaskType = (int)PublicEnum.EE_InspectTaskType.Temp;

                var dbsubjects = from s in temptask.TaskSubjects
                                 select new Bll_InspectTaskSubject
                                 {
                                     ID = Guid.NewGuid(),
                                     InspectTaskID = dbtask.ID,
                                     SubjectID = s.SubjectID,
                                     SubjectType = (int)s.SubjectType
                                 };
                //电子文档
                var files = new AttachFileSave
                {
                    BusinessID = dbtask.ID,
                    files = from f in temptask.AttachFiles
                            select new AttachFileNew
                            {
                                FileTitle = f.FileTitle,
                                FileType = f.FileType,
                                FileUrl = f.FileUrl
                            }
                };

                var fre = srvfile.SaveFiles(files);
                if (fre.state != 200)
                {
                    throw new Exception(fre.msg);
                }


                rpstask.Add(dbtask);
                rpstaskSubject.Add(dbsubjects);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
            
        }
        /// <summary>
        /// 获取临时选择器
        /// </summary>
        /// <returns></returns>
        public ActionResult<TempTaskSelector> GetTempTaskSelector()
        {
            try
            {
                var dgs = _work.Repository<Basic_Danger>().Queryable();
                var dangers = from dg in dgs
                              select new Danger
                              {
                                  DangerID = dg.ID,
                                  DangerName = dg.Name
                              };
                var posts = _work.Repository<Basic_Post>().Queryable();
                var exposts = from p in posts
                              select new Post
                              {
                                  PostID = p.ID,
                                  PostName = p.Name
                              };



                var devices = _work.Repository<Basic_Facilities>().Queryable();
                var sorts = _work.Repository<Basic_FacilitiesSort>().Queryable();

                var opreats = _work.Repository<Basic_Opreation>().Queryable();

                var subtypes = Command.GetItems(typeof(PublicEnum.EE_SubjectType));

                List<Sub> subs = new List<Sub>();
                foreach (var item in subtypes)
                {
                    Sub sub = new Sub();
                    dynamic type = null;
                    if (item.Value ==(int)PublicEnum.EE_SubjectType.Device)
                    {
                        //设备设施，此类别下有设备的才要
                        type = from sort in sorts
                               where devices.Any(p=>p.SortID==sort.ID)==true
                               select new EntityType
                               {
                                    EntityTypeName=sort.SortName,
                                    Entities=from dev in devices
                                             where dev.SortID==sort.ID
                                             select new Entity
                                             {
                                                 SubjectID=dev.ID,
                                                 SubName=dev.Name,
                                             } 

                                };
                        if (type!=null)
                        {
                            sub.SubTypeName = item.Caption;
                        }
                    }
                    else if(item.Value == (int)PublicEnum.EE_SubjectType.Opreate)
                    {
                        //操作流程
                        type = new List<EntityType>();
                        var ent = new EntityType();
                        ent.EntityTypeName = "";
                        ent.Entities = from op in opreats
                                        select new Entity
                                        {
                                            SubjectID = op.ID,
                                            SubName = op.Name
                                        };
                        if (ent.Entities.Count() > 0)
                        {
                            type.Add(ent);//有具体的流程才添加
                            sub.SubTypeName = item.Caption;
                        }

                    }
                    else if (item.Value == (int)PublicEnum.EE_SubjectType.Post)
                    {
                        //岗位
                        type = new List<EntityType>();
                        var ent = new EntityType();
                        ent.EntityTypeName = "";
                        ent.Entities = from op in posts
                                        select new Entity
                                        {
                                            SubjectID = op.ID,
                                            SubName = op.Name
                                        };
                        if (ent.Entities.Count()>0)
                        {
                            type.Add(ent);//有具体岗位才添加
                            sub.SubTypeName = item.Caption;
                        }
                        
                    }

                    sub.Subjects = type;
                    subs.Add(sub);
 
                }


                var re = new TempTaskSelector
                {
                    Dangers = dangers,
                    Posts=exposts,
                    Subs=subs
                };
                return new ActionResult<TempTaskSelector>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<TempTaskSelector>(ex);
            }
            throw new NotImplementedException();
        }
    }
}
