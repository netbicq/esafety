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
    public interface IFacilitiesManageService
    {
        ActionResult<bool> AddFacilitiesSort(FacilitiesSortNew facilitiesSort);

        ActionResult<bool> DelFacilitiesSort(Guid id);
        ActionResult<IEnumerable<FacilitiesSortView>> GetFacilitiesSorts(Guid id);

        ActionResult<bool> AddFacility(FacilityNew facility);

        ActionResult<bool> EditFacility(FacilityEdit facility);

        ActionResult<bool> DelFacility(Guid id);

        ActionResult<Pager<FacilityView>> GetFacilities(PagerQuery<FacilitiesQuery> para);
        /// <summary>
        /// 获取所有设备设施
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<FacilityView>> GetFacilitiesList();
        /// <summary>
        /// 获取设备模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<FacilityView> GetModel(Guid id);
        /// <summary>
        /// 设备类别子级id集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<Guid>> GetSortChildrenIds(Guid id);
        /// <summary>
        /// 设备类别父级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<Basic_FacilitiesSort>> GetSortParents(Guid id);
        /// <summary>
        /// 设备类别树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<FacilitiesSortTree>> GetSortTree(Guid id);

    }
}
