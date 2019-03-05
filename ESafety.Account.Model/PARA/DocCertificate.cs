using ESafety.Core.Model.PARA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    /// <summary>
    /// 资质管理
    /// 需要电子文档
    /// </summary>
    public class DocCertificateNew
    {
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
        /// <summary>
        /// 电子文档
        /// </summary>
        public IEnumerable<AttachFileNew> AttachFiles { get; set; }
    }

    public class DocCertificateEdit: DocCertificateNew
    {
        /// <summary>
        /// 资质ID
        /// </summary>
        public Guid ID { get; set; }
    }

    public class DocCertificateQuery
    {
        /// <summary>
        /// 质资类型
        /// </summary>
        public Guid TypeID { get; set; }
        /// <summary>
        /// 资质名称
        /// </summary>
        public string Name { get; set; }
    }

}
