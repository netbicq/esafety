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
    /// 操作流程
    /// </summary>
    [RoutePrefix("api/opreatinflow")]
    public class OpreationFlowController :ESFAPI
    {
        private IOpreationFlowService bll = null;

        public OpreationFlowController(IOpreationFlowService of)
        {
            bll = of;
            BusinessServices =new List<object>() { of };

        }
        /// <summary>
        /// 新建操作模型
        /// </summary>
        /// <param name="opreation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addopreation")]
        public ActionResult<bool> AddOpreation(OpreationNew opreation)
        {
            LogContent = "新建操作,参数源:" + JsonConvert.SerializeObject(opreation);
            return bll.AddOpreation(opreation);
        }
        /// <summary>
        /// 新建操作流程
        /// </summary>
        /// <param name="flowNew"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addof")]
        public ActionResult<bool> AddOpreationFlow(OpreationFlowNew flowNew)
        {
            LogContent = "新建操作流程，参数源:" + JsonConvert.SerializeObject(flowNew);
            return bll.AddOpreationFlow(flowNew);
        }
        /// <summary>
        /// 根据操作ID删除操作模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delopreation/{id:Guid}")]
        public ActionResult<bool> DelOpreation(Guid id)
        {
            LogContent = "删除了操作，ID为:" + JsonConvert.SerializeObject(id);
            return bll.DelOpreation(id);
        }
        /// <summary>
        /// 根据流程ID 删除操作流程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delof/{id:Guid}")]
        public ActionResult<bool> DelOpreationFlow(Guid id)
        {
            LogContent = "删除了操作，ID为:" + JsonConvert.SerializeObject(id);
            return bll.DelOpreationFlow(id);
        }
        /// <summary>
        /// 修改操作模型
        /// </summary>
        /// <param name="opreation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editopreation")]
        public ActionResult<bool> EditOpreation(OpreationEdit opreation)
        {
            LogContent = "修改了操作模型，参数源:" + JsonConvert.SerializeObject(opreation);
            return bll.EditOpreation(opreation);
        }
        /// <summary>
        /// 根据操作ID，获取操作模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getopreation/{id:Guid}")]
        public ActionResult<OpreationView> GetOpreation(Guid id)
        {
            return bll.GetOpreation(id);
        }
        /// <summary>
        /// 根据操作ID，获取操作流程列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getofs/{id:Guid}")]
        public ActionResult<IEnumerable<OpreationFlowView>> GetOpreationFlows(Guid id)
        {
            return bll.GetOpreationFlows(id);
        }
        /// <summary>
        /// 分页获取操作模型
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getopreationppage")]
        public ActionResult<Pager<OpreationView>> GetOpreationPage(PagerQuery<OpreationQuery> para)
        {
            return bll.GetOpreationPage(para);
        }
        /// <summary>
        ///获取操作列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getopreations")]
        public ActionResult<IEnumerable<OpreationView>> GetOpreations()
        {
            return bll.GetOpreations();
        }
    }
}
