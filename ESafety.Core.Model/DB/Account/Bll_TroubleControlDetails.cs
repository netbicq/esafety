namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// �ܿ���ϸ
    /// </summary>
    public partial class Bll_TroubleControlDetails : ModelBase
    {
        /// <summary>
        /// �ܿ�ID
        /// </summary>
        public Guid TroubleControlID { get; set; }
        /// <summary>
        /// ���񵥾���ϸid
        /// </summary>
        public Guid BillSubjectsID { get; set; }
    }
}
