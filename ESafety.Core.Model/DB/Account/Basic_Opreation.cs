namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Basic_Opreation : ModelBase
    { 
        /// <summary>
        /// ���
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        /// <summary>
        /// ��ҵ����
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// �Ƿ����
        /// </summary>
        public bool IsBackReturn { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public string Principal { get; set; }
        /// <summary>
        /// �����˵绰
        /// </summary>
        public string PrincipalTel { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Memo { get; set; }
    }
}
