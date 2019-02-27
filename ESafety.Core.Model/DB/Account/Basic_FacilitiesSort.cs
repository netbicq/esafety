namespace ESafety.Core.Model.DB.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Basic_FacilitiesSort : ModelBase
    { 
        /// <summary>
        /// �豸��ʩ��𸸼�ID
        /// </summary>
        public Guid ParentID { get; set; }

        public int Level { get; set; }
        /// <summary>
        /// �豸��ʩ�������
        /// </summary>
        [Required]
        [StringLength(100)]
        public string SortName { get; set; }
    }
}
