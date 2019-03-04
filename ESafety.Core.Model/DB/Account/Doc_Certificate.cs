namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ���ʹܹ�
    /// ��Ҫ�����ĵ�
    /// </summary>
    public partial class Doc_Certificate:ModelBase
    { 
        /// <summary>
        /// ��������
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public Guid TypeID { get; set; }
        /// <summary>
        /// ��Ч��
        /// </summary>
        public DateTime InvalidDate { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        public DateTime ApproveDate { get; set; }
        /// <summary>
        /// �䷢����
        /// </summary>
        [Required]
        [StringLength(200)]
        public string IssueOrg { get; set; }
        /// <summary>
        /// ���ʳ�����
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Owner { get; set; }
    }
}
