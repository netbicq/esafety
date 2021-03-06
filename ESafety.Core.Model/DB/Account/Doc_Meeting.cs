namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    /// <summary>
    /// 会议
    /// 需要电子附件
    /// </summary>
    public partial class Doc_Meeting : ModelBase
    { 
        /// <summary>
        /// 会议主题
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Motif { get; set; }
        /// <summary>
        /// 参会人员
        /// </summary>
        [Required]
        [StringLength(4000)]
        public string EmployeeS { get; set; }
        /// <summary>
        /// 会议日期
        /// </summary>
        public DateTime MeetingDate { get; set; }
        /// <summary>
        /// 主持人
        /// </summary>
        [Required]
        [StringLength(50)]
        public string MeetingMaster { get; set; }
        /// <summary>
        /// 会义地点
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Site { get; set; }
        /// <summary>
        /// 会议内容
        /// </summary>
        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }
    }
}
