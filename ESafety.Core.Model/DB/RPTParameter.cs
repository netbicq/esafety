using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.DB
{
    /// <summary>
    /// 报表参数
    /// </summary>
    public class RPTParameter: ModelBase
    {
        /// <summary>
        /// 报表ID
        /// </summary>
        public Guid ReportID { get; set; }
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParameterName { get; set; }
        /// <summary>
        /// 参数类型 来自enum 1 string 2 int 3 numeric 4 date 5bool 6list
        /// </summary>
        public int ParameterType { get; set; }
        /// <summary>
        /// 参数标题
        /// </summary>
        public string ParameterCaption { get; set; }
        /// <summary>
        /// List类型数据源
        /// </summary>
        public string TypeListSource { get; set; }
        /// <summary>
        /// List类型参数值类型 来自enum 1 string 2 int 3 Guid
        /// </summary>
        public int TypeListValueType { get; set; }
        ///// <summary>
        ///// List 属于 报表参数
        ///// </summary>
        //public string ListValueColumn { get; set; }
       
    }
}
