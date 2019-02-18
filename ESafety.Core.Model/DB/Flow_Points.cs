namespace ESafety.Core.Model.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// �����ڵ�
    /// </summary>
    public class Flow_Points: ModelBase 
    { 
        /// <summary>
        /// ҵ�񵥾�����
        /// </summary>
        public int BusinessType { get; set; }
        /// <summary>
        /// �ڵ�����
        /// </summary>
        [Required]
        [StringLength(100)]
        public string PointName { get; set; }
        /// <summary>
        /// �ڵ�����
        /// </summary>
        public int PointType { get; set; }
        /// <summary>
        /// �ڵ�˳��
        /// </summary>
        public int PointIndex { get; set; }
    }
}
