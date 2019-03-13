namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// �ܿص�
    /// </summary>
    public partial class Bll_TroubleControl : ModelBaseEx
    { 
        /// <summary>
        /// �ܿ�����
        /// </summary>
        [Required]
        [StringLength(500)]
        public string ControlName { get; set; }
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public DateTime FinishTime { get; set; }
        /// <summary>
        /// ������id
        /// </summary>
        public Guid PrincipalID { get; set; }
        /// <summary>
        /// �����˵绰
        /// </summary>
        [Required]
        [StringLength(50)]
        public string PrincipalTEL { get; set; }
        /// <summary>
        /// ���β���id
        /// </summary>
        public Guid OrgID { get; set; }
        /// <summary>
        /// �ܿ�����
        /// </summary>
        [Required]
        [StringLength(4000)]
        public string ControlDescription { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public int TroubleLevel { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// ״̬
        /// </summary>
        public int State { get; set; }
    }
}
