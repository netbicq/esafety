namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Basic_OpreationFlow : ModelBase
    { 
        /// <summary>
        /// 作业ID
        /// </summary>
        public Guid OpreationID { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string PointName { get; set; }
        /// <summary>
        /// 岗位ID
        /// </summary>
        public Guid PostID { get; set; }
        /// <summary>
        /// 节点顺序
        /// </summary>
        public int PointIndex { get; set; }
        /// <summary>
        /// 节点描述
        /// </summary>
        public string PointMemo { get; set; }
    }
}
