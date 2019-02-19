using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Unity
{

    /// <summary>
    /// 枚举项
    /// </summary>
    public class EnumItem
    {
        /// <summary>
        /// 值
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Caption { get; set; }
    }


    /// <summary>
    /// 公共枚举
    /// </summary>
    public class PublicEnum
    {

        #region "企业端enum"
        /// <summary>
        /// 审批结果
        /// </summary>
        public enum EE_FlowResult
        {
            /// <summary>
            /// 通过
            /// </summary>
            [Description("通过")]
            Pass=1,
            /// <summary>
            /// 拒绝
            /// </summary>
            [Description("拒绝")]
            Deny=2,
            /// <summary>
            /// 撤回
            /// </summary>
            [Description("撤回")]
            BackR=3,
            /// <summary>
            /// 审批完成
            /// </summary>
            [Description("结束")]
            Over =4
        }

        /// <summary>
        /// 审批节点类型
        /// </summary>
        public enum EE_FlowPointType
        {
            /// <summary>
            /// 普通
            /// </summary>
            [Description("普通")]
            Generic=1,
            /// <summary>
            /// 多人会审
            /// </summary>
            [Description("会审")]
            Multi=2
        }
        /// <summary>
        /// 业务单类型
        /// </summary>
        public enum EE_BusinessType
        {
            /// <summary>
            /// 作业申请
            /// </summary>
            [Description("作业申请")]
            Apply=1
        }
        /// <summary>
        /// 自定义类型
        /// </summary>
        public enum EE_UserDefinedType
        {
            /// <summary>
            /// 设备
            /// </summary>
            [Description("设备")]
            Device = 1,
            /// <summary>
            /// 岗位
            /// </summary>
            [Description("岗位")]
            Position = 2,
            /// <summary>
            /// 职员
            /// </summary>
            [Description("职员")]
            Employee=3

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
            Str = 1,
            /// <summary>
            /// 日期型
            /// </summary>
            [Description("日期")]
            Date = 2,
            /// <summary>
            /// 数字型
            /// </summary>
            [Description("数字")]
            Number = 3,
            /// <summary>
            /// 整数型
            /// </summary>
            [Description("整数")]
            Int = 4,
            /// <summary>
            /// 词典型
            /// </summary>
            [Description("词典")]
            Dict = 5,
            /// <summary>
            /// 布尔型
            /// </summary>
            [Description("是非")]
            Bool = 6

        }

        #endregion




        /// <summary>
        /// 报表参数
        /// </summary>
        public enum ReportParameterType
        {
            /// <summary>
            /// 字符
            /// </summary>
            [Description("字符")]
            Str = 1,
            /// <summary>
            /// 整数
            /// </summary>
            [Description("整数")]
            Int = 2,
            /// <summary>
            /// 数字
            /// </summary>
            [Description("数字")]
            Num = 3,
            /// <summary>
            /// 日期
            /// </summary>
            [Description("日期")]
            Date = 4,
            /// <summary>
            /// 布尔
            /// </summary>
            [Description("布尔")]
            Bool = 5,
            /// <summary>
            /// 下拉框
            /// </summary>
            [Description("选择框")]
            Combox = 6
        }
        /// <summary>
        /// 报表列数据类型
        /// </summary>
        public enum ReportColumnDataType
        {
            /// <summary>
            /// 字符
            /// </summary>
            [Description("字符")]
            Str = 1,
            /// <summary>
            /// 速数
            /// </summary>
            [Description("整数")]
            Int = 2,
            /// <summary>
            /// 数字，如果是数字保留2位小数
            /// </summary>
            [Description("数字")]
            Num = 3,
            /// <summary>
            /// 日期，注意日期格式处理
            /// </summary>
            [Description("日期")]
            Date = 4,
            /// <summary>
            ///布尔类型，请用 Checkbox
            /// </summary>
            [Description("布尔")]
            Bool = 5,
            /// <summary>
            /// GUID类型
            /// </summary>
            [Description("GUID")]
            GUID = 6
        }

        /// <summary>
        /// 账套作用域类型
        /// </summary>
        public enum ReortScopeType
        {
            /// <summary>
            /// 所有账套适用
            /// </summary>
           [Description("全局")]
            Global = 1,
            /// <summary>
            /// 范围内账套适用
            /// </summary>
            [Description("账套")]
            Range = 2
        }
        /// <summary>
        /// 账套态态
        /// </summary>
        public enum AccountState
        {
            /// <summary>
            /// 正常
            /// </summary>
            [Description("正常")]
            Normal = 1,
            /// <summary>
            /// 关闭
            /// </summary>
            [Description("关闭")]
            Closed = 2
        }
        /// <summary>
        /// 通用的状态
        /// </summary>
        public enum GenericState
        {
            /// <summary>
            /// 正常
            /// </summary>
            [Description("正常")]
            Normal = 1,
            /// <summary>
            /// 取消，作废
            /// </summary>
            [Description("作废")]
            Cancel = 2,
            /// <summary>
            /// 已审核
            /// </summary>
            [Description("已审核")]
            Applyed = 3
        }
        /// <summary>
        /// 选项类型
        /// </summary>
        public enum AccountOptionItemType
        {
            /// <summary>
            /// 文本
            /// </summary>
            [Description("文本")]
            String = 1,
            /// <summary>
            /// 数字
            /// </summary>
            [Description("数字")]
            Number = 2,
            /// <summary>
            /// 日期
            /// </summary>
            [Description("日期")]
            Date = 3,
            /// <summary>
            /// 布尔
            /// </summary>
            [Description("布尔")]
            Bool = 4,
            /// <summary>
            /// 枚举
            /// </summary>
            [Description("枚举")]
            Enum = 5,
            /// <summary>
            /// 集合
            /// </summary>
            [Description("集合")]
            List = 6,
            /// <summary>
            /// 多值，最多支持3个值
            /// </summary>
            [Description("多值")]
            MultiValue = 7,
            /// <summary>
            /// 整数
            /// </summary>
            [Description("整数")]
            Int = 8
        }

      

    }
}
