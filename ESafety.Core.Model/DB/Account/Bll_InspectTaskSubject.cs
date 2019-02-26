namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    /// <summary>
    /// 巡检任务主体
    /// </summary>
    public partial class Bll_InspectTaskSubject : ModelBase
    { 
        /// <summary>
        /// 巡检任务id
        /// </summary>
        public Guid InspectTaskID { get; set; }
        /// <summary>
        /// 任务主体类型
        /// </summary>
        public int SubjectType { get; set; }
        /// <summary>
        /// 任务主体id
        /// </summary>
        public Guid SubjectID { get; set; }
    }
}
