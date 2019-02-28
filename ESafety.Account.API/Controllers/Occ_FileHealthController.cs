
//----------Occ_FileHealth开始----------



using System.Web.Http;
using ESafety.Web.Unity;
using ESafety.Account.IService;

namespace ESafety.Account.API.Controllers
{
	[RoutePrefix("api/occ_filehealth")]
    public class Occ_FileHealthController : ESFAPI
    {
        private IOcc_FileHealthService bll = null;
        public Occ_FileHealthController(IOcc_FileHealthService user)
        {

            bll = user;
            BusinessService = user;

        }
    }
}

//----------Occ_FileHealth结束----------

    