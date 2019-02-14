using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.PARA
{


    /// <summary>
    /// 新建账套
    /// </summary>
    public class AccountInfoNew
    {

        /// <summary>
        /// 账套号
        /// </summary>
        public string AccountCode { get; set; }
        /// <summary>
        /// 账套名称
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
    }


    /// <summary>
    /// 账套列表查询参数
    /// </summary>
    public class AccountListQuery
    {

        /// <summary>
        /// 账套状态
        /// </summary>
        public PublicEnum.AccountState State { get; set; }
    }

    /// <summary>
    /// 报表选择器参数
    /// </summary>
    public class AccountSelectorQuery
    {

        public Guid ReportID { get; set; }
    }

    /// <summary>
    /// 平台端设置账套基本信息参数
    /// </summary>
    public class AccountSetInfo
    {
        /// <summary>
        /// 账套ID
        /// </summary>
        public Guid AccountID { get; set; }
        /// <summary>
        /// 账套名称
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string Principal { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// EMail
        /// </summary>
        public string EMail { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime ValidDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// Token有效时长
        /// </summary>
        public int TokenValidTimes { get; set; }


    }
    /// <summary>
    /// 企业端设置账套信息参数
    /// </summary>
    public class AccountInfoSet
    {
        /// <summary>
        /// 账套号
        /// </summary>
        public string AccountCode { get; set; }

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
        public IEnumerable<OptionItemSet> AccountOptions { get; set; }

    }
    /// <summary>
    /// 为账套设备MQTT服务器
    /// </summary>
    public class AccoutSetMQTTServer
    {
        /// <summary>
        /// 账套ID
        /// </summary>
        public Guid AccountID { get; set; }
        /// <summary>
        /// MQTT服务器
        /// </summary>
        public string MQTTServer { get; set; }
        /// <summary>
        /// MQTT用户名
        /// </summary>
        public string MQTTUid { get; set; }
        /// <summary>
        /// MQTT密码
        /// </summary>
        public string MQTTPwd { get; set; }
        /// <summary>
        /// 设备自动休眠时间，单位分钟
        /// </summary>
        public int DeviceSleepTime { get; set; }
    }
    /// <summary>
    /// /为账套设备数据库连接参数
    /// </summary>
    public class AccountSetDBServer
    {
        /// <summary>
        /// 账套ID
        /// </summary>
        public Guid AccountID { get; set; }
        /// <summary>
        /// 数据库服务器
        /// </summary>
        public string DBServer { get; set; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DBName { get; set; }
        /// <summary>
        /// 数据库用户名
        /// </summary>
        public string DBUid { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string DBPwd { get; set; }
    }
}
