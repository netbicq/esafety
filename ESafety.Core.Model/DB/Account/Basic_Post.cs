namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Basic_Post : ModelBase
    { 
        /// <summary>
        /// 编号
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public Guid Principal { get; set; }
        /// <summary>
        /// 组织架构ID
        /// </summary>
        public Guid Org { get; set; }
        /// <summary>
        /// 主要任务
        /// </summary>
        public string MainTasks { get; set; }
    }
}
