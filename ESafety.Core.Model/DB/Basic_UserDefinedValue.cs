namespace ESafety.Core.Model.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 自定义业务数据
    /// </summary>
    public class Basic_UserDefinedValue : ModelBase
    { 
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid BusinessID { get; set; }
        /// <summary>
        /// 自定义类型
        /// </summary>
        public int DefinedType { get; set; }
        /// <summary>
        /// 自定义项ID
        /// </summary>
        public Guid DefinedID { get; set; }
        /// <summary>
        /// 自定义项业务值
        /// </summary> 
        public string DefinedValue { get; set; }
    }
}
