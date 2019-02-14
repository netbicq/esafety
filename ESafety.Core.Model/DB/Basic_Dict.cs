namespace ESafety.Core.Model.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 词典
    /// </summary>
    public partial class Basic_Dict
    {

        public Guid ID { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid ParentID { get; set; }
        /// <summary>
        /// 是否系统
        /// </summary>
        public bool IsSYS { get; set; }
        /// <summary>
        /// 词典名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string DictName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        public string Memo { get; set; }
    }
}
