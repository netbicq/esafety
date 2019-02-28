using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
using ESafety.Core.Model.DB.Account;
using ESafety.Web.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ESafety.Account.API.Controllers
{
    /// <summary>
    /// 档案管理
    /// </summary>
    [System.Web.Http.RoutePrefix("api/occFile")]
    public class Occ_FileManagerController : ESFAPI
    {

        /// <summary>
        /// 风险公示业务接口类
        /// </summary>
        private IOcc_FileManagerService bll = null;
        public Occ_FileManagerController(IOcc_FileManagerService user)
        {
            bll = user;
            BusinessService = user;
        }

        /// <summary>
        /// 根据制度id获取[分页]
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost,Route("GetRegimeData")]
        public ActionResult<Pager<DocCrewView>> GetRegimeData(PagerQuery<Guid> para)
        {
            return bll.GetRegimeData(para);
        }

        /// <summary>
        /// 删除一条制度数据
        /// </summary>
        /// <param name="guid">主键</param>
        /// <returns></returns>
        [HttpGet, Route("DeleteDocCrewById/{guid}")]
        public ActionResult<bool> DeleteDocCrewById(Guid guid)
        {
            return bll.DeleteDocCrewById(guid);
        }

        /// <summary>
        /// 新增制度数据
        /// </summary>
        /// <param name="doc_"></param>
        /// <returns></returns>
        [HttpPost, Route("IncreaseCrew")]
        public ActionResult<bool> IncreaseCrew(Doc_Crew doc_)
        {
            return bll.IncreaseCrew(doc_);
        }

        /// <summary>
        /// 修改制度数据
        /// </summary>
        /// <param name="amend"></param>
        /// <returns></returns>
        [HttpPost, Route("AmendCrew")]
        public ActionResult<bool> AmendCrew(AmendCrew amend)
        {
            return bll.AmendCrew(amend);
        }

        /// <summary>
        /// 获取资质数据[分页]
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost,Route("GetQualData")]
        public ActionResult<Pager<DocQualView>> GetQualData(PagerQuery<Guid> request)
        {
            return bll.GetQualData(request);
        }

        /// <summary>
        /// 删除资质
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet,Route("DeleteQualById/{guid}")]
        public ActionResult<bool> DeleteQualById(Guid guid)
        {
            return bll.DeleteQualById(guid);
        }

        /// <summary>
        /// 新增资质
        /// </summary>
        /// <param name="qual"></param>
        /// <returns></returns>
        [HttpPost,Route("IncreaseQual")]
        public ActionResult<bool> IncreaseQual(Doc_Qualification qual)
        {
            return bll.IncreaseQual(qual);
        }

        /// <summary>
        /// 修改资质
        /// </summary>
        /// <param name="amend"></param>
        /// <returns></returns>
        public ActionResult<bool> AmendQual(AmendQual amend)
        {
            return bll.AmendQual(amend);
        }







    }
}
