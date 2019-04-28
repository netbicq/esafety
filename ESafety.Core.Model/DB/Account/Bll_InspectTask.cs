namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 巡检任务
    /// </summary>
    public partial class Bll_InspectTask : ModelBaseEx
    {

        /// <summary>
        /// 任务编号
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Code { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        /// <summary>
        /// 风险点id
        /// </summary>
        public Guid DangerPointID { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public int TaskType { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 执行频率时间类型 
        /// </summary>
        public int CycleDateType { get; set; }
        /// <summary>
        /// 执行频率值
        /// </summary>
        public int CycleValue { get; set; }
        /// <summary>
        /// 执行岗位id
        /// </summary>
        public Guid ExecutePostID { get; set; }
        
        /// <summary>
        /// 执行人 id
        /// </summary>
        public Guid? EmployeeID { get; set; }
        /// <summary>
        /// 任务描述 
        /// </summary>
        [StringLength(4000)]
        public string TaskDescription { get; set; } 
    }
}
