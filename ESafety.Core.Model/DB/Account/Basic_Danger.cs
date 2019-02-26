namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Basic_Danger:ModelBase
    {

        /// <summary>
        /// ����
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        public Guid DangerLevel { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        public Guid DangerSortID { get; set; }
    }
}
