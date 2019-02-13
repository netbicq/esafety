using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.DB.Platform
{
    /// <summary>
    /// 报表作用域
    /// </summary>
    public class RPTAccountScope: ModelBase
    {
        /// <summary>
        /// 报表ID 属于 报表作用域
        /// </summary>
        public Guid ReportID { get; set; }
        /// <summary>
        /// 账套号
        /// </summary>
        public string AccountCode { get; set; }
       
    }
}
