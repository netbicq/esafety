using ESafety.Core.Model.DB.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.View
{
    /// <summary>
    /// 日志信息
    /// </summary>
    public class Logview
    {
        /// <summary>
        /// 日志信息 
        /// </summary>
        public Sys_Log Sys_Log { get; set; }
    }
    /// <summary>
    /// 日志查询
    /// </summary>
    public class LogQuery
    {
        /// <summary>
        /// 日志内容
        /// </summary>
        public string logContent { get; set; }
    }
    /// <summary>
    /// 删除范围内的日志
    /// </summary>
    public class Logdel
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime SDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EDate { get; set; }
    }
}
