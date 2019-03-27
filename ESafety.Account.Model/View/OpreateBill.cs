using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    /// <summary>
    /// 作业申请单模型
    /// </summary>
    public class OpreateBillModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 业务单据号
        /// </summary>
        public string BillCode { get; set; }
        /// <summary>
        /// 作业流程ID
        /// </summary>
        public Guid OpreationID { get; set; }
        /// <summary>
        /// 作业流程名称
        /// </summary>
        public string OpreationName { get; set; }
        /// <summary>
        /// 单据名称
        /// </summary>
        public string BillName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 作业负责人ID        /// 
        /// </summary>
        public Guid PrincipalEmployeeID { get; set; }
        /// <summary>
        /// 作业负责人姓名
        /// </summary>
        public string PrincipalEmployeeName { get; set; }
        /// <summary>
        /// 作业时长
        /// </summary>
        public int BillLong { get; set; }
        /// <summary>
        /// 单据状态
        /// </summary>
        public PublicEnum.BillFlowState State { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StateName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateMan { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime  CreateDate { get; set; }

        /// <summary>
        /// 组织架构ID
        /// </summary>
        public Guid OrgID { get; set; }

        /// <summary>
        /// 作业描述
        /// </summary>
        public string Description { get; set; }

    }
    /// <summary>
    /// 作业申请单带流程节点
    /// </summary>
    public class OpreateBillFlowModel:OpreateBillModel
    {
        /// <summary>
        /// 业务单据流程节点
        /// </summary>
        public IEnumerable<OpreateBillFlow> BillFlows { get; set; }
    }
    /// <summary>
    /// 作业单节点
    /// </summary>
    public class OpreateBillFlow
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 节点id
        /// </summary>
        public Guid OpreationFlowID { get; set; }
        /// <summary>
        /// 作业流程id
        /// </summary>
        public Guid OpreationID { get; set; }
        /// <summary>
        ///节点名称
        /// </summary>
        public string PointName { get; set; }
        /// <summary>
        /// 岗位id
        /// </summary>
        public Guid PostID { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string PostName { get; set; }
        /// <summary>
        /// 节点顺序
        /// </summary>
        public int PointIndex { get; set; }
        /// <summary>
        /// 节点交互模型
        /// </summary>
        public OpreateFlowUEModel FlowUEModel { get; set; }
    }
    /// <summary>
    /// 作业申请单流程节点
    /// </summary>
    public class OpreateFlowUEModel
    {
        /// <summary>
        /// 完成
        /// </summary>
        public bool FinishEnable { get; set; }
        /// <summary>
        /// 终止
        /// </summary>
        public bool StopEnable { get; set; }
        /// <summary>
        /// 回退
        /// </summary>
        public bool ReBackEnable { get; set; }
        /// <summary>
        /// 左边线
        /// </summary>
        public bool LeftLine { get; set; }
        /// <summary>
        /// 右边线
        /// </summary>
        public bool RightLien { get; set; }
    }
}
