namespace ESafety.Core.Model.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using ESafety.Unity;

    /// <summary>
    /// ��������
    /// </summary>
    public class Flow_Task : ModelBase
    {
        public Flow_Task()
        {
            _version =Command.GetTimestamp();
        } 
        /// <summary>
        /// ҵ��ID
        /// </summary>
        public Guid BusinessID { get; set; }
        /// <summary>
        /// ҵ�񵥾�����
        /// </summary>
        public int BusinessType { get; set; }
        /// <summary>
        /// �ڵ�ID
        /// </summary>
        public Guid PointID { get; set; }
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
        /// ������
        /// </summary>
        public string ApplyUser { get; set; }
        private long _version;
        /// <summary>
        /// �����汾��
        /// </summary>
        public long FlowVersion { get { return _version; } set { _version = value; } }
        /// <summary>
        /// ҵ����
        /// </summary>
        public string BusinessCode { get; set; }
        /// <summary>
        /// ҵ������
        /// </summary>
        public DateTime BusinessDate { get; set; }
    }
}
