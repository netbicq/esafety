
//----------Doc_TrainPeople开始----------



using System.Web.Http;
using ESafety.Web.Unity;
using ESafety.Account.IService;

namespace ESafety.Account.API.Controllers
{
	[RoutePrefix("api/doc_trainpeople")]
    public class Doc_TrainPeopleController : ESFAPI
    {
        private IDoc_TrainPeopleService bll = null;
        public Doc_TrainPeopleController(IDoc_TrainPeopleService user)
        {

            bll = user;
            BusinessService = user;

        }
    }
}

//----------Doc_TrainPeople结束----------

    