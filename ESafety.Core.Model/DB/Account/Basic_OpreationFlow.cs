namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Basic_OpreationFlow : ModelBase
    { 
        /// <summary>
        /// ��ҵID
        /// </summary>
        public Guid OpreationID { get; set; }
        /// <summary>
        /// �ڵ�����
        /// </summary>
        [Required]
        [StringLength(200)]
        public string PointName { get; set; }
        /// <summary>
        /// ��λID
        /// </summary>
        public Guid PostID { get; set; }
        /// <summary>
        /// �ڵ�˳��
        /// </summary>
        public int PointIndex { get; set; }
        /// <summary>
        /// �ڵ�����
        /// </summary>
        public string PointMemo { get; set; }
    }
}
