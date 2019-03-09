using ESafety.Account.Model.View;
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
    public interface ITaskBill
    {
        ActionResult<Pager<TaskSubjectBillView>> GetTaskBillSubjects(Guid id);

        ActionResult<Pager<TaskBillView>> GetTaskBillPage();
    }
}
