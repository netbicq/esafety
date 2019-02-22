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
    public interface IFacilitiesManageServcie
    {
        ActionResult<bool> AddFacilitiesSort(FacilitiesSortNew facilitiesSort);

        ActionResult<bool> DelFacilitiesSort(Guid id);
        ActionResult<IEnumerable<FacilitiesSortView>> GetFacilitiesSorts(Guid id);

        ActionResult<bool> AddFacility(FacilityNew facility);

        ActionResult<bool> EditFacility(FacilityEdit facility);

        ActionResult<bool> DelFacility(Guid id);

        ActionResult<Pager<FacilityView>> GetFacilities(PagerQuery<FacilitiesQuery> para);
    }
}
