using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    /// <summary>
    /// 风险等级与个数
    /// </summary>
    public class DashDangerLevel
    {
        /// <summary>
        /// 等级
        /// </summary>
        public string Level{ get; set; }
        /// <summary>
        /// 计数
        /// </summary>
        public int Count { get; set; }
    }

    /// <summary>
    /// 风险点位置
    /// </summary>
    public class DangerPointLocation
    {
        /// <summary>
        /// 风险点名
        /// </summary>
        public string DPName { get; set; }
        /// <summary>
        /// 风险点位置
        /// </summary>
        public string DPLocation { get; set; }
    }

    public class TroubleCtrl
    {
        /// <summary>
        /// 风控项名
        /// </summary>
        public string DangerName { get; set; }
        /// <summary>
        /// 风险点名
        /// </summary>
        public string DangerPoint { get; set; }
        /// <summary>
        /// 风险等级
        /// </summary>
        public string DLevel { get; set; }

    }

    public class DTroubleCtrl
    {

        /// <summary>
        /// 未整改计数
        /// </summary>
        public int CtrlingCount { get; set; }
        /// <summary>
        /// 整改中计数
        /// </summary>
        public int CtrledCount { get; set; }
        /// <summary>
        /// 管控项
        /// </summary>
        public IEnumerable<TroubleCtrl> Ctrls{ get; set; }
    }
}
