namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Basic_DangerRelation : ModelBase
    { 

        public Guid SubjectID { get; set; }

        public Guid DangerID { get; set; }
    }
}
