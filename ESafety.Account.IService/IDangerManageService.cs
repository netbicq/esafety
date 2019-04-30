using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
using ESafety.Core.Model.DB.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.IService
{
    public interface IDangerManageService
    {
        ActionResult<bool> AddDangerSort(DangerSortNew dangersort);

        ActionResult<bool> DelDangerSort(Guid id);

        ActionResult<IEnumerable<DangerSortView>> GetDangerSorts(Guid id);

        ActionResult<bool> AddDanger(DangerNew danger);

        ActionResult<bool> EditDanger(DangerEdit danger);

        ActionResult<bool> DelDanger(Guid id);

        ActionResult<IEnumerable<DangerView>> GetDangers(Guid dangersortid);

        ActionResult<IEnumerable<DangerView>> GetDangerslist();

        ActionResult<DangerView> GetDanger(Guid dangerid);

        ActionResult<bool> AddDangerSafetyStandard(DangerSafetyStandards safetyStandards);

        ActionResult<bool> DelDangerSafetyStandard(DangerSafetyStandards safetyStandard);

        ActionResult<IEnumerable<Guid>> GetDangerSortChildrenIds(Guid id);

        ActionResult<IEnumerable<Basic_DangerSort>>GetParents(Guid id);

        ActionResult<IEnumerable<DangerSortTree>> GetDangerSortTree(Guid id);

        /// <summary>
        /// 获取风控项选择器根据主体ID
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<DangerSelector>> GetDangerSelectBySubjectId(Guid subjectID);

    }
}
