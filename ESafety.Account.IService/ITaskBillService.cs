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
        ActionResult<bool> EditTaskBillSubjects(TaskBillEval subjectBillEdit);
        /// <summary>
        /// 新建任务单据
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        ActionResult<bool> AddTaskBillMaster(Account.Model.PARA.TaskBillNew bill);
        /// <summary>
        /// 删除任务单据
        /// </summary>
        /// <param name="billid"></param>
        /// <returns></returns>
        ActionResult<bool> DelTaskBillMaster(Guid billid);

        /// <summary>
        /// 获取当前用户待执行的任务单据列表
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<TaskBillModel>> GetTaskBillMasters();

        /// <summary>
        /// 获取当前用户的历史任务单据列表
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<TaskBillModel>> GetTaskBillMastersOver();
        /// <summary>
        /// 根据任务单据ID获取任务的主体集合
        /// 已经执行了的主体则不再显示
        /// </summary> 
        /// <param name="taskbillid">任务单ID</param>
        /// <returns></returns>
        ActionResult<IEnumerable<TaskSubjectView>> GetTaskSubjects(Guid taskbillid);

        /// <summary>
        /// 根据任务单据ID获取已经执行了的任务的主体集合
        /// 
        /// </summary>
        /// <param name="taskbillid">任务单ID</param>
        /// <returns></returns>
        ActionResult<IEnumerable<TaskSubjectOverView>> GetTaskSubjectsOver(Guid taskbillid);
        /// <summary>
        /// 根据检查结果ID，删除检查结果
        /// </summary>
        /// <param name="subresultid"></param>
        /// <returns></returns>
        ActionResult<bool> DelSubResult(Guid subresultid);
        /// <summary>
        /// 根据检查结果ID，获取检查结果模型
        /// </summary>
        /// <param name="subresultid"></param>
        /// <returns></returns>
        ActionResult<SubResultView> GetSubResultModel(Guid subresultid);
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
        /// <summary>
        /// 下载单据数据
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<DownloadData>> DownloadData();
    }
}
