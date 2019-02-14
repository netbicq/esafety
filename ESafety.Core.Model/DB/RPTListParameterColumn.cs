using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.DB
{
    /// <summary>
    /// 参数列
    /// </summary>
    public class RPTListParameterColumn: ModelBase
    {
        /// <summary>
        /// 报表ID
        /// </summary>
        public Guid ReportID { get; set; }
        /// <summary>
        /// 参数ID
        /// </summary>
        public Guid ParameterID { get; set; }
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 列标题
        /// </summary>
        public string ColumnCaption { get; set; }
        
    }
}
