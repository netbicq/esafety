using ESafety.Core.Model.DB;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.View
{
   
    public class UserDefinedView
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 自定义类型
        /// </summary>
        public PublicEnum.EE_UserDefinedType DefinedType { get; set; }
        /// <summary>
        /// 自定义类型名称
        /// </summary>
        public string DefinedTypeName { get; set; }
        /// <summary>
        /// 标题 
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public PublicEnum.EE_UserDefinedDataType DataType { get; set; }
        /// <summary>
        /// 数据类型名称
        /// </summary>
        public string DataTypeName { get; set; }
        /// <summary>
        /// 词典ID
        /// </summary>
        public Guid DictID { get; set; }
        /// <summary>
        /// 词典
        /// </summary>
        public string DictName { get; set; }
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


    public class UserDefinedForm:UserDefinedView
    {
        /// <summary>
        /// 词典选项
        /// </summary>
        public IEnumerable<Basic_Dict> DictSelection { get; set; }
        /// <summary>
        /// 自定义项值 
        /// </summary>
        public object ItemValue { get; set; }
    }
}
