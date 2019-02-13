using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Unity
{

    /// <summary>
    /// 公共枚举
    /// </summary>
    public class PublicEnum
    {


        /// <summary>
        /// 自定义类型
        /// </summary>
        public enum EE_UserDefinedType
        {
            /// <summary>
            /// 设备
            /// </summary>
            [Description("设备")]
            Device=1,
            /// <summary>
            /// 岗位
            /// </summary>
            [Description("岗位")]
            Position=2
        }
        /// <summary>
        /// 自定义类型的数据类型
        /// </summary>
        public enum EE_UserDefinedDataType
        {
            /// <summary>
            /// 字符型
            /// </summary>
            [Description("字符")]
            Str=1,
            /// <summary>
            /// 日期型
            /// </summary>
            [Description("日期")]
            Date =2,
            /// <summary>
            /// 数字型
            /// </summary>
            [Description("数字")]
            Number=3,
            /// <summary>
            /// 整数型
            /// </summary>
            [Description("整数")]
            Int=4,
            /// <summary>
            /// 词典型
            /// </summary>
            [Description("词典")]
            Dict=5,
            /// <summary>
            /// 布尔型
            /// </summary>
            [Description("是非")]
            Bool =6

        }

    }
}
