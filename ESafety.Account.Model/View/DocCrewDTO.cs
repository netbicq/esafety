using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.DB.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    public class DocCrewDTO
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string CName { get; set; }
        /// <summary>
        /// 字号
        /// </summary>
        public string CFontSize { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 发布时间1
        /// </summary>
        public string timStr { get { return CreateTime.ToString("g"); } }
        /// <summary>
        /// 内容
        /// </summary>
        public string CContent { get; set; }
        /// <summary>
        /// 当前选择类别名
        /// </summary>
        public string TName { get; set; }
        /// <summary>
        /// 当前选择类别Id
        /// </summary>
        public Guid TId { get; set; }
    }
}
