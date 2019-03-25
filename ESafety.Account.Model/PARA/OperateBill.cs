using ESafety.Unity;
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
   
    /// <summary>
    /// 作业申请单提交节点处理参数模型
    /// </summary>
    public class OpreateBillFlowResult
    {
        /// <summary>
        /// 作业单单据ID
        /// </summary>
        public Guid BillID { get; set; }
        /// <summary>
        /// 作业流程节点ID
        /// 
        /// </summary>
        public Guid OpreationFlowID { get; set; }
        /// <summary>
        /// 处理结果
        /// </summary>
        public PublicEnum.OpreateFlowResult FlowResult { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string FlowMemo { get; set; }
    }
}
