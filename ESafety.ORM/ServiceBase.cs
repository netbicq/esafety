using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ESafety.Core.Model;

namespace ESafety.ORM
{
    /// <summary>
    /// 服务基类
    /// </summary>
    public class ServiceBase
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public Core.Model.AppUser AppUser { get; set; }  
        /// <summary>
        /// 工作单元
        /// </summary>
        public IUnitwork Unitwork { get; set; }

        /// <summary>
        /// 账套参数
        /// </summary>
        public IEnumerable<OptionItemSet> ACOptions { get; set; }
    }
}
