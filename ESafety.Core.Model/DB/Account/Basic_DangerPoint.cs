using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.DB.Account
{
    public class Basic_DangerPoint:ModelBase
    {
        /// <summary>
        /// 风险点名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 二维码路径
        /// </summary>
        public string QRCoderUrl { get; set; }
    }
}
