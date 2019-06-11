using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.DB.Platform
{
    /// <summary>
    /// 平台微信绑定信息
    /// </summary>
    public class Auth_WxBinds:ModelBase
    {
        /// <summary>
        /// 微信openid
        /// </summary>
        public string openID { get; set; }
        /// <summary>
        /// 企业账号
        /// </summary>
        public string AccountCode { get; set; }
    }
}
