namespace ESafety.Core.Model.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 审批结果记录
    /// </summary>
    public partial class Flow_Result
    {

        public Guid ID { get; set; }
        /// <summary>
        /// 审批版本号
        /// </summary>
        public long FlowVersion { get; set; }
        /// <summary>
        /// 审批用户
        /// </summary>
        [Required]
        [StringLength(200)]
        public string FlowUser { get; set; }
        /// <summary>
        /// 审批结果
        /// </summary>
        public int FlowResult { get; set; }
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime FlowDate { get; set; }
        /// <summary>
        /// 审批意见
        /// </summary>
        [StringLength(500)]
        public string FlowMemo { get; set; }
    }
}
