using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    public class TaskSubjectBillNew
    {
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
        /// 任务结果
        /// </summary>
        public int TaskResult { get; set; }
        /// <summary>
        /// 任务巡检描述
        /// </summary>
        public string TaskResultMemo { get; set; }
        /// <summary>
        /// 危害因素
        /// </summary>
        public Guid Eval_WHYS { get; set; }
        /// <summary>
        /// 事故类型
        /// </summary>
        public Guid Eval_SGLX { get; set; }
        /// <summary>
        /// 事故后果
        /// </summary>
        public Guid Eval_SGJG { get; set; }
        /// <summary>
        /// 影响范围
        /// </summary>
        public Guid Eval_YXFW { get; set; }
        /// <summary>
        /// 评测方法
        /// </summary>
        public int Eval_Method { get; set; }
        /// <summary>
        /// 隐患等级
        /// </summary>
        public int TroubleLevel { get; set; }
    }

    public class TaskBillQuery
    {
        /// <summary>
        /// 任务状态
        /// </summary>
        public int TaskState { get; set; }
        /// <summary>
        /// 执行岗位
        /// </summary>
        public Guid PostID { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string Key { get; set; }
    }

    public class TaskBillSubjectsQuery
    {
        /// <summary>
        /// 任务单据ID
        /// </summary>
        public Guid BillID { get; set; }
        /// <summary>
        /// 主体ID
        /// </summary>
        public Guid SubjectID { get; set; }

    }

}
