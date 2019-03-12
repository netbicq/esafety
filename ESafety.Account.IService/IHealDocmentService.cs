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
    public interface IHealDocmentService
    {
        ActionResult<bool> AddHealDocment(HealDocmentNew docmentNew);
        ActionResult<bool> DelHealDocment(Guid id);
        ActionResult<bool> EditHealDocment(HealDocmentEdit docmentEdit);
        ActionResult<HealDocmentView> GetHealDocment(Guid id);
        ActionResult<Pager<HealDocmentView>> GetHealDocments(PagerQuery<HealDocmentQuery> para);

        ActionResult<IEnumerable<HealDocmentView>> GetHealDocList();
    }
}
