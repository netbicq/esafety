using ESafety.Core.Model.DB.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core
{
    /// <summary>
    /// 报表
    /// </summary>
    public interface IReport
    {

        /// <summary>
        /// 获取报表作用域列表
        /// </summary>
        /// <param name="reportid"></param>
        /// <returns></returns>
        Core.Model.ActionResult<IEnumerable<Model.View.ReportScope>> GetReportScopes(Guid reportid);

        /// <summary>
        /// 删除指定ID的报表作用域
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Core.Model.ActionResult<bool> DelReportScope(Guid id);

        /// <summary>
        /// 根据账套号获取自定义报表列表
        /// </summary>
        /// <param name="accountcode"></param>
        /// <returns></returns>
        Core.Model.ActionResult<IEnumerable<RPTInfo>> GetReportInfo(string accountcode);

    }
}
