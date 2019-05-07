using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
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
    /// 安全标准
    /// </summary>
    [RoutePrefix("api/sftysd")]
    public class SafetyStandardController : ESFAPI
    {
        private ISafetyStandardServcie bll = null;

        public SafetyStandardController(ISafetyStandardServcie sftysd)
        {

            bll = sftysd;
            BusinessServices =new List<object>() { sftysd };

        }
        /// <summary>
        /// 新建安全标准
        /// </summary>
        /// <param name="safetystandard"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addsftysd")]
        public ActionResult<bool> AddSafetyStandard(SafetyStandardNew safetystandard)
        {
            LogContent = "新建了安全标准，数据源:" + JsonConvert.SerializeObject(safetystandard);
            return bll.AddSafetyStandard(safetystandard);
        }
        /// <summary>
        /// 根据ID删除安全标准
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delsftysd/{id:Guid}")]
        public ActionResult<bool> DelSafetyStandard(Guid id)
        {
            LogContent = "删除了安全标准，ID为:" + JsonConvert.SerializeObject(id);
            return bll.DelSafetyStandard(id);
        }
        /// <summary>
        /// 修改安全标准
        /// </summary>
        /// <param name="safetystandard"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editsftysd")]
        public ActionResult<bool> EditSafetyStandard(SafetyStandardEdit safetystandard)
        {
            LogContent = "修改了安全标准，参数源:" + JsonConvert.SerializeObject(safetystandard);
            return bll.EditSafetyStandard(safetystandard);
        }
        /// <summary>
        /// 根据ID获取安全标准
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getsftysd/{id:Guid}")]
        public ActionResult<SafetyStandardView> GetSafetyStandard(Guid id)
        {
            return bll.GetSafetyStandard(id);
        }
        /// <summary>
        /// 获取所有安全标准模型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getsftysds")]
        public ActionResult<IEnumerable<SafetyStandardView>> GetSafetyStandards()
        {
            return bll.GetSafetyStandards();
        }
        /// <summary>
        /// 根据风险点ID获取所有安全标准
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getsftysds/{id:Guid}")]
        public ActionResult<IEnumerable<SafetyStandardView>> GetSafetyStandards(Guid id)
        {
            return bll.GetSafetyStandards(id);
        }

        /// <summary>
        /// 根据风控项类别获取执行标准
        /// </summary>
        /// <param name="dangersortid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getsfttysdsByDangerSortId/{dangersortid:Guid}")]
        public ActionResult<IEnumerable<SafetyStandardView>> GetSafetyStandardsByDangerSort(Guid dangersortid)
        {
            return bll.GetSafetyStandardsByDangerSort(dangersortid);
        }
    }
}
