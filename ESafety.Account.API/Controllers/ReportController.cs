using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
using ESafety.Web.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ESafety.Account.API.Controllers
{
    /// <summary>
    /// 报表控制器
    /// </summary>
    [RoutePrefix("api/report")]
    public class ReportController : ESFAPI
    {
        private IReport bll = null;
        /// <summary>
        /// 报表控制器构造函数
        /// </summary>
        /// <param name="report"></param>
        public ReportController(IReport report)
        {
            bll = report;
            BusinessServices = new List<object>() { report };
        }
        /// <summary>
        /// 企业安全风险三清单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route("getDPReport")]
        [HttpPost]
        public ActionResult<Pager<DPReport>> GetDPReport(PagerQuery<DPReportQuery> query)
        {
            return bll.GetDPReport(query);
        }

        /// <summary>
        /// 企业安全风险分级管控表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route("getSReport")]
        [HttpPost]
        public ActionResult<Pager<DSReport>> GetDSReport(PagerQuery<DSReportQuery> query)
        {
            return bll.GetDSReport(query);
        }

        /// <summary>
        /// 岗位报表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route("getPostReport")]
        [HttpPost]
        public ActionResult<Pager<PostReport>> GetPostReport(PagerQuery<string> query)
        {
            return bll.GetPostReport(query);
        }
        /// <summary>
        ///企业岗位作业内容清单				
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route("getOpreateReport")]
        [HttpPost]
        public ActionResult<Pager<OpreateReport>> GetOpreateReport(PagerQuery<string> query)
        {
            return bll.GetOpreateReport(query);
        }
        /// <summary>
        /// 隐患整改情况
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route("getCtrReport")]
        [HttpPost]
        public ActionResult<Pager<CtrReport>> GetCtrReport(PagerQuery<CtrReportQuery> query)
        {
            return bll.GetCtrReport(query);
        }

        /// <summary>
        /// 设备设施风险分级控制清单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route("getSubDReport")]
        [HttpPost]
        public ActionResult<Pager<SubDReport>> GetSubDReport(PagerQuery<SubDReportQuery> query)
        {
            return bll.GetSubDReport(query);
        }
    }
}
