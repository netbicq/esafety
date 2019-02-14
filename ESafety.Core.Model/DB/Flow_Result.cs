namespace ESafety.Core.Model.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ���������¼
    /// </summary>
    public partial class Flow_Result
    {

        public Guid ID { get; set; }
        /// <summary>
        /// �����汾��
        /// </summary>
        public long FlowVersion { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        [Required]
        [StringLength(200)]
        public string FlowUser { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        public int FlowResult { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime FlowDate { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        [StringLength(500)]
        public string FlowMemo { get; set; }
    }
}
