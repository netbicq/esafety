using ESafety.Core.Model.DB; 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model
{
    /// <summary>
    /// 访问账套的用户
    /// </summary>
    public class AppUser
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public Auth_User UserInfo { get; set; }
        /// <summary>
        /// 用户Profile
        /// </summary>
        public Auth_UserProfile UserProfile { get; set; }
        /// <summary>
        /// 用户的数据库信息
        /// </summary>
        public AppUserDB UserDB { get; set; }

        /// <summary>
        /// 账套号
        /// </summary>
        public string AccountCode { get; set; }
        /// <summary>
        /// 上传文件路径
        /// </summary>
        public string UploadPath { get; set; }
        /// <summary>
        /// 导出文件路径
        /// </summary>
        public string OutPutPaht { get; set; }
        /// <summary>
        /// 职员信息
        /// </summary>
        public Basic_Employee EmployeeInfo { get; set; }

    }

    /// <summary>
    /// 用户登陆的账套数据库信息
    /// </summary>
    public class AppUserDB
    {
        /// <summary>
        /// 数据库名
        /// </summary>
        public string DBName { get; set; }
        /// <summary>
        /// 数据库服务器
        /// </summary>
        public string DBServer { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string DBUid { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string DBPwd { get; set; }
    }
}
