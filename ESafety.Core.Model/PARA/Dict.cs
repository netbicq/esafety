using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.PARA
{
    /// <summary>
    /// 新建词典类型
    /// </summary>
    public class DictTypeNew
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string DictName { get; set; }
    }
     
    /// <summary>
    /// 新建词典
    /// </summary>
    public class DictNew
    {

        /// <summary>
        /// 词典类型id
        /// </summary>
        public Guid ParentID { get; set; }
        /// <summary>
        /// 词典名称
        /// </summary>
        public string DictName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 最小值
        /// </summary>
        public double MinValue { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxValue { get; set; }
    }
    /// <summary>
    /// 修改词典
    /// </summary>
    public class DictEidt : DictNew
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
    }
}
