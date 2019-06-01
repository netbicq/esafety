using ESafety.Core.Model.PARA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    public class DocInstitutionNew
    {
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
        /// <summary>
        /// 自定义类项
        /// </summary>
        public IEnumerable<UserDefinedValue> UserDefineds { get; set; }

    }

    public class DocInstitutionEdit:DocInstitutionNew
    {
        /// <summary>
        /// 制度ID
        /// </summary>
        public Guid ID { get; set; }
    }


    public class DocInstitutionQuery
    {
        /// <summary>
        /// 制度类型ID
        /// </summary>
        public Guid TypeID { set; get; }

        /// <summary>
        /// 制度名称
        /// </summary>
        public string Name { get; set; }
    }

}
