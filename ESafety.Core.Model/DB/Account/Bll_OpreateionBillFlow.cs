namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 作业单节点日志
    /// </summary>
    public partial class Bll_OpreateionBillFlow : ModelBase
    { 
        /// <summary>
        /// 作业单id
        /// </summary>
        public Guid BillID { get; set; }
        /// <summary>
        /// 作业节点id
        /// </summary>
        public Guid OpreationFlowID { get; set; }
        /// <summary>
        /// 节点结果
        /// </summary>
        public int FlowResult { get; set; }
        /// <summary>
        /// 节点执行人
        /// </summary>
        public Guid FlowEmployeeID { get; set; }
        /// <summary>
        /// 节点执行时间
        /// </summary>
        public DateTime FlowTime { get; set; }
        /// <summary>
        /// 节点执行备注
        /// </summary>
        [StringLength(500)]
        public string FlowMemo { get; set; }
    }
}
