using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; 

namespace Quick.WXHelper
{
    /// <summary>
    /// accesstoken中控
    /// </summary>
    public static class Access_TokenModel
    {

        private static string _access_token;//token
        private static string _jsapi_ticket;//jsticket

        /// <summary>
        /// 过期时间
        /// </summary>
        public static double Expiry_Time { get; set; }
        public static double Expiry_Time_JS { get; set; }


        public static string JSapi_Ticket
        {
            get
            {
                var timespan = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0);
                if (Expiry_Time_JS - timespan.TotalSeconds < 300)
                {
                    GetJS_Ticket();
                }
                return _jsapi_ticket;
            }
            set
            {
                _jsapi_ticket = value;
            }
        }

        public static string access_Token
        {
            get
            {
                var timespan = DateTime.Now - new DateTime(1970, 1, 1,0,0,0);

                if (Expiry_Time - timespan.TotalSeconds < 300)//提前5分钟更新token
                {
                    GetAccese_Token();
                }
                return _access_token;
            }
            set
            {
                _access_token = value;
            }
        }

        private static void GetJS_Ticket()
        {
            string url = string.Format(WxConfig.WxUrlGetJS_Ticket, access_Token);
            System.Net.WebRequest request = (System.Net.WebRequest)System.Net.HttpWebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            string re = "";
            using (System.Net.WebResponse response = request.GetResponse())
            {
                if(response==null)
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
            JObject jo = JObject.Parse(re);
            if(re.Contains("ticket"))
            {
                var tiket = jo["ticket"].ToString();
                int time_in = (int)jo["expires_in"];//微信accesstoken有效期，以秒为单位

                JSapi_Ticket = tiket;
                Expiry_Time_JS = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds + time_in;
            }
            else
            {
                throw new Exception(re);
            }
        }

        private static void GetAccese_Token()
        {
            string url = string.Format(WxConfig.WxUrlGetAccess_Token, WxConfig.configModel.AppID, WxConfig.configModel.AppSecret);
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

            JObject jo = JObject.Parse(re);

            if (!re.Contains("errcode"))
            {
                var token = jo["access_token"].ToString();
                int time_in = (int)jo["expires_in"];//微信accesstoken有效期，以秒为单位

                access_Token = token;
                Expiry_Time = (DateTime.Now - new DateTime(1970, 1, 1,0,0,0)).TotalSeconds + time_in;

            }
            else
            {
                throw new Exception(re);
            }
        }

    }
}
