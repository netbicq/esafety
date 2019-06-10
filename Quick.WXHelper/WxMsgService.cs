using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Quick.WXHelper
{
    /// <summary>
    /// 微信公众号消息互动Service
    /// </summary>
    public class WxMsgService
    {

        #region "消息结构"

        public static T XMLToObj<T>(XElement xml) where T : class
        {
            var tType = typeof(T);
            T re = (T)Activator.CreateInstance(tType);

            foreach (var p in tType.GetProperties())
            {
                XElement temp = xml.Element(p.Name);
                if (temp != null)
                {
                    if (p.PropertyType == typeof(string))
                    {
                        p.SetValue(re, temp.Value);
                    }
                    if (p.PropertyType == typeof(int))
                    {
                        p.SetValue(re, int.Parse(temp.Value));
                    }
                    if (p.PropertyType == typeof(decimal))
                    {
                        p.SetValue(re, decimal.Parse(temp.Value));
                    }
                    if(p.PropertyType ==typeof(double))
                    {
                        p.SetValue(re, double.Parse(temp.Value));
                    }
                }
            }

            return re;
        }

        /// <summary>
        /// 文本消息结构
        /// {0}替换为：openID
        /// {1}替换为：appID
        /// {2}替换为：时间戳
        /// {3}替换为：Content 具体文本内容
        /// </summary>
        public const string WxMsgText = @"<xml> 
                                           <ToUserName><![CDATA[{0}]]></ToUserName>
                                           <FromUserName><![CDATA[{1}]]></FromUserName>
                                           <CreateTime>{2}</CreateTime>
                                           <MsgType><![CDATA[text]]></MsgType>
                                           <Content><![CDATA[{3}]]></Content>
                                           </xml>";
        /// <summary>
        /// 图片消息结构
        /// {0}替换为：openID
        /// {1}替换为：appID
        /// {2}替换为：时间戳
        /// {3}替换为：图片ID，素材管理
        /// </summary>
        public const string WxMsgImage = @"<xml>
                                           <ToUserName><![CDATA[{0}]]></ToUserName>
                                           <FromUserName><![CDATA[{1}]]></FromUserName>
                                           <CreateTime>{2}</CreateTime>
                                           <MsgType><![CDATA[image]]></MsgType>
                                           <Image><MediaId><![CDATA[{3}]]></MediaId></Image>
                                           </xml>";

        /// <summary>
        /// 图文消息
        /// {0}替换为 openID
        /// {1}替换为 appID
        /// {2}替换为是间戳
        /// {3}替换为图文条数
        /// {4}替换为 Item内容
        /// </summary>
        public const string WxMsgTextAndImage = @"<xml> 
                                                    <ToUserName><![CDATA[{0}]]></ToUserName 
                                                    <FromUserName><![CDATA[{1}]]></FromUserName> 
                                                    <CreateTime>{2}</CreateTime> 
                                                    <MsgType><![CDATA[news]]></MsgType> 
                                                    <ArticleCount>{3}</ArticleCount> 
                                                    <Articles>{4}</Articles> 
                                                    </xml>";

        /// <summary>
        /// 图文消息Item
        /// {0}替换为 Title标题
        /// {1}替换为 Descrption 描述
        /// {2}替换为 PicUrl图片的地址
        /// {3}替换为 Url 内容连接地址
        /// 
        /// </summary>
        public const string WxMsgTextAndImageItem = @"<item> 
                                                        <Title><![CDATA[{0}]]></Title>
                                                        <Description><![CDATA[{1}]]></Description>
                                                        <PicUrl><![CDATA[{2}]]></PicUrl>
                                                        <Url><![CDATA[{3}]]></Url>
                                                        </item>";

 

        #endregion




        public static bool CheckSignaTrue(string signature, string timestamp, string nonce)
        {
            string token = WxConfig.configModel.CheckToken;

            List<string> strList = new List<string>() {
                token,timestamp,nonce
            };
            strList.Sort();
            return Sha1Encrypt(string.Join("", strList)) == signature;

        }

        public static string Sha1Encrypt(string sourceString)
        {
            byte[] bytes = Encoding.Default.GetBytes(sourceString);
            System.Security.Cryptography.HashAlgorithm hash = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            bytes = hash.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }
         
        public static string Md5Encrypt(string sourceString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.UTF8.GetBytes(sourceString);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }

            return byte2String; 
        }

        /// <summary>
        /// 收到消息返回的回复，也就是被动回复公众号平台
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string GetMessage(XElement msg)
        {
            string MsgType = msg.Element("MsgType").Value;
            string responseXML = string.Empty;
            switch(MsgType)
            {
                case "event":
                    string Eventtype = msg.Element("Event").Value;//事件类型
                    string FromUserName = msg.Element("FromUserName").Value;//微信用户
                    switch (Eventtype)
                    {
                        case "subscribe"://关注公众号
                            var remsg = System.Configuration.ConfigurationManager.AppSettings["SubscribeText"];
                            responseXML = string.Format(WxMsgText, FromUserName, WxConfig.configModel.WXCode, WxAction.GenerateTimeStamp(), remsg);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
                    
            }
            return responseXML;
        }
    }
}
