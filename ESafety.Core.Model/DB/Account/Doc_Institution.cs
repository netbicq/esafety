namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// �ƶ�
    /// ��Ҫ�����ĵ�
    /// </summary>
    public partial class Doc_Institution : ModelBaseEx
    { 

        /// <summary>
        /// �ƶ�����
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        /// <summary>
        /// �ƶ�����
        /// </summary>
        public Guid TypeID { get; set; }
        /// <summary>
        /// �ֺ�
        /// </summary>
        [Required]
        [StringLength(100)]
        public string BigCode { get; set; }
        /// <summary>
        /// �������� 
        /// </summary>
        public DateTime IssueDate { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }
         
    }
}
