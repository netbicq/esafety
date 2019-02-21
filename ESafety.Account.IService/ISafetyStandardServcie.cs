using ESafety.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.IService
{
    public interface ISafetyStandardServcie
    {
        ActionResult<bool> AddSafetyStandard(SafetyStandardNew safetystandard);

        ActionResult<bool> EditSafetyStandard(Guid id);

        ActionResult<bool> DelSafetyStandard(Guid id);

        ActionResult<IEnumerable<SafetyStandardView>> GetSafetyStandards();
    }
}
