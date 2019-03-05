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
    /// 资质管理
    /// </summary>
    [RoutePrefix("api/doccertificate")]
    public class DocCertificateController : ESFAPI
    {
        private IDocCertificateService bll = null;
        public DocCertificateController(IDocCertificateService dc)
        {
            bll = dc;
            BusinessService = dc;
        }
        /// <summary>
        /// 新建资质模型
        /// </summary>
        /// <param name="certificateNew"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("adddc")]
        public ActionResult<bool> AddDocCertificate(DocCertificateNew certificateNew)
        {
            LogContent = "新建了资质模型，参数源:" + JsonConvert.SerializeObject(certificateNew);
            return bll.AddDocCertificate(certificateNew);
        }
        /// <summary>
        /// 删除了资质模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("deldc/{id:Guid}")]
        public ActionResult<bool> DelDocCertificate(Guid id)
        {
            LogContent = "删除了资质模型,ID:" + JsonConvert.SerializeObject(id);
            return bll.DelDocCertificate(id);
        }
        /// <summary>
        /// 修改资质模型
        /// </summary>
        /// <param name="certificateEdit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editdc")]
        public ActionResult<bool> EditDocCertificate(DocCertificateEdit certificateEdit)
        {
            LogContent = "修改了资质模型,参数源:" + JsonConvert.SerializeObject(certificateEdit);
            return bll.EditDocCertificate(certificateEdit);
        }
        /// <summary>
        /// 根据ID,获取资质模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getdc/{id:Guid}")]
        public ActionResult<DocCertificateView> GetDocCertificate(Guid id)
        {
            return bll.GetDocCertificate(id);
        }
        /// <summary>
        /// 分页获取资质模型，可根据资质名查询
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getdcs")]
        public ActionResult<Pager<DocCertificateView>> GetDocCertificates(PagerQuery<DocCertificateQuery> para)
        {
            return bll.GetDocCertificates(para);
        }
    }
}
