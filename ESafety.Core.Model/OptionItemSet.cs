using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model
{

    /// <summary>
    /// 设置参数的模型
    /// </summary>
    public class OptionItemSet
    {

        /// <summary>
        /// 选项Key
        /// </summary>
        public string OptionKey { get; set; }
        /// <summary>
        /// 选项类型
        /// </summary>
        public Unity.PublicEnum.AccountOptionItemType OptionType { get; set; }
        /// <summary>
        /// 枚举类型
        /// </summary>
        public string EnumType { get; set; }
        /// <summary>
        /// 选项值
        /// </summary>
        public string ItemValue { get; set; }
        /// <summary>
        /// 多值类型的值，其它类型此属性为null
        /// </summary>
        public MultiValue MultiValue { get; set; }
        /// <summary>
        /// 集合类型的值，其它类型此属性为null
        /// </summary>
        public IEnumerable<string> ListValue { get; set; }
    }

    /// <summary>
    /// 多值类型
    /// </summary>
    public class MultiValue
    {
        /// <summary>
        /// 值类型
        /// </summary>
        public Unity.PublicEnum.AccountOptionItemType ValueType { get; set; }
        /// <summary>
        /// 值1
        /// </summary>
        public string Value1 { get; set; }
        /// <summary>
        /// 标题1
        /// </summary>
        public string Caption1 { get; set; }
        /// <summary>
        /// 值2
        /// </summary>
        public string Value2 { get; set; }
        /// <summary>
        /// 标题2
        /// </summary>
        public string Caption2 { get; set; }
        /// <summary>
        /// 值3
        /// </summary>
        public string Value3 { get; set; }
        /// <summary>
        /// 标题3
        /// </summary>
        public string Caption3 { get; set; }
    }
}
