using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{

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

    /// <summary>
    /// 修改隐患等级
    /// </summary>
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
    /// <summary>
    /// 调整风险等级
    /// </summary>
    public class ChangeDangerLevel
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 隐患级别
        /// </summary>
        public Guid DangerLevel { get; set; }
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
        /// 关键字(发现人、负责人、负责部门、编号所包含的值)
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

    public class TroubleCtrFlowNew
    {

        /// <summary>
        /// 隐患控制ID
        /// </summary>
        public Guid ControlID { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string FlowMemo { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public PublicEnum.EE_TroubleFlowState FlowType { get; set; }
        /// <summary>
        /// 是否通过验收(0:默认，1通过、2拒绝)
        /// </summary>
        public int FlowResult { get; set; }
    }
}
