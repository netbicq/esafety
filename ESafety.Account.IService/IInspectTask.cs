using ESafety.Account.Model.PARA;
using ESafety.Core.Model;
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
    }
}
