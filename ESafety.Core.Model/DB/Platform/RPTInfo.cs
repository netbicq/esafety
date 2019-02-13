using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.DB.Platform
{
    /// <summary>
    /// 报表信息 自定义
    /// </summary>
    public class RPTInfo: ModelBaseEx
    {
         
        /// <summary>
        /// 报表名称  报表信息
        /// </summary>
        public string ReportName { get; set; }
        /// <summary>
        /// 数据源 
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// 报表作用域类型 来自enum 1全局 2账套
        /// </summary>
        public int ScopeType { get; set; } 
    }
}
