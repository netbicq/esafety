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
    public interface ISafetyStandardServcie
    {
        ActionResult<bool> AddSafetyStandard(SafetyStandardNew safetystandard);

        ActionResult<bool> EditSafetyStandard(SafetyStandardEdit safetystandard);

        ActionResult<bool> DelSafetyStandard(Guid id);

        ActionResult<IEnumerable<SafetyStandardView>> GetSafetyStandards();

        ActionResult<IEnumerable<SafetyStandardView>> GetSafetyStandards(Guid dangerid);

        ActionResult<SafetyStandardView> GetSafetyStandard(Guid id);
    }
}
