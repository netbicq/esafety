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
        private ITree srvTree = null;
        public InspectTaskService(IUnitwork work, Core.IFlow flow,IAttachFile file,ITree tree) : base(work, flow)
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
            srvTree = tree;

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
                                     DangerID=s.DangerID,
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
                                     DangerID=s.DangerID,
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
               (q.DangerPointID == qurey.Query.DangerPointID || qurey.Query.DangerPointID == Guid.Empty)
               && (qurey.Query.PostID == q.ExecutePostID || qurey.Query.PostID == Guid.Empty)
               && (qurey.Query.State.HasValue==false?true:qurey.Query.State==q.State)
               && (q.Name.Contains(qurey.Query.Key) || q.TaskDescription.Contains(qurey.Query.Key) || qurey.Query.Key == "")
               &&q.TaskType==(int)PublicEnum.EE_InspectTaskType.Cycle);


                var empids = retemp.Select(s => s.EmployeeID);//职员id
                var dpids = retemp.Select(s => s.DangerPointID); //风险点id
                var postids = retemp.Select(s => s.ExecutePostID);//岗位id

                var dangerpoints = _work.Repository<Basic_DangerPoint>().Queryable(q => dpids.Contains(q.ID));
                var emps = _work.Repository<Core.Model.DB.Basic_Employee>().Queryable(q => empids.Contains(q.ID));
                var posts = _work.Repository<Basic_Post>().Queryable(q => postids.Contains(q.ID));

                var re = from t in retemp.ToList()
                         let dangepoint = dangerpoints.FirstOrDefault(q => q.ID == t.DangerPointID)
                         let employee = emps.FirstOrDefault(q => q.ID == t.EmployeeID)
                         let postinfo = posts.FirstOrDefault(q => q.ID == t.ExecutePostID)
                         select new InspectTaskView
                         {
                             Code = t.Code,
                             CreateDate = t.CreateDate,
                             CreateMan = t.CreateMan,
                             CycleDateType = t.CycleDateType,
                             CycleValue = t.CycleValue,
                             DangerPointID = t.DangerPointID,
                             DangerPointName = dangepoint == null ? "" : dangepoint.Name,
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
                             TaskType = (PublicEnum.EE_InspectTaskType)t.TaskType,
                             MasterID=t.MasterID
                             

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
               (q.DangerPointID == qurey.Query.DangerPointID || qurey.Query.DangerPointID == Guid.Empty)
               && (qurey.Query.PostID == q.ExecutePostID || qurey.Query.PostID == Guid.Empty)
               && (qurey.Query.State == q.State || qurey.Query.State == 0)
               && (q.Name.Contains(qurey.Query.Key) || q.TaskDescription.Contains(qurey.Query.Key) || qurey.Query.Key == "")
               && q.TaskType == (int)PublicEnum.EE_InspectTaskType.Temp);
                var empids = retemp.Select(s => s.EmployeeID);//职员id
                var dangids = retemp.Select(s => s.DangerPointID); //风险点id
                var postids = retemp.Select(s => s.ExecutePostID);//岗位id

                var dangerpoints = _work.Repository<Basic_DangerPoint>().Queryable(q => dangids.Contains(q.ID));
                var emps = _work.Repository<Core.Model.DB.Basic_Employee>().Queryable(q => empids.Contains(q.ID));
                var posts = _work.Repository<Basic_Post>().Queryable(q => postids.Contains(q.ID));

                var re = from t in retemp.ToList()
                         let dangepoint = dangerpoints.FirstOrDefault(q => q.ID == t.DangerPointID)
                         let employee = emps.FirstOrDefault(q => q.ID == t.EmployeeID)
                         let postinfo = posts.FirstOrDefault(q => q.ID == t.ExecutePostID)
                         select new InspectTempTaskView
                         {
                             Code = t.Code,
                             CreateDate = t.CreateDate,
                             CreateMan = t.CreateMan,
                             DangerPointID = t.DangerPointID,
                             DangerPointName = dangepoint == null ? "" : dangepoint.Name,
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
                             TaskType = (PublicEnum.EE_InspectTaskType)t.TaskType,
                             MasterID=t.MasterID
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
                //当前任务所有主体ID
                var retemp = rpstaskSubject.Queryable(q => q.InspectTaskID == taskid);
                var subids = retemp.Select(s => s.SubjectID);
                //当前任务
                var rt = rpstask.GetModel(taskid);
                //当前任务的所有主体信息
                var subs = _work.Repository<Basic_DangerPointRelation>().Queryable(p=>p.DangerPointID==rt.DangerPointID&&subids.Contains(p.SubjectID));

                //当前任务所风控项
                var dangers = retemp.Select(s => s.DangerID);
                var danger = _work.Repository<Basic_Danger>().Queryable(p=>dangers.Contains(p.ID));
                //当前任务所有风控项的所有风险等级
                var lvids = danger.Select(s => s.DangerLevel);
                var lvs = _work.Repository<Basic_Dict>().Queryable(p=>lvids.Contains(p.ID));

                var re = from s in retemp.ToList()
                         let sub=subs.FirstOrDefault(p=>p.SubjectID==s.SubjectID)
                         let dg=danger.FirstOrDefault(p=>p.ID==s.DangerID)
                         let lv=lvs.FirstOrDefault(p=>p.ID==dg.DangerLevel)
                         select new InspectTaskSubjectView
                         {
                             ID = s.ID,
                             InspectTaskID = s.InspectTaskID,
                             SubjectID = s.SubjectID,
                             SubjectName = sub.SubjectName,
                             SubjectType = s.SubjectType,
                             SubjectTypeName = Command.GetItems(typeof(PublicEnum.EE_SubjectType)).FirstOrDefault(q => q.Value == s.SubjectType).Caption,
                             DangerName=dg.Name,
                             DangerLevel=lv==null?"":lv.DictName,
                             DangerID=dg.ID
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
                    MasterID=businessmodel.MasterID,
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
                    MasterID=businessmodel.MasterID,
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
                var dangmodel = _work.Repository<Basic_DangerPoint>().GetModel(q => q.ID == remodel.DangerPointID);
                var emp = _work.Repository<Basic_Employee>().GetModel(q => q.ID == remodel.EmployeeID);
                var post = _work.Repository<Basic_Post>().GetModel(q => q.ID == remodel.ExecutePostID);

                remodel.DangerPointName= dangmodel == null ? default(string) : dangmodel.Name;
                remodel.EmployeeName = emp == null ? default(string) : emp.CNName;
                remodel.StateName = Command.GetItems(typeof(PublicEnum.BillFlowState)).FirstOrDefault(q => q.Value == remodel.State).Caption;
                remodel.ExecutePostName = post == null ? default(string) : post.Name;


                var tsubs = rpstaskSubject.Queryable(q => q.InspectTaskID == remodel.ID).ToList();
                var subids = tsubs.Select(s => s.SubjectID);

                //当前任务的所有主体信息
                var subs = _work.Repository<Basic_DangerPointRelation>().Queryable(p => p.DangerPointID == dbmodel.DangerPointID && subids.Contains(p.SubjectID));

                //当前任务所风控项
                var dangers = tsubs.Select(s => s.DangerID);
                var danger = _work.Repository<Basic_Danger>().Queryable(p => dangers.Contains(p.ID));
                //当前任务所有风控项的所有风险等级
                var lvids = danger.Select(s => s.DangerLevel);
                var lvs = _work.Repository<Basic_Dict>().Queryable(p => lvids.Contains(p.ID));

                remodel.Subjects = from s in tsubs
                                   let sub = subs.FirstOrDefault(p => p.SubjectID == s.SubjectID)
                                   let dg = danger.FirstOrDefault(p => p.ID == s.DangerID)
                                   let lv = lvs.FirstOrDefault(p => p.ID == dg.DangerLevel)
                                   select new InspectTaskSubjectView
                                   {
                                       ID = s.ID,
                                       InspectTaskID = s.InspectTaskID,
                                       SubjectID = s.SubjectID,
                                       SubjectType = s.SubjectType,
                                       SubjectTypeName = Command.GetItems(typeof(PublicEnum.EE_SubjectType)).FirstOrDefault(q => q.Value == s.SubjectType).Caption,
                                       SubjectName =sub.SubjectName,
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
                var dangerids = tasks.Select(s => s.DangerPointID).ToList();
                var dangers = _work.Repository<Basic_DangerPoint>().Queryable(q=>dangerids.Contains(q.ID)).ToList();

                //任务单据
                var taskids = tasks.Select(s => s.ID);
                var bills = _work.Repository<Bll_TaskBill>().Queryable(q=>taskids.Contains(q.TaskID)).ToList();

                //任务主体
                var billids = bills.Select(s => s.ID);
                var billsubjects = _work.Repository<Bll_TaskBillSubjects>().Queryable(q=>billids.Contains(q.BillID)).ToList();

                var re = from t in tasks
                         let danger = dangers.FirstOrDefault(q => q.ID == t.DangerPointID)
                         let tbills = bills.Where(q => q.TaskID == t.ID)
                        // let bill = tbills == null ? null : tbills.OrderByDescending(o => o.StartTime).FirstOrDefault()
                         let date = t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Year ? t.CycleValue * 365 * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Month ? t.CycleValue * 30 * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Week ? t.CycleValue * 7 * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Day ? t.CycleValue * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Houre ? t.CycleValue * 60
                        : t.CycleValue
                         let billsub = tbills == null ? null : billsubjects.OrderByDescending(o => o.TaskTime).FirstOrDefault(q => tbills.Select(s => s.ID).Contains(q.BillID))
                         let ctime = tbills == null ? (DateTime.Now - t.StartTime).TotalMinutes : billsub == null ? (DateTime.Now - t.StartTime).TotalMinutes : (DateTime.Now-billsub.TaskTime).TotalMinutes
                         where ctime < date
                         select new InsepctTaskByEmployee
                         {

                             TaskID = t.ID,
                             DangerPointID = t.DangerPointID,
                             DangerName = danger.Name,
                             Name = t.Name,
                             TaskTypeID = (PublicEnum.EE_InspectTaskType)t.TaskType,
                             TaskTypeName = Command.GetItems(typeof(PublicEnum.EE_InspectTaskType)).FirstOrDefault(p => p.Value == t.TaskType).Caption,
                             TaskDescription = t.TaskDescription,
                             //最后时间和超时时间
                             LastTime = billsub==null?"":billsub.TaskTime.ToString(),
                             TimeOutHours = 0,
                             CycleDateType = t.CycleDateType,
                             CycleValue = t.CycleValue,
                             CycleName = Command.GetItems(typeof(PublicEnum.EE_CycleDateType)).FirstOrDefault(p => p.Value == t.CycleDateType).Caption
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
            /*
             *超时计算，任务的所有单据，
             * 根据单据获取任务的所有检查明细
             * 如果没有检查详情，则超时时长=当前时间-任务创建时间-执行频率换算的时长
             * 如果有检查详情，则超时时长=当前时间-最后检查的时间-执行频率换算的时长
             */
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
                                                   && q.TaskType==(int)PublicEnum.EE_InspectTaskType.Cycle
                                                   ).ToList();

                //风险点
                var dangerids = tasks.Select(s => s.DangerPointID);
                var dangers = _work.Repository<Basic_DangerPoint>().Queryable(q => dangerids.Contains(q.ID)).ToList();

                //任务单据
                var taskids = tasks.Select(s => s.ID);
                var bills = _work.Repository<Bll_TaskBill>().Queryable(q => taskids.Contains(q.TaskID)).ToList();

                //任务主体
                var billids = bills.Select(s => s.ID).ToList();
                var billsubjects = _work.Repository<Bll_TaskBillSubjects>().Queryable(q => billids.Contains(q.BillID)).ToList();


                var re = from t in tasks
                         let danger = dangers.FirstOrDefault(q => q.ID == t.DangerPointID)
                         let tbills = bills.Where(q => q.TaskID == t.ID)
                         //let bill=tbills==null?null:tbills.OrderByDescending(o=>o.StartTime).FirstOrDefault()
                         let date = t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Year ? t.CycleValue * 365 * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Month ? t.CycleValue * 30 * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Week ? t.CycleValue * 7 * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Day ? t.CycleValue * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Houre ? t.CycleValue * 60
                        : t.CycleValue
                         let billsub = tbills == null ? null : billsubjects.OrderByDescending(o => o.TaskTime).FirstOrDefault(q => tbills.Select(s => s.ID).Contains(q.BillID))
                         let ctime=tbills==null?(DateTime.Now-t.StartTime).TotalMinutes: billsub == null ? (DateTime.Now - t.StartTime).TotalMinutes : (DateTime.Now-billsub.TaskTime).TotalMinutes
                         where ctime> date
                         select new InsepctTaskByEmployee
                         {

                             TaskID = t.ID,
                             DangerPointID = t.DangerPointID,
                             DangerName = danger.Name,
                             Name = t.Name,
                             TaskTypeID = (PublicEnum.EE_InspectTaskType)t.TaskType,
                             TaskTypeName = Command.GetItems(typeof(PublicEnum.EE_InspectTaskType)).FirstOrDefault(p => p.Value == t.TaskType).Caption,
                             TaskDescription = t.TaskDescription,
                             //最后时间和超时时间
                             LastTime = billsub==null?"":billsub.TaskTime.ToString(),
                             TimeOutHours = (int)ctime-date,
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
        /// 获取当前临时任务列表
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
                var dangerids = tasks.Select(s => s.DangerPointID).ToList();
                var dangers = _work.Repository<Basic_Danger>().Queryable(q => dangerids.Contains(q.ID)).ToList();
                var re = from t in tasks
                         let danger = dangers.FirstOrDefault(q => q.ID == t.DangerPointID)
                         select new InsepctTempTaskByEmployee
                         {
                             TaskID = t.ID,
                             DangerID = t.DangerPointID,
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
                    DangerPointID = task.DangerPointID,
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
                var check = rpstask.Any(p=>p.Name==temptask.Name);
                if (check)
                {
                    throw new Exception("任务名已存在!");
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
                                     DangerID=s.DangerID,
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
                var dps = _work.Repository<Basic_DangerPoint>().Queryable();
                var dangerpoints = from dg in dps
                              select new DangerPoint
                              {
                                  DangerPointID = dg.ID,
                                  DangerPointName = dg.Name
                              };
                var posts = _work.Repository<Basic_Post>().Queryable();
                var exposts = from p in posts
                              select new Post
                              {
                                  PostID = p.ID,
                                  PostName = p.Name
                              };

                var subtypes = Command.GetItems(typeof(PublicEnum.EE_SubjectType));
                var subtps = from st in subtypes
                             select new SubType
                             {
                                 SubjectType=(PublicEnum.EE_SubjectType)st.Value,
                                 SubTypeName=st.Caption
                             };
                var re = new TempTaskSelector
                {
                    DangerPoints = dangerpoints,
                    Posts=exposts,
                    SubTypes=subtps
                };
                return new ActionResult<TempTaskSelector>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<TempTaskSelector>(ex);
            }
           
        }

        /// <summary>
        /// 临时任务风控项选择器
        /// </summary>
        /// <param name="select"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<Sub>> GetTempTaskDangerSelector(TempTaskDangerSelect select)
        {
            try
            {
                
                var subs = _work.Repository<Basic_DangerPointRelation>().Queryable(p => p.DangerPointID == select.DangerPointID && p.SubjectType == select.SubType);

                var subids = subs.Select(s => s.SubjectID);

                var drs = _work.Repository<Basic_DangerRelation>().Queryable(p=>subids.Contains(p.SubjectID));

                var dangerids = drs.Select(s => s.DangerID);

                var dangers = _work.Repository<Basic_Danger>().Queryable(p=>dangerids.Contains(p.ID));

                var resubs = from sb in subs
                         select new Sub
                         {
                             SubID = sb.SubjectID,
                             SubName = sb.SubjectName,
                             Dangers=from d in drs
                                     let dg=dangers.FirstOrDefault(p=>p.ID==d.DangerID)
                                     where d.SubjectID==sb.SubjectID
                                     select new Danger
                                     {
                                         DangerID=d.DangerID,
                                         DangerName=dg.Name
                                     }
                         };

               
                return new ActionResult<IEnumerable<Sub>>(resubs);

            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<Sub>>(ex);
            }
           
        }
        /// <summary>
        /// 根据二维码获取任务
        /// </summary>
        /// <param name="dangerPoint"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<InsepctTaskByEmployee>> GetEmpTaskByQRCoder(Guid dangerPoint)
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

                var tasks = rpstask.Queryable(q => (q.EmployeeID == user.ID || postids.Contains(q.ExecutePostID))
                                                 && q.DangerPointID==dangerPoint
                                                 && q.State == (int)PublicEnum.BillFlowState.audited
                                                 && q.TaskType == (int)PublicEnum.EE_InspectTaskType.Cycle
                                                 ).ToList();

                //风险点
                var danger = _work.Repository<Basic_DangerPoint>().GetModel(q =>q.ID==dangerPoint);

                //任务单据
                var taskids = tasks.Select(s => s.ID);
                var bills = _work.Repository<Bll_TaskBill>().Queryable(q => taskids.Contains(q.TaskID)).ToList();

                //任务主体
                var billids = bills.Select(s => s.ID);
                var billsubjects = _work.Repository<Bll_TaskBillSubjects>().Queryable(q => billids.Contains(q.BillID)).ToList();

                var re = from t in tasks
                         let tbills = bills.Where(q => q.TaskID == t.ID)
                         // let bill = tbills == null ? null : tbills.OrderByDescending(o => o.StartTime).FirstOrDefault()
                         let date = t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Year ? t.CycleValue * 365 * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Month ? t.CycleValue * 30 * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Week ? t.CycleValue * 7 * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Day ? t.CycleValue * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Houre ? t.CycleValue * 60
                        : t.CycleValue
                         let billsub = tbills == null ? null : billsubjects.OrderByDescending(o => o.TaskTime).FirstOrDefault(q => tbills.Select(s => s.ID).Contains(q.BillID))
                         let ctime = tbills == null ? (DateTime.Now - t.StartTime).TotalMinutes : billsub == null ? (DateTime.Now - t.StartTime).TotalMinutes : (DateTime.Now - billsub.TaskTime).TotalMinutes
                         //where ctime < date
                         orderby (ctime-date) descending
                         select new InsepctTaskByEmployee
                         {

                             TaskID = t.ID,
                             DangerPointID = t.DangerPointID,
                             DangerName = danger.Name,
                             Name = t.Name,
                             TaskTypeID = (PublicEnum.EE_InspectTaskType)t.TaskType,
                             TaskTypeName = Command.GetItems(typeof(PublicEnum.EE_InspectTaskType)).FirstOrDefault(p => p.Value == t.TaskType).Caption,
                             TaskDescription = t.TaskDescription,
                             //最后时间和超时时间
                             LastTime = billsub == null ? "" : billsub.TaskTime.ToString(),
                             TimeOutHours = ctime < date?0:(int)(ctime-date),
                             CycleDateType = t.CycleDateType,
                             CycleValue = t.CycleValue,
                             CycleName = Command.GetItems(typeof(PublicEnum.EE_CycleDateType)).FirstOrDefault(p => p.Value == t.CycleDateType).Caption
                         };


                return new ActionResult<IEnumerable<InsepctTaskByEmployee>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<InsepctTaskByEmployee>>(ex);
            }


        }

        /// <summary>
        /// 获取超时的任务根据二维码
        /// </summary>
        /// <param name="dangerPoint"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<InsepctTaskByEmployee>> GetEmpTimeOutTaskByQRCoder(Guid dangerPoint)
        {
            /*
           *超时计算，任务的所有单据，
           * 根据单据获取任务的所有检查明细
           * 如果没有检查详情，则超时时长=当前时间-任务创建时间-执行频率换算的时长
           * 如果有检查详情，则超时时长=当前时间-最后检查的时间-执行频率换算的时长
           */
            try
            {
                var user = AppUser.EmployeeInfo;
                if (user == null)
                {
                    throw new Exception("还未配置该用户!");
                }
                var userpost = _work.Repository<Basic_PostEmployees>().Queryable(p => p.EmployeeID == user.ID);
                var postids = userpost.Select(s => s.PostID);

                var tasks = rpstask.Queryable(q => (q.EmployeeID == user.ID || postids.Contains(q.ExecutePostID))
                                                   && q.State == (int)PublicEnum.BillFlowState.audited
                                                   && q.TaskType == (int)PublicEnum.EE_InspectTaskType.Cycle
                                                   ).ToList();

                //风险点

                var danger = _work.Repository<Basic_DangerPoint>().GetModel(dangerPoint);
                //任务单据
                var taskids = tasks.Select(s => s.ID);
                var bills = _work.Repository<Bll_TaskBill>().Queryable(q => taskids.Contains(q.TaskID)).ToList();

                //任务主体
                var billids = bills.Select(s => s.ID);
                var billsubjects = _work.Repository<Bll_TaskBillSubjects>().Queryable(q => billids.Contains(q.BillID)).ToList();


                var re = from t in tasks
                         let tbills = bills.Where(q => q.TaskID == t.ID)
                         //let bill=tbills==null?null:tbills.OrderByDescending(o=>o.StartTime).FirstOrDefault()
                         let date = t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Year ? t.CycleValue * 365 * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Month ? t.CycleValue * 30 * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Week ? t.CycleValue * 7 * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Day ? t.CycleValue * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Houre ? t.CycleValue * 60
                        : t.CycleValue
                         let billsub = tbills == null ? null : billsubjects.OrderByDescending(o => o.TaskTime).FirstOrDefault(q => tbills.Select(s => s.ID).Contains(q.BillID))
                         let ctime = tbills == null ? (DateTime.Now - t.StartTime).TotalMinutes : billsub == null ? (DateTime.Now - t.StartTime).TotalMinutes : (DateTime.Now - billsub.TaskTime).TotalMinutes
                         where ctime > date
                         select new InsepctTaskByEmployee
                         {

                             TaskID = t.ID,
                             DangerPointID = t.DangerPointID,
                             DangerName = danger.Name,
                             Name = t.Name,
                             TaskTypeID = (PublicEnum.EE_InspectTaskType)t.TaskType,
                             TaskTypeName = Command.GetItems(typeof(PublicEnum.EE_InspectTaskType)).FirstOrDefault(p => p.Value == t.TaskType).Caption,
                             TaskDescription = t.TaskDescription,
                             //最后时间和超时时间
                             LastTime = billsub == null ? "" : billsub.TaskTime.ToString(),
                             TimeOutHours = (int)ctime - date,
                             CycleDateType = t.CycleDateType,
                             CycleValue = t.CycleValue,
                             CycleName = Command.GetItems(typeof(PublicEnum.EE_CycleDateType)).FirstOrDefault(p => p.Value == t.CycleDateType).Caption
                         };

                return new ActionResult<IEnumerable<InsepctTaskByEmployee>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<InsepctTaskByEmployee>>(ex);
            }
        }
        /// <summary>
        /// APP 统计 当前人组织架构下 获取所有超期任务
        /// </summary>
        /// <returns></returns>
        public ActionResult<Pager<TimeOutTask>> GetTimeOutTask(PagerQuery<string> query)
        {
            
            try
            {
                var user = AppUser.EmployeeInfo;
                (srvTree as TreeService).AppUser = AppUser;
                var orgIDs = srvTree.GetChildrenIds<Core.Model.DB.Basic_Org>(user.OrgID);

                //所有人
                var emps = _work.Repository<Basic_Employee>().Queryable();
                var empsByOrg = emps.Where(p=>orgIDs.Contains(p.OrgID));
                var empIds = empsByOrg.Select(s => s.ID);
                //所有岗位
                var posts = _work.Repository<Basic_PostEmployees>().Queryable(p=>empIds.Contains(p.EmployeeID));
                var postIds = posts.Select(p => p.PostID);
                var tasks = rpstask.Queryable(p=>postIds.Contains(p.ExecutePostID)&&p.TaskType==(int)PublicEnum.EE_InspectTaskType.Cycle).ToList();
                var postss = _work.Repository<Basic_Post>().Queryable(p => postIds.Contains(p.ID));


                //风险点

                var danger = _work.Repository<Basic_DangerPoint>().Queryable();
                //任务单据
                var taskids = tasks.Select(s => s.ID);
                var bills = _work.Repository<Bll_TaskBill>().Queryable(q => taskids.Contains(q.TaskID)).ToList();

                //任务主体
                var billids = bills.Select(s => s.ID);
                var billsubjects = _work.Repository<Bll_TaskBillSubjects>().Queryable(q => billids.Contains(q.BillID)).ToList();

                var dicts = _work.Repository<Core.Model.DB.Basic_Dict>().Queryable(p => p.ParentID == OptionConst.DangerLevel);

              

                var retemp = from t in tasks.ToList()
                         let tbills = bills.Where(q => q.TaskID == t.ID)
                         let dp=danger.FirstOrDefault(p=>p.ID==t.DangerPointID)
                         let lv=dicts.FirstOrDefault(p=>p.ID==dp.DangerLevel)
                         let emp=t.EmployeeID==null?null:emps.FirstOrDefault(p=>p.ID==t.EmployeeID)
                         let post=postss.FirstOrDefault(p=>p.ID==t.ExecutePostID)
                         //let bill=tbills==null?null:tbills.OrderByDescending(o=>o.StartTime).FirstOrDefault()
                         let date = t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Year ? t.CycleValue * 365 * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Month ? t.CycleValue * 30 * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Week ? t.CycleValue * 7 * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Day ? t.CycleValue * 24 * 60
                        : t.CycleDateType == (int)PublicEnum.EE_CycleDateType.Houre ? t.CycleValue * 60
                        : t.CycleValue
                         let billsub = tbills == null ? null : billsubjects.OrderByDescending(o => o.TaskTime).FirstOrDefault(q => tbills.Select(s => s.ID).Contains(q.BillID))
                         let ctime = tbills == null ? (DateTime.Now - t.StartTime).TotalMinutes : billsub == null ? (DateTime.Now - t.StartTime).TotalMinutes : (DateTime.Now - billsub.TaskTime).TotalMinutes
                         where ctime > date
                         select new TimeOutTask
                         {
                             Name ="任务名:"+t.Name,
                             EmpOrPost="执行人/岗位:"+(emp==null?post.Name:emp.CNName),
                             OverHours="超期时长:"+ (((int)ctime - date)/60.0).ToString("0.0")+"小时",
                             DangerLevel="风险等级:"+lv.DictName,
                             DangerPoint="风险点:"+dp.Name
                         };
                var re=new Pager<TimeOutTask>().GetCurrentPage(retemp,query.PageSize,query.PageIndex);
                return new ActionResult<Pager<TimeOutTask>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<TimeOutTask>>(ex);
            }

        }
    }
}
