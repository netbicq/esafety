namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 管控单
    /// </summary>
    public partial class Bll_TroubleControl : ModelBase
    { 
        /// <summary>
        /// 管控名称
        /// </summary>
        [Required]
        [StringLength(500)]
        public string ControlName { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime FinishTime { get; set; }
        /// <summary>
        /// 责任人id
        /// </summary>
        public Guid PrincipalID { get; set; }
        /// <summary>
        /// 责任人电话
        /// </summary>
        [Required]
        [StringLength(50)]
        public string PrincipalTEL { get; set; }
        /// <summary>
        /// 责任部门id
        /// </summary>
        public Guid OrgID { get; set; }
        /// <summary>
        /// 管控描述
        /// </summary>
        [Required]
        [StringLength(4000)]
        public string ControlDescription { get; set; }
        /// <summary>
        /// 隐患级别
        /// </summary>
        public int TroubleLevel { get; set; }
        /// <summary>
        /// 发现日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
    }
}
