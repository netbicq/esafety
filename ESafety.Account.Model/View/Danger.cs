using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    class Danger
    {
    }

    public class DangerSortView
    {
        public  Guid ID { get; set; }
        public Guid ParetID { get; set; }
        public int Level { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string SortName { get; set; }
    }

    public class DangerView
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
    }
}
