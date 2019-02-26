namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Basic_DangerRelation : ModelBase
    { 

        /// <summary>
        /// Ö÷Ìåid
        /// </summary>
        public Guid SubjectID { get; set; }
        /// <summary>
        /// ·çÏÕid
        /// </summary>
        public Guid DangerID { get; set; }
    }
}
