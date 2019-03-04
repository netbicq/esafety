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
    /// 紧急预案
    /// </summary>
    [RoutePrefix("api/docsolution")]
    public class DocSolutionController : ESFAPI
    {
        private IDocSolutionService bll = null;
        public  DocSolutionController(IDocSolutionService ds)
        {
            bll = ds;
            BusinessService = ds;
        }
        /// <summary>
        /// 新建紧急预案模型
        /// </summary>
        /// <param name="solutionNew"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addds")]
        public ActionResult<bool> AddDocSolution(DocSolutionNew solutionNew)
        {
            LogContent = "新建紧急预案模型，数据源:" + JsonConvert.SerializeObject(solutionNew);
            return bll.AddDocSolution(solutionNew);
        }
        /// <summary>
        /// 删除紧急预案模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delds/{id:Guid}")]
        public ActionResult<bool> DelDocSolution(Guid id)
        {
            LogContent = "删除了紧急预案模型，ID:" + JsonConvert.SerializeObject(id);
            return bll.DelDocSolution(id);
        }
        /// <summary>
        /// 修改紧急预案
        /// </summary>
        /// <param name="solutionEdit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editds")]
        public ActionResult<bool> EditDocSolution(DocSolutionEdit solutionEdit)
        {
            LogContent = "修改了紧急预案模型，数据源:" + JsonConvert.SerializeObject(solutionEdit);
            return bll.EditDocSolution(solutionEdit);
        }
        /// <summary>
        /// 根据ID，获取紧急预案模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getds/{id:Guid}")]
        public ActionResult<DocSolutionView> GetDocSolution(Guid id)
        {
            return bll.GetDocSolution(id);
        }
        /// <summary>
        /// 分页获取紧急预案模型
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getdss")]
        public ActionResult<Pager<DocSolutionView>> GetDocSolutions(PagerQuery<DocSolutionQuery> para)
        {
            return bll.GetDocSolutions(para);
        }
    }
}
