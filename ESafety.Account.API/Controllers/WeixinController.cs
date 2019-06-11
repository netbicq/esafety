using ESafety.Core.Model;
using ESafety.Core.Model.PARA;
using ESafety.Core.Model.View;
using ESafety.Web.Unity;
using Newtonsoft.Json.Linq;
using Quick.WXHelper;
using Quick.WXHelper.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;

namespace ESafety.Account.API.Controllers
{
    [RoutePrefix("api/wx")]
    public class WeixinController : ESFAPI
    {

        private Core.IAuth_User bll = null;

        public WeixinController(Core.IAuth_User user)
        {
            bll = user as Service.Auth_UserService;
            BusinessServices = new List<object> { bll as Service.Auth_UserService };
        }
        /// <summary>
        /// 通过openid登陆
        /// </summary>
        /// <param name="openID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("signinbyopenid/{openID:string}")]
        [AllowAnonymous]
        public ActionResult<UserView> UserSignin(string openID)
        {
            LogContent = "用户登陆,用户：" + openID;
            return bll.UserSigninByopenID(openID);
        }
        /// <summary>
        /// 用户绑写微信
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("userwxbind")]
        [AllowAnonymous]
        public ActionResult<UserView> UserWxBind(UserSigninWX para)
        {
            LogContent = "用户绑定微信账号：" + Newtonsoft.Json.JsonConvert.SerializeObject(para);
            return bll.UserSigninBind(para);
        }
        /// <summary>
        /// 解绑，非匿名访问
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("userwxunbind/{openid:string}")]
        [AllowAnonymous]
        public ActionResult<bool> UserWxUnBind(string openid)
        {
            LogContent = "解绑微信号：" + openid;
            return bll.UserWxUnBind(openid);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("wxjscfg")]
        public APIResult<JSConfig> GetWXConfig(string url)
        {

            return Quick.WXHelper.WxService.GetJSWxConfig(url);
        }

        [AllowAnonymous]
        [HttpGet]
        [HttpPost]
        [Route("getmsg")]
        public async Task GetMessageAsync()
        {
            HttpContext context = HttpContext.Current;
            context.Response.ContentType = "text/plain";
            string HttpMethod = context.Request.HttpMethod.ToUpper();
            string responseXML = string.Empty;
            XElement requestXML;
            switch (HttpMethod)
            {
                case "GET":
                    string signature = context.Request.QueryString["signature"]; //微信加密签名
                    string timestamp = context.Request.QueryString["timestamp"];// 时间戳
                    string nonce = context.Request.QueryString["nonce"]; //   随机数
                    string echostr = context.Request.QueryString["echostr"]; //随机字符串
                    if (!string.IsNullOrEmpty(signature) || !string.IsNullOrEmpty(timestamp) || !string.IsNullOrEmpty(nonce) || !string.IsNullOrEmpty(echostr))
                    {
                        //检查加密签名是否正确
                        if (Quick.WXHelper.WxMsgService.CheckSignaTrue(signature, timestamp, nonce))
                        {
                            responseXML = echostr;
                        }
                    }
                    break;
                case "POST":
                    // 解析微信请求
                    requestXML = XElement.Load(context.Request.InputStream);
                    try
                    {
                        //获取地理位署
                        //await _wechatservice.ReceiveMsg(requestXML);
                        //Task.Run(async () => {

                        //});

                    }
                    catch (Exception ex)
                    {
                        var result = ex;
                    }
                    //处理微信消息，返回的消息是发送
                    responseXML = Quick.WXHelper.WxMsgService.GetMessage(requestXML);
                    //if (Logger.IsInfoEnabled)
                    //{
                    //    Logger.Info("接收到的消息：" + requestXML.ToString());
                    //    Logger.Info("回复的消息：" + responseXML);
                    //}
                    break;
            }

            context.Response.Write(responseXML);
            context.Response.End();
        }

        /// <summary>
        /// 静默获取openid鉴权
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("authcode")]
        public void GetCode()
        {
            HttpContext context = HttpContext.Current;
            context.Response.ContentType = "text/plain";

            string BusinessUrl = context.Request.QueryString["url"]; //业务页面地址
            string codeurl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + Quick.WXHelper.WxConfig.configModel.AppID + "&redirect_uri=" + Quick.WXHelper.WxConfig.configModel.Openid_redirecturl + "&response_type=code&scope=snsapi_base&state=" + BusinessUrl + "#wechat_redirect";

            context.Response.Redirect(codeurl);
        }

        
        /// <summary>
        /// 获取openid
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("authopenid")]
        public void GetOpenID()
        {
            HttpContext context = HttpContext.Current;
            context.Response.ContentType = "text/plain";

            string code = context.Request.QueryString["code"];
            string state = context.Request.QueryString["state"];
            //获取openid
            string resultJosn = Quick.WXHelper.WxAction.wxAPIGet(Quick.WXHelper.WxConfig.wxActionType.GetOpenID, "", code);
            string openid = "";

            JObject jo = JObject.Parse(resultJosn);

            if (!resultJosn.Contains("errcode"))
            {
                openid = jo["openid"].ToString();
                if (state.Contains("%3F") || state.Contains("?"))//如果返回页面带参数
                {

                    context.Response.Redirect(state + "&openID=" + openid);
                }
                else
                {

                    context.Response.Redirect(state + "?openID=" + openid);
                }
            }
            else
            {
                string errmsg = jo["errmsg"].ToString();
                context.Response.Redirect(state + "?errmsg=" + errmsg);
            }
        }
    }
}
