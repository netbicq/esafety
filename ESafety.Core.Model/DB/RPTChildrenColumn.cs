using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.DB
{
    /// <summary>
    /// 子表列
    /// </summary>
    public class RPTChildrenColumn: ModelBase
    {
        /// <summary>
        /// 报表ID
        /// </summary>
        public Guid ReportID { get; set; }
        /// <summary>
        /// 子表ID
        /// </summary>
        public Guid ChildeID { get; set; }
        /// <summary>
        /// 子表列名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 子表列标题
        /// </summary>
        public string ColumnCaption { get; set; }
        /// <summary>
        /// 数据类型 来自enum1string 2int 3numeric 4 date 5 bool
        /// </summary>
        public int DataType { get; set; }
        /// <summary>
        /// 列排序值
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Visiable { get; set; }

    }
}
