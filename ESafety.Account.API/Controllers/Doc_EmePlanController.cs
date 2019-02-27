
//----------Doc_EmePlan开始----------



using System.Web.Http;
using ESafety.Web.Unity;
using ESafety.Account.IService;

namespace ESafety.Account.API.Controllers
{
	[RoutePrefix("api/doc_emeplan")]
    public class Doc_EmePlanController : ESFAPI
    {
        private IDoc_EmePlanService bll = null;
        public Doc_EmePlanController(IDoc_EmePlanService user)
        {

            bll = user;
            BusinessService = user;

        }
    }
}

//----------Doc_EmePlan结束----------

    