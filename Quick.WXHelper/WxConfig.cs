using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Quick.WXHelper
{
    public static class WxConfig
    {

        public static ConfigModel configModel { get; set; }


        public static void LoadConfig()
        {
            string cfJson = "";
            using (System.IO.StreamReader rd = new System.IO.StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + "/wx.config", System.Text.Encoding.Default))
            {
                cfJson = rd.ReadToEnd();
                rd.Close();
            }
            configModel = JsonConvert.DeserializeObject<ConfigModel>(cfJson);
         
        }

        #region "消息类型"

        public const string MsgTypeText = "text";
        public const string MsgTypeIMG = "image";
        public const string MsgTypeVoice = "voice";
        public const string MsgTypeVideo = "video";
        public const string MsgTypeShortvideo = "shortvideo";
        public const string MsgTypelocation = "location";
        public const string MsgTypelink = "link";
        public const string MsgTypeevent = "event";

        public const string EventTypesubscribe = "subscribe";
        public const string EventTypeSCAN = "SCAN";
        public const string EventTypeLOCATION = "LOCATION";
        public const string EventTypeCLICK = "CLICK";
        public const string EventTypeVIEW = "VIEW";

        #endregion

        #region "微信接口URL地址"

        /// <summary>
        /// 新增素材
        /// 请求方法:POST
        /// {0}accesstoken
        /// </summary>
        public const string WxUrlMaterialNew = "https://api.weixin.qq.com/cgi-bin/material/add_news?access_token={0}";

        /// <summary>
        /// 获取指定ID的素材
        /// 请求方法：GET
        /// {0}accesstoken
        /// </summary>
        public const string WxUrlMaterialGet = "https://api.weixin.qq.com/cgi-bin/material/get_material?access_token={0}";

        /// <summary>
        /// 获取指定serverid的临时素材
        /// 请求方法：GET
        /// {0}accesstoken
        /// {1}serverid
        /// </summary>
        public const string WxUrlMaterialVoice = "https://api.weixin.qq.com/cgi-bin/media/get/jssdk?access_token={0}&media_id={1}";
        /// <summary>
        /// 删除素材
        /// 请求方法：POST
        /// {0}accesstoken
        /// 
        /// </summary>
        public const string WxUrlMaterialDel = "https://api.weixin.qq.com/cgi-bin/material/del_material?access_token={0}";

        /// <summary>
        /// 修改素材内容
        /// 请求方法：POST
        /// {0}accesstoken
        /// </summary>
        public const string WxUrlMaterialupdate = "https://api.weixin.qq.com/cgi-bin/material/update_news?access_token={0}";
        /// <summary>
        /// 获取素材总量
        /// 请求方法:GET
        /// {0} accesstoken
        /// </summary>
        public const string WxUrlMaterialCount = "https://api.weixin.qq.com/cgi-bin/material/get_materialcount?access_token={0}";

        /// <summary>
        /// 设备用户备注
        /// 请求方法：POST
        /// {0}accesstoken
        /// </summary>
        public const string WxUrlUserSetRemark = "https://api.weixin.qq.com/cgi-bin/user/info/updateremark?access_token={0}";
        /// <summary>
        /// 批量获取用户基本信息列表
        /// 请求方法：POST
        /// {0}accesstoken
        /// </summary>

        public const string WxUrlUserInfoList = "https://api.weixin.qq.com/cgi-bin/user/info/batchget?access_token={0}";

        /// <summary>
        /// 批量获取用户openid列表
        /// 请求方法：GET
        /// {0}accesstoken
        /// {1}next_openid
        /// </summary>
        public const string WxUrlUserOpenidList = "https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid={1}";

        /// <summary>
        /// 获取微信用户的基本信息
        /// 请求方法：GET
        /// 返回JSON：
        /// {0}替换为 access_token
        /// {1}替换为 openID
        /// </summary>
        public const string WxUrlUserInfo = "https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN";
        /// <summary>
        /// 获取素材库列表
        /// {0}替换为access_token
        /// </summary>
        public const string WxUrlMaterialList = "https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={0}";
        /// <summary>
        /// 创建自定义菜单
        /// 请求方法：POST
        /// 返回JSON:{"errcode":0,"errmsg":"ok"} 
        /// {0} 规换为 access_token
        /// </summary>
        public const string WxUrlCreateMenu = " https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}";
        /// <summary>
        /// 获取自定义菜单
        /// 请求方法：GET
        /// {0}替换为 access_token
        /// </summary>
        public const string WxUrlQueryMenu = "https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}";
        /// <summary>
        /// 删除自定义菜单
        /// 请求方法：GET
        /// 返回JSON：{"errcode":0
        /// {0}规换为 access_token
        /// /// 
        /// </summary>
        public const string WxUrlDelMenu = "https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}";
        /// <summary>
        /// 获取Access_Token
        /// 请求方法：Get
        /// 返回JSON：{"access_token":"ACCESS_TOKEN","expires_in":7200}
        /// 返回异常：{"errcode":40013,"errmsg":"invalid appid"}
        /// {0}替换为 appID
        /// {1}替换为 appSecret
        /// 
        /// </summary>
        public const string WxUrlGetAccess_Token = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
        /// <summary>
        /// 获取JS_Ticket
        /// 请求方法：Get
        /// {0}替换为 accesstoken
        /// 
        /// </summary>
        public const string WxUrlGetJS_Ticket = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi";
        /// <summary>
        /// 获取微信服务器地址
        /// 请求方法：Get
        /// 返回JSON：{"ip_list":["127.0.0.1","127.0.0.1"]}
        /// {0}替换为 access_token
        /// </summary>
        public const string WxUrlServerIP = "https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token={0}";

        /// <summary>
        /// 通过页面获取OpenID
        /// 请求方法：Get
        /// 返回JSON：{"access_token":"ACCESS_TOKEN","expires_in":7200, 
        /// "refresh_token":"REFRESH_TOKEN", "openid":"OPENID",
        /// "scope":"SCOPE" }
        /// {0}替换为 appid
        /// {1}替换为 secret
        /// {2}替换为 页面获取到的code
        /// </summary>
        public const string WxUrlGetOpenID = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code ";
        
        /// <summary>
        /// 设备消息模板的行业
        /// 请求方法：POST 
        /// {0}替换为 access_token
        /// </summary>
        public const string WxUrlSetIndustry = "https://api.weixin.qq.com/cgi-bin/template/api_set_industry?access_token={0}";


        /// <summary>
        /// 获取模板消息列表
        /// 请求方法：GET
        /// {0}替换为access_token
        /// </summary>
        public const string WxUrlGetTemplateMessages = "https://api.weixin.qq.com/cgi-bin/template/get_all_private_template?access_token={0}";

        /// <summary>
        /// 发送模板消息
        /// 请求方法：POST
        /// {0}替换为access_token
        /// </summary>
        public const string WxUrlSendTemplateMessage = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}";

        /// <summary>
        /// 发红包链接
        /// </summary>
        public const string WxUrlSendRedPack = "https://api.mch.weixin.qq.com/mmpaymkttransfers/sendredpack";
         
        #endregion





        #region "素材类型"

        public const string MaterialTypeIMG = "image";

        public const string MaterialVideo = "video";

        public const string MaterialVoice = "voice";

        public const string MaterialNew = "news";

        public const string Materialthumb = "thumb";

        #endregion







        public enum wxActionType
        {

            /// <summary>
            /// 获取token
            /// </summary>
            access_Token = 0,
            /// <summary>
            /// 获取用户信息
            /// </summary>
            Userinfo = 1,
            /// <summary>
            /// 创建菜单
            /// </summary>
            CreateMenu = 2,
            /// <summary>
            /// 删除菜单
            /// </summary>
            DelMenu = 3,
            /// <summary>
            /// 页面获取openid
            /// </summary>
            GetOpenID = 4,
            /// <summary>
            /// 设置模板消息行业
            /// </summary>
            SetIndustry=5,
            /// <summary>
            /// 其它
            /// </summary>
            Other=6,
            /// <summary>
            /// 发红包
            /// </summary>
            SendRedPack=7



        }



}
}
