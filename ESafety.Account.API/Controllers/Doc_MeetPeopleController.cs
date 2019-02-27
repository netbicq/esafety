
//----------Doc_MeetPeople开始----------



using System.Web.Http;
using ESafety.Web.Unity;
using ESafety.Account.IService;

namespace ESafety.Account.API.Controllers
{
	[RoutePrefix("api/doc_meetpeople")]
    public class Doc_MeetPeopleController : ESFAPI
    {
        private IDoc_MeetPeopleService bll = null;
        public Doc_MeetPeopleController(IDoc_MeetPeopleService user)
        {

            bll = user;
            BusinessService = user;

        }
    }
}

//----------Doc_MeetPeople结束----------

    