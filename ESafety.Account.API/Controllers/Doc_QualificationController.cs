
//----------Doc_Qualification开始----------



using System.Web.Http;
using ESafety.Web.Unity;
using ESafety.Account.IService;
using ESafety.Core.Model;
using ESafety.Account.Model.View;
using ESafety.Account.Model.PARA;
using System;

namespace ESafety.Account.API.Controllers
{
	[RoutePrefix("api/doc_qualification")]
    public class Doc_QualificationController : ESFAPI
    {
        private IDoc_QualificationService bll = null;
        public Doc_QualificationController(IDoc_QualificationService user)
        {

            bll = user;
            BusinessService = user;

        }

        [HttpGet,Route("GetQualData")]
        /// <summary>
        /// 根据资质种类id获取资质数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult<Pager<DocQualView>> GetQualData(DocQualPara request)
        {
            try
            {
                return bll.GetQualData(request);
            }
            catch(Exception ex)
            {
                return new ActionResult<Pager<DocQualView>>(ex);
            }
        }


    }
}

//----------Doc_Qualification结束----------

    