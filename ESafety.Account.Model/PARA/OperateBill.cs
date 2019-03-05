using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    /// <summary>
    /// 新建作业申请
    /// </summary>
    public class OperateBillNew
    {
        /// <summary>
        /// 作业名称
        /// </summary>
        public string BillName { get; set; }
        /// <summary>
        /// 作业ID
        /// </summary>
        public Guid OpreationID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 作业时长
        /// </summary>
        public int BillLong { get; set; }
        /// <summary>
        /// 作业负责人
        /// </summary>
        public Guid PrincipalEmployeeID { get; set; }

        /// <summary>
        /// 任务描述
        /// </summary>
        public string Description { get; set; }

    }
    /// <summary>
    /// 修改作业申请
    /// </summary>
    public class OpreateBillEdit : OperateBillNew
    {
        public Guid ID { get; set; }
    }
}
