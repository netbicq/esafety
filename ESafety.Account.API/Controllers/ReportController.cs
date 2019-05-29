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
    }
}
