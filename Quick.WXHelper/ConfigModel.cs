using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.WXHelper
{
    public class ConfigModel
    {

        /// <summary>
        /// 公众号微信号
        /// </summary>
        public string WXCode { get; set; }
        public string AppID { get; set; }
        public string AppSecret { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string MCHID { get; set; }
        /// <summary>
        /// 接入token
        /// </summary>
        public string CheckToken { get; set; }
        public string KEY { get; set; }
        /// <summary>
        /// 授权域名
        /// </summary>
        public string OAuthHost { get; set; }
        /// <summary>
        /// 支付回调地址
        /// </summary>
        public string PayReURL { get; set; }
        /// <summary>
        /// IP白名单
        /// </summary>
        public string IP { get; set; }

        public string Actname { get; set; }
        /// <summary>
        /// 红包备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 发送红包商家名称
        /// </summary>
        public string send_name { get; set; }
        /// <summary>
        /// 证书路径
        /// </summary>
        public string SSLCERT_PATH { get; set; }

        /// <summary>
        /// 证书口令
        /// </summary>
        public string SSLCERT_PASSWORD { get; set; }

        private string _openidredirecturl = string.Empty;

        public string Openid_redirecturl
        {
            get { return System.Web.HttpUtility.UrlEncode( _openidredirecturl); }
            set { _openidredirecturl = value; }
        }
    }
}
