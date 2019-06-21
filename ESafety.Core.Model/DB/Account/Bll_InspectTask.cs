namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Ѳ������
    /// </summary>
    public partial class Bll_InspectTask : ModelBaseEx
    {

        /// <summary>
        /// ������
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Code { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        /// <summary>
        /// ���յ�id
        /// </summary>
        public Guid DangerPointID { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public int TaskType { get; set; }
        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// ִ��Ƶ��ʱ������ 
        /// </summary>
        public int CycleDateType { get; set; }
        /// <summary>
        /// ִ��Ƶ��ֵ
        /// </summary>
        public int CycleValue { get; set; }
        /// <summary>
        /// ִ�и�λid
        /// </summary>
        public Guid ExecutePostID { get; set; }
        
        /// <summary>
        /// ������ id
        /// </summary>
        public Guid? EmployeeID { get; set; }
        /// <summary>
        /// �������� 
        /// </summary>
        [StringLength(4000)]
        public string TaskDescription { get; set; }
        /// <summary>
        /// ����MasterID
        /// </summary>
        public Guid MasterID { get; set; }
    }
}
