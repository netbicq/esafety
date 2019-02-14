namespace ESafety.Core.Model.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// �Զ���ҵ������
    /// </summary>
    public partial class Basic_UserDefinedValue
    {
        public Guid ID { get; set; }
        /// <summary>
        /// ҵ��ID
        /// </summary>
        public Guid BusinessID { get; set; }
        /// <summary>
        /// �Զ�������
        /// </summary>
        public int DefinedType { get; set; }
        /// <summary>
        /// �Զ�����ID
        /// </summary>
        public Guid DefinedID { get; set; }
        /// <summary>
        /// �Զ�����ҵ��ֵ
        /// </summary>
        [Required]
        public string DefinedValue { get; set; }
    }
}
