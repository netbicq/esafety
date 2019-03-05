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
    /// 体检管理
    /// </summary>
    [RoutePrefix("api/healrecords")]
    public class HealRecordsController : ESFAPI
    {
        private IHealRecordsService bll = null;
        public HealRecordsController(IHealRecordsService hr)
        {
            bll = hr;
            BusinessService = hr;
        }
        /// <summary>
        /// 新建体检信息模型
        /// </summary>
        /// <param name="recordsNew"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addhr")]
        public ActionResult<bool> AddHealRecord(HealRecordsNew recordsNew)
        {
            LogContent = "新建了体检模型,参数源:" + JsonConvert.SerializeObject(recordsNew);
            return bll.AddHealRecord(recordsNew);
        }
        /// <summary>
        /// 删除体检信息模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delhr/{id:Guid}")]
        public ActionResult<bool> DelHealRecord(Guid id)
        {
            LogContent = "删除了体检信息，ID:" + JsonConvert.SerializeObject(id);
            return bll.DelHealRecord(id);
        }
        /// <summary>
        /// 修改体检信息
        /// </summary>
        /// <param name="recordsEdit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("edithr")]
        public ActionResult<bool> EditHealRecord(HealRecordsEdit recordsEdit)
        {
            LogContent = "修改了体检信息，参数源:" + JsonConvert.SerializeObject(recordsEdit);
            return bll.EditHealRecord(recordsEdit);
        }
        /// <summary>
        /// 获取体检模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("gethr/{id:Guid}")]
        public ActionResult<HealRecordsView> GetHealRecord(Guid id)
        {
            return bll.GetHealRecord(id);
        }
        /// <summary>
        /// 分页获取体检模型
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("gethrs")]
        public ActionResult<Pager<HealRecordsView>> GetHealRecords(PagerQuery<HealRecordsQuery> para)
        {
            return bll.GetHealRecords(para);
        }
    }
}
