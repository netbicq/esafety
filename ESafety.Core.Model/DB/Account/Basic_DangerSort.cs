namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Basic_DangerSort : ModelBase
    { 

        public Guid ParetID { get; set; }

        public int Level { get; set; }

        [Required]
        [StringLength(100)]
        public string SortName { get; set; }
    }
}
