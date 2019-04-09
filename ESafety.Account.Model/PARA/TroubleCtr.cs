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
        /// 单据ID
        /// </summary>
        public Guid BillID { get; set; }
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
        /// 隐患控制明细
        /// </summary>
        public IEnumerable<Guid> BillSubjectsIDs { get; set; }
    }

    public class DelayFinishTime
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime FinishTime { get; set; }

    }


    public class ChangeLevel
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
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
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? EndTime { get; set; }
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

        /// <summary>
        /// 隐患控制ID
        /// </summary>
        public Guid ControlID { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime FlowDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string FlowMemo { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int FlowType { get; set; }
        /// <summary>
        /// 是否通过验收(0:当前为状态为管控状态时，1通过、2拒绝)
        /// </summary>
        public int FlowResult { get; set; }
    }
}
