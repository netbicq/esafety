using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    public class DocSolutionNew
    {
        /// <summary>
        /// 预案名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 预案类型
        /// </summary>
        public Guid TypeID { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime IssueDate { get; set; }
        /// <summary>
        /// 风险等级
        /// </summary>
        public Guid DangerLevel { get; set; }
        /// <summary>
        /// 预案内容
        /// </summary>
        public string Content { get; set; }
    }

    public class DocSolutionEdit: DocSolutionNew
    {
        /// <summary>
        /// 预案ID
        /// </summary>
        public Guid ID { get; set; }
    }

    public class DocSolutionQuery
    {
        /// <summary>
        /// 预案类型ID
        /// </summary>
        public Guid TypeID { get; set; }
        /// <summary>
        /// 风险等级
        /// </summary>
        public Guid DangerLevel { get; set; }
        /// <summary>
        /// 预案名称
        /// </summary>
        public string Name { get; set; }
    }

}
