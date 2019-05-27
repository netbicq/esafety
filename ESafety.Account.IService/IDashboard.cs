using ESafety.Account.Model.View;
using ESafety.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.IService
{
    /// <summary>
    /// 数据看板
    /// </summary>
    public interface IDashboard
    {
        /// <summary>
        /// 风险等级
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<DashDangerLevel>> GetDashDLevel();
        /// <summary>
        /// 风险点位置
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<DangerPointLocation>> GetDPLocation();
        /// <summary>
        /// 管控项
        /// </summary>
        /// <returns></returns>
        ActionResult<DTroubleCtrl> GetDctrl();
    }
}
