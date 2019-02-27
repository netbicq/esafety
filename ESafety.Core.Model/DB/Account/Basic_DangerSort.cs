namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Basic_DangerSort : ModelBase
    { 
        /// <summary>
        /// ����ID
        /// </summary>
        public Guid ParetID { get; set; }

        public int Level { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        [Required]
        [StringLength(100)]
        public string SortName { get; set; }
    }
}
