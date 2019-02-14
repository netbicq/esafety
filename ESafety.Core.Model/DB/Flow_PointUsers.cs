namespace ESafety.Core.Model.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 节点用户
    /// </summary>
    public partial class Flow_PointUsers
    {
        public Guid ID { get; set; }
        /// <summary>
        /// 节点ID
        /// </summary>
        public Guid PointID { get; set; }
        /// <summary>
        /// 节点用户
        /// </summary>
        [Required]
        [StringLength(100)]
        public string PointUser { get; set; }
        /// <summary>
        /// 用户顺序
        /// </summary>
        public int UserIndex { get; set; }
    }
}
