namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    /// <summary>
    /// 任务单据主体
    /// </summary>
    public partial class Bll_TaskBillSubjects : ModelBase
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
        /// 风险控项
        /// </summary>
        public Guid DangerID { get; set; }
        /// <summary>
        /// 任务提交时间
        /// </summary>
        public DateTime TaskTime { get; set; }
        /// <summary>
        /// 任务结果
        /// </summary>
        public int TaskResult { get; set; }
        /// <summary>
        /// 任务巡检描述
        /// </summary>
        [StringLength(2000)]
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
        /// <summary>
        /// 是否管控
        /// </summary>
        public bool IsControl { get; set; }
    }
}
