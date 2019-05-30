namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Basic_Post : ModelBase
    { 
        /// <summary>
        /// ���
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        /// <summary>
        /// ��λ����
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public Guid Principal { get; set; }
        /// <summary>
        /// ��֯�ܹ�ID
        /// </summary>
        public Guid Org { get; set; }
        /// <summary>
        /// ��Ҫ����
        /// </summary>
        public string MainTasks { get; set; }
    }
}
