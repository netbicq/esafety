namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    ///��ѵ��Ա��
    /// </summary>
    public partial class Doc_TrainEmpoyees : ModelBase
    { 

        /// <summary>
        /// ��ѵID
        /// </summary>
        public Guid TrainID { get; set; }
        /// <summary>
        /// ��ԱID
        /// </summary>
        public Guid EmployeeID { get; set; }
    }
}
