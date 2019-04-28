using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.DB.Account
{
    public class Basic_DangerPointRelation:ModelBase
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
}
