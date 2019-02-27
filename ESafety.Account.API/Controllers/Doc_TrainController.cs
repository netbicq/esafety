
//----------Doc_Train开始----------



using System.Web.Http;
using ESafety.Web.Unity;
using ESafety.Account.IService;

namespace ESafety.Account.API.Controllers
{
	[RoutePrefix("api/doc_train")]
    public class Doc_TrainController : ESFAPI
    {
        private IDoc_TrainService bll = null;
        public Doc_TrainController(IDoc_TrainService user)
        {

            bll = user;
            BusinessService = user;

        }
    }
}

//----------Doc_Train结束----------

    