using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.PARA
{
    /// <summary>
    /// 流程Master
    /// </summary>
    public class FlowMasterNew
    {
        /// <summary>
        /// 控制名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public PublicEnum.EE_BusinessType BusinessType { get; set; }
    }
    /// <summary>
    ///  修改流程Master
    /// </summary>
    public class FlowMasterEdit
    {
        /// <summary>
        /// MasterID
        /// </summary>
        public Guid MasterID { get; set; }
        /// <summary>
        /// 控制名
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 新建审批节点
    /// </summary>
    public class Flow_PointsNew
    {
        /// <summary>
        /// 流程MasterID
        /// </summary>
        public Guid MasterID { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public PublicEnum.EE_BusinessType BusinessType { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string PointName { get; set; }
        /// <summary>
        /// 节点类型
        /// </summary>
        public PublicEnum.EE_FlowPointType PointType { get; set; }
        /// <summary>
        /// 节点顺序
        /// </summary>
        public int PointIndex { get; set; }
    }

    /// <summary>
    /// 修改审批节点
    /// </summary>
    public class Flow_PointsEdit : Flow_PointsNew
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
    }
    /// <summary>
    /// 新建审批节点用户
    /// </summary>
    public class Flow_PointUsersNew
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        public Guid PointID { get; set; }
        /// <summary>
        /// 节点用户
        /// </summary>
        public string PointUser { get; set; }
        /// <summary>
        /// 用户顺序
        /// </summary>
        public int UserIndex { get; set; }
    }
    /// <summary>
    /// 修必审批节点用户
    /// </summary>
    public class Flow_PointUserEdit : Flow_PointUsersNew
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
    }
    /// <summary>
    /// 流程节点查询条件
    /// </summary>
    public class FlowPointQuery
    {
        /// <summary>
        /// MasterID
        /// </summary>
        public Guid MasterID { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public PublicEnum.EE_BusinessType BusinessType{ get; set; }
    }
    /// <summary>
    /// 审批初始任务参数
    /// </summary>
    public class InitTask
    {
        /// <summary>
        /// 流程MasterID
        /// </summary>
        public Guid MasterID { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public PublicEnum.EE_BusinessType BusinessType { get; set; }
        /// <summary>
        /// 业务单据ID
        /// </summary>
        public Guid BusinessID { get; set; }
        /// <summary>
        /// 业务单据号
        /// </summary>
        public string BusinessCode { get; set; }
        /// <summary>
        /// 业务单据创建时间
        /// </summary>
        public DateTime BusinessDate { get; set; }
    }
    /// <summary>
    /// 审批
    /// </summary>
    public class Approve
    {
        /// <summary>
        /// 审批结果
        /// </summary>
        public PublicEnum.EE_FlowResult FlowResult { get; set; }
        /// <summary>
        /// 审批意见
        /// </summary>
        public string FlowMemo { get; set; }
        /// <summary>
        /// 审批任务ID
        /// </summary>
        public Guid TaskID { get; set; }
    }
    
     /// <summary>
     /// 审批撤回
     /// </summary>
    public class FlowRecall
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid BusinessID { get; set; }
        /// <summary>
        /// 审批版本
        /// </summary>
        public long FlowVersion { get; set; }
    }
    /// <summary>
    /// 业务审批参数
    /// </summary>
    public class BusinessAprovePara
    {
        /// <summary>
        /// 流程MasterID
        /// </summary>
        public Guid MasterID { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public PublicEnum.EE_BusinessType BusinessType { get; set; }
        /// <summary>
        /// 业务id
        /// </summary>
        public Guid BusinessID { get; set; }
        /// <summary>
        /// 业务单据号
        /// </summary>
        public string BusinessCode { get; set; }
        /// <summary>
        /// 业务单据创建时间
        /// </summary>
        public DateTime BusinessDate { get; set; }
    }
}
