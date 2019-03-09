namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Basic_Facilities : ModelBase
    { 
        /// <summary>
        /// 编号
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        /// <summary>
        /// 设备设施名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        /// <summary>
        /// 设备设施类别ID
        /// </summary>
        public Guid SortID { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        [StringLength(100)]
        public string Principal { get; set; }
        /// <summary>
        /// 负责人电话
        /// </summary>
        [StringLength(100)]
        public string PrincipalTel { get; set; }
        /// <summary>
        /// 设备定位
        /// </summary>
        public string Location { get; set; }
    }
}
