namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 视频范围
    /// </summary>
    public partial class Basic_VedioSubject : ModelBase
    { 
        /// <summary>
        /// 视频id
        /// </summary>
        public Guid VedioID { get; set; }
        /// <summary>
        /// 主体id
        /// </summary>
        public Guid SubjectID { get; set; }
        /// <summary>
        /// 主休类型
        /// </summary>
        public int SubjectType { get; set; }
    }
}
