using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.WXHelper
{
    public class WxAction
    {

        public static string wxAPIGet(WxConfig.wxActionType t, string openID = "", string code = "", string geturl = "")
        {
            string url = "";
            switch (t)
            {
                case WxConfig.wxActionType.GetOpenID:

                    url = string.Format(WxConfig.WxUrlGetOpenID, WxConfig.configModel.AppID, WxConfig.configModel.AppSecret, code);
                    break;
                case WxConfig.wxActionType.Userinfo:
                    url = string.Format(WxConfig.WxUrlUserInfo, Access_TokenModel.access_Token, openID);
                    break;
                default:
                    url = geturl;
                    break;
            }
            System.Net.WebRequest request = (System.Net.WebRequest)System.Net.HttpWebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            string re = "";
            using (System.Net.WebResponse response = request.GetResponse())
            {
                if (response == null)
                {
                    throw new Exception("Response is not Created");
                }
                using (System.IO.Stream stream = response.GetResponseStream())
                {
                    using (System.IO.StreamReader rd = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8))
                    {
                        re = rd.ReadToEnd();
                        rd.Close();
                    }
                    stream.Close();
                }
            }
            return re;

        }

        public static string WxAPIPOST(WxConfig.wxActionType t, string ParameterJson = "", string posturl = "")
        {
            string url = "";
            switch (t)
            {
                case WxConfig.wxActionType.CreateMenu:
                    url = string.Format(WxConfig.WxUrlCreateMenu, Access_TokenModel.access_Token);
                    break;
                case WxConfig.wxActionType.SetIndustry:
                    url = string.Format(WxConfig.WxUrlSetIndustry, Access_TokenModel.access_Token);
                    break;
                case WxConfig.wxActionType.SendRedPack:
                    url = WxConfig.WxUrlSendRedPack;
                    break;
                default:
                    url = posturl;
                    break;
            }
            System.Net.WebRequest request = (System.Net.WebRequest)System.Net.HttpWebRequest.Create(url);
            request.Method = "POST";
            Byte[] postbytes = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(ParameterJson);
            request.ContentType = "application/x-www-form-urlencded";
            request.ContentLength = postbytes.Length;
            using (System.IO.Stream stream = request.GetRequestStream())
            {
                stream.Write(postbytes, 0, postbytes.Length);
                stream.Close();
            }
            string re = "";
            using (System.Net.WebResponse response = request.GetResponse())
            {
                if (response == null)
                {
                    throw new Exception("Response is not Created");
                }
                using (System.IO.Stream restream = response.GetResponseStream())
                {
                    using (System.IO.StreamReader getrd = new System.IO.StreamReader(restream, System.Text.Encoding.UTF8))
                    {
                        re = getrd.ReadToEnd();
                        getrd.Close();
                    }
                    restream.Close();
                }
            }
            return re;
        }

        /**
      * 生成时间戳，标准北京时间，时区为东八区，自1970年1月1日 0点0分0秒以来的秒数
       * @return 时间戳
      */
        public static string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /**
     * 生成随机串，随机串包含字母或数字
     * @return 随机串
     */
        public static string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <returns></returns>
        public static string GenerateBillNo()
        {
            var dstr = DateTime.Now.ToString("yyyyMMdd");
            var randstr = GenerateRandomNumber(10);
            return WxConfig.configModel.MCHID + dstr + randstr;
        }
        /// <summary>
        /// 随机数因子
        /// </summary>
        private static char[] constant =
      {
        '0','1','2','3','4','5','6','7','8','9',
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
      };
        /// <summary>
        /// 获取指定长度的随机串
        /// </summary>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string GenerateRandomNumber(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }

    }
}
