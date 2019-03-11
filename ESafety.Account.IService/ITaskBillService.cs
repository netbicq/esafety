using ESafety.Account.Model.PARA;
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
    public interface ITaskBillService
    {
        ActionResult<TaskBillModelView> GetTaskBillModel(Guid id);
        ActionResult<Pager<TaskSubjectBillView>> GetTaskBillSubjects(PagerQuery<TaskBillSubjectsQuery> para);

        ActionResult<Pager<TaskBillView>> GetTaskBillPage(PagerQuery<TaskBillQuery> para);

        ActionResult<bool> EditTaskBillSubjects(TaskSubjectBillEdit subjectBillEdit);
    }
}
