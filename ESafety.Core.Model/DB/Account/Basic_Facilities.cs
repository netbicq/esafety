namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Basic_Facilities : ModelBase
    { 
        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public Guid SortID { get; set; }

        [StringLength(100)]
        public string Principal { get; set; }

        [StringLength(100)]
        public string PrincipalTel { get; set; }
    }
}
