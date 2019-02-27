namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Basic_PostEmployees : ModelBase
    { 
        /// <summary>
        /// ∏⁄ŒªID
        /// </summary>
        public Guid PostID { get; set; }
        /// <summary>
        /// »À‘±ID
        /// </summary>
        public Guid EmployeeID { get; set; }
    }
}
