namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    /// <summary>
    /// Ѳ����������
    /// </summary>
    public partial class Bll_InspectTaskSubject : ModelBase
    { 
        /// <summary>
        /// Ѳ������id
        /// </summary>
        public Guid InspectTaskID { get; set; }
        /// <summary>
        /// ������������
        /// </summary>
        public int SubjectType { get; set; }
        /// <summary>
        /// ��������id
        /// </summary>
        public Guid SubjectID { get; set; }
        /// <summary>
        ///�����
        /// </summary>
        public Guid DangerID { get; set; }
    }
}
