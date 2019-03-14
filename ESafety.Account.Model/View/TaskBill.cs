using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    public class TaskBillView
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 单据号
        /// </summary>
        public string BillCode { get; set; }
        /// <summary>
        /// 任务id
        /// </summary>
        public Guid TaskID { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 风险点
        /// </summary>
        public Guid DangerID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 执行岗位
        /// </summary>
        public Guid PostID { get; set; }
        /// <summary>
        /// 执行岗位
        /// </summary>
        public string PostName { get; set; }
        /// <summary>
        /// 执行人
        /// </summary>
        public Guid EmployeeID { get; set; }
        /// <summary>
        /// 执行人名
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// 单据状态
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 任务结果
        /// </summary>
        public string TaskResult { get; set; }
    }

    public class TaskBillModelView: TaskSubjectBillView
    {
        /// <summary>
        /// 主体名
        /// </summary>
        public string SubName { get; set; }
        /// <summary>
        /// 能否处理
        /// </summary>
        public bool CanHandle { get; set; }
    }

    public class TaskSubjectBillView
    {
        /// <summary>
        /// 详情ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 单据id
        /// </summary>
        public Guid BillID { get; set; }
        /// <summary>
        /// 主体类型
        /// </summary>
        public int SubjectType { get; set; }
        /// <summary>
        /// 主体id
        /// </summary>
        public Guid SubjectID { get; set; }
        /// <summary>
        /// 风险点id
        /// </summary>
        public Guid DangerID { get; set; }
        /// <summary>
        ///危险源名称 
        /// </summary>
        public string DangerName { get; set; }
        /// <summary>
        /// 任务结果
        /// </summary>
        public int TaskResult { get; set; }
        /// <summary>
        /// 任务结果名
        /// </summary>
        public string TaskResultName { get; set; }
        /// <summary>
        /// 任务巡检描述
        /// </summary>
        public string TaskResultMemo { get; set; }
        /// <summary>
        /// 危害因素
        /// </summary>
        public Guid Eval_WHYS { get; set; }
        /// <summary>
        /// 危害因素名
        /// </summary>
        public string WHYSDic { get; set; }
        /// <summary>
        /// 事故类型
        /// </summary>
        public Guid Eval_SGLX { get; set; }
        /// <summary>
        /// 事故类型名
        /// </summary>
        public string SGLXDic { get; set; }
        /// <summary>
        /// 事故后果
        /// </summary>
        public Guid Eval_SGJG { get; set; }
        /// <summary>
        /// 事故后果名
        /// </summary>
        public string SGJGDic { get; set; }
        /// <summary>
        /// 影响范围
        /// </summary>
        public Guid Eval_YXFW { get; set; }
        /// <summary>
        /// 影响范围名
        /// </summary>
        public string YXFWDic { get; set; }
        /// <summary>
        /// 评测方法
        /// </summary>
        public int Eval_Method { get; set; }
        /// <summary>
        /// 评测方法名
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 隐患等级
        /// </summary>
        public int TroubleLevel { get; set; }
        /// <summary>
        /// 隐患等级名
        /// </summary>
        public string TroubleLevelName { get; set; }
        /// <summary>
        /// 是否隐患管控
        /// </summary>
        public bool IsControl { get; set; }
    }
}
