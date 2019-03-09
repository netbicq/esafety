using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    class DangerManage
    {
    }

    public class DangerSortView
    {
        /// <summary>
        ///类别ID
        /// </summary>
        public  Guid ID { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid ParentID { get; set; }
        public int Level { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string SortName { get; set; }
    }

    public class DangerView
    {
        /// <summary>
        /// 风险点ID
        /// </summary>
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
        /// <summary>
        /// 类别名称
        /// </summary>
        public string DangerSortName { get; set; }
    }
}
