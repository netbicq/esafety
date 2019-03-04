namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    ///培训人员表
    /// </summary>
    public partial class Doc_TrainEmpoyees : ModelBase
    { 

        /// <summary>
        /// 培训ID
        /// </summary>
        public Guid TrainID { get; set; }
        /// <summary>
        /// 人员ID
        /// </summary>
        public Guid EmployeeID { get; set; }
    }
}
