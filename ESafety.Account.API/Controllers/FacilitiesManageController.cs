using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
using ESafety.Core.Model.DB.Account;
using ESafety.Web.Unity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ESafety.Account.API.Controllers
{
    /// <summary>
    /// 设备设施管理
    /// </summary>
    [RoutePrefix("api/facility")]
    public class FacilitiesManageController : ESFAPI
    {
        private IFacilitiesManageService bll = null;
        public FacilitiesManageController(IFacilitiesManageService fm)
        {
            bll = fm;
            BusinessServices =new List<object>() { fm };
        }
        /// <summary>
        /// 新建设施设备类别模型
        /// </summary>
        /// <param name="facilitiesSort"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addfs")]
        public ActionResult<bool> AddFacilitiesSort(FacilitiesSortNew facilitiesSort)
        {
            LogContent = "新建设备设施类别，数据源:" + JsonConvert.SerializeObject(facilitiesSort);
            return bll.AddFacilitiesSort(facilitiesSort);
        }
        /// <summary>
        /// 新建设备设施模型
        /// </summary>
        /// <param name="facility"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addfacility")]
        public ActionResult<bool> AddFacility(FacilityNew facility)
        {
            LogContent = "新建设备设施，参数源：" + JsonConvert.SerializeObject(facility);
            return bll.AddFacility(facility);
        }
        /// <summary>
        /// 根据id获取自定义模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getmode/{id:Guid}")]
        public ActionResult<FacilityView> GetModel(Guid id)
        {
            return bll.GetModel(id);
        }
        /// <summary>
        /// 根据ID，删除设备设施类别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delfs/{id:Guid}")]
        public ActionResult<bool> DelFacilitiesSort(Guid id)
        {
            LogContent = "删除了设备设施类别,ID为：" + JsonConvert.SerializeObject(id);
            return bll.DelFacilitiesSort(id);
        }
        /// <summary>
        /// 根据ID，删除设备设施
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delfacility/{id:Guid}")]
        public ActionResult<bool> DelFacility(Guid id)
        {
            LogContent = "删除了设备设施,ID为：" + JsonConvert.SerializeObject(id);
            return bll.DelFacility(id);
        }
        /// <summary>
        /// 修改设备设施
        /// </summary>
        /// <param name="facility"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editfacility")]
        public ActionResult<bool> EditFacility(FacilityEdit facility)
        {
            LogContent = "修改了设备设施,参数源：" + JsonConvert.SerializeObject(facility);
            return bll.EditFacility(facility);
        }
        /// <summary>
        /// 分页获取设施设备
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getfacilitiespage")]
        public ActionResult<Pager<FacilityView>> GetFacilities(PagerQuery<FacilitiesQuery> para)
        {
            return bll.GetFacilities(para);
        }
        /// <summary>
        /// 获取设备设施类别树节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getfstr/{id:Guid}")]
        public ActionResult<IEnumerable<FacilitiesSortView>> GetFacilitiesSorts(Guid id)
        {
            return bll.GetFacilitiesSorts(id);
        }

        /// <summary>
        /// 获取指定ID的父级集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getsortparents/{id:Guid}")]
        public ActionResult<IEnumerable<Basic_FacilitiesSort>> GetSortParent(Guid id)
        {
            return bll.GetSortParents(id);

        }
        /// <summary>
        /// 获取指定ID的子级ID集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getchildrenids/{id:Guid}")]
        public ActionResult<IEnumerable<Guid>> GetSortChildrenIds(Guid id)
        {

            return bll.GetSortChildrenIds(id);
        }
        /// <summary>
        /// 获取指定ID的树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getsorttree/{id:Guid}")]
        public ActionResult<IEnumerable<FacilitiesSortTree>> GetSortTree(Guid id)
        {
            return bll.GetSortTree(id);
        }
        /// <summary>
        /// 获取指定ID的树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getFacilitiesList")]
        public ActionResult<IEnumerable<FacilityView>> GetFacilitiesList()
        {
            return bll.GetFacilitiesList();
        }
    }
}
