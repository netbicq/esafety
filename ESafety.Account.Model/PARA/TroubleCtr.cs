using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    public class TroubleCtrNew
    {
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
        /// 责任人电话
        /// </summary>
        public string PrincipalTEL { get; set; }
        /// <summary>
        /// 责任部门id
        /// </summary>
        public Guid OrgID { get; set; }
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
        /// <summary>
        /// 隐患控制明细
        /// </summary>
        public IEnumerable<TroubleControlDetail> TroubleCtrDetails { get; set; }
    }

    public class TroubleControlDetail
    {
        /// <summary>
        /// 管控ID
        /// </summary>
        public Guid TroubleControlID { get; set; }
        /// <summary>
        /// 任务单据明细id
        /// </summary>
        public Guid BillSubjectsID { get; set; }
    }

    public class TroubleCtrEdit
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime FinishTime { get; set; }
        /// <summary>
        /// 隐患级别
        /// </summary>
        public int TroubleLevel { get; set; }
    }

    public class TroubleCtrQuery
    {
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 是否历史
        /// </summary>
        public bool IsHistory { get; set; }
        /// <summary>
        /// 隐患等级
        /// </summary>
        public int TroubleLevel { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string Key { get; set; }
    }

    public class TroubleControlDetailQuery
    {
        /// <summary>
        /// 管控ID
        /// </summary>
        public Guid TroubleControlID { get; set; }
    }

    public class TroubleCtrChangeState
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public PublicEnum.EE_TroubleState State { get; set; }
    }

    public class TroubleCtrFlowNew
    {

    }

    public class TroubleCtrFlowEdit
    {

    }
}
