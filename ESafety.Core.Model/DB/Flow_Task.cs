namespace ESafety.Core.Model.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using ESafety.Unity;

    /// <summary>
    /// 审批任务
    /// </summary>
    public class Flow_Task : ModelBase
    {
        public Flow_Task()
        {
            _version =Command.GetTimestamp();
        } 
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid BusinessID { get; set; }
        /// <summary>
        /// 业务单据类型
        /// </summary>
        public int BusinessType { get; set; }
        /// <summary>
        /// 节点ID
        /// </summary>
        public Guid PointID { get; set; }
        /// <summary>
        /// 任务用户
        /// </summary>
        [Required]
        [StringLength(100)]
        public string TaskUser { get; set; }
        /// <summary>
        /// 任务时间
        /// </summary>
        public DateTime TaskDate { get; set; } 
        /// <summary>
        /// 发起人
        /// </summary>
        public string ApplyUser { get; set; }
        private long _version;
        /// <summary>
        /// 审批版本号
        /// </summary>
        public long FlowVersion { get { return _version; } set { _version = value; } }
        /// <summary>
        /// 业务编号
        /// </summary>
        public string BusinessCode { get; set; }
        /// <summary>
        /// 业务日期
        /// </summary>
        public DateTime BusinessDate { get; set; }
        /// <summary>
        /// 流程控制ID
        /// </summary>
        public Guid MasterID { get; set; }
    }
}
