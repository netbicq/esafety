using ESafety.Core.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.View
{
    /// <summary>
    /// 报表作用域
    /// </summary>
    public class ReportScope
    {

        /// <summary>
        /// 报表信息
        /// </summary>
        public RPTInfo ReportInfo { get; set; }
        /// <summary>
        /// 账套信息
        /// </summary>
        public  AccountInfo AccountInfo { get; set; }

        /// <summary>
        /// 作用域定义ID
        /// </summary>
        public Guid ID { get; set; }
    }
}
