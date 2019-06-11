using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{

    public class InspectTempTaskView
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 任务编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 任务名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 风险点id
        /// </summary>
        public Guid DangerPointID { get; set; }
        /// <summary>
        /// 风险点名称
        /// </summary>
        public string DangerPointName { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public PublicEnum.EE_InspectTaskType TaskType { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 执行岗位
        /// </summary>
        public Guid ExecutePostID { get; set; }
        /// <summary>
        /// 岗位名
        /// </summary>
        public string ExecutePostName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string StateName { get; set; }
        /// <summary>
        /// 仓建人
        /// </summary>
        public string CreateMan { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 队员id
        /// </summary>
        public Guid EmployeeID { get; set; }
        /// <summary>
        /// 职员姓名
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// 任务描述
        /// </summary>
        public string TaskDescription { get; set; }
        /// <summary>
        /// MasterID
        /// </summary>
        public Guid MasterID { get; set; }
    }
    public class InspectTaskView : InspectTempTaskView
    {
        /// <summary>
        /// 频率值
        /// </summary>
        public int CycleValue { get; set; }
        /// <summary>
        /// 频率日期类型
        /// </summary>
        public int CycleDateType { get; set; }
    }
    /// <summary>
    /// 任务主体明细
    /// </summary>
    public class InspectTaskSubjectView
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 任务id
        /// </summary>
        public Guid InspectTaskID { get; set; }
        /// <summary>
        /// 主体类型
        /// </summary>
        public int SubjectType { get; set; }
        /// <summary>
        /// 主体类型名
        /// </summary>
        public string SubjectTypeName { get; set; }
        /// <summary>
        /// 主体id
        /// </summary>
        public Guid SubjectID { get; set; }
        /// <summary>
        /// 主体名
        /// </summary>
        public string SubjectName { get; set; }
        /// <summary>
        /// 风控项ID
        /// </summary>
        public Guid DangerID { get; set; }
        /// <summary>
        /// 风控项
        /// </summary>
        public string DangerName { get; set; }
        /// <summary>
        /// 风险等级
        /// </summary>
        public string DangerLevel { get; set; }
    }
    /// <summary>
    /// 巡查任务模型
    /// </summary>
    public class InspectTaskModelView : InspectTaskView
    {
        /// <summary>
        /// 明细主体
        /// </summary>
        public IEnumerable<InspectTaskSubjectView> Subjects { get; set; }

    }
    /// <summary>
    /// 任务列表
    /// </summary>
    public class InsepctTaskByEmployee
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        public Guid TaskID { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 风险点ID
        /// </summary>
        public Guid DangerPointID { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public PublicEnum.EE_InspectTaskType TaskTypeID { get; set; }
        /// <summary>
        /// 风险点名称
        /// </summary>
        public string DangerName { get; set; }
        /// <summary>
        /// 任务描述
        /// </summary>
        public string TaskDescription { get; set; }
        /// <summary>
        /// 任务类型名称
        /// </summary>
        public string TaskTypeName { get; set; }
        /// <summary>
        /// 最后执行时间
        /// </summary>
        public string LastTime { get; set; }
        /// <summary>
        /// 超时小时数
        /// </summary>
        public int TimeOutHours { get; set; }
        /// <summary>
        /// 频率值
        /// </summary>
        public int CycleValue { get; set; }
        /// <summary>
        /// 频率日期类型
        /// </summary>
        public int CycleDateType { get; set; }
        /// <summary>
        /// 执行频率
        /// </summary>
        public string CycleName { get; set; }
        /// <summary>
        /// 是否做过，false没有，true有
        /// </summary>
        public bool HasDone { get; set; }
    }
    /// <summary>
    /// 临时任务模型
    /// </summary>
    public class InsepctTempTaskByEmployee
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        public Guid TaskID { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 风险点ID
        /// </summary>
        public Guid DangerID { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public PublicEnum.EE_InspectTaskType TaskTypeID { get; set; }
        /// <summary>
        /// 风险点名称
        /// </summary>
        public string DangerName { get; set; }
        /// <summary>
        /// 任务描述
        /// </summary>
        public string TaskDescription { get; set; }
        /// <summary>
        /// 任务类型名称
        /// </summary>
        public string TaskTypeName { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndTime { get; set; }
    }

    /// <summary>
    /// 临时任务选择器参数
    /// </summary>
    public class TempTaskSelector
    {
        /// <summary>
        /// 风险点集合
        /// </summary>
        public IEnumerable<DangerPoint> DangerPoints { get; set; }
        /// <summary>
        /// 岗位集合
        /// </summary>
        public IEnumerable<Post> Posts { get; set; }
        /// <summary>
        /// 主体明细集合
        /// </summary>
        public IEnumerable<SubType> SubTypes { get; set; }
    }


    /// <summary>
    /// 风险点集合
    /// </summary>
    public class DangerPoint
    {
        /// <summary>
        /// 风险点ID
        /// </summary>
        public Guid DangerPointID { get; set; }
        /// <summary>
        /// 风险点名字
        /// </summary>
        public string DangerPointName { get; set; }
    }
    /// <summary>
    /// 岗位
    /// </summary>
    public class Post
    {
        /// <summary>
        /// 岗位ID
        /// </summary>
        public Guid PostID { get; set; }
        /// <summary>
        /// 岗位名
        /// </summary>
        public string PostName { get; set; }
    }
    /// <summary>
    /// 主体类型
    /// </summary>
    public class SubType
    {
        /// <summary>
        /// 主体类型
        /// </summary>
        public string SubTypeName { get; set; }
        /// <summary>
        /// 主体类型
        /// </summary>
        public PublicEnum.EE_SubjectType SubjectType { get; set; }
    }
    /// <summary>
    /// 主体集合
    /// </summary>
    public class CSub
    {
        /// <summary>
        /// ID
        /// </summary>
        public int SubTypeID { get; set; }
        /// <summary>
        /// 主体类型
        /// </summary>
        public string SubType { get; set; }
        /// <summary>
        /// 主体集合
        /// </summary>
        public IEnumerable<Sub> Subs { get; set; }
    }

    /// <summary>
    /// 主体
    /// </summary>
    public class Sub
    {
        /// <summary>
        /// 主体名称
        /// </summary>
        public string SubName { get; set; }
        /// <summary>
        /// 主体ID
        /// </summary>
        public Guid SubID { get; set; }
        /// <summary>
        /// 风控项集合
        /// </summary>
        public IEnumerable<Danger> Dangers{ get; set; }
    }

    /// <summary>
    /// 风控项
    /// </summary>
    public class Danger
    {

        /// <summary>
        ///风控项ID
        /// </summary>
        public Guid DangerID { get; set; }
        /// <summary>
        /// 风控项名
        /// </summary>
        public string DangerName { set; get; }
    }
    /// <summary>
    ///超期任务
    /// </summary>
    public class TimeOutTask
    {
        /// <summary>
        /// 任务名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 人或者岗位
        /// </summary>
        public string EmpOrPost { get; set; }
        /// <summary>
        /// 超期任务数
        /// </summary>
        public string OverHours { get; set; }
        /// <summary>
        /// 风险等级
        /// </summary>
        public string DangerLevel { get; set; }
        /// <summary>
        /// 风险等级
        /// </summary>
        public string DangerPoint { get; set; }
    }

}
