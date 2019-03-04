namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    /// <summary>
    /// ��ѵ
    /// ��Ҫ�����ĵ�
    /// </summary>
    public partial class Doc_Training : ModelBase
    {
        /// <summary>
        /// ��ѵ����
        /// </summary>
        [Required]
        [StringLength(1000)]
        public string Motif { get; set; }
        /// <summary>
        /// ��ѵ����
        /// </summary>
        public DateTime TrainDate { get; set; }
        /// <summary>
        /// ��ѵʱ��
        /// </summary>
        public int TrainLong { get; set; }
        /// <summary>
        /// ��ѵ��
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Trainer { get; set; }
        /// <summary>
        /// ��ѵ����
        /// </summary>
        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }
    }
}
