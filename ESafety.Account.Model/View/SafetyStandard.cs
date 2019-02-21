using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    class SafetyStandard
    {
    }
    public class SafetyStandardView
    {
        public Guid ID { get; set; }
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
        public string DangerSort { get; set; }
        /// <summary>
        /// 管控措施
        /// </summary>
        public string Controls { get; set; }
    }
}
