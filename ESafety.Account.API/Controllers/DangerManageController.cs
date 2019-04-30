using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core;
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
    /// 风险点管理
    /// </summary>
    [RoutePrefix("api/dangermanage")]
    public class DangerManageController : ESFAPI
    {
        private IDangerManageService bll = null;

        public DangerManageController(IDangerManageService dms)
        {
            bll = dms;
            BusinessServices =new List<object>() { dms };
        }

        /// <summary>
        /// 新建风险点
        /// </summary>
        /// <param name="danger"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("adddanger")]
        public ActionResult<bool> AddDanger(DangerNew danger)
        {
            LogContent="新建风险点，参数源:"+ JsonConvert.SerializeObject(danger);
            return bll.AddDanger(danger);
        }
        /// <summary>
        /// 新建风险点与安全准则的联系
        /// </summary>
        /// <param name="safetyStandards"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("adddgsftysd")]
        public ActionResult<bool> AddDangerSafetyStandard(DangerSafetyStandards safetyStandards)
        {
            LogContent = "新建风险点与安全准则的联系，参数源:" + JsonConvert.SerializeObject(safetyStandards);
            return bll.AddDangerSafetyStandard(safetyStandards);
        }
        /// <summary>
        /// 新建风险点类别
        /// </summary>
        /// <param name="dangersort"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("adddangersort")]
        public ActionResult<bool> AddDangerSort(DangerSortNew dangersort)
        {
            LogContent = "新建风险点与安全准则的联系，参数源:" + JsonConvert.SerializeObject(dangersort);
            return bll.AddDangerSort(dangersort);
        }

        /// <summary>
        /// 删除风险点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("deldanger/{id:Guid}")]
        public ActionResult<bool> DelDanger(Guid id)
        {
            LogContent = "根据删除了风险点，ID:" + JsonConvert.SerializeObject(id);
            return bll.DelDanger(id);
        }

        /// <summary>
        /// 删除风险点与安全准则联系
        /// </summary>
        /// <param name="safetyStandard"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("deldgsftysd")]
        public ActionResult<bool> DelDangerSafetyStandard(DangerSafetyStandards safetyStandard)
        {
            LogContent = "删除风险点与安全准则联系，参数源:" + JsonConvert.SerializeObject(safetyStandard);
            return bll.DelDangerSafetyStandard(safetyStandard);
        }
        /// <summary>
        /// 删除风险点的类别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("deldangersort/{id:Guid}")]
        public ActionResult<bool> DelDangerSort(Guid id)
        {
            LogContent = "删除风险点的类别，ID:" + JsonConvert.SerializeObject(id);
            return bll.DelDangerSort(id);
        }
        /// <summary>
        /// 修改风险点
        /// </summary>
        /// <param name="danger"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editdanger")]
        public ActionResult<bool> EditDanger(DangerEdit danger)
        {
            LogContent = "修改了风险点，参数源:" + JsonConvert.SerializeObject(danger);
            return bll.EditDanger(danger);
        }
        /// <summary>
        /// 根据id获取风险点
        /// </summary>
        /// <param name="dangerid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getdanger/{dangerid:Guid}")]
        public ActionResult<DangerView> GetDanger(Guid dangerid)
        {
            return bll.GetDanger(dangerid);
        }
        /// <summary>
        /// 根据风险类别ID获取风险点
        /// </summary>
        /// <param name="dangersortid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getdangers/{dangersortid:Guid}")]
        public ActionResult<IEnumerable<DangerView>> GetDangers(Guid dangersortid)
        {
            return bll.GetDangers(dangersortid);
        }
        /// <summary>
        /// 获取风险点list集合
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getdangerlist")]
        public ActionResult<IEnumerable<DangerView>> GetDangerslist()
        {
            return bll.GetDangerslist();
        }

        /// <summary>
        /// 根据ID获取风险点类别树节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getdangersorts/{id:Guid}")]
        public ActionResult<IEnumerable<DangerSortView>> GetDangerSorts(Guid id)
        {
            return bll.GetDangerSorts(id);
        }
        /// <summary>
        /// 获取指定ID的父级集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getsortparents/{id:Guid}")]
        public ActionResult<IEnumerable<Basic_DangerSort>> GetSortParent(Guid id)
        {
            return bll.GetParents(id);

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

            return bll.GetDangerSortChildrenIds(id);
        }
        /// <summary>
        /// 获取指定ID的树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getsorttree/{id:Guid}")]
        public ActionResult<IEnumerable<DangerSortTree>> GetSortTree(Guid id)
        {
            return bll.GetDangerSortTree(id);
        }
        /// <summary>
        /// 根据主体ID获取风控项选择器
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getDangerSelectBySubjectId/{subjectId:Guid}")]
        public ActionResult<IEnumerable<DangerSelector>> GetDangerSelectBySubjectId(Guid subjectID)
        {
            return bll.GetDangerSelectBySubjectId(subjectID);
        }
    }
}
