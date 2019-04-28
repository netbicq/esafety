using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
   /// <summary>
   /// 新建风险点
   /// </summary>
    public class DangerPointNew
    {
        /// <summary>
        /// 风险点名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///备注
        /// </summary>
        public string Memo { get; set; }
    }
    /// <summary>
    /// 修改风险点
    /// </summary>
    public class DangerPointEdit:DangerPointNew
    {
        /// <summary>
        /// 风险点ID
        /// </summary>
        public Guid ID { get; set; }
    }

    public class DangerPointRelationNew
    {
        /// <summary>
        ///风险点ID
        /// </summary>
        public Guid DangerPointID { get; set; }
        /// <summary>
        /// 主体ID
        /// </summary>
        public Guid SubjectID { get; set; }
        /// <summary>
        /// 主体名
        /// </summary>
        public string SubjectName { get; set; }
        /// <summary>
        ///主体类型
        /// </summary>
        public int SubjectType { get; set; }
        /// <summary>
        /// 主体负责人
        /// </summary>
        public string SubjectPrincipal { get; set; }
        /// <summary>
        /// 主体负责人电话
        /// </summary>
        public string SubjectPrincipalTel { get; set; }
    }

    public class DangerPointRelationSelect
    {
        /// <summary>
        /// 风险点ID
        /// </summary>
        public Guid DangerPointID { get; set; }
        /// <summary>
        /// 主体类型
        /// </summary>
        public int SubjectType { get; set; }
    }
  
}
