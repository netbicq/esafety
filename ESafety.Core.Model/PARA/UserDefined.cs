using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.PARA
{
    public class UserDefinedNew
    {
        /// <summary>
        /// 自定义类型
        /// </summary>
        public PublicEnum.EE_UserDefinedType DefinedType { get; set; }
        /// <summary>
        /// 自定义项标题 
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public PublicEnum.EE_UserDefinedDataType DataType { get; set; }
        /// <summary>
        /// 词典ID
        /// </summary>
        public Guid DictID { get; set; }
        /// <summary>
        /// 多选
        /// </summary>
        public bool IsMulti { get; set; }
        /// <summary>
        /// 可空
        /// </summary>
        public bool IsEmpty { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int VisibleIndex { get; set; }

    }
    /// <summary>
    /// 修改自定义项
    /// </summary>
    public class UserDefinedEdit : UserDefinedNew
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid ID { get; set; }
    }
    /// <summary>
    /// 自定义业务数据
    /// </summary>
    public class UserDefinedBusiness
    {
        /// <summary>
        /// 自定义类型
        /// </summary>
        public PublicEnum.EE_UserDefinedType DefinedType { get; set; }
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid BusinessID { get; set; }

    }
    /// <summary>
    /// 用于业务参数
    /// </summary>
    public class UserDefinedValue
    {
        /// <summary>
        /// 自定义项id
        /// </summary>
        public Guid DefinedID { get; set; }
        /// <summary>
        /// 自定义项业务值
        /// </summary>
        public object DefinedValue { get; set; }
    }
    /// <summary>
    /// 业务数据
    /// </summary>
    public class UserDefinedBusinessValue
    {
        /// <summary>
        /// 业务数据id
        /// </summary>
        public Guid BusinessID { get; set; }
        /// <summary>
        /// 自定义项的值
        /// </summary>
        public IEnumerable<UserDefinedValue> Values { get; set; }
    }
}
