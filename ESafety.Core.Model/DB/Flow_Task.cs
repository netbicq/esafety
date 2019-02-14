namespace ESafety.Core.Model.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 审批任务
    /// </summary>
    public partial class Flow_Task
    {
        public Guid ID { get; set; }
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid BusinessID { get; set; }
        /// <summary>
        /// 业务单据类型
        /// </summary>
        public int BusinessType { get; set; }
        /// <summary>
        /// 任务用户
        /// </summary>
        [Required]
        [StringLength(100)]
        public string TaskUser { get; set; }
        /// <summary>
        /// 任务时间
        /// </summary>
        public DateTime TaskDate { get; set; }
        /// <summary>
        /// 审批版本号
        /// </summary>
        public long FlowVersion { get; set; }
    }
}
