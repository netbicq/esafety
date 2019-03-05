using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    public class DocCertificateView
    {
        /// <summary>
        /// 资质ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 质资名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 质资类型
        /// </summary>
        public Guid TypeID { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime InvalidDate { get; set; }
        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime ApproveDate { get; set; }
        /// <summary>
        /// 颁发机构
        /// </summary>
        public string IssueOrg { get; set; }
        /// <summary>
        /// 质资持有人
        /// </summary>
        public string Owner { get; set; }
    }
}
