namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// �ܿ�����������־
    /// </summary>
    public partial class Bll_TroubleControlFlows:ModelBase
    { 
        /// <summary>
        /// ����
        /// </summary>
        public DateTime FlowDate { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        public int FlowResult { get; set; }
        /// <summary>
        /// ������Աid
        /// </summary>
        public Guid FlowEmployeeID { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        [Required]
        [StringLength(500)]
        public string FlowMemo { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public int FlowType { get; set; }
    }
}
