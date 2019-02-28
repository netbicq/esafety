
//----------Doc_Meeting开始----------



using System.Web.Http;
using ESafety.Web.Unity;
using ESafety.Account.IService;

namespace ESafety.Account.API.Controllers
{
	[RoutePrefix("api/doc_meeting")]
    public class Doc_MeetingController : ESFAPI
    {
        private IDoc_MeetingService bll = null;
        public Doc_MeetingController(IDoc_MeetingService user)
        {

            bll = user;
            BusinessService = user;

        }
    }
}

//----------Doc_Meeting结束----------

    