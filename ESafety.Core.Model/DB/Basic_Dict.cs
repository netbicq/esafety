namespace ESafety.Core.Model.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// �ʵ�
    /// </summary>
    public class Basic_Dict:ModelBase 
    {
         
        /// <summary>
        /// ����ID
        /// </summary>
        public Guid ParentID { get; set; }
        /// <summary>
        /// �Ƿ�ϵͳ
        /// </summary>
        public bool IsSYS { get; set; } = false;
        /// <summary>
        /// �ʵ�����
        /// </summary>
        [Required]
        [StringLength(200)]
        public string DictName { get; set; }
        /// <summary>
        /// ��Сֵ
        /// </summary>
        public double MinValue { get; set; }
        /// <summary>
        /// ���ֵ
        /// </summary>
        public double MaxValue { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        [StringLength(500)]
        public string Memo { get; set; }
        /// <summary>
        /// LECD��Сֵ
        /// </summary>
        public int LECD_DMaxValue { get; set; }
        /// <summary>
        ///  LECD���ֵ
        /// </summary>
        public int LECD_DMinValue { get; set; }

        /// <summary>
        ///  LSD��Сֵ
        /// </summary>
        public int LSD_DMaxValue { get; set; }
        /// <summary>
        ///  LSD���ֵ
        /// </summary>
        public int LSD_DMinValue { get; set; }
        /// <summary>
        ///  ����ʱ��
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
