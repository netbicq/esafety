namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ��Ƶ��Χ
    /// </summary>
    public partial class Basic_VedioSubject : ModelBase
    { 
        /// <summary>
        /// ��Ƶid
        /// </summary>
        public Guid VedioID { get; set; }
        /// <summary>
        /// ����id
        /// </summary>
        public Guid SubjectID { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public int SubjectType { get; set; }
    }
}
