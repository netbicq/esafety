namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    /// <summary>
    /// 培训
    /// 需要电子文档
    /// </summary>
    public partial class Doc_Training : ModelBase
    {
        /// <summary>
        /// 培训主题
        /// </summary>
        [Required]
        [StringLength(1000)]
        public string Motif { get; set; }
        /// <summary>
        /// 培训日期
        /// </summary>
        public DateTime TrainDate { get; set; }
        /// <summary>
        /// 培训时长
        /// </summary>
        public int TrainLong { get; set; }
        /// <summary>
        /// 培训人
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Trainer { get; set; }
        /// <summary>
        /// 培训内容
        /// </summary>
        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }
    }
}
