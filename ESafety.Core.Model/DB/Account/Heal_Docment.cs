namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 健康档案
    /// 需要电子文档
    /// </summary>
    public partial class Heal_Docment:ModelBase
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        public Guid EmployeeID { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime BirthDay { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Nation { get; set; }
        /// <summary>
        /// 疾病史
        /// </summary>
        [StringLength(4000)]
        public string IllnessRec { get; set; }
        /// <summary>
        /// 遗传病史
        /// </summary>
        [StringLength(4000)]
        public string HeredityRec { get; set; }
        /// <summary>
        /// 手术史
        /// </summary>
        [StringLength(4000)]
        public string OpreatRec { get; set; }
    }
}
