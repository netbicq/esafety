using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    class DangerSet
    {
    }

    public class DangerRelationView
    {
       /// <summary>
       /// 风险点与主题关系的ID
       /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 主题ID
        /// </summary>
        public Guid SubjectID { get; set; }
        /// <summary>
        /// 风险点ID
        /// </summary>
        public Guid DangerID { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类别ID
        /// </summary>
        public Guid DangerSortID { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string DangerSortName { get; set; }

    }
}
