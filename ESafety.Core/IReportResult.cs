using ESafety.Core.Model;
using ESafety.Core.Model.DB.Platform;
using ESafety.Core.Model.ReportResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core
{
    /// <summary>
    /// 报表执行
    /// </summary>
    public interface IReportResult
    {
        #region "报表执行部分"
        /// <summary>
        /// 执行报表前获取报表所需参数
        /// </summary>
        /// <param name="reportid"></param>
        /// <returns></returns>
        ActionResult< ExcuteReportView> GetExcuteReportInfo(Guid reportid);

        /// <summary>
        /// 执行报表并返回分页结构
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult< ReportResult> ReportResult(PagerQuery< ExcuteReprot> para);

        /// <summary>
        /// 获取指定账套的自定义报表集合
        /// </summary>
        /// <param name="accountid"></param>
        /// <returns></returns>
        ActionResult<IEnumerable< RPTInfo>> GetReports(Guid accountid);
        #endregion
    }
}
