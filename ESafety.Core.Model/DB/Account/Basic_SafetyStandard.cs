namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Basic_SafetyStandard : ModelBase
    { 
        /// <summary>
        /// ���
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        /// <summary>
        /// ��ȫ��׼����
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// ���յ����ID
        /// </summary>
        public Guid DangerSortID { get; set; }
        /// <summary>
        /// �ܿش�ʩ
        /// </summary>
        public string Controls { get; set; }
        /// <summary>
        /// ���̴�ʩ
        /// </summary>
        public string Engineering { get; set; }
        /// <summary>
        /// �¹ʴ�ʩ
        /// </summary>
        public string Accident { get; set; }
        /// <summary>
        /// �����ʩ
        /// </summary>
        public string Individual { get; set; }
    }
}
