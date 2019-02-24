using ESafety.Core.Model.PARA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    class OpreationFlow
    {
    }

    public class OpreationNew
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 操作名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否回流
        /// </summary>
        public bool IsBackReturn { get; set; }
        /// <summary>
        /// 操作描述
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 自定义类项
        /// </summary>
        public IEnumerable<UserDefinedValue> UserDefineds { get; set; }
    }

    public class OpreationEdit:OpreationNew
    {
        public Guid ID { get; set; }
    }
    public class OpreationQuery
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 操作名称
        /// </summary>
        public string Name { get; set; }
    }
    public class OpreationFlowNew
    {
        /// <summary>
        /// 操作ID
        /// </summary>
        public Guid OpreationID { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string PointName { get; set; }
        /// <summary>
        /// 岗位ID
        /// </summary>
        public Guid PostID { get; set; }
        /// <summary>
        /// 节点顺序
        /// </summary>
        public int PointIndex { get; set; }
        /// <summary>
        /// 节点描述
        /// </summary>
        public string PointMemo { get; set; }
    }
}
