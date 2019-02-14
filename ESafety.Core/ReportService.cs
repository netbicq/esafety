using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.View;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core
{
    public class ReportService:ORM.ServiceBase,IReport
    {
        private ORM.IUnitwork _work = null;

        private ORM.IRepository< AccountInfo> _rpsaccountinfo = null;
        private ORM.IRepository< RPTAccountScope> _rpsreportscope = null;


        public ReportService(ORM.IUnitwork work)
        {

            _work = work;
            _rpsaccountinfo = work.Repository< AccountInfo>();
            _rpsreportscope = work.Repository< RPTAccountScope>();

        }
        /// <summary>
        /// 删除报表作用域
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelReportScope(Guid id)
        {
            var scope = _rpsreportscope.GetModel(q => q.ID == id);
            if (scope == null)
            {
                throw new Exception("信息不存在");
            }

            _rpsreportscope.Delete(scope);
            _work.Commit();

            return new ActionResult<bool>(true);

        }
        /// <summary>
        /// 获取指定账套号的报表集合
        /// </summary>
        /// <param name="accountcode"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<RPTInfo>> GetReportInfo(string accountcode)
        {
            var rpsreport = _work.Repository< RPTInfo>();

            var scopes = _rpsreportscope.GetList(q => q.AccountCode == accountcode).Select(q => q.ReportID);

            var reports = rpsreport.GetList(q => q.ScopeType == (int)PublicEnum.ReortScopeType.Global

            || scopes.Contains(q.ID));

            return new ActionResult<IEnumerable<RPTInfo>>(reports);

        }
        /// <summary>
        /// 获取批定报表的账套作用域
        /// </summary>
        /// <param name="reportid"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<ReportScope>> GetReportScopes(Guid reportid)
        {
            var scopes = _rpsreportscope.GetList(q => q.ReportID == reportid);
            var _rpsreport = _work.Repository< RPTInfo>();
            var accounts = _rpsaccountinfo.Queryable();
            var reports = _rpsreport.Queryable();

            var retemp = from scop in scopes
                         let account = accounts.FirstOrDefault(q => q.AccountCode == scop.AccountCode)
                         let report = reports.FirstOrDefault(q => q.ID == scop.ReportID)
                         select new ReportScope
                         {
                             AccountInfo = account,
                             ID = scop.ID,
                             ReportInfo = report
                         };

            return new ActionResult<IEnumerable<ReportScope>>(retemp);

        }

    }
}
