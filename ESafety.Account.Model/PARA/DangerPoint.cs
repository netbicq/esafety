using ESafety.Core.Model.PARA;
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
        /// <summary>
        /// 风险等级
        /// </summary>
        public Guid DangerLevel { get; set; }
        /// <summary>
        /// 危险因素词典ID
        /// </summary>
        public IEnumerable<Guid> WXYSDictIDs { get; set; }
        /// <summary>
        /// 管控措施
        /// </summary>
        public string ControlMeasure { get; set; }
        /// <summary>
        /// 应急处理措施
        /// </summary>
        public string EmergencyMeasure { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public Guid Principal { get; set; }
        /// <summary>
        /// 文件列表
        /// </summary>
        public IEnumerable<AttachFileNew> fileNews { get; set; }
        /// <summary>
        /// 组织架构ID
        /// </summary>
        public Guid OrgID { get; set; }
        /// <summary>
        /// 警示牌
        /// </summary>
        public string WarningSign { get; set; }
        /// <summary>
        /// 风险点图片
        /// </summary>
        public string DangerPointImg { get; set; }
        /// <summary>
        /// 后果
        /// </summary>
        public string Consequence { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public string Location { get; set; }
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
