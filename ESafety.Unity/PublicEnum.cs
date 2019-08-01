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
            Pass = 1,
            /// <summary>
            /// 拒绝
            /// </summary>
            [Description("拒绝")]
            Deny = 2,
            /// <summary>
            /// 撤回
            /// </summary>
            [Description("撤回")]
            BackR = 3,
            /// <summary>
            /// 审批完成
            /// </summary>
            [Description("结束")]
            Over = 4
        }
       
        /// <summary>
        /// 作业申请单节点流转状态
        /// </summary>
        public enum OpreateFlowResult
        {
            /// <summary>
            /// 完成
            /// </summary>
            [Description("完成")]
            over=1,
            /// <summary>
            /// 作业终止
            /// </summary>
            [Description("作业终止")]
            stop=2,
            /// <summary>
            /// 作业回退
            /// </summary>
            [Description("作业回退")]
            reback=3
        }
        /// <summary>
        /// 审批业务单据状态
        /// </summary>
        public enum BillFlowState
        {

            /// <summary>
            /// 待检查完成
            /// </summary>
            [Description("待检查完成")]
            wait=0,
            /// <summary>
            /// 待审批
            /// </summary>
            [Description("正常")]
            normal = 1,
            /// <summary>
            /// 审批拒绝
            /// </summary>
            [Description("审批拒绝")]
            deny = 2,
            /// <summary>
            /// 审批撤消
            /// </summary>
            [Description("撤消审批")]
            recalled = 3,
            /// <summary>
            /// 审批中
            /// </summary>
            [Description("审批中")]
            pending = 4,
            /// <summary>
            /// 审批通过
            /// </summary>
            [Description("审批通过")]
            approved = 5,
            /// <summary>
            /// 已审核
            /// </summary>
            [Description("已审核")]
            audited = 6,
            /// <summary>
            /// 作废
            /// </summary>
            [Description("已作废")]
            cancel = 7,
            /// <summary>
            /// 申请中
            /// </summary>
            [Description("申请中")]
            appling = 8,
            /// <summary>
            /// 已验收
            /// </summary>
            [Description("已验收")]
            check = 9,
            /// <summary>
            /// 已终止
            /// </summary>
            [Description("已终止")]
            stop =10,
            /// <summary>
            /// 已完成
            /// </summary>
            [Description("已完成")]
            Over =11,
            /// <summary>
            /// 已退回
            /// </summary>
            [Description("已退回")]
            Reback=12

        }
        /// <summary>
        /// 审批流程审批返回结果
        /// </summary>
        public enum EE_FlowApproveResult
        {
            /// <summary>
            /// 审批正常
            /// </summary>
            [Description("审批正常")]
            normal=1,
            /// <summary>
            /// 审批异常
            /// </summary>
            [Description("审批异常")]
            abnormal=2,
            /// <summary>
            /// 审批结束
            /// </summary>
            [Description("审批结束")]
            over=3,
            /// <summary>
            /// 审批拒绝
            /// </summary>
            [Description("审批拒绝")]
            deny=4
        }
       
        /// <summary>
        /// 巡检任务
        /// </summary>
        public enum EE_InspectTaskType
        {
            /// <summary>
            /// 周期任务
            /// </summary>
            [Description("周期任务")]
            Cycle = 1,
            /// <summary>
            /// 临时任务
            /// </summary>
            [Description("临时任务")]
            Temp = 2
        }
        /// <summary>
        /// 巡检结果类型
        /// </summary>
        public enum EE_TaskResultType
        {
            /// <summary>
            /// 正常
            /// </summary>
            [Description("正常")]
            normal = 1,
            /// <summary>
            /// 异常
            /// </summary>
            [Description("异常")]
            abnormal = 2,
            /// <summary>
            /// 管控中
            /// </summary>
            [Description("处理中")]
            pending = 3,
            /// <summary>
            /// 处理后正常
            /// </summary>
            [Description("处理后正常")]
            donormal = 4
        }
        /// <summary>
        /// 管控状态
        /// </summary>
        public enum EE_TroubleState
        {
            /// <summary>
            /// 管控中
            /// </summary>
            [Description("管控中")]
            pending = 1,
            /// <summary>
            /// 申请验收
            /// </summary>
            [Description("验收中")]
            applying = 2,
            /// <summary>
            /// 已验收
            /// </summary>
            [Description("已验收")]
            over = 3,
            /// <summary>
            /// 已归档
            /// </summary>
            [Description("已归档")]
            history = 4
        }
        /// <summary>
        ///管控流程状态
        /// </summary>
        public enum EE_TroubleFlowState
        {
            /// <summary>
            /// 管控验收申请
            /// </summary>
            [Description("申请")]
            TroubleApply = 1,
            /// <summary>
            /// 管控验收
            /// </summary>
            [Description("验收")]
            TroubleR = 2
        }
        /// <summary>
        /// 评测方法
        /// </summary>
        public enum EE_EvaluateMethod
        {
            /// <summary>
            /// 手动评测
            /// </summary>
            [Description("手动评测")]
            Hand = 1,
            /// <summary>
            /// LECD
            /// </summary>
            [Description("LECD法")]
            LECD = 2,
            /// <summary>
            /// LSD法
            /// </summary>
            [Description("LSD法")]
            LSD=3
        }

        /// <summary>
        /// 执行频率类型
        /// </summary>
        public enum EE_CycleDateType
        {
            /// <summary>
            /// 年
            /// </summary>
            [Description("年")]
            Year = 1,
            /// <summary>
            /// 月
            /// </summary>
            [Description("月")]
            Month = 2,
            /// <summary>
            /// 周
            /// </summary>
            [Description("周")]
            Week = 3,
            /// <summary>
            /// 日
            /// </summary>
            [Description("日")]
            Day = 4,
            /// <summary>
            /// 小时
            /// </summary>
            [Description("时")]
            Houre = 5,
            /// <summary>
            /// 分钟
            /// </summary>
            [Description("分")]
            Minute = 6

        }

        /// <summary>
        ///隐患等级
        /// </summary>
        public enum EE_TroubleLevel
        {
            /// <summary>
            /// 一般风险
            /// </summary>
            [Description("一般风险")]
            OneLevel=1,
            /// <summary>
            /// 较大风险
            /// </summary>
            [Description("较大风险")]
            TwoLevel = 2,
            /// <summary>
            ///严重风险
            /// </summary>
            [Description("严重风险")]
            ThreeLevel = 3,
            /// <summary>
            /// 重大风险
            /// </summary>
            [Description("重大风险")]
            FourLevel = 4,

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
            Generic = 1,
            /// <summary>
            /// 多人会审
            /// </summary>
            [Description("会审")]
            Multi = 2
        }
        /// <summary>
        /// 审批业务单类型
        /// </summary>
        public enum EE_BusinessType
        {
            /// <summary>
            /// 作业申请
            /// </summary>
            [Description("作业申请")]
            Apply = 1,
            /// <summary>
            /// 巡检任务
            /// </summary>
            [Description("巡检任务")]
            InspectTask = 2,
            /// <summary>
            /// 任务单据
            /// </summary>
            [Description("任务单据")]
            TaskBill = 3,
            /// <summary>
            /// 隐患管控
            /// </summary>
            [Description("隐患管控")]
            TroubleControl = 4,
            /// <summary>
            /// 临时任务
            /// </summary>
            [Description("临时任务")]
            TempTask = 5,

        }
        /// <summary>
        /// 检查主体类型
        /// </summary>
        public enum EE_SubjectType
        {
            /// <summary>
            /// 设备设施
            /// </summary>
            [Description("设备设施")]
            Device = 1,
            /// <summary>
            /// 岗位
            /// </summary>
            [Description("岗位")]
            Post = 2,
            /// <summary>
            /// 作业
            /// </summary>
            [Description("作业")]
            Opreate = 3,
 


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
            Employee = 3,
            /// <summary>
            /// 作业流程
            /// </summary>
            [Description("作业流程")]
            OpreateFlow=4,
            /// <summary>
            /// 风险公示
            /// </summary>      
            [Description("风险公示")]
            Risk = 5,
            /// <summary>
            /// 会议
            /// </summary>
            [Description("安全会议")]
            Meeting =6,
            /// <summary>
            /// 健康档案
            /// </summary>
            [Description("健康档案")]
            HealDocment = 7,
            /// <summary>
            /// 体检管理
            /// </summary>
            [Description("体检管理")]
            HealRecord = 8,

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
        /// 任务状态
        /// </summary>
        public enum TaskState
        {
            /// <summary>
            /// 正常状态
            /// </summary>
            [Description("正常")]
            normal =1,
            /// <summary>
            /// 停止状态
            /// </summary>
            [Description("停止")]
            cancel=2
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
