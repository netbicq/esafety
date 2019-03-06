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
    /// 培训管理
    /// </summary>
    public interface IDocTrainManageService
    {
        ActionResult<bool> AddTraining(DocTrainingNew trainingNew);
        ActionResult<bool> DelTraining(Guid id);
        ActionResult<bool> EditTraining(DocTrainingEdit trainingEdit);
        ActionResult<DocTrainingView> GetTraining(Guid id);
        ActionResult<Pager<DocTrainingView>> GetTrainings(PagerQuery<DocTrainingQuery> para);
       // ActionResult<bool> AddTrainEmployee(DocTrainEmpoyeesNew empoyeesNew);
       //ActionResult<bool> DelTrainEmplyee(Guid id);
        ActionResult<Pager<DocTrainEmpoyeesView>> GetTrainEmployee(PagerQuery<DocTrainEmpoyeesQuery> para);
    }
}
