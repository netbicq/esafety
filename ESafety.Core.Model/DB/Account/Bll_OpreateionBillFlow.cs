namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ��ҵ���ڵ���־
    /// </summary>
    public partial class Bll_OpreateionBillFlow : ModelBase
    { 
        /// <summary>
        /// ��ҵ��id
        /// </summary>
        public Guid BillID { get; set; }
        /// <summary>
        /// ��ҵ�ڵ�id
        /// </summary>
        public Guid OpreationFlowID { get; set; }
        /// <summary>
        /// �ڵ���
        /// </summary>
        public int FlowResult { get; set; }
        /// <summary>
        /// �ڵ�ִ����
        /// </summary>
        public Guid FlowEmployeeID { get; set; }
        /// <summary>
        /// �ڵ�ִ��ʱ��
        /// </summary>
        public DateTime FlowTime { get; set; }
        /// <summary>
        /// �ڵ�ִ�б�ע
        /// </summary>
        [StringLength(500)]
        public string FlowMemo { get; set; }
    }
}
