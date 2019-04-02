using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core;
using ESafety.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.IService
{
    /// <summary>
    /// 巡查任务
    /// </summary>
    public interface ITaskBillService : IBusinessFlowBase
    {
        /// <summary>
        /// 获取任务单模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<TaskBillModelView> GetTaskBillModel(Guid id);
        /// <summary>
        /// 获获任务单主体
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<Pager<TaskSubjectBillView>> GetTaskBillSubjects(PagerQuery<TaskBillSubjectsQuery> para);
        /// <summary>
        /// 获取任务单列表
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<Pager<TaskBillView>> GetTaskBillPage(PagerQuery<TaskBillQuery> para);
        /// <summary>
        /// 修改任务单
        /// </summary>
        /// <param name="subjectBillEdit"></param>
        /// <returns></returns>
        ActionResult<bool> EditTaskBillSubjects(TaskSubjectBillEdit subjectBillEdit);
        /// <summary>
        /// 新建任务单据
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        ActionResult<bool> AddTaskBillMaster(Account.Model.PARA.TaskBillNew bill);

        /// <summary>
        /// 根据任务单据ID获取任务的设备集合
        /// 已经执行了的设备则不再显示
        /// </summary> 
        /// <param name="taskbillid">任务单ID</param>
        /// <returns></returns>
        ActionResult<IEnumerable<InspectTaskSubjectView>> GetTaskSubjects(Guid taskbillid);
        /// <summary>
        /// 新建任务的主体检查
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        ActionResult<bool> AddTaskSubject(TaskBillSubjectNew bill);
        /// <summary>
        /// 任务单完成处理
        /// </summary>
        /// <param name="billid"></param>
        /// <returns></returns>
        ActionResult<bool> TaskBillOver(Guid billid);
    }
}
