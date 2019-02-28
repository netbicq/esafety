
//----------Occ_Medical开始----------



using System.Web.Http;
using ESafety.Web.Unity;
using ESafety.Account.IService;

namespace ESafety.Account.API.Controllers
{
	[RoutePrefix("api/occ_medical")]
    public class Occ_MedicalController : ESFAPI
    {
        private IOcc_MedicalService bll = null;
        public Occ_MedicalController(IOcc_MedicalService user)
        {

            bll = user;
            BusinessService = user;

        }
    }
}

//----------Occ_Medical结束----------

    