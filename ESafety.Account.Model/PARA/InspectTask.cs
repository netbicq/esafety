using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{

    /// <summary>
    /// 巡检任务新建
    /// </summary>
    public class InspectTaskNew
    {
        public InspectTaskNew()
        {
            TaskSubjects = new List<InspectTaskSubjectNew>();
        }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 风险点ID
        /// </summary>
        public Guid DangerPointID { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public PublicEnum.EE_InspectTaskType TaskType { get; set; }
        /// <summary>
        /// 执行频率值
        /// </summary>
        public int CycleValue { get; set; }
        /// <summary>
        /// 执行频率类型
        /// </summary>
        public PublicEnum.EE_CycleDateType CycleDateType { get; set; }
        /// <summary>
        /// 执行岗位
        /// </summary>
        public Guid ExecutePostID { get; set; }
        /// <summary>
        /// 执行人id
        /// </summary>
        public Guid EmployeeID { get; set; }

        /// <summary>
        /// 巡检任务主体明细
        /// </summary>
        public IEnumerable<InspectTaskSubjectNew> TaskSubjects { get; set; }
        /// <summary>
        /// 任务描述 
        /// </summary>
        public string TaskDescription { get; set; }

    }
    /// <summary>
    /// 修改任务
    /// </summary>
    public class InspectTaskEdit : InspectTaskNew
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid ID { get; set; }
    }
    /// <summary>
    /// 巡检任务主体新建
    /// </summary>
    public class InspectTaskSubjectNew
    {
        /// <summary>
        /// 主体类型
        /// </summary>
        public PublicEnum.EE_SubjectType SubjectType { get; set; }
        /// <summary>
        /// 主体ID
        /// </summary>
        public Guid SubjectID { get; set; }
        /// <summary>
        /// 风控项ID
        /// </summary>
        public Guid DangerID { get; set; }
    }
    /// <summary>
    /// 改变任务状态
    /// </summary>
    public class InspectTaskChangeState
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public PublicEnum.BillFlowState State { get; set; }
    }
    /// <summary>
    /// 临时任务分配执行人员
    /// </summary>
    public class AllotTempTaskEmp
    {
        /// <summary>
        /// 临时任务ID
        /// </summary>
        public Guid TempTaskID { get; set; }
        /// <summary>
        /// 岗位ID
        /// </summary>
        public Guid PostID { get; set; }
        /// <summary>
        /// 执行人
        /// </summary>
        public Guid EmpID { get; set; }
    }

    /// <summary>
    /// 查询参数
    /// </summary>
    public class InspectTaskQuery
    {
        /// <summary>
        /// 状态
        /// </summary>
        public int? State { get; set; }
        /// <summary>
        /// 岗位id
        /// </summary>
        public Guid PostID { get; set; }
        /// <summary>
        /// 风险点
        /// </summary>
        public Guid DangerPointID { get; set; }
        /// <summary>
        /// 关键字，只支持任务名 和描述检索
        /// </summary>
        public string Key { get; set; }
    }

    public class AddTempTask
    {
        public AddTempTask()
        {
            TaskSubjects = new List<InspectTaskSubjectNew>();
        }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 风险点id
        /// </summary>
        public Guid DangerPointID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 执行岗位id
        /// </summary>
        public Guid ExecutePostID { get; set; }
        /// <summary>
        /// 任务描述 
        /// </summary>
        public string TaskDescription { get; set; }

        /// <summary>
        /// 巡检任务主体明细
        /// </summary>
        public IEnumerable<InspectTaskSubjectNew> TaskSubjects { get; set; }

        /// <summary>
        /// 电子文档
        /// </summary>
        public IEnumerable<Core.Model.PARA.AttachFileNew> AttachFiles { get; set; }
    }
    /// <summary>
    /// 临时任务风控项选择器参数
    /// </summary>
    public class TempTaskDangerSelect
    {
        /// <summary>
        /// 风险点ID
        /// </summary>
        public Guid DangerPointID { get; set; }
        /// <summary>
        /// 主体类型
        /// </summary>
        public int SubType { get; set; }
    }
}
