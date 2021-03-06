namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Basic_SafetyStandard : ModelBase
    { 
        /// <summary>
        /// 编号
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        /// <summary>
        /// 安全标准名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// 风险点类别ID
        /// </summary>
        public Guid DangerSortID { get; set; }
        /// <summary>
        /// 管控措施
        /// </summary>
        public string Controls { get; set; }
        /// <summary>
        /// 工程措施
        /// </summary>
        public string Engineering { get; set; }
        /// <summary>
        /// 事故措施
        /// </summary>
        public string Accident { get; set; }
        /// <summary>
        /// 个体措施
        /// </summary>
        public string Individual { get; set; }
    }
}
