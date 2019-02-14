namespace ESafety.Core.Model.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 自定义
    /// </summary>
    public partial class Basic_UserDefined
    {
        public Guid ID { get; set; }
        /// <summary>
        /// 自定义类型
        /// </summary>
        public int DefinedType { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Caption { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public int DataType { get; set; }
        /// <summary>
        /// 词典ID
        /// </summary>
        public Guid DictID { get; set; }
        /// <summary>
        /// 是否多选
        /// </summary>
        public bool IsMulti { get; set; }
        /// <summary>
        /// 是否可空
        /// </summary>
        public bool IsEmpty { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int VisibleIndex { get; set; }
    }
}
