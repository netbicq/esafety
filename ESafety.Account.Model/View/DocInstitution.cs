using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    public class DocInstitutionView
    {
        /// <summary>
        /// 制度ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 制度名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 制度类型
        /// </summary>
        public Guid TypeID { get; set; }
        /// <summary>
        /// 字号
        /// </summary>
        public string BigCode { get; set; }
        /// <summary>
        /// 发布日期 
        /// </summary>
        public DateTime IssueDate { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

    }

    public class PhoneDocInstitutionView
    {
        /// <summary>
        /// 制度ID
        /// </summary>
        public Guid InstitutionID { get; set; }
        /// <summary>
        /// 制度名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 制度类型
        /// </summary>
        public string InstitutionType { get; set; }
        /// <summary>
        /// 字号
        /// </summary>
        public string BigCode { get; set; }
        /// <summary>
        /// 发布日期 
        /// </summary>
        public DateTime IssueDate { get; set; }

    }
    public class PhoneDocInstitutionModelView: PhoneDocInstitutionView
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }

}
