using ESafety.Web.Unity;
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


        [HttpGet]
        [Route("wxjscfg")]
        public APIResult<JSConfig> GetWXConfig(string url)
        {

            return Quick.WXHelper.WxService.GetJSWxConfig(url);
        }

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
        
    }
}
