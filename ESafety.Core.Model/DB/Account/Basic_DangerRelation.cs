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
        /// ����id
        /// </summary>
        public Guid SubjectID { get; set; }
        /// <summary>
        /// ����id
        /// </summary>
        public Guid DangerID { get; set; }
    }
}
