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
    /// 安全制度
    /// </summary>
    [RoutePrefix("api/docinstitution")]
    public class DocInstitutionController : ESFAPI
    {
        private IDocInstitutionService bll = null;
        public DocInstitutionController(IDocInstitutionService din)
        {
            bll = din;
            BusinessServices =new List<object>() { din };
        }
        /// <summary>
        /// 新建安全制度模型
        /// </summary>
        /// <param name="institutionNew"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("adddin")]
        public ActionResult<bool> AddDocInstitution(DocInstitutionNew institutionNew)
        {
            LogContent = "新建了安全制度，参数源:" + JsonConvert.SerializeObject(institutionNew);
            return bll.AddDocInstitution(institutionNew);
        }
        /// <summary>
        /// 删除了安全制度模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("deldin/{id:Guid}")]
        public ActionResult<bool> DelDocInstitution(Guid id)
        {
            LogContent = "删除了安全制度,ID:" + JsonConvert.SerializeObject(id);
            return bll.DelDocInstitution(id);
        }
        /// <summary>
        /// 修改了安全制度模型
        /// </summary>
        /// <param name="institutionEdit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editdin")]
        public ActionResult<bool> EditDocInstitution(DocInstitutionEdit institutionEdit)
        {
            LogContent = "修改了安全模型，参数源：" + JsonConvert.SerializeObject(institutionEdit);
            return bll.EditDocInstitution(institutionEdit);
        }
        /// <summary>
        /// 根据ID，获取安全制度模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getdin/{id:Guid}")]
        public ActionResult<DocInstitutionView> GetDocInstitutionModel(Guid id)
        {
            return bll.GetDocInstitutionModel(id);
        }
        /// <summary>
        /// 根据类型ID，分页获取安全制度(可根据制度名称查询)
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getdins")]
        public ActionResult<Pager<DocInstitutionView>> GetDocInstitutions(PagerQuery<DocInstitutionQuery> para)
        {
            return bll.GetDocInstitutions(para);
        }
    }
}
