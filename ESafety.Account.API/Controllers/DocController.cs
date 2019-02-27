using ESafety.Web.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ESafety.Account.API.Controllers
{
    public class DocController : ESFAPI
    {
        private IAuth_User bll = null;
        public AuthController(IAuth_User user)
        {

            bll = user;
            BusinessService = bll as Account.Service.Auth_UserService;

        }
    }
}
