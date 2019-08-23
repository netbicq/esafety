namespace ESafety.Core.Model.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 词典
    /// </summary>
    public class Basic_Dict:ModelBase 
    {
         
        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid ParentID { get; set; }
        /// <summary>
        /// 是否系统
        /// </summary>
        public bool IsSYS { get; set; } = false;
        /// <summary>
        /// 词典名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string DictName { get; set; }
        /// <summary>
        /// 最小值
        /// </summary>
        public double MinValue { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxValue { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        public string Memo { get; set; }
        /// <summary>
        /// LECD最小值
        /// </summary>
        public int LECD_DMaxValue { get; set; }
        /// <summary>
        ///  LECD最大值
        /// </summary>
        public int LECD_DMinValue { get; set; }

        /// <summary>
        ///  LSD最小值
        /// </summary>
        public int LSD_DMaxValue { get; set; }
        /// <summary>
        ///  LSD最大值
        /// </summary>
        public int LSD_DMinValue { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
