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
        /// <summary>
        /// 企业作业岗位清单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ActionResult<Pager<PostReport>> GetPostReport(PagerQuery<string> query);
        /// <summary>
        ///  企业岗位作业内容清单				
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ActionResult<Pager<OpreateReport>> GetOpreateReport(PagerQuery<string> query);
        /// <summary>
        /// 隐患整改情况
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ActionResult<Pager<CtrReport>> GetCtrReport(PagerQuery<CtrReportQuery> query);
        /// <summary>
        /// 设备设施风险分级控制清单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ActionResult<Pager<SubDReport>> GetSubDReport(PagerQuery<SubDReportQuery> query);
    }


}
