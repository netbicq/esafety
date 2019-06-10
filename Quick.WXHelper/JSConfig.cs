using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.WXHelper
{
    public class JSConfig
    {
        /// <summary>
        /// appid
        /// </summary>
        public string appId { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// 随机串
        /// </summary>
        public string nonceStr { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string signature { get; set; }
        /// <summary>
        /// JS接口列表
        /// </summary>
        public IEnumerable<string> jsApiList { get; set; }

        public string JS_ticket { get; set; }
    }

    public class JSConfigPara
    {
        /// <summary>
        /// 当前url
        /// </summary>
        public string url { get; set; }
    }
}
