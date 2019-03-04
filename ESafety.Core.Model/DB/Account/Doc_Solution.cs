namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    /// <summary>
    /// 应急预案
    /// 需要电子文档
    /// </summary>
    public partial class Doc_Solution : ModelBaseEx
    {
        /// <summary>
        /// 预案名称
        /// </summary>
        [Required]
        [StringLength(10)]
        public string Name { get; set; }
        /// <summary>
        /// 预案类型
        /// </summary>
        public Guid TypeID { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime IssueDate { get; set; }
        /// <summary>
        /// 风险等级
        /// </summary>
        public Guid DangerLevel { get; set; }
        /// <summary>
        /// 预案内容
        /// </summary>
        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }
         
    }
}
