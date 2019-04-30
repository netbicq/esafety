using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    class OpreationFlow
    {
    }

    public class OpreationView
    {
        /// <summary>
        /// 操作ID
        /// </summary>
        public Guid ID { get; set; }
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
        /// 负责人
        /// </summary>
        public string Principal { get; set; }
        /// <summary>
        /// 负责人电话
        /// </summary>
        public string PrincipalTel { get; set; }
    }
    public class OpreationFlowView
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 操作ID
        /// </summary>
        public Guid OpreationID { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string PointName { get; set; }
        /// <summary>
        /// 节点岗位
        /// </summary>
        public string PostName{ get; set; }
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
