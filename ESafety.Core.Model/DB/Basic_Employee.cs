namespace ESafety.Core.Model.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 人员
    /// </summary>
    public class Basic_Employee : ModelBase
    { 
        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        [StringLength(50)]
        public string CNName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Gender { get; set; }
        /// <summary>
        /// Leader
        /// </summary>
        public bool IsLeader { get; set; }
        /// <summary>
        /// 接受平级
        /// </summary>
        public bool IsLevel { get; set; }
        /// <summary>
        /// 操作用户
        /// </summary>
        [StringLength(50)]
        public string Login { get; set; }
        /// <summary>
        /// 头像IMG
        /// </summary>
        [StringLength(1000)]
        public string HeadIMG { get; set; }
        /// <summary>
        /// 组织ID
        /// </summary>
        public Guid OrgID { get; set; }
    }
}
