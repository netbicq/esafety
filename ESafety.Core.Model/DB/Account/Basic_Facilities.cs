namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Basic_Facilities : ModelBase
    { 
        /// <summary>
        /// ���
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        /// <summary>
        /// �豸��ʩ����
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        /// <summary>
        /// �豸��ʩ���ID
        /// </summary>
        public Guid SortID { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        [StringLength(100)]
        public string Principal { get; set; }
        /// <summary>
        /// �����˵绰
        /// </summary>
        [StringLength(100)]
        public string PrincipalTel { get; set; }
    }
}
