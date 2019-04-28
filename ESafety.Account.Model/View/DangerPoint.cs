using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    /// <summary>
    /// 风险点视图
    /// </summary>
    public class DangerPointView
    {
        /// <summary>
        /// 风险点ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 风险点名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 二维码路径
        /// </summary>
        public string QRCoderUrl { get; set; }
    }
    /// <summary>
    /// 风险点选择器
    /// </summary>
    public class DangerPointSelector
    {
        /// <summary>
        /// 风险点ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 风险点名
        /// </summary>
        public string Name { get; set; }
    }
    /// <summary>
    /// 二维码
    /// </summary>
    public class QRCoder
    {
        /// <summary>
        /// 风险点ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 风险点名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 二维码路径
        /// </summary>
        public string QRCoderUrl { get; set; }
    }


    /// <summary>
    /// 风险点与主体关系
    /// </summary>
    public class DangerPointRelationView
    {
        /// <summary>
        /// 关系ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 主体类型
        /// </summary>
        public string SubjectType { get; set; }
        /// <summary>
        /// 主体名
        /// </summary>
        public string SubjectName { get; set; }
    }

    /// <summary>
    /// 检查主体选择器
    /// </summary>
    public class DangerPointRelationSelector
    {
        /// <summary>
        /// 主体名
        /// </summary>
        public Guid SubjectID { get; set; }
        /// <summary>
        /// 主体名
        /// </summary>
        public string SubjectName { get; set; }
    }
}
