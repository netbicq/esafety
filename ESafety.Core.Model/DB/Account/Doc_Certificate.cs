namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 资质管管
    /// 需要电子文档
    /// </summary>
    public partial class Doc_Certificate:ModelBase
    { 
        /// <summary>
        /// 质资名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// 质资类型
        /// </summary>
        public Guid TypeID { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime InvalidDate { get; set; }
        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime ApproveDate { get; set; }
        /// <summary>
        /// 颁发机构
        /// </summary>
        [Required]
        [StringLength(200)]
        public string IssueOrg { get; set; }
        /// <summary>
        /// 质资持有人
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Owner { get; set; }
    }
}
