using ESafety.Core.Model;
using ESafety.Core.Model.DB.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    public class DocCrewPara
    {
        /// <summary>
        /// 根据选项卡id 获取数据
        /// </summary>
        public Guid Id { get; set; }

    }

    public class AmendCrew
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string CName { get; set; }
        /// <summary>
        /// 字号
        /// </summary>
        public string CFontSize { get; set; }
        /// <summary>
        /// 制度Id[从词典获取]
        /// </summary>
        public Guid CType { get; set; }
    }
}
