namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Basic_OpreationFlow : ModelBase
    { 

        public Guid OpreationID { get; set; }

        [Required]
        [StringLength(200)]
        public string PointName { get; set; }

        public Guid PostID { get; set; }

        public int PointIndex { get; set; }

        public string PointMemo { get; set; }
    }
}
