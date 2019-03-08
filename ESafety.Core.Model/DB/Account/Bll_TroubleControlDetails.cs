namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 管控明细
    /// </summary>
    public partial class Bll_TroubleControlDetails : ModelBase
    {
        /// <summary>
        /// 管控ID
        /// </summary>
        public Guid TroubleControlID { get; set; }
        /// <summary>
        /// 任务单据明细id
        /// </summary>
        public Guid BillSubjectsID { get; set; }
    }
}
