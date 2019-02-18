using ESafety.Core.Model.DB;
using ESafety.Core.Model.DB.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.View
{
    /// <summary>
    /// 账套信息
    /// </summary>
    public class AccountInfoList
    {
        /// <summary>
        /// 账套信息 
        /// </summary>
        public AccountInfo AccountInfo { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string StateStr { get; set; }
    }
    /// <summary>
    /// 账套信息
    /// </summary>
    public class AccountInfoView
    {
        /// <summary>
        /// 设备自动休眠时间，单位为分钟
        /// </summary>
        public int DeviceSleepTime { get; set; }

        /// <summary>
        /// token过期时长
        /// </summary>
        public int TokenValidTimes { get; set; }
        /// <summary>
        /// 账套选项
        /// </summary>
        public IEnumerable<OptionsItemView> AccountOptions { get; set; }
    }
    /// <summary>
    /// 创建数据库的返回结构
    /// </summary>
    public class CreateDBResult
    {
        public string DBServer { get; set; }

        public string DBUid { get; set; }

        public string DBPwd { get; set; }

        public string DBName { get; set; }
    }
}
