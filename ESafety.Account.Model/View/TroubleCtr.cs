using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    public class TroubleCtrView
    {
        /// <summary>
        /// 隐患管控ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 管控名称
        /// </summary>
        public string ControlName { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime FinishTime { get; set; }
        /// <summary>
        /// 责任人id
        /// </summary>
        public Guid PrincipalID { get; set; }
        /// <summary>
        /// 负责人名
        /// </summary>
        public string  PrincipalName { get; set; }
        /// <summary>
        /// 巡检人名
        /// </summary>
        public string BillEmpName { get; set; }
        /// <summary>
        /// 责任人电话
        /// </summary>
        public string PrincipalTEL { get; set; }
        /// <summary>
        /// 责任部门id
        /// </summary>
        public Guid OrgID { get; set; }
        /// <summary>
        /// 负责人部门名
        /// </summary>
        public String OrgName { get; set; }
        /// <summary>
        /// 管控描述
        /// </summary>
        public string ControlDescription { get; set; }
        /// <summary>
        /// 隐患级别
        /// </summary>
        public int TroubleLevel { get; set; }
        /// <summary>
        /// 发现日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
    }

    public class TroubleCtrDetailView
    {

    }

    public class TroubleCtrFlowView
    {

    }

}
