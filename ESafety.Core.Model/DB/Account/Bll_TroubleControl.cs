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
    public partial class Bll_TroubleControl : ModelBase
    {

        /// <summary>
        ///�����ܿر��
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// �ܿ�����
        /// </summary>
        public string ControlName { get; set; }
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public DateTime? FinishTime { get; set; }
        /// <summary>
        /// ������id
        /// </summary>
        public Guid PrincipalID { get; set; }
        /// <summary>
        ///�����ܿ�ִ����
        /// </summary>
        public Guid? ExecutorID { get; set; }
        /// <summary>
        /// �����ܿ�������
        /// </summary>
        public Guid? AcceptorID { get; set; }
        /// <summary>
        /// �ܿ�����
        /// </summary>
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
        /// <summary>
        ///���񵥾�ID
        /// </summary>
        public Guid BillID { get; set; }
        /// <summary>
        /// ���յȼ�
        /// </summary>
        public Guid DangerLevel { get; set; }
    }
}
