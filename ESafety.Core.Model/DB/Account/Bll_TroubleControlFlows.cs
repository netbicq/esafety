namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 管控验收申请日志
    /// </summary>
    public partial class Bll_TroubleControlFlows:ModelBase
    { 
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime FlowDate { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public int FlowResult { get; set; }
        /// <summary>
        /// 操作人员id
        /// </summary>
        public Guid FlowEmployeeID { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [StringLength(500)]
        public string FlowMemo { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int FlowType { get; set; }
    }
}
