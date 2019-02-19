namespace ESafety.Core.Model.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ��֯�ܹ�
    /// </summary>
    public class Basic_Org : ModelBase
    { 
        public Guid ParentID { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public int Level { get; set; } 
        /// <summary>
        /// ����
        /// </summary>
        [Required]
        [StringLength(100)]
        public string OrgName { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        [StringLength(50)]
        public string Principal { get; set; }
        /// <summary>
        /// �����˵绰
        /// </summary>
        [StringLength(50)]
        public string PrincipalTel { get; set; }
    }
}
