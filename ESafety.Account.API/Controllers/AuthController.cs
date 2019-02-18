using ESafety.Core;
using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.PARA;
using ESafety.Core.Model.View;
using ESafety.Web.Unity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ESafety.Account.API.Controllers
{
    /// <summary>
    /// 用户及鉴权
    /// </summary>
    [RoutePrefix("api/auth")]
    public class AuthController : ESFAPI
    {
        private IAuth_User bll = null;

        public AuthController(IAuth_User user)
        {

            bll = user;
            BusinessService = bll as Account.Service.Auth_UserService;

        }
        /// <summary>
        /// 新建用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addnew")]
        public ActionResult<bool> Add(UserNew user)
        {
            LogContent = "新建用户，用户名：" + user.Login;
            return bll.Add(user);
        }
        /// <summary>
        /// 新建 角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addrole")]
        public ActionResult<bool> AddRole(RoleNew role)
        {
            LogContent = "新建角色，参数源：" + JsonConvert.SerializeObject(role);
            return bll.AddRole(role);
        }



        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("changepwd")]
        public ActionResult<bool> ChangePwd(UserPwdChange para)
        {
            LogContent = "修改密码";
            return bll.ChangePwd(para);
        }

        /// <summary>
        /// 删除指定ID的用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("deluser/{id:Guid}")]
        [HttpGet]
        public ActionResult<bool> Delete(Guid id)
        {
            LogContent = "删除用户";
            return bll.Delete(id);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delrole/{roleid:Guid}")]
        public ActionResult<bool> DelRole(Guid roleid)
        {
            LogContent = "删除角色";
            return bll.DelRole(roleid);
        }
        /// <summary>
        /// 获取操作员菜单
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getmenu/{login}")]
        public ActionResult<IEnumerable<AuthModuleMenu>> GetLoginMenu(string login)
        {
            return bll.GetLoginMenu(login);
        }
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getrole/{login}")]
        public ActionResult<UserRole> GetLoginRoles(string login)
        {
            return bll.GetLoginRoles(login);
        }
        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getauth/{roleid:Guid}")]
        public ActionResult<IEnumerable<AuthModule>> GetRoleAuth(Guid roleid)
        {
            return bll.GetRoleAuth(roleid);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [Route("getlist")]
        [HttpPost]
        public ActionResult<Pager<UserView>> GetUserList(Core.Model.PagerQuery<UserQuery> para)
        {
            return bll.GetUserList(para);
        }
        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("seturole")]
        public ActionResult<bool> LoginSetRole(LoginSetRole para)
        {
            LogContent = "设置用户角色，参数源：" + JsonConvert.SerializeObject(para);
            return bll.LoginSetRole(para);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("resetpwd")]
        public ActionResult<bool> ReSetPwd(UserReSetPwd para)
        {
            LogContent = "重置密码";
            return bll.ReSetPwd(para);
        }
        /// <summary>
        /// 设置用户Profile
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("setprofile")]
        public ActionResult<bool> SetProfile(UserSetProfile para)
        {
            LogContent = "设置用户Profile，参数源：" + JsonConvert.SerializeObject(para);
            return bll.SetProfile(para);
        }

        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("setrauth")]
        public ActionResult<bool> SetRoleAuth(RoleSet para)
        {
            LogContent = "设置角色权限，参数源：" + JsonConvert.SerializeObject(para);
            return bll.SetRoleAuth(para);
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("edituser")]
        public ActionResult<bool> Update(UserEdit para)
        {
            LogContent = "修改用户，参数源:" + JsonConvert.SerializeObject(para);
            return bll.Update(para);
        }
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("signin")]
        [AllowAnonymous]
        public ActionResult<UserView> UserSignin(UserSignin para)
        {
            LogContent = "用户登陆,用户：" + para.Login;
            return bll.UserSignin(para);
        }
        /// <summary>
        /// 用户注销
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [Route("exituser")]
        [HttpPost]
        public ActionResult<bool> exit()
        {
            LogContent = "用户注销";
            return bll.exit();
        }
        /// <summary>
        /// 第一次进入修改密码
        /// </summary>
        /// <returns></returns>
        [Route("checkuser")]
        [HttpPost]
        public ActionResult<bool> check()
        {
            return bll.check();
        }
        /// <summary>
        /// 获取用户选择器
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getbin")]
        public ActionResult<IEnumerable<Auth_User>> Getbin()
        {
            return bll.userbin();
        }
    }
}
