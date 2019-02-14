namespace ESafety.Core.Model.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// �Զ���
    /// </summary>
    public partial class Basic_UserDefined
    {
        public Guid ID { get; set; }
        /// <summary>
        /// �Զ�������
        /// </summary>
        public int DefinedType { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Caption { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public int DataType { get; set; }
        /// <summary>
        /// �ʵ�ID
        /// </summary>
        public Guid DictID { get; set; }
        /// <summary>
        /// �Ƿ��ѡ
        /// </summary>
        public bool IsMulti { get; set; }
        /// <summary>
        /// �Ƿ�ɿ�
        /// </summary>
        public bool IsEmpty { get; set; }
        /// <summary>
        /// ��ʾ˳��
        /// </summary>
        public int VisibleIndex { get; set; }
    }
}
