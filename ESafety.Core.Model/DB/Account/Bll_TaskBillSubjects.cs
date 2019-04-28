namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    /// <summary>
    /// ���񵥾�����
    /// </summary>
    public partial class Bll_TaskBillSubjects : ModelBase
    { 
        /// <summary>
        /// ����id
        /// </summary>
        public Guid BillID { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public int SubjectType { get; set; }
        /// <summary>
        /// ����id
        /// </summary>
        public Guid SubjectID { get; set; }
        /// <summary>
        /// ���տ���
        /// </summary>
        public Guid DangerID { get; set; }
        /// <summary>
        /// �����ύʱ��
        /// </summary>
        public DateTime TaskTime { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public int TaskResult { get; set; }
        /// <summary>
        /// ����Ѳ������
        /// </summary>
        [StringLength(2000)]
        public string TaskResultMemo { get; set; }
        /// <summary>
        /// Σ������
        /// </summary>
        public Guid Eval_WHYS { get; set; }
        /// <summary>
        /// �¹�����
        /// </summary>
        public Guid Eval_SGLX { get; set; }
        /// <summary>
        /// �¹ʺ��
        /// </summary>
        public Guid Eval_SGJG { get; set; }
        /// <summary>
        /// Ӱ�췶Χ
        /// </summary>
        public Guid Eval_YXFW { get; set; }
        /// <summary>
        /// ���ⷽ��
        /// </summary>
        public int Eval_Method { get; set; }
        /// <summary>
        /// �����ȼ�
        /// </summary>
        public int TroubleLevel { get; set; }
        /// <summary>
        /// �Ƿ�ܿ�
        /// </summary>
        public bool IsControl { get; set; }
    }
}
