using ESafety.Core.Model.PARA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    /// <summary>
    /// 体检记录
    /// 需要电子文档
    /// </summary>
    public class HealRecordsNew
    {
        /// <summary>
        /// 健康档案ID
        /// </summary>
        public Guid DocmentID { get; set; }
        /// <summary>
        /// 体健日期
        /// </summary>
        public DateTime RecDate { get; set; }
        /// <summary>
        /// 体检结果
        /// </summary>
        public string RecResult { get; set; }
        /// <summary>
        /// 电子文档
        /// </summary>
        public IEnumerable<AttachFileNew> AttachFiles { get; set; }
    }
    public class HealRecordsEdit: HealRecordsNew
    {
        /// <summary>
        /// 体检记录ID
        /// </summary>
        public Guid ID { get; set; }
    }

    public class HealRecordsQuery
    {
        /// <summary>
        /// 健康档案ID
        /// </summary>
        public Guid DocmentID { get; set; }
    }

}
