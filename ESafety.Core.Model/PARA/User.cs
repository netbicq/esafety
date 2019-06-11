using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.PARA
{
    /// <summary>
    /// 添加用户
    /// </summary>
    public class UserNew
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 查看他人数据
        /// </summary>
        public bool OtherView { get; set; }
        /// <summary>
        /// 修改他人数据
        /// </summary>
        public bool OtherEdit { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string CNName { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }
    }
    /// <summary>
    /// 修改用户
    /// </summary>
    public class UserEdit
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 查看他人数据
        /// </summary>
        public bool OtherView { get; set; }
        /// <summary>
        /// 修改他人数据
        /// </summary>
        public bool OtherEdit { get; set; }
    }
    /// <summary>
    /// 修改
    /// </summary>
    public class UserPwdChange
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 旧密码
        /// </summary>
        public string OldPwd { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string Pwd { get; set; }
    }
    /// <summary>
    /// 设置用户Profile
    /// </summary>
    public class UserSetProfile
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string CNName { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadIMG { get; set; }

    }
    /// <summary>
    /// 用户重置密码
    /// </summary>
    public class UserReSetPwd
    {

        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }

    }

    /// <summary>
    /// 用户登陆，平台用户不需要AccountCode
    /// </summary>
    public class UserSignin
    {
        /// <summary>
        /// 账套代码
        /// </summary>
        public string AccountCode { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; } 

    }
    /// <summary>
    /// 微信绑定用户参数
    /// </summary>
    public class UserSigninWX
    {
        /// <summary>
        /// 账套代码
        /// </summary>
        public string AccountCode { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 微信openID
        /// </summary>
        public string openID { get; set; }
    }

    /// <summary>
    /// 搜索
    /// </summary>
    public class UserQuery
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string QueryKeyWord { get; set; }
    }


    /// <summary>
    /// 新建 角色
    /// </summary>
    public class RoleNew
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
    }
    /// <summary>
    /// 设置角色的权限
    /// </summary>
    public class RoleSet
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleID { get; set; }
        /// <summary>
        /// 权限KEYS
        /// </summary>
        public IEnumerable<string> AuthKeys { get; set; }
    }
    /// <summary>
    /// 用户设置角色
    /// </summary>
    public class LoginSetRole
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public IEnumerable<Guid> RoleID { get; set; }
    }
}
