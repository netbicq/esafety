namespace ESafety.Core.Model.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ��Ա
    /// </summary>
    public partial class Basic_Employee
    {
        public Guid ID { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [Required]
        [StringLength(50)]
        public string CNName { get; set; }
        /// <summary>
        /// �Ա�
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Gender { get; set; }
        /// <summary>
        /// Leader
        /// </summary>
        public bool IsLeader { get; set; }
        /// <summary>
        /// ����ƽ��
        /// </summary>
        public bool IsLevel { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        [StringLength(50)]
        public string Login { get; set; }
        /// <summary>
        /// ͷ��IMG
        /// </summary>
        [StringLength(1000)]
        public string HeadIMG { get; set; }
    }
}
