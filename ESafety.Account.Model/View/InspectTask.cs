using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    public class InspectTaskView
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
        public Guid DangerID { get; set; }
        /// <summary>
        /// 风险点名称
        /// </summary>
        public string DangerName { get; set; }
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
        /// 频率值
        /// </summary>
        public int CycleValue { get; set; }
        /// <summary>
        /// 频率日期类型
        /// </summary>
        public int CycleDateType { get; set; }
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
    }
    /// <summary>
    /// 巡查任务模型
    /// </summary>
    public class InspectTaskModelView:InspectTaskView
    {
        /// <summary>
        /// 明细主体
        /// </summary>
        public IEnumerable<InspectTaskSubjectView> Subjects { get; set; }

    }
}
