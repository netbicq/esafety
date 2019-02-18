using ESafety.Core;
using ESafety.Core.Model;
using ESafety.Core.Model.DB.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ESafety.Web.Unity
{

    public class ExceptionFilter : ExceptionFilterAttribute
    {



        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {

            string errmsg = actionExecutedContext.Exception.Message;
            if (actionExecutedContext.Exception.InnerException != null)
            {
                errmsg += "详情：" + actionExecutedContext.Exception.InnerException.Message;
                if (actionExecutedContext.Exception.InnerException.InnerException != null)
                {
                    errmsg += actionExecutedContext.Exception.InnerException.InnerException.Message;
                }
            }


            var dmsg = new System.Net.Http.HttpResponseMessage();
            var dobj = new ActionResult<bool>(actionExecutedContext.Exception);
            var dmsgstr = Newtonsoft.Json.JsonConvert.SerializeObject(dobj);
            var drcontent = new System.Net.Http.StringContent(dmsgstr);


            if (actionExecutedContext.ActionContext.ControllerContext.RouteData.Route.RouteTemplate == "api/device/getmqtt/{deviceid}"
                || actionExecutedContext.ActionContext.ControllerContext.RouteData.Route.RouteTemplate == "api/device/getrules"
                || actionExecutedContext.ActionContext.ControllerContext.RouteData.Route.RouteTemplate == "api/device/getdevicedata/{deviceid}"
                || actionExecutedContext.ActionContext.ControllerContext.RouteData.Route.RouteTemplate == "api/device/getindentity")
            {
 
                var objBytes = Encoding.GetEncoding("UTF-8").GetBytes(dmsgstr);
                var reBytes = Encoding.Convert(Encoding.GetEncoding("UTF-8"), Encoding.GetEncoding("GB2312"), objBytes);
                var reStr = Encoding.GetEncoding("GB2312").GetString(reBytes);
                StringContent recontent = new StringContent(reStr, Encoding.GetEncoding("GB2312"));

                dmsg.Content = recontent;
            }
            else
            {
                dmsg.Content = drcontent;
            }

            if (actionExecutedContext.Exception.Source == "mqtt")
            {
                dmsg.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
            else
            {
                dmsg.StatusCode = System.Net.HttpStatusCode.OK;
            }



            actionExecutedContext.Response = dmsg;
            base.OnException(actionExecutedContext);
        }
    }

    public class AccountAuthFilter : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            ESFAPI api = (ESFAPI)actionContext.ControllerContext.Controller;

            IEnumerable<string> Token;
            if (!(actionContext.Request.Headers.TryGetValues("token", out Token)))
                throw new Exception("非法请求");

            IEnumerable<string> AccountID;
            if (!(actionContext.Request.Headers.TryGetValues("accountid", out AccountID)))
                throw new Exception("非法请求");

            ORM.ServiceBase obj = api.BusinessService as ORM.ServiceBase;


            var acdb = obj.Unitwork.Repository<AccountInfo>();
            var account = acdb.GetModel(new Guid(AccountID.FirstOrDefault()));
            if (account == null)
                throw new Exception("账套不存在");

            var userdb = new AppUserDB
            {

                DBName = account.DBName,
                DBPwd = account.DBPwd,
                DBServer = account.DBServer,
                DBUid = account.DBUid
            };

            //处理账套参数
            if (!string.IsNullOrEmpty(account.AccountOptions))
            {
                api.ACOptions = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<OptionItemSet>>(account.AccountOptions);
            }

            obj.Unitwork.SetUserDB(userdb);

            var authuser = obj.Unitwork.Repository<Core.Model.DB.Auth_User>();
            var user = authuser.GetModel(q => q.Token == Token.FirstOrDefault());
            if (user == null)
                throw new Exception("非法请求");

            //更新用户的有效期，检查token是否有效，如果失败则返回异常

            if ((DateTime.Now - user.TokenValidTime).TotalSeconds > 0)
            {
                throw new Exception("登录超时");
            }
            user.TokenValidTime = DateTime.Now.AddMinutes(account.TokenValidTimes);
            authuser.Update(user);
            obj.Unitwork.Commit();


            // user.Pwd = "";
            var dbuser = obj.Unitwork.Repository<Core.Model.DB.Auth_UserProfile>();
            var userpro = dbuser.GetModel(q => q.Login == user.Login);
            api.AppUser = new AppUser()
            {
                UserInfo = user,
                UserDB = userdb,
                AccountCode = account.AccountCode,
                UserProfile = userpro
            };


            //权限验证
            //var auth = new Core.Bll.Auth_UserService(obj.Unitwork).GetAllAuth(user.Login);
            //var currentkey = actionContext.ControllerContext.RouteData.Route.RouteTemplate;
            //if (AuthKey.AuthKeys.Any(q => q.ActionFullName == currentkey))//如果需要权限控制则验证用户权限
            //{
            //    if (!auth.data.Any(q => q.ActionFullName == currentkey))
            //    {
            //        throw new Exception("无权操作");
            //    }
            //}

            return true;
        }
    }

    public class AccountActionFilter : ActionFilterAttribute
    {

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.ActionContext.ControllerContext.RouteData.Route.RouteTemplate == "api/device/getmqtt/{deviceid}"
                || actionExecutedContext.ActionContext.ControllerContext.RouteData.Route.RouteTemplate == "api/device/getrules"
                || actionExecutedContext.ActionContext.ControllerContext.RouteData.Route.RouteTemplate == "api/device/getdevicedata/{deviceid}"
                || actionExecutedContext.ActionContext.ControllerContext.RouteData.Route.RouteTemplate == "api/device/getindentity")
            {
                ObjectContent reobj = actionExecutedContext.Response.Content as ObjectContent;
                var objStr = Newtonsoft.Json.JsonConvert.SerializeObject(reobj.Value);
                var objBytes = Encoding.GetEncoding("UTF-8").GetBytes(objStr);
                var reBytes = Encoding.Convert(Encoding.GetEncoding("UTF-8"), Encoding.GetEncoding("GB2312"), objBytes);
                var reStr = Encoding.GetEncoding("GB2312").GetString(reBytes);
                StringContent recontent = new StringContent(reStr, Encoding.GetEncoding("GB2312"));

                actionExecutedContext.Response.Content = recontent;
            }
            base.OnActionExecuted(actionExecutedContext);
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            ESFAPI api = (ESFAPI)actionContext.ControllerContext.Controller;
            api.SetService();
            base.OnActionExecuting(actionContext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            ESFAPI api = (ESFAPI)actionExecutedContext.ActionContext.ControllerContext.Controller;
            api.WriteLog(actionExecutedContext.Exception);//写日志

            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }
    }


    public class PlatformAuthFilter : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            ESFAPI api = (ESFAPI)actionContext.ControllerContext.Controller;

            IEnumerable<string> Token;
            if (!(actionContext.Request.Headers.TryGetValues("token", out Token)))
                throw new Exception("非法请求");


            ORM.ServiceBase obj = api.BusinessService as ORM.ServiceBase;


            var db = obj.Unitwork.Repository<Core.Model.DB.Auth_User>();
            var user = db.GetModel(q => q.Token == Token.FirstOrDefault());
            if (user == null)
                throw new Exception("非法请求");

            //token的有效期更新，和过期检查
            if ((DateTime.Now - user.TokenValidTime).TotalMinutes > 0)
            {
                throw new Exception("登录超时");
            }

            int vitmes = int.Parse(System.Configuration.ConfigurationManager.AppSettings["TokenValidTimes"]);

            user.TokenValidTime = DateTime.Now.AddMinutes(vitmes);
            db.Update(user);
            obj.Unitwork.Commit();

            //user.Pwd = "";
            var dbuser = obj.Unitwork.Repository<Core.Model.DB.Auth_UserProfile>();
            var userpro = dbuser.GetModel(q => q.Login == user.Login);
            api.AppUser = new AppUser()
            {
                UserInfo = new Core.Model.DB.Auth_User
                {
                    Login = user.Login,
                    Pwd = "",
                    CreateDate = user.CreateDate,
                    CreateMan = user.CreateMan,
                    Token = user.Token,
                    ID = user.ID,
                    OtherEdit = user.OtherEdit,
                    State = user.State,
                    OtherView = user.OtherView
                },
                UserProfile = userpro
            };


            //权限验证
            var auth = new Auth_UserService(obj.Unitwork).GetAllAuth(user.Login);
            var currentkey = actionContext.ControllerContext.RouteData.Route.RouteTemplate;
            if (AuthKey.AuthKeys.Any(q => q.ActionFullName == currentkey))//如果需要权限控制则验证用户权限
            {
                if (!auth.data.Any(q => q.ActionFullName == currentkey))
                {
                    throw new Exception("无权操作");
                }
            }

            return true;
        }
    }
    /// <summary>
    /// 权限鉴权
    /// </summary>
    public class PlatformActionFilter : ActionFilterAttribute
    {

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.ActionContext.ControllerContext.RouteData.Route.RouteTemplate == "api/device/getmqtt/{deviceid}"
                || actionExecutedContext.ActionContext.ControllerContext.RouteData.Route.RouteTemplate == "api/device/getrules"
                || actionExecutedContext.ActionContext.ControllerContext.RouteData.Route.RouteTemplate == "api/device/getdevicedata/{deviceid}"
                || actionExecutedContext.ActionContext.ControllerContext.RouteData.Route.RouteTemplate == "api/device/getindentity")
            {
                ObjectContent reobj = actionExecutedContext.Response.Content as ObjectContent;
                var objStr = Newtonsoft.Json.JsonConvert.SerializeObject(reobj.Value);
                var objBytes = Encoding.GetEncoding("UTF-8").GetBytes(objStr);
                var reBytes = Encoding.Convert(Encoding.GetEncoding("UTF-8"), Encoding.GetEncoding("GB2312"), objBytes);
                var reStr = Encoding.GetEncoding("GB2312").GetString(reBytes);
                StringContent recontent = new StringContent(reStr, Encoding.GetEncoding("GB2312"));

                actionExecutedContext.Response.Content = recontent;
            }
            base.OnActionExecuted(actionExecutedContext);
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            ESFAPI api = (ESFAPI)actionContext.ControllerContext.Controller;
            api.SetService();
            base.OnActionExecuting(actionContext);
        }
        
        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            ESFAPI api = (ESFAPI)actionExecutedContext.ActionContext.ControllerContext.Controller;
            api.WriteLog(actionExecutedContext.Exception);//写日志

            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }
    }

    public class AuthKey
    {

        public static IEnumerable<Core.Model.DB.Auth_KeyDetail> AuthKeys { get; set; }

    } 
}
