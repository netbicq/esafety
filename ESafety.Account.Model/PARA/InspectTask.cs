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
        public Guid DangerID { get; set; }
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
    }
}
