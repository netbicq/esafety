namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    /// <summary>
    /// Ӧ��Ԥ��
    /// ��Ҫ�����ĵ�
    /// </summary>
    public partial class Doc_Solution : ModelBaseEx
    {
        /// <summary>
        /// Ԥ������
        /// </summary>
        [Required]
        [StringLength(10)]
        public string Name { get; set; }
        /// <summary>
        /// Ԥ������
        /// </summary>
        public Guid TypeID { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public DateTime IssueDate { get; set; }
        /// <summary>
        /// ���յȼ�
        /// </summary>
        public Guid DangerLevel { get; set; }
        /// <summary>
        /// Ԥ������
        /// </summary>
        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }
         
    }
}
