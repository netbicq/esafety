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
    class TaskBillService : ServiceBase, ITaskBillService
    {
        private IUnitwork _work = null;
        private IRepository<Bll_TaskBill> _rpstb = null;
        private IRepository<Bll_TaskBillSubjects> _rpstbs = null;
        public TaskBillService(IUnitwork work)
        {
            _work = work;
            Unitwork = work;
            _rpstb = work.Repository<Bll_TaskBill>();
            _rpstbs = work.Repository<Bll_TaskBillSubjects>();
        }
        /// <summary>
        /// 新建任务单据详情
        /// </summary>
        /// <param name="subjectBillNew"></param>
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
        public ActionResult<TaskBillModelView> GetTaskBillModel()
        {
            throw new NotImplementedException();
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
                var dbtb = _rpstb.Queryable(q=>q.PostID==para.Query.PostID&&q.State==para.Query.TaskState&&(q.BillCode.Contains(para.Query.Key)||q.BillCode==string.Empty)).ToList();
                var taskid = dbtb.Select(s => s.TaskID);
                var empids = dbtb.Select(p=>p.EmployeeID);
                var emps = _work.Repository<Core.Model.DB.Basic_Employee>().Queryable(p=>empids.Contains(p.ID)).ToList();

                var dbts = _work.Repository<Bll_InspectTask>().Queryable(p=>taskid.Contains(p.ID));

                var rev = from s in dbtb
                          let emp=emps.FirstOrDefault(p=>p.ID==s.EmployeeID)
                          let ts=dbts.FirstOrDefault(p=>p.ID==s.TaskID)
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
            var dbtb = _rpstb.GetModel(para.Query.BillID);
            var dbtbs = _rpstbs.Queryable(p => p.BillID == para.Query.BillID && p.SubjectID == para.Query.SubjectID).ToList() ;

            var dangers = _work.Repository<Basic_DangerRelation>().Queryable(p => p.SubjectID == para.Query.SubjectID).ToList();
            var ids = dangers.Select(p=>p.DangerID);
            var dbdanger = _work.Repository<Basic_Danger>().Queryable(p=>ids.Contains(p.ID)).ToList();

            var dict = _work.Repository<Core.Model.DB.Basic_Dict>();

            var rev = from s in dbdanger
                     let tbd=dbtbs.FirstOrDefault(q=>q.DangerID==s.ID)
                     select new TaskSubjectBillView
                     {
                         
                         DangerName=s.Name,
                         DangerID=s.ID,
                         BillID=dbtb.ID,
                         TaskResult=dbtb.State,
                         TaskResultName = Command.GetItems(typeof(PublicEnum.EE_TaskResultType)).FirstOrDefault(q => q.Value ==dbtb.State).Caption,

                         TroubleLevel =tbd==null?0:tbd.TroubleLevel,
                         TaskResultMemo=tbd==null?string.Empty:tbd.TaskResultMemo,

                         Eval_Method=tbd==null?0:tbd.Eval_Method,
                         MethodName =tbd==null?string.Empty:Command.GetItems(typeof(PublicEnum.EE_EvaluateMethod)).FirstOrDefault(q => q.Value == tbd.Eval_Method).Caption,

                         Eval_SGJG =tbd==null?Guid.Empty:tbd.Eval_SGJG,
                         SGJGDic=tbd==null?string.Empty:dict.GetModel(tbd.Eval_SGJG).DictName,
                      

                         Eval_SGLX=tbd==null?Guid.Empty:tbd.Eval_SGLX,
                         SGLXDic =tbd==null?string.Empty:dict.GetModel(tbd.Eval_SGLX).DictName,

                         Eval_WHYS=tbd.Eval_WHYS,
                         WHYSDic=tbd==null?string.Empty:dict.GetModel(tbd.Eval_WHYS).DictName,

                         Eval_YXFW=tbd.Eval_YXFW,
                         YXFWDic=tbd==null?string.Empty:dict.GetModel(tbd.Eval_YXFW).DictName,

                         SubjectID=tbd.SubjectID,
                         SubjectType=tbd.SubjectType,
                     };
            var re = new Pager<TaskSubjectBillView>().GetCurrentPage(rev,para.PageSize,para.PageIndex);
            return new ActionResult<Pager<TaskSubjectBillView>>(re);

        }
    }
}
