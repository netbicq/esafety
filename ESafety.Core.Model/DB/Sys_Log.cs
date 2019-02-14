using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.DB
{
    /// <summary>
    /// 日志
    /// </summary>
    public class Sys_Log:ModelBase
    {
        /// <summary>
        /// ip
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime LogTime { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public string LogUser { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string LogContent { get; set; }
        /// <summary>
        /// 操作结果
        /// 成功/失败
        /// </summary>
        public bool OperateResult { get; set; }
        /// <summary>
        /// 失败消息
        /// </summary>
        public string MSG { get; set; }
        
    }
}
