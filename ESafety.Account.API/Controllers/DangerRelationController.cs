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
    /// 风险点配置
    /// </summary>
    [RoutePrefix("api/dangerrelation")]
    public class DangerRelationController : ESFAPI
    {

        private IDangerSetService bll = null;

        public DangerRelationController(IDangerSetService ds)
        {

            bll = ds;
            BusinessService = ds;

        }
        /// <summary>
        /// 新增风险点关系模型
        /// </summary>
        /// <param name="dangerRelation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("adddgset")]
        public ActionResult<bool> AddDangerRelation(DangerRelationNew dangerRelation)
        {
            LogContent = "新建了风险点配置,参数源:" + JsonConvert.SerializeObject(dangerRelation);
            return bll.AddDangerRelation(dangerRelation);
        }
        /// <summary>
        /// 根据Id删除风险点关系模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("deldgset/{id:Guid}")]
        public ActionResult<bool> DelDangerRelation(Guid id)
        {
            LogContent = "删除了风险点配置,id为:" + JsonConvert.SerializeObject(id);
            return bll.DelDangerRelation(id);
        }
        /// <summary>
        /// 根据主题ID,获取关系分页
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getdrpage")]
        public ActionResult<Pager<DangerRelationView>> GetDangerRelations(PagerQuery<DangerRelationQuery> para)
        {
            return bll.GetDangerRelations(para);
        }
    }
}
