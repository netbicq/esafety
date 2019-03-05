namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ��ҵ���뵥
    /// </summary>
    public partial class Bll_OpreationBill : ModelBaseEx
    { 

        /// <summary>
        /// ���ݺ�
        /// </summary>
        [Required]
        [StringLength(50)]
        public string BillCode { get; set; }
        /// <summary>
        /// ��ҵid
        /// </summary>
        public Guid OpreationID { get; set; }


        /// <summary>
        /// ��������
        /// </summary>
        [Required]
        [StringLength(200)]
        public string BillName { get; set; }

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// ��ҵ������
        /// </summary>
        public Guid PrincipalEmployeeID { get; set; }
        /// <summary>
        /// ��ҵʱ��
        /// </summary>
        public int BillLong { get; set; }
       
        /// <summary>
        /// ��ҵ����
        /// </summary>
        [StringLength(4000)]
        public string Description { get; set; }
        /// <summary>
        /// ��ҵ��ǰ���������JSON
        /// </summary>
        public string FlowJson { get; set; }
    }
}
