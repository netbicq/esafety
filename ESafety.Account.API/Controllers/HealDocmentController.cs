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
    /// 健康档案
    /// </summary>
    [RoutePrefix("api/healdocment")]
    public class HealDocmentController : ESFAPI
    {
        private IHealDocmentService bll = null;
        public HealDocmentController(IHealDocmentService hd)
        {
            bll = hd;
            BusinessService = hd;
        }
        /// <summary>
        /// 新建健康档案模型
        /// </summary>
        /// <param name="docmentNew"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addhd")]
        public ActionResult<bool> AddHealDocment(HealDocmentNew docmentNew)
        {
            LogContent = "新建了健康档案，参数源:" + JsonConvert.SerializeObject(docmentNew);
            return bll.AddHealDocment(docmentNew);
        }
        /// <summary>
        /// 删除健康文档
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delhd/{id:Guid}")]
        public ActionResult<bool> DelHealDocment(Guid id)
        {
            LogContent = "删除了健康档案，ID：" + JsonConvert.SerializeObject(id);
            return bll.DelHealDocment(id);
        }
        /// <summary>
        /// 修改健康文档
        /// </summary>
        /// <param name="docmentEdit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("edithd")]
        public ActionResult<bool> EditHealDocment(HealDocmentEdit docmentEdit)
        {
            LogContent = "修改了健康文档" + JsonConvert.SerializeObject(docmentEdit);
            return bll.EditHealDocment(docmentEdit);
        }
        /// <summary>
        /// 根据ID,获取健康档案
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("gethd/{id:Guid}")]
        public ActionResult<HealDocmentView> GetHealDocment(Guid id)
        {
            return bll.GetHealDocment(id);
        }
        /// <summary>
        /// 分页获取健康文档
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("gethds")]
        public ActionResult<Pager<HealDocmentView>> GetHealDocments(PagerQuery<HealDocmentQuery> para)
        {
            return bll.GetHealDocments(para);
        }
    }
}
