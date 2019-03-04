namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 体检记录
    /// 需要电子文档
    /// </summary>
    public partial class Heal_Records:ModelBase
    {

        /// <summary>
        /// 健康档案ID
        /// </summary>
        public Guid DocmentID { get; set; }
        /// <summary>
        /// 体健日期
        /// </summary>
        public DateTime RecDate { get; set; }
        /// <summary>
        /// 体检结果
        /// </summary>
        [Required]
        [StringLength(200)]
        public string RecResult { get; set; }
    }
}
