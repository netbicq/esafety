using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    public class HealRecordsView
    {
        /// <summary>
        /// 体检ID
        /// </summary>
        public Guid ID { get; set; }
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
        /// 人员名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }
    }
}
