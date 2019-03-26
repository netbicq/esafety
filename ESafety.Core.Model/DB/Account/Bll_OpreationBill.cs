namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 作业申请单
    /// </summary>
    public partial class Bll_OpreationBill : ModelBaseEx
    { 

        /// <summary>
        /// 单据号
        /// </summary>
        [Required]
        [StringLength(50)]
        public string BillCode { get; set; }
        /// <summary>
        /// 作业id
        /// </summary>
        public Guid OpreationID { get; set; }


        /// <summary>
        /// 单据名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string BillName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 作业负责人
        /// </summary>
        public Guid PrincipalEmployeeID { get; set; }
        /// <summary>
        /// 作业时长
        /// </summary>
        public int BillLong { get; set; }
       
        /// <summary>
        /// 作业描述
        /// </summary>
        [StringLength(4000)]
        public string Description { get; set; }
        /// <summary>
        /// 作业当前定义的流程JSON
        /// </summary>
        public string FlowsJson { get; set; }
        /// <summary>
        /// 流程JSON
        /// </summary>
        public string OpreationJSON { get; set; }
    }
}
