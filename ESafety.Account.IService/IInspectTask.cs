using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.IService
{
    /// <summary>
    /// 巡检任务
    /// </summary>
    public interface IInspectTask
    {
        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        ActionResult<bool> AddNew(InspectTaskNew task);
        /// <summary>
        /// 删除指定ID的巡检任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<bool> DelTask(Guid id);
        /// <summary>
        /// 发起审批
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        ActionResult<bool> StartFlow(Guid taskid);
        /// <summary>
        /// 修改任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        ActionResult<bool> EditTask(InspectTaskEdit task);
        /// <summary>
        /// 改变状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        ActionResult<bool> ChangeState(InspectTaskChangeState state);

        /// <summary>
        /// 获取任务列表 
        /// </summary>
        /// <param name="qurey"></param>
        /// <returns></returns>
        ActionResult<Pager<InspectTaskView>> GetTasks(PagerQuery<InspectTaskQuery> qurey);
        /// <summary>
        /// 获取指定任务id的任务主体明细集合
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<InspectTaskSubjectView>> GetTaskSubjects(Guid taskid);
    }
}
