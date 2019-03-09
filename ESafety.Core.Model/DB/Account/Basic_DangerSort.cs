namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Basic_DangerSort : ModelBaseTree
    { 

        /// <summary>
        /// 级次
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string SortName { get; set; }
    }
}
