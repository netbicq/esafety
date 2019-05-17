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
        ///隐患管控编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 管控名称
        /// </summary>
        public string ControlName { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? FinishTime { get; set; }
        /// <summary>
        /// 责任人id
        /// </summary>
        public Guid PrincipalID { get; set; }
        /// <summary>
        ///隐患管控执行人
        /// </summary>
        public Guid? ExecutorID { get; set; }
        /// <summary>
        /// 隐患管控验收人
        /// </summary>
        public Guid? AcceptorID { get; set; }
        /// <summary>
        /// 管控描述
        /// </summary>
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
        /// <summary>
        ///任务单据ID
        /// </summary>
        public Guid BillID { get; set; }
        /// <summary>
        /// 风险等级
        /// </summary>
        public Guid DangerLevel { get; set; }
    }
}
