namespace ESafety.Core.Model.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// �ʵ�
    /// </summary>
    public partial class Basic_Dict
    {

        public Guid ID { get; set; }
        /// <summary>
        /// ����ID
        /// </summary>
        public Guid ParentID { get; set; }
        /// <summary>
        /// �Ƿ�ϵͳ
        /// </summary>
        public bool IsSYS { get; set; }
        /// <summary>
        /// �ʵ�����
        /// </summary>
        [Required]
        [StringLength(200)]
        public string DictName { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        [StringLength(500)]
        public string Memo { get; set; }
    }
}
