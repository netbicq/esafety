using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.DB
{
    /// <summary>
    /// 账套信息
    /// </summary>
    public class AccountInfo: ModelBaseEx
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
        /// 账套简称
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// 数据库服务器
        /// </summary>
        public string DBServer { get; set; }
        /// <summary>
        /// 数据库名
        /// </summary>
        public string DBName { get; set; }
        /// <summary>
        /// 数据库uid
        /// </summary>
        public string DBUid { get; set; }
        /// <summary>
        /// 数据库密码
        /// </summary>
        public string DBPwd { get; set; } 
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; } 
        /// <summary>
        /// 负责人
        /// </summary>
        public string Principal { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 地址 属于 账套的信息
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        ///有效期
        /// </summary>
        public DateTime ValidDate { get; set; }
        /// <summary>
        /// 邮件 属于 账套的信息
        /// </summary>
        public string EMail { get; set; }
        /// <summary>
        /// token过期时长
        /// </summary>
        public int TokenValidTimes { get; set; }
        /// <summary>
        /// 账套选项
        /// </summary>
        public string AccountOptions { get; set; }
    }
}
