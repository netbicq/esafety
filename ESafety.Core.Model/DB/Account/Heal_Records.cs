namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ����¼
    /// ��Ҫ�����ĵ�
    /// </summary>
    public partial class Heal_Records:ModelBase
    {

        /// <summary>
        /// ��������ID
        /// </summary>
        public Guid DocmentID { get; set; }
        /// <summary>
        /// �彡����
        /// </summary>
        public DateTime RecDate { get; set; }
        /// <summary>
        /// �����
        /// </summary>
        [Required]
        [StringLength(200)]
        public string RecResult { get; set; }
    }
}
