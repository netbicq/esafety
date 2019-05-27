using ESafety.Core.Model.DB.Account;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    /// <summary>
    /// 来自任务的设备集合
    /// </summary>
    public class TaskBillModel
    {
        /// <summary>
        /// 单据ID
        /// </summary>
        public Guid BillID { get; set; }
        /// <summary>
        /// 任务名
        /// </summary>
        public string TaskName { set; get; }
        /// <summary>
        /// 职员名称
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 预计完成时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 风险点名
        /// </summary>
        public string DangerPointName { get; set; }

        /// <summary>
        /// 检查主体总数
        /// </summary>
        public int SubCount { get; set; }
        /// <summary>
        /// 已检查主体数
        /// </summary>
        public int SubCheckedCount { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public PublicEnum.EE_InspectTaskType TaskType { set; get; }

        /// <summary>
        /// 危害因素
        /// </summary>
        public IEnumerable<Dict> WHYSDicts { get; set; }

    }

    public class TaskSubjectView
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid KeyID { get; set; }
        /// <summary>
        /// 单据ID
        /// </summary>
        public Guid BillID { get; set; }
        /// <summary>
        /// 主体ID
        /// </summary>
        public Guid SubID { get; set; }
        /// <summary>
        /// 主体名
        /// </summary>
        public string SubName { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string Principal { get; set; }
        /// <summary>
        /// 负责人电话
        /// </summary>
        public string PrincipalTel { get; set; }
        /// <summary>
        /// 风险等级
        /// </summary>
        public string DangerLevel { get; set; }
        /// <summary>
        /// 主体类型
        /// </summary>
        public PublicEnum.EE_SubjectType SubType { get; set; }
        /// <summary>
        /// 主体类型名称
        /// </summary>
        public string SubTypeName { get; set; }
        /// <summary>
        /// 风控项
        /// </summary>
        public string DangerName { get; set; }
        /// <summary>
        /// 风控项ID
        /// </summary>
        public Guid DangerID { get; set; }
        /// <summary>
        /// 是否管控中，只有不是管控中的时候，才能调整检查的检查结果
        /// </summary>
        public bool IsControl { get; set; }
    }

    public class TaskSubjectOverView: TaskSubjectView
    {
        public Guid SubResultID { get; set; }
    }


    public class SubResultView
    {
        /// <summary>
        /// 检查时间
        /// </summary>
        public DateTime ResultTime { get; set; }
        /// <summary>
        /// 检查结果
        /// </summary>
        public PublicEnum.EE_TaskResultType TaskResult { get; set; }
        /// <summary>
        /// 检查描述
        /// </summary>
        public string TaskResultMemo { get; set; }
        /// <summary>
        /// 危害因素
        /// </summary>
        public string WHYSDict{ get; set; }
        /// <summary>
        /// 事故类型
        /// </summary>
        public string SGLXDict { get; set; }
        /// <summary>
        /// 事故后果
        /// </summary>
        public string SGJGDict { get; set; }
        /// <summary>
        /// 影响范围
        /// </summary>
        public string YXFWDict { get; set; }
        /// <summary>
        /// 评估方法
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 隐患等级
        /// </summary>
        public string TLevel { get; set; }
        /// <summary>
        /// 风险等级
        /// </summary>
        public string DLevel { get; set; }
        /// <summary>
        /// 管控负责人
        /// </summary>
        public string CtrPrincipal { get; set; }
        /// <summary>
        /// LECD计算方法的L
        /// </summary>
        public string LECD_L { get; set; }
        /// <summary>
        /// LECD计算方法的E
        /// </summary>
        public string LECD_E { get; set; }
        /// <summary>
        /// LECD计算方法的C
        /// </summary>
        public string LECD_C { get; set; }
        /// <summary>
        /// LSD计算方法的L
        /// </summary>
        public string LSD_L { get; set; }
        /// <summary>
        /// LSD计算方法的S
        /// </summary>
        public string LSD_S { get; set; }
    }

    /// <summary>
    /// 数据下载
    /// </summary>
    public class DownloadData
    {
        /// <summary>
        /// 超时任务个数
        /// </summary>
        public int OverTimeTaskCount { get; set; }
        /// <summary>
        /// 事故类型
        /// </summary>
        public IEnumerable<Dict> SGLXDicts { get; set; }
        /// <summary>
        /// 事故后果
        /// </summary>
        public IEnumerable<Dict> SGHGDicts { get; set; }
        /// <summary>
        /// 影响范围
        /// </summary>
        public IEnumerable<Dict> YXFWDicts { get; set; }
        /// <summary>
        /// 风险等级
        /// </summary>
        public IEnumerable<DangerLevelDict> DangerLevels { get; set; }
        /// <summary>
        /// 评测方法
        /// </summary>
        public IEnumerable<EnumItem> EvaluateMethod { get; set; }
        /// <summary>
        /// 隐患等级
        /// </summary>
        public IEnumerable<Dict> TroubleLevels { get; set; }

        /// <summary>
        /// 用于LECD方法计算的选项，L
        /// </summary>
        public IEnumerable<Eval_Dict> LECD_Ls { get; set; }

        /// <summary>
        /// 用于LECD方法计算的选项，E
        /// </summary>
        public IEnumerable<Eval_Dict> LECD_Es { get; set; }
        /// <summary>
        /// 用于LECD方法计算的选项，C
        /// </summary>
        public IEnumerable<Eval_Dict> LECD_Cs { get; set; }

        /// <summary>
        /// 用于LSD方法计算的选项，L
        /// </summary>
        public IEnumerable<Eval_Dict> LSD_Ls { get; set; }
        /// <summary>
        /// 用于LSD方法计算的选项，S
        /// </summary>
        public IEnumerable<Eval_Dict> LSD_Ss { get; set; }

        /// <summary>
        /// 任务单据
        /// </summary>
        public IEnumerable<BillData> BillDatas { get; set; }
        /// <summary>
        /// 人员表
        /// </summary>
        public IEnumerable<Emp> Emps { get; set; }
        /// <summary>
        /// 部门表
        /// </summary>
        public IEnumerable<Org> Orgs { get; set; }
    }
    /// <summary>
    /// 人员
    /// </summary>
    public class Emp
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid KeyID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string CNName { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public Guid OrgID { get; set; }
    }
    /// <summary>
    /// 部门
    /// </summary>
    public class Org
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid KeyID { get; set; }
        /// <summary>
        /// 部门名
        /// </summary>
        public string OrgName { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>
        public Guid ParentID { get; set; }
    }

    /// <summary>
    /// 任务单据
    /// </summary>
    public class BillData : TaskBillModel
    {
        /// <summary>
        /// 待检测主体集合
        /// </summary>
        public IEnumerable<TaskSubjectView> CheckSubs{ get; set; }
        /// <summary>
        /// 危害因素
        /// </summary>
        public IEnumerable<Dict> WHYSDicts { get; set; }

    }
    /// <summary>
    /// 词典
    /// </summary>
    public class Dict
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid KeyID { get; set; }
        /// <summary>
        /// 词典名称
        /// </summary>
        public string DictName { get; set; }
        /// <summary>
        ///描述
        /// </summary>
        public string Memo { get; set; }
    }
    /// <summary>
    /// 风险等级词典
    /// </summary>
    public class DangerLevelDict
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid KeyID { get; set; }
        /// <summary>
        /// 词典名称
        /// </summary>
        public string DictName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// LECD法 最大值
        /// </summary>
        public int LECD_DMaxValue { get; set; }
        /// <summary>
        /// LECD法 最小值
        /// </summary>
        public int LECD_DMinValue { get; set; }
        /// <summary>
        /// LECD法 最大值
        /// </summary>
        public int LSD_DMaxValue { get; set; }
        /// <summary>
        /// LECD法 最小值
        /// </summary>
        public int LSD_DMinValue { get; set; }
    }
    /// <summary>
    /// 用于风险等级计算的
    /// </summary>
    public class  Eval_Dict
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid KeyID { get; set; }
        /// <summary>
        /// 词典名称
        /// </summary>
        public string DictName { get; set; }

        /// <summary>
        /// 参与计算的值
        /// </summary>
        public double Value { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
    }

    public class OpreateBillByEmp
    {
        /// <summary>
        /// 作业ID
        /// </summary>
        public Guid OpreateBillID { get; set; }
        /// <summary>
        /// 作业名称
        /// </summary>
        public string OpreateBillName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string Principal { get; set; }
        /// <summary>
        /// 作业时长
        /// </summary>
        public int BillLong { get; set; }
        /// <summary>
        /// 作业描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 当前节点位置
        /// </summary>
        public int CurrentIndex { get; set; }
        /// <summary>
        /// 总小标数
        /// </summary>
        public int AllCount { get; set; }
    }
}
