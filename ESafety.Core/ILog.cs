using ESafety.Core.Model;
using ESafety.Core.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core
{
    public interface ILog
    {
        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        ActionResult<Pager<Logview>> GetListlog(PagerQuery<LogQuery> para);
        /// <summary>
        /// 删除某条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<bool> DelLog(Guid id);
        //时间删除
        ActionResult<int> DelLogtime(Logdel para);
        //用户删除
        ActionResult<int> DelLoglogin(string Login);

    }
}
