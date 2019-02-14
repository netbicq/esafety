using ESafety.Core.Model;
using ESafety.Core.Model.DB.Platform;
using ESafety.Core.Model.View;
using ESafety.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core
{
    /// <summary>
    /// 日志
    /// </summary>
    public class LogService : ORM.ServiceBase, ILog
    {
        private IUnitwork _work = null;

        private IRepository< Sys_Log> _log = null;

        public LogService(ORM.IUnitwork work)
        {
            _work = work;
            Unitwork = work;

            _log = work.Repository<Sys_Log>();

        }
        //删除单条
        public ActionResult<bool> DelLog(Guid id)
        {
            var log = _log.Delete(q => q.ID == id);
            return new ActionResult<bool>(log > 0);
        }
        //根据用户名删除
        public ActionResult<int> DelLoglogin(string Login)
        {
            var log = _log.Delete(u => u.LogUser == Login);
            return new ActionResult<int>(log);
        }
        //根据时间删除
        public ActionResult<int> DelLogtime(Logdel para)
        {
            DateTime entime = DateTime.Parse(para.EDate.ToString("yyyy-MM-dd 23:59:59.999"));
            var log = _log.Delete(q => entime > q.LogTime && para.SDate <= q.LogTime);
            return new ActionResult<int>(log);
        }
        //日志列表
        public ActionResult<Pager<Logview>> GetListlog(PagerQuery<LogQuery> para)
        {
            var logs = _log.Queryable(q => q.LogContent.Contains(para.Query.logContent) || string.IsNullOrEmpty(para.Query.logContent));

            var retmp = from ac in logs
                        orderby ac.LogTime descending
                        select new Logview
                        {
                            Sys_Log = ac
                        };

            var re = new Pager<Logview>().GetCurrentPage(retmp, para.PageSize, para.PageIndex);

            return new ActionResult<Pager<Logview>>(re);
        }
    }
}
