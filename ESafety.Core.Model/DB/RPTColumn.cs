using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.DB
{
    /// <summary>
    /// 报表列
    /// </summary>
    public class RPTColumn: ModelBase
    {
        /// <summary>
        /// 报表ID 属于 报表列
        /// </summary>
        public Guid ReportID { get; set; }
        /// <summary> 
        ///  子表列名  ColumnCaption 属于 报表列
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 子表列标题 ColumnCaption 属于 报表列
        /// </summary>
        public string ColumnCaption { get; set; }
        /// <summary>
        /// 数据类型 来自enum 1string 2int 3numeric 4 date 5 bool
        /// </summary>
        public int DataType { get; set; }
        /// <summary>
        /// 排序值
        /// </summary>
        public int OrderIndex { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Visiable { get; set; }
        
    }
}
