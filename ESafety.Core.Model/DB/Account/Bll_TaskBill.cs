namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 任务单据
    /// </summary>
    public partial class Bll_TaskBill : ModelBase
    { 
        /// <summary>
        /// 单据号
        /// </summary>
        [Required]
        [StringLength(50)]
        public string BillCode { get; set; }
        /// <summary>
        /// 任务id
        /// </summary>
        public Guid TaskID { get; set; }
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
        /// 执行人
        /// </summary>
        public Guid EmployeeID { get; set; }
        /// <summary>
        /// 单据状态
        /// </summary>
        public int State { get; set; }

    }
}
