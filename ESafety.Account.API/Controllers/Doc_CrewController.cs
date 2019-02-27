
//----------Doc_Crew开始----------



using System.Web.Http;
using ESafety.Web.Unity;
using ESafety.Account.IService;
using System.Web.Mvc;
using ESafety.Core.Model;
using System.Collections.Generic;
using ESafety.Core.Model.DB.Account;
using System;
using System.Data.SqlClient;

namespace ESafety.Account.API.Controllers
{
	[System.Web.Http.RoutePrefix("api/doc_crew")]
    public class Doc_CrewController : ESFAPI
    {
        private IDoc_CrewService bll = null;
        public Doc_CrewController(IDoc_CrewService user)
        {
            bll = user;
            BusinessService = user;
        }

        /// <summary>
        /// 获取菜单(小)
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.Route("start"), System.Web.Http.HttpGet]
        public ActionResult<object> start()
        {
            try
            {
                return new ActionResult<object>()
                {
                    data = bll.GetMenus()
                };
            }
            catch(SqlException ex)
            {
                return new ActionResult<object>(ex.Message);
            }
            
        }
    }
}

//----------Doc_Crew结束----------

    