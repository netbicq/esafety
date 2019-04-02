using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ESafety.Web.Unity
{
    public class ESFAPI : ApiController
    {
        /// <summary>
        /// 账套用户信息
        /// </summary>
        public AppUser AppUser { get; set; }
        /// <summary>
        /// 账套参数
        /// </summary>
        public IEnumerable<OptionItemSet> ACOptions { get; set; }
        /// <summary>
        /// 业务类
        /// </summary>
        public List<object> BusinessServices { get; set; }
        /// <summary>
        /// 当前请求的客户端IP
        /// </summary>
        public string ClientIP
        {
            get
            {

                var ip = ((HttpContextWrapper)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
                if (!string.IsNullOrEmpty(ip) && IsIP(ip))
                {
                    return ip;
                }
                else
                {

                    return "127.0.0.1";
                }
            }
        }

        private static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string LogContent { get; set; }

        [NonAction]
        public void SetService()
        {
            foreach (var s in BusinessServices)
            {
                ORM.ServiceBase obj = s as ServiceBase;
                if (AppUser != null)
                {
                    obj.AppUser = AppUser;
                    obj.AppUser.UploadPath = uploadPath;
                    obj.AppUser.OutPutPaht = OutPutPath;
                    obj.ACOptions = ACOptions;
                    if (AppUser.UserDB != null)
                    {
                        obj.Unitwork.SetUserDB(AppUser.UserDB);
                    }
                }
            }

        }

        /// <summary>
        /// 写日志
        /// </summary>
        [NonAction]
        public void WriteLog(Exception ex)
        {
            string lg = AppUser == null ? "" : AppUser.UserInfo.Login;

            string msg = "";

            if (ex != null)
            {
                msg += ex.Message;
                while (ex.InnerException != null)
                {

                    ex = ex.InnerException;
                    msg += ex.Message;

                }
            }
            //如果设置了日志内容，则写入，否则不写日志
            if (!string.IsNullOrEmpty(LogContent))
            {
                string logcontent = LogContent;
                var log = new Sys_Log()
                {
                    ID = Guid.NewGuid(),
                    IP = ClientIP,
                    LogContent = logcontent,
                    LogTime = DateTime.Now,
                    LogUser = lg,
                    MSG = msg,
                    OperateResult = ex == null
                };
                ORM.ServiceBase obj = BusinessServices[0] as ORM.ServiceBase;
                var db = obj.Unitwork.Repository<Sys_Log>();
                db.Add(log);
                obj.Unitwork.Commit();

            }


        }
        /// <summary>
        /// 上传文件地址
        /// </summary>
        protected string uploadPath = HttpContext.Current.Server.MapPath("~/uploads/");
        /// <summary>
        /// 导出临时文件夹
        /// </summary>
        protected string OutPutPath = HttpContext.Current.Server.MapPath("~/OutPutTemp/");
    }
}
