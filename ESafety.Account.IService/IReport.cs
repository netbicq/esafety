using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;

namespace ESafety.Account.IService
{
    /// <summary>
    /// 报表接口
    /// </summary>
    public interface IReport
    {
        /// <summary>
        /// 获取XXX企业安全风险三清单报表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ActionResult<Pager<DPReport>> GetDPReport(PagerQuery<DPReportQuery> query);
        /// <summary>
        /// 企业安全风险分级管控表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ActionResult<Pager<DSReport>> GetDSReport(PagerQuery<DSReportQuery> query);
    }


}
