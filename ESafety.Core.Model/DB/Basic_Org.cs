namespace ESafety.Core.Model.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 组织架构
    /// </summary>
    public partial class Basic_Org
    {
        public Guid ID { get; set; }

        public Guid ParentID { get; set; }
        /// <summary>
        /// 级次
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        [Required]
        [StringLength(50)]
        public string OrgCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string OrgName { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        [StringLength(50)]
        public string Principal { get; set; }
        /// <summary>
        /// 负责人电话
        /// </summary>
        [StringLength(50)]
        public string PrincipalTel { get; set; }
    }
}
