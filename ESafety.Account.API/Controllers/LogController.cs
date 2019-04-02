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

namespace ESafety.Account.API.Controllers
{

    /// <summary>
    /// 日志管理
    /// </summary>
    [RoutePrefix("api/log")]
    public class LogController : ESFAPI
    {
        private ILog bll = null;

        public LogController(ILog log)
        {
            bll = log;
            BusinessServices =new List<object>() { log };
        }
        /// <summary>
        /// 删除批定id的日志
        /// </summary>
        [HttpGet]
        [Route("dellogbyid/{id:Guid}")]
        public ActionResult<bool> DelLog(Guid id)
        {
            LogContent = "删除日志，id：" + id.ToString();
            return bll.DelLog(id);

        }
        /// <summary>
        /// 同时除指定用户的操作日志
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("dellogbylogin/{Login}")]
        public ActionResult<int> DelLoglogin(string Login)
        {
            LogContent = "删除了用户：" + Login + "的操作日志";
            return bll.DelLoglogin(Login);

        }
        /// <summary>
        /// 删除指定时间的日志
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delogtime")]
        public ActionResult<int> DelLogtime(Logdel para)
        {
            LogContent = "删除了指定时间的日志，参数源：" + JsonConvert.SerializeObject(para);
            return bll.DelLogtime(para);

        }
        /// <summary>
        /// 获取操作日志列表，支持分页
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getloglist")]
        public ActionResult<Pager<Logview>> GetListlog(PagerQuery<LogQuery> para)
        {
            return bll.GetListlog(para);
        }
    }
}
