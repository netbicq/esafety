namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    /// <summary>
    /// 视频
    /// </summary>
    public partial class Basic_Vedio : ModelBase
    { 
        /// <summary>
        /// 视频编号
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        /// <summary>
        /// 视频地点
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Site { get; set; }
        /// <summary>
        /// 视频url
        /// </summary>
        [Required]
        [StringLength(1000)]
        public string Url { get; set; }
    }
}
