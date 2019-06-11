using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core
{
    /// <summary>
    /// 用户相关
    /// </summary>
    public interface IAuth_User
    {
        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        ActionResult<bool> Add(Model.PARA.UserNew user);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<bool> Delete(Guid id);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<bool> Update(Model.PARA.UserEdit para);
        /// <summary>
        /// 设置用户Profile
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<bool> SetProfile(Model.PARA.UserSetProfile para);
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<bool> ReSetPwd(Model.PARA.UserReSetPwd para);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<bool> ChangePwd(Model.PARA.UserPwdChange para);
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>

        ActionResult<Model.View.UserView> UserSignin(Model.PARA.UserSignin para);
        /// <summary>
        /// 用户绑定openid
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<Model.View.UserView> UserSigninBind(Model.PARA.UserSigninWX para);
        /// <summary>
        /// 用户解绑
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        ActionResult<bool> UserWxUnBind(string openid);
        /// <summary>
        /// 通过openid登陆
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        ActionResult<Model.View.UserView> UserSigninByopenID(string openid);
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<Pager<Model.View.UserView>> GetUserList(PagerQuery<Model.PARA.UserQuery> para);
        /// <summary>
        /// 获取权限key
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<Core.Model.DB.Auth_KeyDetail>> GetAllAuth(string login);

        /// <summary>
        /// 新建角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        ActionResult<bool> AddRole(Model.PARA.RoleNew role);
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        ActionResult<bool> DelRole(Guid roleid);
        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<bool> SetRoleAuth(Model.PARA.RoleSet para);

        /// <summary>
        /// 获取操作员角色列表
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        ActionResult<Model.View.UserRole> GetLoginRoles(string login);

        /// <summary>
        /// 操作员设置角色
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<bool> LoginSetRole(Model.PARA.LoginSetRole para);

        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<Model.View.AuthModule>> GetRoleAuth(Guid roleid);

        /// <summary>
        /// 获取操作员的菜单
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<Model.View.AuthModuleMenu>> GetLoginMenu(string login);
        /// <summary>
        /// 用户注销
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        ActionResult<bool> exit();
        /// <summary>
        /// 第一次修改密码
        /// </summary>
        /// <returns></returns>
        ActionResult<bool> check();
        /// <summary>
        /// 获取用户名选择器
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<Auth_User>> userbin();

    }
}
