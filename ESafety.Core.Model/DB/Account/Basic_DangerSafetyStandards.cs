namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Basic_DangerSafetyStandards : ModelBase
    { 
        /// <summary>
        /// ���յ�id
        /// </summary>
        public Guid DangerID { get; set; }
        /// <summary>
        /// �����׼id
        /// </summary>
        public Guid SafetyStandardID { get; set; }
    }
}
