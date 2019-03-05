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
    public interface IHealRecordsService
    {
        ActionResult<bool> AddHealRecord(HealRecordsNew recordsNew);
        ActionResult<bool> DelHealRecord(Guid id);
        ActionResult<bool> EditHealRecord(HealRecordsEdit recordsEdit);
        ActionResult<HealRecordsView> GetHealRecord(Guid id);
        ActionResult<Pager<HealRecordsView>> GetHealRecords(PagerQuery<HealRecordsQuery> para);
    }
}
