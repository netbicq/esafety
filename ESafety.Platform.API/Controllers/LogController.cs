using ESafety.Core;
using ESafety.Core.Model;
using ESafety.Core.Model.View;
using ESafety.Web.Unity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ESafety.Platform.API.Controllers
{
    /// <summary>
    /// 日志
    /// </summary>
    [RoutePrefix("api/Logs")]
    public class LogController : ESFAPI
    {
        private  ILog bll = null;

        public LogController( ILog Log)
        {

            bll = Log;
            BusinessService = bll;

        }
        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [Route("getlistlog")]
        [HttpPost]
        public ActionResult<Pager<Logview>> GetListlog(PagerQuery<LogQuery> para)
        {
            return bll.GetListlog(para);
        }
        /// <summary>
        /// 删除指定日期范围的日志
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("dellog")]
        public ActionResult<int> Devlogdel(Logdel para)
        {
            LogContent = "删除指定日期范围的日志，参数源：" + JsonConvert.SerializeObject(para);

            return bll.DelLogtime(para);
        }
        /// <summary>
        /// 删除指定ID的日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("dellog/{id:Guid}")]
        [HttpGet]
        public ActionResult<bool> Dellog(Guid id)
        {
            LogContent = "删除一条日志";

            return bll.DelLog(id);
        }
        /// <summary>
        /// 删除指定的日志
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        [Route("delbylog/{Login}")]
        [HttpGet]
        public ActionResult<int> Delbylog(string Login)
        {
            LogContent = "删除指定用户日志，用户：" + Login;
            return bll.DelLoglogin(Login);
        }
    }
}
