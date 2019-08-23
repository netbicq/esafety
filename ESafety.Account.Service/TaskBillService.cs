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
using Newtonsoft.Json;
using Quick.WXHelper;
using Quick.WXHelper.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Service
{
    class TaskBillService : ServiceBase, ITaskBillService
    {
        private IUnitwork _work = null;
        private IRepository<Bll_TaskBill> _rpstb = null;
        private IRepository<Bll_TaskBillSubjects> _rpstbs = null;

        private Core.IFlow srvFlow = null;
        private IAttachFile srvFile = null;
        private IInspectTask srvTask = null;
        public TaskBillService(IUnitwork work, /*IFlow flow,*/ IAttachFile file, IInspectTask task) /*: base(work, flow)*/
        {
            _work = work;
            Unitwork = work;
            _rpstb = work.Repository<Bll_TaskBill>();
            _rpstbs = work.Repository<Bll_TaskBillSubjects>();

            //srvFlow = flow;
            //var flowser = srvFlow as FlowService;
            srvTask = task;
            srvFile = file;

        }
        ///// <summary>
        ///// 修改任务单据详情
        ///// </summary>
        ///// <param name="subjectBillEdit"></param>
        ///// <returns></returns>
        //public ActionResult<bool> EditTaskBillSubjects(TaskBillEval subjectBillEdit)
        //{
        //    try
        //    {

        //        var dbtbs = _rpstbs.GetModel(subjectBillEdit.ID);
        //        if (dbtbs == null)
        //        {
        //            throw new Exception("未找到要处理的信息");
        //        }
        //        dbtbs = subjectBillEdit.CopyTo<Bll_TaskBillSubjects>(dbtbs);
        //        _rpstbs.Update(dbtbs);
        //        _work.Commit();
        //        return new ActionResult<bool>(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ActionResult<bool>(ex);
        //    }
        //}
        /// <summary>
        /// 获取巡检任务模型
        /// </summary>
        /// <returns></returns>
        public ActionResult<TaskBillModelView> GetTaskBillModel(Guid id)
        {
            try
            {
                var dbtbs = _rpstbs.GetModel(id);
                var dbtb = _rpstb.GetModel(dbtbs.BillID);
                var _CanHandle = true;
                if (dbtbs == null)
                {
                    throw new Exception("未找到提交的单据的具体详情");
                }
                //当前单据若是已经有管控项则不能处理
                var check = _work.Repository<Bll_TroubleControlDetails>().Any(p => p.BillSubjectsID == dbtbs.BillID);
                if (check)
                {
                    _CanHandle = false;
                }


                var dict = _work.Repository<Core.Model.DB.Basic_Dict>();
                var dbdanger = _work.Repository<Basic_Danger>().GetModel(dbtbs.DangerID);
                var dbdpr = _work.Repository<Basic_DangerPointRelation>().GetModel(p => p.DangerPointID == dbtb.DangerPointID && p.SubjectID == dbtbs.SubjectID);
                var lv = dict.GetModel(dbdanger.DangerLevel);

                var re = new TaskBillModelView
                {
                    BillID = dbtbs.BillID,
                    DangerID = dbtbs.DangerID,
                    DangerName = dbdanger.Name,
                    SubjectID = dbtbs.SubjectID,
                    SubName = dbdpr.SubjectName,
                    SubjectType = dbtbs.SubjectType,
                    TaskResult = dbtbs.TaskResult,
                    TaskResultMemo = dbtbs.TaskResultMemo,
                    TaskResultName = Command.GetItems(typeof(PublicEnum.EE_TaskResultType)).FirstOrDefault(q => q.Value == dbtbs.TaskResult).Caption,
                    CanHandle = _CanHandle,
                    ID = dbtbs.ID,

                    TroubleLevel = dbtbs.TroubleLevel,
                    TroubleLevelName = dbtbs.TroubleLevel == Guid.Empty ? string.Empty : dict.GetModel(dbtbs.TroubleLevel).DictName,
                    Eval_Method = dbtbs.Eval_Method == -1 ? 0 : dbtbs.Eval_Method,
                    MethodName = dbtbs.Eval_Method == 0 ? string.Empty : dbtbs.Eval_Method == -1 ? string.Empty : Command.GetItems(typeof(PublicEnum.EE_EvaluateMethod)).FirstOrDefault(q => q.Value == dbtbs.Eval_Method).Caption,

                    Eval_SGJG = dbtbs.Eval_SGJG,
                    SGJGDic = dbtbs.Eval_SGJG == Guid.Empty ? string.Empty : dict.GetModel(dbtbs.Eval_SGJG).DictName,

                    Eval_SGLX = dbtbs.Eval_SGLX,
                    SGLXDic = dbtbs.Eval_SGLX == Guid.Empty ? string.Empty : dict.GetModel(dbtbs.Eval_SGLX).DictName,

                    Eval_WHYS = dbtbs.Eval_WHYS,
                    WHYSDic = dbtbs.Eval_WHYS == Guid.Empty ? string.Empty : dict.GetModel(dbtbs.Eval_WHYS).DictName,

                    Eval_YXFW = dbtbs.Eval_YXFW,
                    YXFWDic = dbtbs.Eval_YXFW == Guid.Empty ? string.Empty : dict.GetModel(dbtbs.Eval_YXFW).DictName,


                    LECD_L = dbtbs.LECD_L == Guid.Empty ? string.Empty : dict.GetModel(dbtbs.LECD_L).DictName,

                    LECD_E = dbtbs.LECD_E == Guid.Empty ? string.Empty : dict.GetModel(dbtbs.LECD_E).DictName,

                    LECD_C = dbtbs.LECD_C == Guid.Empty ? string.Empty : dict.GetModel(dbtbs.LECD_C).DictName,

                    LSD_L = dbtbs.LSD_L == Guid.Empty ? string.Empty : dict.GetModel(dbtbs.LSD_L).DictName,
                    LSD_S = dbtbs.LSD_S == Guid.Empty ? string.Empty : dict.GetModel(dbtbs.LSD_S).DictName,

                    CtrPrincipal = dbtbs.CtrPrincipal == Guid.Empty ? string.Empty : _work.Repository<Basic_Employee>().GetModel(dbtbs.CtrPrincipal).CNName,
                    IsControl = dbtbs.IsControl,
                    DangerLevel = lv.DictName

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
                var dbtb = _rpstb.Queryable(q => (q.PostID == para.Query.PostID || para.Query.PostID == Guid.Empty) && (para.Query.TaskState.HasValue == false ? true : q.State == para.Query.TaskState.Value));
                var tbid = dbtb.Select(s => s.ID);
                var dpids = dbtb.Select(s => s.DangerPointID);

                var popstid = dbtb.Select(s => s.PostID);
                var taskid = dbtb.Select(s => s.TaskID);
                var empids = dbtb.Select(p => p.EmployeeID);

                var emps = _work.Repository<Core.Model.DB.Basic_Employee>().Queryable(p => empids.Contains(p.ID));

                var dbts = _work.Repository<Bll_InspectTask>().Queryable(p => taskid.Contains(p.ID));

                var dbtbs = _rpstbs.Queryable(p => tbid.Contains(p.BillID));

                var posts = _work.Repository<Basic_Post>().Queryable(q => popstid.Contains(q.ID));

                var dps = _work.Repository<Basic_DangerPoint>().Queryable(p => dpids.Contains(p.ID));

                var dict = _work.Repository<Core.Model.DB.Basic_Dict>().Queryable();
                var rev = from s in dbtb
                          let emp = emps.FirstOrDefault(p => p.ID == s.EmployeeID)
                          let ts = dbts.FirstOrDefault(p => p.ID == s.TaskID)
                          let tbs = dbtbs.OrderByDescending(o => o.TroubleLevel).FirstOrDefault(p => p.BillID == s.ID)
                          let post = posts.FirstOrDefault(p => p.ID == s.PostID)
                          let dp = dps.FirstOrDefault(p => p.ID == s.DangerPointID)
                          let tlv = tbs == null ? null : dict.FirstOrDefault(p => p.ID == tbs.TroubleLevel)
                          where s.BillCode.Contains(para.Query.Key) || ts.Name.Contains(para.Query.Key) || para.Query.Key == string.Empty
                          orderby s.StartTime descending
                          select new TaskBillView
                          {
                              ID = s.ID,
                              BillCode = s.BillCode,
                              StartTime = s.StartTime,
                              TaskID = s.TaskID,
                              EmployeeID = s.EmployeeID,
                              DangerPointID = s.DangerPointID,
                              DangerPointName = dp.Name,
                              State = s.State,
                              StateName = (PublicEnum.BillFlowState)s.State,
                              EndTime = s.EndTime,
                              EmployeeName = emp.CNName,
                              PostID = s.PostID,
                              PostName = post.Name,
                              TaskName = ts.Name,
                              TaskResult = tlv == null ? "" : tlv.DictName,
                              //TaskResultValue = tbs == null ? 0 : tbs.TroubleLevel == -1 ? 0 : tbs.TroubleLevel
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
        /// 根据任务单据ID获取任务详情
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<TaskSubjectBillView>> GetTaskBillSubjects(PagerQuery<TaskBillSubjectsQuery> para)
        {
            try
            {

                var dbtb = _rpstb.GetModel(para.Query.BillID);
                if (dbtb.State == (int)PublicEnum.BillFlowState.wait)
                {
                    throw new Exception("单据还未完成，请等待单据完成!");
                }
                //风险点
                var dbDangerpPoint = _work.Repository<Basic_DangerPoint>().GetModel(dbtb.DangerPointID);


                var dbtbs = _rpstbs.Queryable(p => p.BillID == para.Query.BillID).ToList();
                var subids = dbtbs.Select(s => s.SubjectID);

                var dict = _work.Repository<Core.Model.DB.Basic_Dict>();



                //当前任务的所有主体信息
                var subs = _work.Repository<Basic_DangerPointRelation>().Queryable(p => p.DangerPointID == dbDangerpPoint.ID && subids.Contains(p.SubjectID));

                //当前任务所风控项
                var dangers = dbtbs.Select(s => s.DangerID);
                var danger = _work.Repository<Basic_Danger>().Queryable(p => dangers.Contains(p.ID));
                ////当前任务所有风控项的所有风险等级
                var lvids = danger.Select(s => s.DangerLevel);
                var lvs = _work.Repository<Basic_Dict>().Queryable(p => lvids.Contains(p.ID));

                var rev = from tbd in dbtbs
                          let sub = subs.FirstOrDefault(p => p.SubjectID == tbd.SubjectID)
                          let dg = danger.FirstOrDefault(p => p.ID == tbd.DangerID)
                          let lv = lvs.FirstOrDefault(p => p.ID == dg.DangerLevel)
                          select new TaskSubjectBillView
                          {
                              ID = tbd.ID,
                              DangerName = dg.Name,
                              DangerID = dg.ID,
                              BillID = dbtb.ID,
                              SubName = sub.SubjectName,
                              TaskResult = tbd.TaskResult,
                              TaskResultName = Command.GetItems(typeof(PublicEnum.EE_TaskResultType)).FirstOrDefault(q => q.Value == tbd.TaskResult).Caption,

                              TroubleLevel = tbd.TroubleLevel,
                              TroubleLevelName = tbd.TroubleLevel == Guid.Empty ? string.Empty : dict.GetModel(tbd.TroubleLevel).DictName,

                              TaskResultMemo = tbd.TaskResultMemo,

                              Eval_Method = tbd.Eval_Method == -1 ? 0 : tbd.Eval_Method,
                              MethodName = tbd.Eval_Method == 0 ? string.Empty : tbd.Eval_Method == -1 ? string.Empty : Command.GetItems(typeof(PublicEnum.EE_EvaluateMethod)).FirstOrDefault(q => q.Value == tbd.Eval_Method).Caption,

                              Eval_SGJG = tbd.Eval_SGJG,
                              SGJGDic = tbd.Eval_SGJG == Guid.Empty ? string.Empty : dict.GetModel(tbd.Eval_SGJG).DictName,

                              Eval_SGLX = tbd.Eval_SGLX,
                              SGLXDic = tbd.Eval_SGLX == Guid.Empty ? string.Empty : dict.GetModel(tbd.Eval_SGLX).DictName,

                              Eval_WHYS = tbd.Eval_WHYS,
                              WHYSDic = tbd.Eval_WHYS == Guid.Empty ? string.Empty : dict.GetModel(tbd.Eval_WHYS).DictName,

                              Eval_YXFW = tbd.Eval_YXFW,
                              YXFWDic = tbd.Eval_YXFW == Guid.Empty ? string.Empty : dict.GetModel(tbd.Eval_YXFW).DictName,

                              LECD_L = tbd.LECD_L == Guid.Empty ? string.Empty : dict.GetModel(tbd.LECD_L).DictName,

                              LECD_E = tbd.LECD_E == Guid.Empty ? string.Empty : dict.GetModel(tbd.LECD_E).DictName,

                              LECD_C = tbd.LECD_C == Guid.Empty ? string.Empty : dict.GetModel(tbd.LECD_C).DictName,

                              LSD_L = tbd.LSD_L == Guid.Empty ? string.Empty : dict.GetModel(tbd.LSD_L).DictName,
                              LSD_S = tbd.LSD_S == Guid.Empty ? string.Empty : dict.GetModel(tbd.LSD_S).DictName,

                              CtrPrincipal = tbd.CtrPrincipal == Guid.Empty ? string.Empty : _work.Repository<Basic_Employee>().GetModel(tbd.CtrPrincipal).CNName,

                              SubjectID = tbd.SubjectID,
                              SubjectType = tbd.SubjectType,
                              IsControl = tbd.IsControl,
                              DangerLevel = lv.DictName

                          };
                var re = new Pager<TaskSubjectBillView>().GetCurrentPage(rev, para.PageSize, para.PageIndex);
                return new ActionResult<Pager<TaskSubjectBillView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<TaskSubjectBillView>>(ex);
            }
        }

        #region Approve
        ///// <summary>
        ///// 业务单据审核
        ///// </summary>
        ///// <param name="businessid"></param>
        ///// <returns></returns>
        //public override ActionResult<bool> Approve(Guid businessid)
        //{
        //    try
        //    {
        //        var businessmodel = _rpstb.GetModel(businessid);
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
        //            BusinessType = PublicEnum.EE_BusinessType.TaskBill
        //        });
        //        if (flowcheck.state != 200)
        //        {
        //            throw new Exception(flowcheck.msg);
        //        }
        //        if (flowcheck.data)
        //        {
        //            businessmodel.State = (int)PublicEnum.BillFlowState.audited;
        //            _rpstb.Update(businessmodel);
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
        //        var businessmodel = _rpstb.GetModel(taskid);
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
        //            BusinessType = PublicEnum.EE_BusinessType.TaskBill
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

        //            _rpstb.Update(businessmodel);
        //            _work.Commit();
        //        }
        //        else
        //        {
        //            //更新业务单据状态
        //            businessmodel.State = (int)PublicEnum.BillFlowState.pending;
        //            _rpstb.Update(businessmodel);

        //            //写入审批流程起始任务
        //            taskmodel.BusinessCode = businessmodel.BillCode;
        //            taskmodel.BusinessDate = businessmodel.StartTime;

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
        #endregion

        /// <summary>
        /// 新建巡检任务单
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        public ActionResult<bool> AddTaskBillMaster(TaskBillNew bill)
        {
            try
            {
                if (bill == null)
                {
                    throw new Exception("参数错误!");
                }
                var check = _rpstb.Any(p => p.TaskID == bill.TaskID && p.State == (int)PublicEnum.BillFlowState.wait && p.EmployeeID == AppUser.EmployeeInfo.ID);
                if (check)
                {
                    throw new Exception("该待检查任务单据已存在!");
                }
                var dbbill = bill.MAPTO<Bll_TaskBill>();
                //单据编号
                dbbill.BillCode = Command.CreateCode();

                var task = _work.Repository<Bll_InspectTask>().GetModel(bill.TaskID);
                if (task == null)
                {
                    throw new Exception("未找到该任务!");
                }
                //执行岗位ID
                dbbill.PostID = task.ExecutePostID;
                //执行人ID
                dbbill.EmployeeID = AppUser.EmployeeInfo.ID;
                //风险点ID
                dbbill.DangerPointID = task.DangerPointID;
                //单据状态
                dbbill.State = (int)PublicEnum.BillFlowState.wait;

                _rpstb.Add(dbbill);
                _work.Commit();
                return new ActionResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 根据任务单id获取设备集合
        /// 如果在此单据中已经执行了的设备则不再提供
        /// </summary>
        /// <param name="taskbillid"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<TaskSubjectView>> GetTaskSubjects(Guid taskbillid)
        {
            try
            {
                var bill = _rpstb.GetModel(taskbillid);
                if (bill == null)
                {
                    throw new Exception("未找到该任务单!");
                }
                //获取已经执行了的主体
                var subs = _rpstbs.Queryable(p => p.BillID == taskbillid);
                var osubid = subs.Select(s => s.SubjectID);
                var odangerid = subs.Select(s => s.DangerID);

                //获取任务主体中未执行的主体
                var tasksubs = _work.Repository<Bll_InspectTaskSubject>().Queryable(q => q.InspectTaskID == bill.TaskID && (!osubid.Contains(q.SubjectID) || !odangerid.Contains(q.DangerID)));
                var subids = tasksubs.Select(s => s.SubjectID);


                //获取任务主体中未执行的主体信息
                var csubs = _work.Repository<Basic_DangerPointRelation>().Queryable(p => p.DangerPointID == bill.DangerPointID && subids.Contains(p.SubjectID)).Distinct();

                //当前任务所风控项
                var dangers = tasksubs.Select(s => s.DangerID);
                var danger = _work.Repository<Basic_Danger>().Queryable(p => dangers.Contains(p.ID));
                //当前任务所有风控项的所有风险等级
                var lvids = danger.Select(s => s.DangerLevel);
                var lvs = _work.Repository<Basic_Dict>().Queryable(p => lvids.Contains(p.ID));

                //所有未归档的管控项
                var ctrs = _work.Repository<Bll_TroubleControl>().Queryable(p => p.State != (int)PublicEnum.EE_TroubleState.history);
                var ctrids = ctrs.Select(s => s.ID);
                // 在此单据中未执行的主体信息
                var re = from sub in tasksubs.ToList()
                         let sb = csubs.FirstOrDefault(p => p.SubjectID == sub.SubjectID)
                         let dg = danger.FirstOrDefault(p => p.ID == sub.DangerID)
                         let lv = lvs.FirstOrDefault(p => p.ID == dg.DangerLevel)
                         let isCtr = _work.Repository<Bll_TroubleControlDetails>().Queryable(p => ctrids.Contains(p.TroubleControlID) && p.TaskSubjectsID == sub.ID).FirstOrDefault()
                         let sdids = _work.Repository<Basic_DangerSafetyStandards>().Queryable(p => p.DangerID == sub.DangerID).Select(s => s.SafetyStandardID)
                         let sds = _work.Repository<Basic_SafetyStandard>().Queryable(p => sdids.Contains(p.ID))
                         let rest = isCtr == null ? null : _rpstbs.Queryable(p => p.ID == isCtr.BillSubjectsID).FirstOrDefault()
                         select new TaskSubjectView
                         {
                             KeyID = sub.ID,
                             BillID = bill.ID,
                             SubID = sub.SubjectID,
                             DangerLevel = lv == null ? "" : lv.DictName,
                             SubType = (PublicEnum.EE_SubjectType)sub.SubjectType,
                             SubTypeName = (PublicEnum.EE_SubjectType)sub.SubjectType,
                             SubName = sb.SubjectName,
                             Principal = sb.SubjectPrincipal,
                             PrincipalTel = sb.SubjectPrincipalTel,
                             DangerName = dg.Name,
                             DangerID = dg.ID,
                             IsControl = isCtr == null ? false : true,
                             LastResult = rest == null ? "" : rest.TaskResultMemo,
                             SubStandards = from sd in sds
                                            select new SubStandard
                                            {
                                                SubStandardID = sd.ID,
                                                Name = "执行标准名:" + sd.Name,
                                                Accident = "事故措施:" + sd.Accident,
                                                Controls = "管理措施:" + sd.Controls,
                                                Engineering = "工程措施:" + sd.Engineering,
                                                Individual = "个体措施:" + sd.Individual
                                            }

                         };
                return new ActionResult<IEnumerable<TaskSubjectView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<TaskSubjectView>>(ex);
            }
        }




        /// <summary>
        /// 新建任务单据主体详情
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        public ActionResult<bool> AddTaskSubject(TaskBillSubjectNew bill)
        {
            try
            {
                if (bill == null)
                {
                    throw new Exception("参数有误!");
                }
                var check = _rpstbs.Any(p => p.BillID == bill.BillID && p.DangerID == bill.DangerID && p.SubjectID == bill.SubjectID);
                if (check)
                {
                    throw new Exception("检查结果已存在!");
                }
                var dbsub = bill.MAPTO<Bll_TaskBillSubjects>();
                dbsub.TaskTime = DateTime.Now;

                //电子文档
                var files = new AttachFileSave
                {
                    BusinessID = dbsub.ID,
                    files = from f in bill.AttachFiles
                            select new AttachFileNew
                            {
                                FileTitle = f.FileTitle,
                                FileType = f.FileType,
                                FileUrl = f.FileUrl
                            }
                };
                var fileresult = srvFile.SaveFiles(files);
                if (fileresult.state != 200)
                {
                    throw new Exception(fileresult.msg);
                }
                if (dbsub.TaskResult == (int)PublicEnum.EE_TaskResultType.abnormal)
                {
                    var tb = _rpstbs.Queryable(p => p.DangerID == bill.DangerID && p.SubjectID == bill.SubjectID).OrderByDescending(o => o.TaskTime).FirstOrDefault();



                    if (bill.Eval_WHYS == Guid.Empty)
                    {
                        throw new Exception("请选择危害因素！");
                    }
                    if (bill.Eval_SGLX == Guid.Empty)
                    {
                        throw new Exception("请选择事故类型！");
                    }
                    if (bill.Eval_YXFW == Guid.Empty)
                    {
                        throw new Exception("请选择影响范围！");
                    }
                    if (bill.Eval_SGJG == Guid.Empty)
                    {
                        throw new Exception("请选择事故后果！");
                    }
                    if (bill.Eval_Method == 0)
                    {
                        throw new Exception("请选择评估方法！");
                    }
                    else if (bill.Eval_Method == (int)PublicEnum.EE_EvaluateMethod.LECD)
                    {
                        if (bill.LECD_L == Guid.Empty)
                        {
                            throw new Exception("请选择LECD法的L");
                        }
                        if (bill.LECD_E == Guid.Empty)
                        {
                            throw new Exception("请选择LECD法的E");
                        }
                        if (bill.LECD_C == Guid.Empty)
                        {
                            throw new Exception("请选择LECD法的C");
                        }
                    }
                    else if (bill.Eval_Method == (int)PublicEnum.EE_EvaluateMethod.LSD)
                    {
                        if (bill.LSD_L == Guid.Empty)
                        {
                            throw new Exception("请选择LSD法的L");
                        }
                        if (bill.LSD_S == Guid.Empty)
                        {
                            throw new Exception("请选择LSD法的S");
                        }
                    }
                    if (bill.DangerLevel == Guid.Empty)
                    {
                        throw new Exception("请选择风险等级！");
                    }
                    if (bill.TroubleLevel == Guid.Empty)
                    {
                        throw new Exception("请选择隐患等级！");
                    }
                    if (bill.PrincipalID == Guid.Empty)
                    {
                        throw new Exception("请分配管控责任人！");
                    }
                    dbsub.IsControl = true;
                    dbsub.CtrPrincipal = bill.PrincipalID;
                }
                _rpstbs.Add(dbsub);
                _work.Commit();
                return new ActionResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 完成任务单据
        /// </summary>
        /// <param name="billid"></param>
        /// <returns></returns>
        public ActionResult<bool> TaskBillOver(Guid billid)
        {
            try
            {
                var dbbill = _rpstb.GetModel(billid);
                if (dbbill == null)
                {
                    throw new Exception("未找到该任务单据!");
                }
                if (dbbill.State != (int)PublicEnum.BillFlowState.wait)
                {
                    throw new Exception("当前状态不允许");
                }
                var resubs = _work.Repository<Bll_TaskBillSubjects>().Queryable(p => p.BillID == billid);
                var subids = resubs.Select(s => s.SubjectID);
                var dangerids = resubs.Select(s => s.DangerID);
                var check = _work.Repository<Bll_InspectTaskSubject>().Any(p => p.InspectTaskID == dbbill.TaskID /*&& !subids.Contains(p.SubjectID)*/ && !dangerids.Contains(p.DangerID));


                if (check)
                {
                    throw new Exception("存在未检查的项，无法提交完成单据!");
                }

                var bills = _rpstbs.Queryable(p => p.BillID == dbbill.ID).ToList();
                foreach (var bill in bills)
                {
                    //新建管控项
                    if (bill.TaskResult == (int)PublicEnum.EE_TaskResultType.abnormal)
                    {
                        var task = _work.Repository<Bll_InspectTaskSubject>().GetModel(p => p.InspectTaskID == dbbill.TaskID && p.DangerID == bill.DangerID && p.SubjectID == bill.SubjectID);
                        //检查时为异常状态时直接新建管控项
                        //所有未归档的管控项
                        var rpsCtr = _work.Repository<Bll_TroubleControl>();
                        var ctrs = rpsCtr.Queryable(p => p.State != (int)PublicEnum.EE_TroubleState.history);
                        var ctrids = ctrs.Select(s => s.ID);
                        var rpsCtrDetail = _work.Repository<Bll_TroubleControlDetails>();
                        var check1 = rpsCtrDetail.Any(p => ctrids.Contains(p.TroubleControlID) && p.BillSubjectsID == task.ID);
                        if (check1)
                        {
                            throw new Exception("当前项已处于管控在中，无法再提交异常状态!");
                        }

                        var ctr = new Bll_TroubleControl
                        {
                            ID = Guid.NewGuid(),
                            BillID = bill.BillID,
                            Code = Command.CreateCode(),
                            CreateDate = DateTime.Now,
                            State = (int)PublicEnum.EE_TroubleState.pending,
                            DangerLevel = bill.DangerLevel,
                            TroubleLevel = bill.TroubleLevel,
                            PrincipalID = bill.CtrPrincipal,
                            ControlDescription = "",
                            ControlName = "",
                            DangerPoint = dbbill.DangerPointID,

                        };
                        var ctrDetail = new Bll_TroubleControlDetails
                        {
                            ID = Guid.NewGuid(),
                            TaskSubjectsID = task.ID,
                            BillSubjectsID = bill.ID,
                            TroubleControlID = ctr.ID
                        };
                        rpsCtr.Add(ctr);
                        rpsCtrDetail.Add(ctrDetail);
                        /******************************发送管控项信息**************************************/
                        var ctrp = _work.Repository<Basic_Employee>().GetModel(bill.CtrPrincipal);
                        var msgToUser = _work.Repository<Auth_User>().GetModel(p => p.Login ==ctrp.Login);
                        var dangerPoint = _work.Repository<Basic_DangerPoint>().GetModel(dbbill.DangerPointID);
                        var sendData = new Dictionary<string, MessageDataBase>();
                        sendData.Add("first",new MessageDataBase { value="安全隐患待处理"});
                        sendData.Add("keyword1", new MessageDataBase { value = AppUser.EmployeeInfo.CNName });
                        sendData.Add("keyword2", new MessageDataBase { value = DateTime.Now.ToString("yyyy-MM-dddd HH:mm:ss") });
                        sendData.Add("keyword3", new MessageDataBase { value =dangerPoint.Name});
                        sendData.Add("keyword4", new MessageDataBase { value = bill.TaskTime.ToString("yyyy-MM-dddd HH:mm:ss") });
                        sendData.Add("remark", new MessageDataBase { value = "ESF微服为安全护航。" });
                        var Msg = new TemplateMessagePara
                        {
                            template_id = "46LiWSOZ1MGVh2j8_pvyMkmzfw4ItkVVrV0DQUg24cU",
                            touser =msgToUser.openID,
                            data=sendData,
                            url= "http://esfwx.quickcq.com/HiddenTrouble"
                        };
                        WxService.SendTemplateMessage(Msg);
                        /************************************************************************/
                    }
                }
                dbbill.EndTime = DateTime.Now;
                dbbill.State = (int)PublicEnum.BillFlowState.normal;
                _rpstb.Update(dbbill);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 删除任务单据
        /// </summary>
        /// <param name="billid"></param>
        /// <returns></returns>
        public ActionResult<bool> DelTaskBillMaster(Guid billid)
        {
            try
            {
                var bill = _rpstb.GetModel(billid);
                if (bill == null)
                {
                    throw new Exception("未找到所需删除的任务单据!");
                }
                if (bill.State != (int)PublicEnum.BillFlowState.wait)
                {
                    throw new Exception("单据状态不允许删除!");
                }
                var subs = _rpstbs.Queryable(p => p.BillID == billid);
                var businessids = subs.Select(s => s.ID);
                //删除单据处理上传的文件
                foreach (var id in businessids)
                {
                    srvFile.DelFileByBusinessId(id);
                }
                //删除单据处理的信息
                _rpstbs.Delete(p => p.BillID == bill.ID);
                //删除单据
                _rpstb.Delete(bill);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 获取当前待执行的任务单据
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<TaskBillModel>> GetTaskBillMasters()
        {
            try
            {
                var rpsDict = _work.Repository<Basic_Dict>();
                //当前人
                var user = AppUser.EmployeeInfo;
                //当前人的所有待完成单据
                var tbs = _rpstb.Queryable(q => q.EmployeeID == user.ID && q.State == (int)PublicEnum.BillFlowState.wait);

                //当前人所有单据对应的任务
                var taskids = tbs.Select(s => s.TaskID);
                var tasks = _work.Repository<Bll_InspectTask>().Queryable(p => taskids.Contains(p.ID));

                //单据的风险点
                var dangerids = tbs.Select(s => s.DangerPointID);
                var dangers = _work.Repository<Basic_DangerPoint>().Queryable(p => dangerids.Contains(p.ID));
                var subs = _work.Repository<Bll_InspectTaskSubject>().Queryable();
                var osubs = _rpstbs.Queryable();

                var re = from tb in tbs.ToList()
                         let task = tasks.FirstOrDefault(q => q.ID == tb.TaskID)
                         let danger = dangers.FirstOrDefault(q => q.ID == tb.DangerPointID)
                         let subcount = subs.Where(q => q.InspectTaskID == tb.TaskID).Count()//当前单据检查主体总数
                         let osubcount = osubs.Where(p => p.BillID == tb.ID).Count()//已查主体数
                         let whysids = JsonConvert.DeserializeObject<IEnumerable<Guid>>(danger.WXYSJson)
                         let dicts = rpsDict.Queryable(p => whysids.Contains(p.ID))
                         select new TaskBillModel
                         {
                             BillID = tb.ID,
                             StartTime = tb.StartTime,
                             EndTime = (DateTime)tb.EndTime,
                             EmployeeName = user.CNName,
                             TaskName = task.Name,
                             State = (PublicEnum.BillFlowState)tb.State,
                             DangerPointName = danger.Name,
                             SubCheckedCount = osubcount,
                             SubCount = subcount,
                             TaskType = (PublicEnum.EE_InspectTaskType)task.TaskType,
                             WHYSDicts = from d in dicts
                                         select new Dict
                                         {
                                             KeyID = d.ID,
                                             DictName = d.DictName,
                                             Memo = d.Memo
                                         }
                         };
                return new ActionResult<IEnumerable<TaskBillModel>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<TaskBillModel>>(ex);
            }

        }
        /// <summary>
        /// 获取历史任务单据
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<TaskBillModel>> GetTaskBillMastersOver()
        {
            try
            {
                //当前人
                var user = AppUser.EmployeeInfo;
                //当前人的所有已完成单据
                var tbs = _rpstb.Queryable(q => q.EmployeeID == user.ID && q.State >= (int)PublicEnum.BillFlowState.normal);

                //当前人所有单据对应的任务
                var taskids = tbs.Select(s => s.TaskID);
                var tasks = _work.Repository<Bll_InspectTask>().Queryable(p => taskids.Contains(p.ID));

                //单据的风险点
                var dangerids = tbs.Select(s => s.DangerPointID);
                var dangers = _work.Repository<Basic_DangerPoint>().Queryable(p => dangerids.Contains(p.ID));
                var osubs = _rpstbs.Queryable();
                var re = from tb in tbs.ToList()
                         let task = tasks.FirstOrDefault(q => q.ID == tb.TaskID)
                         let danger = dangers.FirstOrDefault(q => q.ID == tb.DangerPointID)
                         let osubcount = osubs.Where(p => p.BillID == tb.ID).Count()//已查主体数
                         select new TaskBillModel
                         {
                             BillID = tb.ID,
                             StartTime = tb.StartTime,
                             EndTime = (DateTime)tb.EndTime,
                             EmployeeName = user.CNName,
                             TaskName = task.Name,
                             State = (PublicEnum.BillFlowState)tb.State,
                             DangerPointName = danger.Name,
                             SubCheckedCount = osubcount,
                             SubCount = osubcount,
                             TaskType = (PublicEnum.EE_InspectTaskType)task.TaskType,
                         };
                return new ActionResult<IEnumerable<TaskBillModel>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<TaskBillModel>>(ex);
            }
        }

        /// <summary>
        /// /// 根据任务单id获取已执行了的集合
        /// </summary>
        /// <param name="taskbillid"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<TaskSubjectOverView>> GetTaskSubjectsOver(Guid taskbillid)
        {
            try
            {
                var bill = _rpstb.GetModel(taskbillid);
                if (bill == null)
                {
                    throw new Exception("未找到该任务单!");
                }
                //获取已经执行了的主体
                var subs = _rpstbs.Queryable(p => p.BillID == taskbillid);
                var osubid = subs.Select(s => s.SubjectID);
                var odangerids = subs.Select(s => s.DangerID);

                //获取任务主体中已执行的主体
                var tasksubs = _work.Repository<Bll_InspectTaskSubject>().Queryable(q => q.InspectTaskID == bill.TaskID && osubid.Contains(q.SubjectID) && odangerids.Contains(q.DangerID)).ToList();
                var subids = tasksubs.Select(s => s.SubjectID);

                //获取任务主体中未执行的主体信息
                var csubs = _work.Repository<Basic_DangerPointRelation>().Queryable(p => p.DangerPointID == bill.DangerPointID && subids.Contains(p.SubjectID)).Distinct();

                //当前任务所风控项
                var dangers = tasksubs.Select(s => s.DangerID);
                var danger = _work.Repository<Basic_Danger>().Queryable(p => dangers.Contains(p.ID));
                //当前任务所有风控项的所有风险等级
                var lvids = danger.Select(s => s.DangerLevel);
                var lvs = _work.Repository<Basic_Dict>().Queryable(p => lvids.Contains(p.ID));
                //所有未归档的管控项
                var ctrs = _work.Repository<Bll_TroubleControl>().Queryable(p => p.State != (int)PublicEnum.EE_TroubleState.history);
                var ctrids = ctrs.Select(s => s.ID);
                // 在此单据中未执行的主体信息
                var re = from sub in tasksubs.ToList()
                         let sb = csubs.FirstOrDefault(p => p.SubjectID == sub.SubjectID)
                         let dg = danger.FirstOrDefault(p => p.ID == sub.DangerID)
                         let lv = lvs.FirstOrDefault(p => p.ID == dg.DangerLevel)
                         let rest = subs.FirstOrDefault(p => p.SubjectID == sub.SubjectID && p.DangerID == sub.DangerID)
                         let isCtr = _work.Repository<Bll_TroubleControlDetails>().Any(p => ctrids.Contains(p.TroubleControlID) && p.BillSubjectsID == sub.ID)
                         let sdids = _work.Repository<Basic_DangerSafetyStandards>().Queryable(p => p.DangerID == sub.DangerID).Select(s => s.SafetyStandardID)
                         let sds = _work.Repository<Basic_SafetyStandard>().Queryable(p => sdids.Contains(p.ID))
                         select new TaskSubjectOverView
                         {
                             KeyID = sub.ID,
                             SubResultID = rest.ID,
                             BillID = bill.ID,
                             SubID = sub.SubjectID,
                             DangerLevel = lv == null ? "" : lv.DictName,
                             SubType = (PublicEnum.EE_SubjectType)sub.SubjectType,
                             SubTypeName = (PublicEnum.EE_SubjectType)sub.SubjectType,
                             SubName = sb.SubjectName,
                             Principal = sb.SubjectPrincipal,
                             PrincipalTel = sb.SubjectPrincipalTel,
                             DangerID = dg.ID,
                             DangerName = dg.Name,
                             IsControl = isCtr,
                             SubStandards = from sd in sds
                                            select new SubStandard
                                            {
                                                SubStandardID = sd.ID,
                                                Name = "执行标准名:" + sd.Name,
                                                Accident = "事故措施:" + sd.Accident,
                                                Controls = "管理措施:" + sd.Controls,
                                                Engineering = "工程措施:" + sd.Engineering,
                                                Individual = "个体措施:" + sd.Individual
                                            }
                         };
                return new ActionResult<IEnumerable<TaskSubjectOverView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<TaskSubjectOverView>>(ex);
            }
        }
        /// <summary>
        /// 根据结果ID，删除检查结果
        /// </summary>
        /// <param name="subresultid"></param>
        /// <returns></returns>
        public ActionResult<bool> DelSubResult(Guid subresultid)
        {
            try
            {
                var subresult = _rpstbs.GetModel(subresultid);
                if (subresult == null)
                {
                    throw new Exception("未找到所需删除的主体检查结果！");
                }
                var check = _rpstb.Any(p => p.ID == subresult.BillID && p.State >= (int)PublicEnum.BillFlowState.normal);
                if (check)
                {
                    throw new Exception("当前单据状态属于已完成检查状态，无法删除检查结果！");
                }
                //删除文件
                srvFile.DelFileByBusinessId(subresultid);
                _rpstbs.Delete(subresult);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 获取检查结果模型
        /// </summary>
        /// <param name="subresultid"></param>
        /// <returns></returns>
        public ActionResult<SubResultView> GetSubResultModel(Guid subresultid)
        {
            try
            {
                var subresult = _rpstbs.GetModel(subresultid);
                var dict = _work.Repository<Basic_Dict>();
                if (subresult == null)
                {
                    throw new Exception("未找到检查结果");
                }
                var re = new SubResultView
                {
                    ResultTime = subresult.TaskTime,
                    TaskResult = (PublicEnum.EE_TaskResultType)subresult.TaskResult,
                    TaskResultMemo = subresult.TaskResultMemo,
                    DLevel = subresult.DangerLevel == Guid.Empty ? "" : dict.GetModel(subresult.DangerLevel).DictName,
                    WHYSDict = subresult.Eval_WHYS == Guid.Empty ? "" : dict.GetModel(subresult.Eval_WHYS).DictName,
                    SGLXDict = subresult.Eval_SGLX == Guid.Empty ? "" : dict.GetModel(subresult.Eval_SGLX).DictName,
                    SGJGDict = subresult.Eval_SGJG == Guid.Empty ? "" : dict.GetModel(subresult.Eval_SGJG).DictName,
                    YXFWDict = subresult.Eval_YXFW == Guid.Empty ? "" : dict.GetModel(subresult.Eval_YXFW).DictName,
                    LECD_L = subresult.LECD_L == Guid.Empty ? "" : dict.GetModel(subresult.LECD_L).DictName,
                    LECD_E = subresult.LECD_E == Guid.Empty ? "" : dict.GetModel(subresult.LECD_E).DictName,
                    LECD_C = subresult.LECD_C == Guid.Empty ? "" : dict.GetModel(subresult.LECD_C).DictName,
                    LSD_L = subresult.LSD_L == Guid.Empty ? "" : dict.GetModel(subresult.LSD_L).DictName,
                    LSD_S = subresult.LSD_S == Guid.Empty ? "" : dict.GetModel(subresult.LSD_S).DictName,

                    Method = (subresult.Eval_Method == 0 || subresult.Eval_Method == -1) ? "" : Command.GetItems(typeof(PublicEnum.EE_EvaluateMethod)).FirstOrDefault(v => v.Value == subresult.Eval_Method).Caption,
                    TLevel = subresult.TroubleLevel == Guid.Empty ? string.Empty : dict.GetModel(subresult.TroubleLevel).DictName,
                    CtrPrincipal = subresult.CtrPrincipal == Guid.Empty ? "" : _work.Repository<Basic_Employee>().GetModel(p => p.ID == subresult.CtrPrincipal).CNName
                };
                return new ActionResult<SubResultView>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<SubResultView>(ex);
            }
        }
        /// <summary>
        /// 同步当前人单据数据
        /// </summary>
        /// <returns></returns>
        public ActionResult<DownloadData> DownloadData()
        {
            try
            {
                var rpsDict = _work.Repository<Basic_Dict>();
                //当前人
                var user = AppUser.EmployeeInfo;
                //当前人的所有待完成单据
                var tbs = _rpstb.Queryable(q => q.EmployeeID == user.ID && q.State == (int)PublicEnum.BillFlowState.wait);

                //当前人所有单据对应的任务
                var taskids = tbs.Select(s => s.TaskID);
                var tasks = _work.Repository<Bll_InspectTask>().Queryable(p => taskids.Contains(p.ID));

                //单据的风险点
                var dangerids = tbs.Select(s => s.DangerPointID);
                var dangers = _work.Repository<Basic_DangerPoint>().Queryable(p => dangerids.Contains(p.ID));



                var overtimetaskcount = srvTask.GetTaskListByTimeOut().data.Count();

                //所有未归档的管控项
                var ctrs = _work.Repository<Bll_TroubleControl>().Queryable(p => p.State != (int)PublicEnum.EE_TroubleState.history);
                var ctrids = ctrs.Select(s => s.ID);

                var re = from tb in tbs.ToList()
                         let task = tasks.FirstOrDefault(q => q.ID == tb.TaskID)
                         let danger = dangers.FirstOrDefault(q => q.ID == tb.DangerPointID)
                         let csubs = _work.Repository<Bll_InspectTaskSubject>().Queryable(q => q.InspectTaskID == tb.TaskID)//当前单据检查主体
                         let osubs = _rpstbs.Queryable(p => p.BillID == tb.ID)//已查风控项
                         let osubids = osubs.Select(s => s.DangerID)
                         let subs = csubs.Where(p => !osubids.Contains(p.DangerID)).ToList()//待查风控项
                         let subids = subs.Select(p => p.SubjectID)
                         let sbs = _work.Repository<Basic_DangerPointRelation>().Queryable(p => subids.Contains(p.SubjectID)).ToList()
                         let whysids = JsonConvert.DeserializeObject<IEnumerable<Guid>>(danger.WXYSJson)
                         let dicts = rpsDict.Queryable(p => whysids.Contains(p.ID))
                         select new BillData
                         {
                             BillID = tb.ID,
                             StartTime = tb.StartTime,
                             EndTime = (DateTime)tb.EndTime,
                             EmployeeName = user.CNName,
                             TaskName = task.Name,
                             State = (PublicEnum.BillFlowState)tb.State,
                             DangerPointName = danger.Name,
                             SubCheckedCount = osubs.Count(),
                             SubCount = csubs.Count(),
                             TaskType = (PublicEnum.EE_InspectTaskType)task.TaskType,
                             WHYSDicts = from d in dicts
                                         select new Dict
                                         {
                                             KeyID = d.ID,
                                             DictName = d.DictName,
                                             Memo = d.Memo
                                         },
                             CheckSubs = from sub in subs
                                         let dg = _work.Repository<Basic_Danger>().GetModel(sub.DangerID)
                                         let lv = rpsDict.GetModel(dg.DangerLevel)
                                         let sb = sbs.FirstOrDefault(q => q.SubjectID == sub.SubjectID)
                                         let isCtr = _work.Repository<Bll_TroubleControlDetails>().Queryable(p => ctrids.Contains(p.TroubleControlID) && p.TaskSubjectsID == sub.ID).FirstOrDefault()
                                         let sdids = _work.Repository<Basic_DangerSafetyStandards>().Queryable(p => p.DangerID == sub.DangerID).Select(s => s.SafetyStandardID)
                                         let sds = _work.Repository<Basic_SafetyStandard>().Queryable(p => sdids.Contains(p.ID))
                                         let rest = isCtr == null ? null : _rpstbs.Queryable(p => p.ID == isCtr.BillSubjectsID).FirstOrDefault()
                                         select new TaskSubjectView
                                         {
                                             KeyID = sub.ID,
                                             BillID = tb.ID,
                                             SubID = sub.SubjectID,
                                             DangerLevel = lv == null ? "" : lv.DictName,
                                             SubType = (PublicEnum.EE_SubjectType)sub.SubjectType,
                                             SubTypeName = (PublicEnum.EE_SubjectType)sub.SubjectType,
                                             SubName = sb.SubjectName,
                                             Principal = sb.SubjectPrincipal,
                                             PrincipalTel = sb.SubjectPrincipalTel,
                                             DangerName = dg.Name,
                                             DangerID = dg.ID,
                                             IsControl = isCtr == null ? false : true,
                                             LastResult = rest == null ? "" : rest.TaskResultMemo,
                                             SubStandards = from sd in sds
                                                            select new SubStandard
                                                            {
                                                                SubStandardID = sd.ID,
                                                                Name = "执行标准名:" + sd.Name,
                                                                Accident = "事故措施:" + sd.Accident,
                                                                Controls = "管理措施:" + sd.Controls,
                                                                Engineering = "工程措施:" + sd.Engineering,
                                                                Individual = "个体措施:" + sd.Individual
                                                            }



                                         }
                         };

                var emps = _work.Repository<Basic_Employee>().Queryable(p => p.IsQuit == false);
                var orgs = _work.Repository<Basic_Org>().Queryable();
                var dlvs = rpsDict.Queryable(p => p.ParentID == OptionConst.DangerLevel);
                var sghgs = rpsDict.Queryable(p => p.ParentID == OptionConst.Eval_SGJG);
                var sglxs = rpsDict.Queryable(p => p.ParentID == OptionConst.Eval_SGLX);
                var yxfws = rpsDict.Queryable(p => p.ParentID == OptionConst.Eval_YXFW);
                var methods = Command.GetItems(typeof(PublicEnum.EE_EvaluateMethod));


                var lecdls = rpsDict.Queryable(p => p.ParentID == OptionConst.Eval_LECD_L);
                var lecdes = rpsDict.Queryable(p => p.ParentID == OptionConst.Eval_LECD_E);
                var lecdcs = rpsDict.Queryable(p => p.ParentID == OptionConst.Eval_LECD_C);
                var lsdls = rpsDict.Queryable(p => p.ParentID == OptionConst.Eval_LSD_L);
                var lsdss = rpsDict.Queryable(p => p.ParentID == OptionConst.Eval_LSD_S);
                var tlvs = rpsDict.Queryable(p => p.ParentID == OptionConst.TroubleLevel);
                DownloadData data = new DownloadData
                {
                    Emps = from e in emps
                           select new Emp
                           {
                               KeyID = e.ID,
                               OrgID = e.OrgID,
                               CNName = e.CNName
                           },
                    Orgs = from o in orgs
                           select new Org
                           {
                               KeyID = o.ID,
                               OrgName = o.OrgName,
                               ParentID = o.ParentID
                           },
                    SGHGDicts = from hg in sghgs
                                select new Dict
                                {
                                    KeyID = hg.ID,
                                    DictName = hg.DictName,
                                    Memo = hg.Memo
                                },
                    SGLXDicts = from lx in sglxs
                                select new Dict
                                {
                                    KeyID = lx.ID,
                                    DictName = lx.DictName,
                                    Memo = lx.Memo
                                },
                    YXFWDicts = from fw in yxfws
                                select new Dict
                                {
                                    KeyID = fw.ID,
                                    DictName = fw.DictName,
                                    Memo = fw.Memo

                                },
                    TroubleLevels = from tlv in tlvs
                                    select new Dict
                                    {
                                        KeyID = tlv.ID,
                                        DictName = tlv.DictName,
                                        Memo = tlv.Memo

                                    },
                    LECD_Ls = from lecdl in lecdls
                              select new Eval_Dict
                              {
                                  KeyID = lecdl.ID,
                                  DictName = lecdl.DictName,
                                  Memo = lecdl.Memo,
                                  Value = lecdl.MinValue
                              },
                    LECD_Es = from lecde in lecdes
                              select new Eval_Dict
                              {
                                  KeyID = lecde.ID,
                                  DictName = lecde.DictName,
                                  Memo = lecde.Memo,
                                  Value = lecde.MinValue
                              },
                    LECD_Cs = from lecdc in lecdcs
                              select new Eval_Dict
                              {
                                  KeyID = lecdc.ID,
                                  DictName = lecdc.DictName,
                                  Memo = lecdc.Memo,
                                  Value = lecdc.MinValue
                              },
                    LSD_Ls = from lsdl in lsdls
                             select new Eval_Dict
                             {
                                 KeyID = lsdl.ID,
                                 DictName = lsdl.DictName,
                                 Memo = lsdl.Memo,
                                 Value = lsdl.MinValue
                             },
                    LSD_Ss = from lsds in lsdss
                             select new Eval_Dict
                             {
                                 KeyID = lsds.ID,
                                 DictName = lsds.DictName,
                                 Memo = lsds.Memo,
                                 Value = lsds.MinValue
                             },
                    DangerLevels = from l in dlvs
                                   orderby l.MinValue
                                   select new DangerLevelDict
                                   {
                                       KeyID = l.ID,
                                       DictName = l.DictName,
                                       Memo = l.Memo,
                                       LECD_DMaxValue = l.LECD_DMaxValue,
                                       LECD_DMinValue = l.LECD_DMinValue,
                                       LSD_DMaxValue = l.LSD_DMaxValue,
                                       LSD_DMinValue = l.LSD_DMinValue,
                                   },
                    EvaluateMethod = methods,
                    BillDatas = re,
                    OverTimeTaskCount = overtimetaskcount
                };
                return new ActionResult<DownloadData>(data);

            }
            catch (Exception ex)
            {
                return new ActionResult<DownloadData>(ex);
            }
        }

        /// <summary>
        /// 根据二维码获取历史任务单据
        /// </summary>
        /// <param name="pointID"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<TaskBillModel>> GetTaskBillMastersOverByQRCoder(Guid pointID)
        {

            try
            {

                //当风险点的所有已完成单据
                var tbs = _rpstb.Queryable(q => q.DangerPointID == pointID && q.State >= (int)PublicEnum.BillFlowState.normal);

                var empids = tbs.Select(s => s.EmployeeID);
                //当前人所有单据对应的任务
                var taskids = tbs.Select(s => s.TaskID);
                var tasks = _work.Repository<Bll_InspectTask>().Queryable(p => taskids.Contains(p.ID));

                var emps = _work.Repository<Basic_Employee>().Queryable(p => empids.Contains(p.ID));

                //单据的风险点
                var dangerids = tbs.Select(s => s.DangerPointID);
                var dangers = _work.Repository<Basic_DangerPoint>().Queryable(p => dangerids.Contains(p.ID));
                var re = from tb in tbs
                         let task = tasks.FirstOrDefault(q => q.ID == tb.TaskID)
                         let danger = dangers.FirstOrDefault(q => q.ID == tb.DangerPointID)
                         let osubcount = _rpstbs.Queryable(p => p.BillID == tb.ID).Count()//已查主体数
                         let user = emps.FirstOrDefault(p => p.ID == tb.EmployeeID)
                         select new TaskBillModel
                         {
                             BillID = tb.ID,
                             StartTime = tb.StartTime,
                             EndTime = (DateTime)tb.EndTime,
                             EmployeeName = user.CNName,
                             TaskName = task.Name,
                             State = (PublicEnum.BillFlowState)tb.State,
                             DangerPointName = danger.Name,
                             SubCheckedCount = osubcount,
                             SubCount = osubcount,
                             TaskType = (PublicEnum.EE_InspectTaskType)task.TaskType,

                         };
                return new ActionResult<IEnumerable<TaskBillModel>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<TaskBillModel>>(ex);
            }
        }
    }
}
