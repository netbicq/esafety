using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.DB
{
    /// <summary>
    /// DBModel基类
    /// </summary>
    public class ModelBase
    {
        private Guid _id =Guid.NewGuid();
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get { return _id; } set { _id = value; } }
    }

    /// <summary>
    /// 树形表
    /// </summary>
    public class ModelBaseTree:ModelBase
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid ParentID { get; set; }
    }
    public class ModelBaseEx:ModelBase
    {
        private DateTime _createdate=DateTime.Now;
        private string _crateman=string.Empty;
        private int _state=0;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get { return _createdate; } set { _createdate = value; } }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateMan { get { return _crateman; } set { _crateman = value; } }
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get { return _state; } set { _state = value; } }
    }
}
