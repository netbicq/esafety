namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 制度
    /// 需要电子文档
    /// </summary>
    public partial class Doc_Institution : ModelBaseEx
    { 

        /// <summary>
        /// 制度名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        /// <summary>
        /// 制度类型
        /// </summary>
        public Guid TypeID { get; set; }
        /// <summary>
        /// 字号
        /// </summary>
        [Required]
        [StringLength(100)]
        public string BigCode { get; set; }
        /// <summary>
        /// 发布日期 
        /// </summary>
        public DateTime IssueDate { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }
         
    }
}
