using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.DB
{
    /// <summary>
    /// 子表
    /// </summary>
    public class RPTChildrenTable: ModelBase
    {
        /// <summary>
        /// 报表ID 子表
        /// </summary>
        public Guid ReportID { get; set; }
        /// <summary>
        /// 子表标标题
        /// </summary>
        public string ChildeCaption { get; set; }
        /// <summary>
        /// 主表列 属于 子表
        /// </summary>
        public string MasterKeyColumn { get; set; }
        /// <summary>
        /// 子表列 属于 子表
        /// </summary>
        public string ChildeKeyColumn { get; set; }
        /// <summary>
        /// 子表顺序 属于 子表
        /// </summary>
        public int ChildeIndex { get; set; }
        
    }
}
