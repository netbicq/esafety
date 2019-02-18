namespace ESafety.Core.Model.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// �ڵ��û�
    /// </summary>
    public class Flow_PointUsers : ModelBase
    { 
        /// <summary>
        /// �ڵ�ID
        /// </summary>
        public Guid PointID { get; set; }
        /// <summary>
        /// �ڵ��û�
        /// </summary>
        [Required]
        [StringLength(100)]
        public string PointUser { get; set; }
        /// <summary>
        /// �û�˳��
        /// </summary>
        public int UserIndex { get; set; }
    }
}