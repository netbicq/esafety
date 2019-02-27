namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    /// <summary>
    /// ��Ƶ
    /// </summary>
    public partial class Basic_Vedio : ModelBase
    { 
        /// <summary>
        /// ��Ƶ���
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        /// <summary>
        /// ��Ƶ�ص�
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Site { get; set; }
        /// <summary>
        /// ��Ƶurl
        /// </summary>
        [Required]
        [StringLength(1000)]
        public string Url { get; set; }
    }
}
