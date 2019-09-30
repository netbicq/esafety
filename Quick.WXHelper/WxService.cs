using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Quick.WXHelper.Dto;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Xml.Linq;

namespace Quick.WXHelper
{
    public class WxService
    {


        #region"用户管理"
        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="openid">openid</param>
        /// <returns></returns>
        public static Dto.APIResult<Dto.UserInfo> UserInfo(string openid)
        {
            var re = WxAction.wxAPIGet(WxConfig.wxActionType.Userinfo, openid);
            if (re.Contains("errcode"))
            {
                var robj = (JObject)JsonConvert.DeserializeObject(re);
                var msg = robj.GetValue("errmsg").ToString();

                return new Dto.APIResult<Dto.UserInfo>(new Exception(msg));
            }
            else
            {
                var reobj = JsonConvert.DeserializeObject<Dto.UserInfo>(re);
                return new Dto.APIResult<Dto.UserInfo>(reobj);
            }
        }

        /// <summary>
        /// 设置关注者备注
        /// </summary>
        /// <param name="paraJosn"></param>
        /// <returns></returns>
        public static Dto.APIResult<string> UserSetRemark(Dto.SetUserReamrk paraJosn)
        {
            var url = string.Format(WxConfig.WxUrlUserSetRemark, Access_TokenModel.access_Token);
            var para = JsonConvert.SerializeObject(paraJosn);

            var re = WxAction.WxAPIPOST(WxConfig.wxActionType.Other, para, url);
            var robj = (JObject)JsonConvert.DeserializeObject(re);
            var msg = robj.GetValue("errmsg").ToString();
            if (msg == "ok")
            {
                return new Dto.APIResult<string>("ok");
            }
            else
            {
                return new Dto.APIResult<string>(new Exception(msg));
            }
        }

        /// <summary>
        ///获取用户列表
        /// </summary>
        /// <param name="next_openid"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public static Dto.APIResult<Dto.UserInfoListResult> GetUserInfoList(string next_openid, int pagesize)
        {
            try
            {
                var openidlisturl = string.Format(WxConfig.WxUrlUserOpenidList, Access_TokenModel.access_Token, next_openid);

                var openidresult = WxAction.wxAPIGet(WxConfig.wxActionType.Other, "", "", openidlisturl);
                if (openidresult.Contains("errcode"))
                {

                    JObject oresult = (JObject)JsonConvert.DeserializeObject(openidresult);

                    throw new Exception(oresult.GetValue("errmsg").ToString());
                }

                var openidlist = JsonConvert.DeserializeObject<Dto.UserListResult>(openidresult);

                var infopara = new Dto.UserInfoListPara
                {
                    user_list = from s in openidlist.data.openid.Take(pagesize)
                                select new Dto.UserInfoListParaBase
                                {
                                    openid = s
                                }

                };

                var infolisturl = string.Format(WxConfig.WxUrlUserInfoList, Access_TokenModel.access_Token);

                var infolistre = WxAction.WxAPIPOST(WxConfig.wxActionType.Other, Newtonsoft.Json.JsonConvert.SerializeObject(infopara), infolisturl);
                if (infolistre.Contains("errcode"))
                {
                    JObject inforesult = (JObject)JsonConvert.DeserializeObject(infolistre);
                    throw new Exception(inforesult.GetValue("errmsg").ToString());

                }
                var infore = JsonConvert.DeserializeObject<Dto.UserInfoListResult>(infolistre);

                var re = new Dto.UserInfoListResult
                {
                    user_info_list = infore.user_info_list,
                    next_openid = infopara.user_list.LastOrDefault().openid,
                    total = openidlist.total
                };


                return new Dto.APIResult<Dto.UserInfoListResult>(re);
            }
            catch (Exception ex)
            {
                return new Dto.APIResult<Dto.UserInfoListResult>(ex);

            }


        }
        #endregion



        #region "菜单"
        /// <summary>
        /// 创建 菜单
        /// </summary>
        /// <param name="menu">菜单json</param>
        /// <returns></returns>
        public static Dto.APIResult<string> CreateMenu(Dto.Menu menu)
        {
            if (menu.button.Count() > 3)
            {
                throw new Exception("一级菜单菜单数超过3个不被允许");
            }
            if (menu.button.Select(s => s.sub_button.Count()).Any(q => q > 5))
            {
                throw new Exception("二级菜单数超过5个不被允许");
            }

            var re = WxAction.WxAPIPOST(WxConfig.wxActionType.CreateMenu, JsonConvert.SerializeObject(menu));


            var robj = (JObject)JsonConvert.DeserializeObject(re);
            var msg = robj.GetValue("errmsg").ToString();
            if (msg == "ok")
            {
                return new Dto.APIResult<string>("ok");
            }
            else
            {
                return new Dto.APIResult<string>(new Exception(msg));
            }
        }
        /// <summary>
        /// 查询菜单
        /// </summary>
        /// <returns></returns>
        public static Dto.APIResult<Dto.MenuQuery> GetMenu()
        {
            var url = string.Format(WxConfig.WxUrlQueryMenu, Access_TokenModel.access_Token);
            var re = WxAction.wxAPIGet(WxConfig.wxActionType.Other, "", "", url);
            if (re.Contains("errcode"))
            {
                var robj = (JObject)JsonConvert.DeserializeObject(re);
                var msg = robj.GetValue("errmsg").ToString();
                return new Dto.APIResult<Dto.MenuQuery>(new Exception(msg));
            }
            else
            {
                var reobj = JsonConvert.DeserializeObject<Dto.MenuQuery>(re);
                return new Dto.APIResult<Dto.MenuQuery>(reobj);
            }
        }
        #endregion


        #region"素材管理"

        /// <summary>
        /// 素材列表
        /// </summary>
        /// <param name="materialtype"></param>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <returns></returns>
        public static Dto.APIResult<dynamic> GetMaterialList(string materialtype, int pagesize, int pageindex)
        {
            var url = string.Format(WxConfig.WxUrlMaterialList, Access_TokenModel.access_Token);

            dynamic para = new System.Dynamic.ExpandoObject();
            para.type = materialtype;
            para.offset = pagesize * pageindex;
            para.count = pagesize;
            var parastr = Newtonsoft.Json.JsonConvert.SerializeObject(para);

            var resultstr = (string)WxAction.WxAPIPOST(WxConfig.wxActionType.Other, parastr, url);
            string msg = "";
            if (resultstr.Contains("errcode"))
            {
                var robj = (JObject)JsonConvert.DeserializeObject(resultstr);
                msg = robj.GetValue("errmsg").ToString();
                return new Dto.APIResult<dynamic>(new Exception(msg));
            }
            else
            {
                if (materialtype == "news")
                {
                    var reobj = JsonConvert.DeserializeObject<Dto.MaterialTXTIMGResult>(resultstr);
                    return new Dto.APIResult<dynamic>(reobj);
                }
                else if (materialtype == "image" || materialtype == "video" || materialtype == "voice")
                {
                    var reobjv = JsonConvert.DeserializeObject<Dto.MaterialIVVResult>(resultstr);
                    return new Dto.APIResult<dynamic>(reobjv);
                }
                else
                {
                    return new Dto.APIResult<dynamic>(new Exception("未支持的类型"));
                }
            }

        }
        /// <summary>
        /// 获取素材总量
        /// </summary>
        /// <returns></returns>
        public static Dto.APIResult<string> GetMaterialCount()
        {
            var url = string.Format(WxConfig.WxUrlMaterialCount, Access_TokenModel.access_Token);

            var restr = WxAction.wxAPIGet(WxConfig.wxActionType.Other, "", "", url);
            if (restr.Contains("errcode"))
            {
                var robj = (JObject)JsonConvert.DeserializeObject(restr);
                var msg = robj.GetValue("errmsg").ToString();
                return new Dto.APIResult<string>(new Exception(msg));

            }
            else
            {
                return new Dto.APIResult<string>(restr);
            }
        }
        /// <summary>
        /// 修改素材
        /// </summary>
        /// <param name="parajosn"></param>
        /// <returns></returns>
        public static Dto.APIResult<string> UpdateMaterial(string parajosn)
        {
            var url = string.Format(WxConfig.WxUrlMaterialupdate, Access_TokenModel.access_Token);

            var restr = WxAction.WxAPIPOST(WxConfig.wxActionType.Other, parajosn, url);
            var robj = (JObject)JsonConvert.DeserializeObject(restr);

            if (robj.GetValue("errcode").ToString() != "0")
            {
                var msg = robj.GetValue("errmsg").ToString();
                return new Dto.APIResult<string>(new Exception(msg));
            }
            else
            {
                return new Dto.APIResult<string>("ok");
            }
        }
        /// <summary>
        /// 删除素材
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Dto.APIResult<string> DeleteMaterial(string id)
        {
            var url = string.Format(WxConfig.WxUrlMaterialDel, Access_TokenModel.access_Token);
            dynamic para = new System.Dynamic.ExpandoObject();
            para.media_id = id;

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(para);
            var restr = WxAction.WxAPIPOST(WxConfig.wxActionType.Other, json, url);
            var robj = (JObject)JsonConvert.DeserializeObject(restr);
            if (robj.GetValue("errcode").ToString() != "0")
            {
                var msg = robj.GetValue("errmsg").ToString();
                return new Dto.APIResult<string>(new Exception(msg));
            }
            else
            {
                return new Dto.APIResult<string>("ok");
            }
        }
        /// <summary>
        /// 获取素材
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Dto.APIResult<string> GetMaterial(string id)
        {
            var url = string.Format(WxConfig.WxUrlMaterialGet, Access_TokenModel.access_Token);
            dynamic para = new System.Dynamic.ExpandoObject();
            para.media_id = id;
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(para);

            var restr = WxAction.wxAPIGet(WxConfig.wxActionType.Other, "", "", url);
            if (restr.Contains("errcode"))
            {
                var robj = (JObject)JsonConvert.DeserializeObject(restr);
                var msg = robj.GetValue("errmsg").ToString();
                return new Dto.APIResult<string>(new Exception(msg));
            }
            else
            {
                return new Dto.APIResult<string>(restr);
            }
        }
        /// <summary>
        /// 下载声音文件到本地
        /// </summary>
        /// <param name="serverid"></param>
        /// <returns></returns>
        public static APIResult<string> DowloadVoiceAsync(string serverid)
        {
            //string path = System.Web.Hosting.HostingEnvironment.MapPath($"~/upload/audio/{serverid}.speex");
            try
            {
                var url = string.Format(WxConfig.WxUrlMaterialVoice, Access_TokenModel.access_Token, serverid);
                HttpWebRequest request = (HttpWebRequest)System.Net.HttpWebRequest.Create(url);
                request.Method = "GET";
                using (System.Net.WebResponse response = request.GetResponse())
                {
                    using (System.IO.Stream stream = response.GetResponseStream())
                    {


                        int filesize = int.Parse(response.Headers.Get("Content-Length"));
                        Byte[] buffer = new Byte[filesize];

                        stream.Read(buffer, 0, filesize);
                        //"F:/speex/";// + serverid + ".speex";// 
                        string webpath = System.Web.Hosting.HostingEnvironment.MapPath($"~/upload/audio/");
                        if (!System.IO.Directory.Exists(webpath))
                        {
                            System.IO.Directory.CreateDirectory(webpath);
                        }
                        string filepath = webpath + serverid + ".speex";

                        using (System.IO.FileStream fr = new System.IO.FileStream(filepath, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                        {

                            fr.Write(buffer, 0, filesize);
                            fr.Close();
                        }

                        stream.Close();
                    }
                }

                return new APIResult<string>($"~/upload/audio/{serverid}.speex");
            }
            catch (Exception ex)
            {
                return new APIResult<string>(ex);
            }

        }
        /// <summary>
        /// 新建素材
        /// </summary>
        /// <param name="parajson"></param>
        /// <returns></returns>
        public static Dto.APIResult<string> AddMaterial(string parajson)
        {
            var url = string.Format(WxConfig.WxUrlMaterialNew, Access_TokenModel.access_Token);

            var restr = WxAction.WxAPIPOST(WxConfig.wxActionType.Other, parajson, url);

            var robj = (JObject)JsonConvert.DeserializeObject(restr);
            var msg = robj.GetValue("errmsg").ToString();
            if (msg != "ok")
            {
                return new Dto.APIResult<string>(new Exception(msg));
            }
            else
            {
                return new Dto.APIResult<string>(restr);
            }

        }


        #endregion


        #region"模板消息"
        /// <summary>
        /// 设置模板消息行业
        /// </summary>
        /// <param name="ParaJson"></param>
        /// <returns></returns>
        public static Dto.APIResult<string> SetIndustry(string ParaJson)
        {
            var restr = WxAction.WxAPIPOST(WxConfig.wxActionType.SetIndustry, ParaJson);
            var robj = (JObject)JsonConvert.DeserializeObject(restr);
            var msg = robj.GetValue("errmsg").ToString();

            if (msg != "ok")
            {
                return new Dto.APIResult<string>(new Exception(msg));
            }
            else
            {
                return new Dto.APIResult<string>(restr);
            }

        }

        public static Dto.APIResult<TemplateMessage> GetTemplateMessages()
        {
            var url = string.Format(WxConfig.WxUrlGetTemplateMessages, Access_TokenModel.access_Token);

            var restr = WxAction.wxAPIGet(WxConfig.wxActionType.Other, "", "", url);
            if (restr.Contains("errcode"))
            {
                var robj = (JObject)JsonConvert.DeserializeObject(restr);
                var msg = robj["errmsg"].ToString();
                return new Dto.APIResult<TemplateMessage>(new Exception(msg));
            }
            else
            {
                var reobj = JsonConvert.DeserializeObject<TemplateMessage>(restr);
                return new Dto.APIResult<TemplateMessage>(reobj);
            }
        }
        /// <summary>
        /// 返回msgid
        /// </summary>
        /// <param name="mmsg"></param>
        /// <returns></returns>
        public static Dto.APIResult<string> SendTemplateMessage(Dto.TemplateMessagePara mmsg)
        {
            var url = string.Format(WxConfig.WxUrlSendTemplateMessage, Access_TokenModel.access_Token);
            var para = JsonConvert.SerializeObject(mmsg);
            var restr = WxAction.WxAPIPOST(WxConfig.wxActionType.Other, para, url);
            var robj = (JObject)JsonConvert.DeserializeObject(restr);
            if (robj["errcode"].ToString() == "0")
            {
                return new Dto.APIResult<string>(robj["msgid"].ToString());
            }
            else
            {
                return new Dto.APIResult<string>(new Exception(robj["errmsg"].ToString()));
            }
        }


        #endregion



        #region"JS-SDK处理"

        public static APIResult<JSConfig> GetJSWxConfig(string url)
        {
            var noncestr = Guid.NewGuid().ToString().Replace("-", "");
            var jsapi_ticket = Access_TokenModel.JSapi_Ticket;
            var timestamp = ((int)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds).ToString();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("noncestr", noncestr);
            dict.Add("jsapi_ticket", jsapi_ticket);
            dict.Add("timestamp", timestamp);
            dict.Add("url", url);

            
            List<string> paras = new List<string>();
            paras.Add("noncestr");
            paras.Add("jsapi_ticket");
            paras.Add("timestamp");
            paras.Add("url");

            paras.Sort();

            string signStr = "";

            paras.ForEach(f =>
            {
                string valueStr = "";
                dict.TryGetValue(f, out valueStr);

                signStr += f + "=" + valueStr + "&";
            });

            string signSource = signStr.Substring(0, signStr.Length - 1);

            var signature = WxMsgService.Sha1Encrypt(signSource);

            var jslist = new List<string>()
            {
                "startRecord","stopRecord","onVoiceRecordEnd",
                "playVoice","pauseVoice","stopVoice","onVoicePlayEnd",
                "uploadVoice","downloadVoice","scanQRCode"
            };


            return new APIResult<JSConfig>(new JSConfig
            {
                appId = WxConfig.configModel.AppID,
                nonceStr = noncestr,
                signature = signature,
                timestamp = timestamp,
                jsApiList = jslist,
                JS_ticket = jsapi_ticket
            });

        }

        #endregion


        #region "微信红包"
        /// <summary>
        /// 发送红包
        /// </summary>
        /// <param name="openid">微信openid</param>
        /// <param name="sendmoney">发送金额,单位分</param>
        /// <param name="wishing">红包祝福语</param>
        /// <param name="actname">活动名称</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public static APIResult<bool> SendRedPack(string openid,int sendmoney,string Wishing)
        {
            Dictionary<string, string> paraDict = new Dictionary<string, string>();

            //加入常规参数

            paraDict.Add("nonce_str", WxAction.GenerateNonceStr());//随机串
            paraDict.Add("mch_billno", WxAction.GenerateBillNo());//订单号
            paraDict.Add("mch_id", WxConfig.configModel.MCHID);//商户号
            paraDict.Add("wxappid", WxConfig.configModel.AppID);//公众号appid
            paraDict.Add("client_ip", WxConfig.configModel.IP);//调用接口的ip地址
            paraDict.Add("send_name", WxConfig.configModel.send_name);//商户名称
            paraDict.Add("re_openid", openid);//微信openid
            paraDict.Add("total_amount", sendmoney.ToString());//发送金额，单位分
            paraDict.Add("total_num", "1");//红包个数，固定为1个
            paraDict.Add("wishing", Wishing);//红包祝福语 
            paraDict.Add("act_name", WxConfig.configModel.Actname);//活动名称
            paraDict.Add("remark", WxConfig.configModel.Remark);//红包显示的备注
            paraDict.Add("scene_id", "PRODUCT_2");//模板ID 固定普通红包ID
           

            string reStr = string.Empty;
             
           var odict= paraDict.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
            
            foreach (var item in odict)
            {
                reStr += item.Key + "=" + item.Value + "&";
            }
            reStr +="key="+ WxConfig.configModel.KEY;//最后加上支付的key

            var singstr = WxMsgService.Md5Encrypt(reStr).ToUpper();//算出签名 大写

            paraDict.Add("sign", singstr);//签名字串


            string nonce_str;
            paraDict.TryGetValue("nonce_str",out nonce_str);

            string mch_billno;
            paraDict.TryGetValue("mch_billno", out mch_billno);

            string mch_id;
            paraDict.TryGetValue("mch_id", out mch_id);

            string wxappid;
            paraDict.TryGetValue("wxappid", out wxappid);

            string client_ip;
            paraDict.TryGetValue("client_ip", out client_ip);

            string send_name;
            paraDict.TryGetValue("send_name", out send_name);

            string re_openid;
            paraDict.TryGetValue("re_openid", out re_openid);

            string total_amount;
            paraDict.TryGetValue("total_amount", out total_amount);

            string total_num;
            paraDict.TryGetValue("total_num", out total_num);

            string wishing;
            paraDict.TryGetValue("wishing", out wishing);

            string act_name;
            paraDict.TryGetValue("act_name",out act_name);

            string remark;
            paraDict.TryGetValue("remark", out remark);

            string scene_id;
            paraDict.TryGetValue("scene_id", out scene_id);

            //拼接参数
            string paraString = 
                $"<xml>" +
                $"<sign><![CDATA[{singstr}]]></sign>" +
                $"<mch_billno><![CDATA[{mch_billno}]]></mch_billno>" +
                $"<mch_id><![CDATA[{mch_id}]]></mch_id>" +
                $"<wxappid><![CDATA[{wxappid}]]></wxappid>" +
                $"<send_name><![CDATA[{send_name}]]></send_name>" +
                $"<re_openid><![CDATA[{re_openid}]]></re_openid>" +
                $"<total_amount><![CDATA[{total_amount}]]></total_amount>" +
                $"<total_num><![CDATA[{total_num}]]></total_num>" +
                $"<wishing><![CDATA[{wishing}]]></wishing>" +
                $"<client_ip><![CDATA[{client_ip}]]></client_ip>" +
                $"<act_name><![CDATA[{act_name}]]></act_name>" +
                $"<remark><![CDATA[{remark}]]></remark>" +
                $"<scene_id><![CDATA[{scene_id}]]></scene_id>" + 
                $"<nonce_str><![CDATA[{nonce_str}]]></nonce_str>" + 
                $"</xml>";

            var apire =HttpService.Post(paraString, WxConfig.WxUrlSendRedPack,true,50);
            XElement xml = XElement.Parse(apire);
            RedPackResponse remodel = WxMsgService.XMLToObj<RedPackResponse>(xml);
             
            if (remodel.result_code== "SUCCESS")
            {
                return new APIResult<bool>(true);
            }
            else
            {
                return new APIResult<bool>(new Exception(remodel.return_msg));
            }
        }
        #endregion
    }

}
