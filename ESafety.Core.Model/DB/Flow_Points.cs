namespace ESafety.Core.Model.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 审批节点
    /// </summary>
    public partial class Flow_Points
    {
        public Guid ID { get; set; }
        /// <summary>
        /// 业务单据类型
        /// </summary>
        public int BusinessType { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string PointName { get; set; }
        /// <summary>
        /// 节点类型
        /// </summary>
        public int PointType { get; set; }
        /// <summary>
        /// 节点顺序
        /// </summary>
        public int PointIndex { get; set; }
    }
}
