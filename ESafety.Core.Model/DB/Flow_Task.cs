namespace ESafety.Core.Model.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ��������
    /// </summary>
    public partial class Flow_Task
    {
        public Guid ID { get; set; }
        /// <summary>
        /// ҵ��ID
        /// </summary>
        public Guid BusinessID { get; set; }
        /// <summary>
        /// ҵ�񵥾�����
        /// </summary>
        public int BusinessType { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        [Required]
        [StringLength(100)]
        public string TaskUser { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime TaskDate { get; set; }
        /// <summary>
        /// �����汾��
        /// </summary>
        public long FlowVersion { get; set; }
    }
}
