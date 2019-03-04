namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ��������
    /// ��Ҫ�����ĵ�
    /// </summary>
    public partial class Heal_Docment:ModelBase
    {
        /// <summary>
        /// ��ԱID
        /// </summary>
        public Guid EmployeeID { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public DateTime BirthDay { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Nation { get; set; }
        /// <summary>
        /// ����ʷ
        /// </summary>
        [StringLength(4000)]
        public string IllnessRec { get; set; }
        /// <summary>
        /// �Ŵ���ʷ
        /// </summary>
        [StringLength(4000)]
        public string HeredityRec { get; set; }
        /// <summary>
        /// ����ʷ
        /// </summary>
        [StringLength(4000)]
        public string OpreatRec { get; set; }
    }
}
