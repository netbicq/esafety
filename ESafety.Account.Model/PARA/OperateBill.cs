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
    /// 作业申请单模型
    /// </summary>
    public class OpreateBillModel
    {

        public Guid ID { get; set; }

        public string BillCode { get; set; }

        public Guid OpreationID { get; set; }

        public string OpreationName { get; set; }

        public string BillName { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Guid PrincipalEmployeeID { get; set; }

        public int BillLong { get; set; }

        public PublicEnum.BillFlowState State { get; set; }

        public string StateName { get; set; }

        public string CreateMan { get; set; }

        public string CreateDate { get; set; }


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


    }
    /// <summary>
    /// 
    /// </summary>
    public class OpreateFlowUEModel
    {

        public bool FinishEnable { get; set; }

        public bool StopEnable { get; set; }

        public bool ReBackEnable { get; set; }

        public bool LeftLine { get; set; }

        public bool RightLien { get; set; }
    }
}
