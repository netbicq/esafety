using ESafety.Account.IService;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
using ESafety.Web.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ESafety.Account.API.Controllers
{
    /// <summary>
    /// 看板
    /// </summary>
    [RoutePrefix("api/dashboard")]
    public class DashboardController : ESFAPI
    {
        private IDashboard bll = null;
        public DashboardController(IDashboard dashboard)
        {
            bll = dashboard;
            BusinessServices = new List<object>() { dashboard };
        }

        /// <summary>
        /// 获取风险点位置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getDPLocation")]
        public ActionResult<IEnumerable<DangerPointLocation>> GetDPLocation()
        {
           return bll.GetDPLocation();
        }

        /// <summary>
        /// 获取风险等级的个数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getDashDLevel")]
        public ActionResult<IEnumerable<DashDangerLevel>> GetDashDLevel()
        {
            return bll.GetDashDLevel();
        }

        /// <summary>
        /// 获取管控项
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getDctrl")]
        public ActionResult<DTroubleCtrl> GetDctrl()
        {
            return bll.GetDctrl();
        }
      
    }
}
