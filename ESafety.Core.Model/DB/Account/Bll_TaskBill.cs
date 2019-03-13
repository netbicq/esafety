namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ���񵥾�
    /// </summary>
    public partial class Bll_TaskBill : ModelBase
    { 
        /// <summary>
        /// ���ݺ�
        /// </summary>
        [Required]
        [StringLength(50)]
        public string BillCode { get; set; }
        /// <summary>
        /// ����id
        /// </summary>
        public Guid TaskID { get; set; }
        /// <summary>
        /// ���յ�
        /// </summary>
        public Guid DangerID { get; set; }
        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// ִ�и�λ
        /// </summary>
        public Guid PostID { get; set; }
        /// <summary>
        /// ִ����
        /// </summary>
        public Guid EmployeeID { get; set; }
        /// <summary>
        /// ����״̬
        /// </summary>
        public int State { get; set; }

    }
}
