using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core;
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
    public interface IInspectTask:IBusinessFlowBase
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
        /// 获取临时任务列表
        /// </summary>
        /// <param name="qurey"></param>
        /// <returns></returns>
        ActionResult<Pager<InspectTempTaskView>> GetTempTasks(PagerQuery<InspectTaskQuery> qurey);
        /// <summary>
        /// 获取指定任务id的任务主体明细集合
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<InspectTaskSubjectView>> GetTaskSubjects(Guid taskid); 
        /// <summary>
        /// 获取指定ID的模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<InspectTaskModelView> GetModel(Guid id);
        /// <summary>
        /// 获取职员的任务列表
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<InsepctTaskByEmployee>> GetTaskListByEmployee();
        /// <summary>
        /// 获取职员的超时任务列表
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<InsepctTaskByEmployee>> GetTaskListByTimeOut();
        /// <summary>
        /// 获取职员的临时任务列表
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<InsepctTempTaskByEmployee>> GetTempTaskListByEmployee();
        /// <summary>
        /// 分配执行人员
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        ActionResult<bool> AllotTempTaskEmp(AllotTempTaskEmp emp);
        /// <summary>
        /// 移动端新建临时任务
        /// </summary>
        /// <param name="temptask"></param>
        /// <returns></returns>
        ActionResult<bool> AddTempTask(AddTempTask temptask);

        /// <summary>
        /// 获取选择器
        /// </summary>
        /// <returns></returns>
        ActionResult<TempTaskSelector> GetTempTaskSelector();
        /// <summary>
        /// 获取风控项选择器
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<Sub>> GetTempTaskDangerSelector(TempTaskDangerSelect select);

        /// <summary>
        /// 通过风险点ID获取当前人员的任务
        /// </summary>
        /// <param name="dangerPoint"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<InsepctTaskByEmployee>> GetEmpTaskByQRCoder(Guid dangerPoint);
        /// <summary>
        /// 通过风险点ID获取当前人员的任务
        /// </summary>
        /// <param name="dangerPoint"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<InsepctTaskByEmployee>> GetEmpTimeOutTaskByQRCoder(Guid dangerPoint);

    }
}
