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
    /// 风险点Api
    /// </summary>
    [RoutePrefix("api/dangerpoint")]
    public class DangerPointController : ESFAPI 
    {
        private IDangerPointService bll;
        
        public DangerPointController(IDangerPointService pointService)
        {
            bll = pointService;
            BusinessServices = new List<object>(){ pointService};
        }
        /// <summary>
        /// 新建风险点模型
        /// </summary>
        /// <param name="pointNew"></param>
        /// <returns></returns>
        [Route("addDangerPoint")]
        [HttpPost]
        public ActionResult<bool> AddDangerPoint(DangerPointNew pointNew)
        {
            LogContent = "新建了风险点模型，参数源:" + JsonConvert.SerializeObject(pointNew);
            return bll.AddDangerPoint(pointNew);
        }
        /// <summary>
        /// 新建风险点配置模型
        /// </summary>
        /// <param name="relationNew"></param>
        /// <returns></returns>
        [Route("addDangerPointRelation")]
        [HttpPost]
        public ActionResult<bool> AddDangerPointRelation(DangerPointRelationNew relationNew)
        {
            LogContent = "新建了风险点关系模型,参数源:" + JsonConvert.SerializeObject(relationNew);
            return bll.AddDangerPointRelation(relationNew);
        }
        /// <summary>
        /// 删除风险点
        /// </summary>
        /// <param name="pointID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delDangerPoint/{pointID:Guid}")]
        public ActionResult<bool> DelDangerPoint(Guid pointID)
        {
            LogContent = "删除了风险点,ID:" + JsonConvert.SerializeObject(pointID);
            return bll.DelDangerPoint(pointID);
        }
        /// <summary>
        /// 删除风险点关系模型
        /// </summary>
        /// <param name="relationID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delDangerPointRelation/{relationID:Guid}")]
        public ActionResult<bool> DelDangerPointRelation(Guid relationID)
        {
            LogContent = "删除了风险点关系模型，ID：" + JsonConvert.SerializeObject(relationID);
            return bll.DelDangerPointRelation(relationID);
        }
        /// <summary>
        /// 修改风险点模型
        /// </summary>
        /// <param name="pointEdit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editDangerPoint")]
        public ActionResult<bool> EditDangerPoint(DangerPointEdit pointEdit)
        {
            LogContent = "修改了风险点模型，参数源:" + JsonConvert.SerializeObject(pointEdit);
            return bll.EditDangerPoint(pointEdit);

        }
        /// <summary>
        /// 获取风险点模型
        /// </summary>
        /// <param name="pointID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getDangerPointModel/{pointID:Guid}")]
        public ActionResult<DangerPointModel> GetDangerPointModel(Guid pointID)
        {
            return bll.GetDangerPointModel(pointID);
        }
        /// <summary>
        /// 分页获取风险点信息
        /// </summary>
        /// <param name="pointName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getDangerPointPages")]
        public ActionResult<Pager<DangerPointView>> GetDangerPointPages(PagerQuery<string> pointName)
        {
            return bll.GetDangerPointPages(pointName);
        }
        /// <summary>
        /// 分页获取风险点关系信息
        /// </summary>
        /// <param name="pointID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getDangerPointRelationPages")]
        public ActionResult<Pager<DangerPointRelationView>> GetDangerPointRelationPages(PagerQuery<Guid> pointID)
        {
            return bll.GetDangerPointRelationPages(pointID);
        }
        /// <summary>
        /// 根据风险点ID和主体类型的主体选择器
        /// </summary>
        /// <param name="select"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getDangerPointRelationSelector")]
        public ActionResult<IEnumerable<DangerPointRelationSelector>> GetDangerPointRelationSelector(DangerPointRelationSelect select)
        {
            return bll.GetDangerPointRelationSelector(select);
        }
        /// <summary>
        /// 风险点选择器
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getDangerPointSelector")]
        public ActionResult<IEnumerable<DangerPointSelector>> GetDangerPointSelector()
        {
            return bll.GetDangerPointSelector();
        }
        /// <summary>
        /// 根据风险点ID集合批量获取二维码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("getDangerPointSelector")]
        public ActionResult<IEnumerable<QRCoder>> GetQRCoders(IEnumerable<Guid> pointIds)
        {
            return bll.GetQRCoders(pointIds);
        }

        /// <summary>
        /// 根据风险点ID获取危险因素
        /// </summary>
        /// <param name="pointID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getWXYSSelector/{pointID:Guid}")]
        public ActionResult<IEnumerable<WXYSSelector>> GetWXYSSelectorByDangerPointId(Guid pointID)
        {
            return bll.GetWXYSSelectorByDangerPointId(pointID);
        }
    }
}
