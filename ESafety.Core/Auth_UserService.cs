using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.PARA;
using ESafety.Core.Model.View;
using ESafety.ORM;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration; 


namespace ESafety.Core
{
    /// <summary>
    /// 用户Service
    /// </summary>
    public class Auth_UserService : ServiceBase,IAuth_User
    {
        private IUnitwork _work = null;

        private IRepository<Model.DB.Auth_User> _rpsuser = null;
        private IRepository<Model.DB.Auth_UserProfile> _rpsprofiel = null;
        private IRepository<Model.DB.Platform.Auth_WxBinds> _rpswxbinds = null;

        public Auth_UserService(ORM.IUnitwork work)
        {
            _work = work;
            Unitwork = work;

            _rpsprofiel = work.Repository<Model.DB.Auth_UserProfile>();
            _rpsuser = work.Repository<Model.DB.Auth_User>();
            _rpswxbinds = work.Repository<Model.DB.Platform.Auth_WxBinds>();

        }

        /// <summary>
        /// 新建用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        public ActionResult<bool> Add(UserNew user)
        {
            var dbuser = new Model.DB.Auth_User()
            {
                Login = user.Login,
                OtherEdit = user.OtherEdit,
                OtherView = user.OtherView,
                Pwd = user.Pwd,
                TokenValidTime = DateTime.Now,
                State = 1,
                Token = "",
                openID="",
            };
            var profile = new Model.DB.Auth_UserProfile()
            {
                CNName = user.CNName,
                Login = user.Login,
                HeadIMG = "",
                Tel = user.Tel
            };

            if (string.IsNullOrEmpty(user.Pwd) || string.IsNullOrEmpty(user.Login))
            {
                throw new Exception("用户名密码均不能为空");
            }
            if (_rpsuser.Any(q => q.Login == user.Login))
            {
                throw new Exception("用户名：" + user.Login + "已经存在");
            }

            _rpsprofiel.Add(profile);
            _rpsuser.Add(dbuser);

            _work.Commit();

            return new ActionResult<bool>(true);

        }
        /// <summary>
        /// 新建角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public ActionResult<bool> AddRole(RoleNew role)
        {
            var rolerps = _work.Repository<Model.DB.Auth_Role>();

            if (rolerps.Any(q => q.RoleName == role.RoleName))
            {
                throw new Exception("角色名称:" + role.RoleName + " 已经存在");
            }
            var dbrole = new Model.DB.Auth_Role
            {
                RoleName = role.RoleName
            };

            rolerps.Add(dbrole);
            _work.Commit();

            return new ActionResult<bool>(true);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<bool> ChangePwd(UserPwdChange para)
        {
            var user = _rpsuser.GetModel(q => q.ID == para.ID);

            if (user == null)

            {
                throw new Exception("用户不存在");
            }

            if (string.Compare(user.Pwd, para.OldPwd, false) != 0)
            {
                throw new Exception("原密码错误");
            }
            user.Pwd = para.Pwd;

            _rpsuser.Update(user);
            _work.Commit();

            return new ActionResult<bool>(true);

        }
        /// <summary>
        /// 第一次进入需要改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult<bool> check()
        {
            var token = AppUser.UserInfo.Token;
            var user = _rpsuser.GetModel(q => q.Token == token).Pwd;
            if (user == "admin")
            {
                return new ActionResult<bool>(true);
            }
            else
            {
                return new ActionResult<bool>(false);
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>  
        public ActionResult<bool> Delete(Guid id)
        {
            var dbuser = _rpsuser.GetModel(q => q.ID == id);
            if (dbuser == null)
            {
                throw new Exception("用户不存在");
            }
            if(dbuser.Login == "admin")
            {
                throw new Exception("系统内置超级用户不允许删除");
            }

            _rpsuser.Delete(dbuser);
            _rpsprofiel.Delete(q => q.Login
            == dbuser.Login);

            _work.Repository<Model.DB.Auth_UserRole>().Delete(q => q.Login == dbuser.Login);

            _work.Commit();

            return new ActionResult<bool>(true);
        }
        /// <summary>
        /// 删除指定ID的角色
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public ActionResult<bool> DelRole(Guid roleid)
        {
            var rolerps = _work.Repository<Model.DB.Auth_Role>();
            var roleauth = _work.Repository<Model.DB.Auth_RoleAuthScope>();
            var urole = _work.Repository<Model.DB.Auth_UserRole>();

            var role = rolerps.GetModel(roleid);
            if (role == null)
            {
                throw new Exception("角色不存在");
            }

            rolerps.Delete(role);
            roleauth.Delete(q => q.RoleID == roleid);
            urole.Delete(q => q.RoleID == roleid);


            _work.Commit();

            return new ActionResult<bool>(true);

        }
        /// <summary>
        /// 用户注销
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult<bool> exit()
        {
            var da = AppUser.UserInfo.ID;
            var dbuser = _rpsuser.GetModel(q => q.ID == da);
            if (dbuser == null)
            {
                throw new Exception("用户不存在");
            }
            dbuser.Token = "";
            _rpsuser.Update(dbuser);

            _work.Commit();

            return new ActionResult<bool>(true);
        }
        /// <summary>
        /// 获取权限key
        /// 如果login为空则取所有定义的key
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<Auth_KeyDetail>> GetAllAuth(string login = "")
        {
            var reauth = _work.Repository<Model.DB.Auth_KeyDetail>().Queryable();

            if(login == "")
            {
                return new ActionResult<IEnumerable<Auth_KeyDetail>>(reauth);
            }
            else
            {
                var urdb = _work.Repository<Model.DB.Auth_UserRole>().Queryable(q => q.Login == login);

                var roles = urdb.Select(s => s.RoleID);
                var rolescop = _work.Repository<Model.DB.Auth_RoleAuthScope>().Queryable(q =>
                 roles.Contains(q.RoleID));
                var rolescops = rolescop.Select(s => s.AuthKey);

                var auth = reauth.Where(
                    q => rolescops.Contains(q.AuthKey));

                return new ActionResult<IEnumerable<Auth_KeyDetail>>(auth);
            }
           

        }

        /// <summary>
        /// 获取指定的操作员的菜单
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<AuthModuleMenu>> GetLoginMenu(string login)
        {
            var roles = _work.Repository<Model.DB.Auth_UserRole>().Queryable(q => q.Login == login);
            var roleids = roles.Select(s => s.RoleID);

            var authkeys = _work.Repository<Model.DB.Auth_RoleAuthScope>().Queryable(q => roleids.Contains(q.RoleID));

            var keyss = authkeys.Select(s => s.AuthKey);

            var keybase = _work.Repository<Model.DB.Auth_Key>().Queryable(q => keyss.Contains(q.AuthKey));


            var modulestrs = keybase.Select(s => s.ModuleName);
            var menustrs = keybase.Select(s => s.MenuName);

            var modulebase = _work.Repository<Model.DB.Auth_Key>().Queryable(q => modulestrs.Contains(q.ModuleName) && string.IsNullOrEmpty(q.FunctionName) && string.IsNullOrEmpty(q.MenuName));

            var menubase = _work.Repository<Model.DB.Auth_Key>().Queryable(q => modulestrs.Contains(q.ModuleName) && menustrs.Contains(q.MenuName));


            //if (ACOptions != null)
            //{

            //    int optlanguage;
            //    var optvalue = ACOptions.FirstOrDefault(q => q.OptionKey == OptionConst.OC_CONST_Language) == null ?
            //        new OptionItemSet { ItemValue = "1" } : ACOptions.FirstOrDefault(q => q.OptionKey == OptionConst.OC_CONST_Language);

            //    var islang = int.TryParse(string.IsNullOrEmpty(optvalue.ItemValue) ? "1" : optvalue.ItemValue, out optlanguage);

            //    var language = (PublicEnum.Language)(islang ? optlanguage : 1);

            //    var localkey = language == PublicEnum.Language.CN ? "CN" :
            //         language == PublicEnum.Language.EN ? "EN" : "CN";


            //    var locals = QMES.Core.Model.LocalizationCurrent.LocalizationResource.FirstOrDefault(q => q.Language == localkey);

            //    IEnumerable<Auth_Key> keysall = null;

            //    if (locals != null)
            //    {
            //        if (locals.MenuKeys != null)
            //        {
            //            keysall = from key in keybase.Union(modulebase).Union(menubase).ToList()
            //                      let local = locals.MenuKeys.FirstOrDefault(q => q.Key == key.AuthKey)
            //                      select new Auth_Key
            //                      {
            //                          AuthKey = key.AuthKey,
            //                          ID = key.ID,
            //                          IMGUrl = key.IMGUrl,
            //                          OrderIndex = key.OrderIndex,
            //                          RoutUrl = key.RoutUrl,
            //                          ModuleName = local == null ? key.ModuleName : local.MoTitle,
            //                          FunctionName = local == null ? key.FunctionName : local.FuTitle,
            //                          MenuName = local == null ? key.MenuName : local.MeTitle
            //                      };
            //        }
            //        else
            //        {
            //            keysall = keybase.Union(modulebase).Union(menubase);
            //        }
            //    }
            //    else
            //    {
                    //keysall = keybase.Union(modulebase).Union(menubase);
                //}
            //    var re = from ak in keysall
            //             group ak by ak.ModuleName into modeulg
            //             select new AuthModuleMenu
            //             {
            //                 ModuleName = modeulg.Key,
            //                 ModulInfo = keysall.FirstOrDefault(q => q.ModuleName == modeulg.Key && string.IsNullOrEmpty(q.MenuName) && string.IsNullOrEmpty(q.RoutUrl)),
            //                 Menu = keysall.Where(q => q.ModuleName == modeulg.Key && !string.IsNullOrEmpty(q.MenuName) && !string.IsNullOrEmpty(q.RoutUrl)).OrderBy(q => q.OrderIndex)

            //             };
            //    var result = from r in re
            //                 orderby r.ModulInfo.OrderIndex
            //                 select r;

            //    return new ActionResult<IEnumerable<AuthModuleMenu>>(result);
            //}
            //else
            //{


                IEnumerable<Auth_Key> keysall = null;

                keysall = keybase.Union(modulebase).Union(menubase);
                var re = from ak in keysall
                         group ak by ak.ModuleName into modeulg
                         select new AuthModuleMenu
                         {
                             ModuleName = modeulg.Key,
                             ModulInfo = keysall.FirstOrDefault(q => q.ModuleName == modeulg.Key && string.IsNullOrEmpty(q.MenuName) && string.IsNullOrEmpty(q.RoutUrl)),
                             Menu = keysall.Where(q => q.ModuleName == modeulg.Key && !string.IsNullOrEmpty(q.MenuName) && !string.IsNullOrEmpty(q.RoutUrl)).OrderBy(q => q.OrderIndex)

                         };
                var result = from r in re
                             orderby r.ModulInfo.OrderIndex
                             select r;

                return new ActionResult<IEnumerable<AuthModuleMenu>>(result);
            //}


        }

        /// <summary>
        /// 获取用户的角色
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public ActionResult<UserRole> GetLoginRoles(string login)
        {
            var templist = _work.Repository<Model.DB.Auth_UserRole>().Queryable(q => q.Login == login);
            var roles = _work.Repository<Model.DB.Auth_Role>().Queryable();
            var lg = _work.Repository<Model.DB.Auth_User>().GetModel(q => q.Login == login);
            if (lg == null)
            {
                throw new Exception("用户" + login + "不存在");
            }
            var re = new UserRole()
            {
                Login = lg.Login
            };

            var lgroles = from role in roles
                          let check = templist.Any(q => q.RoleID == role.ID)
                          select new RoleView
                          {
                              Checked = check,
                              Role = role
                          };
            re.Roles = lgroles;

            return new ActionResult<UserRole>(re);

        }
        /// <summary>
        /// 获取角色的
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<AuthModule>> GetRoleAuth(Guid roleid)
        {

            IEnumerable<Auth_Key> authlist = null;
            IEnumerable<Auth_Key> authkeys = null;


            //if (ACOptions != null)
            //{
            //    int optlanguage;
            //    var optvalue = ACOptions.FirstOrDefault(q => q.OptionKey == OptionConst.OC_CONST_Language) == null ?
            //        new OptionItemSet { ItemValue = "1" } : ACOptions.FirstOrDefault(q => q.OptionKey == OptionConst.OC_CONST_Language);
            //    var islang = int.TryParse(string.IsNullOrEmpty(optvalue.ItemValue) ? "1" : optvalue.ItemValue, out optlanguage);
            //    var language = (PublicEnum.Language)(islang ? optlanguage : 1);

            //    var localkey = language == PublicEnum.Language.CN ? "CN" :
            //        language == PublicEnum.Language.EN ? "EN" : "CN";
            //    var locals = QMES.Core.Model.LocalizationCurrent.LocalizationResource.FirstOrDefault(q => q.Language == localkey);

            //    if (locals != null)
            //    {
            //        if (locals.MenuKeys != null)
            //        {
            //            authlist = from key in _work.Repository<Model.DB.Auth_Key>().Queryable().ToList()
            //                       let local = locals.MenuKeys.FirstOrDefault(q => q.Key == key.AuthKey)
            //                       select new Auth_Key
            //                       {
            //                           AuthKey = key.AuthKey,
            //                           ID = key.ID,
            //                           IMGUrl = key.IMGUrl,
            //                           OrderIndex = key.OrderIndex,
            //                           RoutUrl = key.RoutUrl,
            //                           ModuleName = local == null ? key.ModuleName : local.MoTitle,
            //                           FunctionName = local == null ? key.FunctionName : local.FuTitle,
            //                           MenuName = local == null ? key.MenuName : local.MeTitle
            //                       };
            //            authkeys = from key in _work.Repository<Model.DB.Auth_Key>().Queryable().ToList()
            //                       let local = locals.MenuKeys.FirstOrDefault(q => q.Key == key.AuthKey)
            //                       select new Auth_Key
            //                       {
            //                           AuthKey = key.AuthKey,
            //                           ID = key.ID,
            //                           IMGUrl = key.IMGUrl,
            //                           OrderIndex = key.OrderIndex,
            //                           RoutUrl = key.RoutUrl,
            //                           ModuleName = local == null ? key.ModuleName : local.MoTitle,
            //                           FunctionName = local == null ? key.FunctionName : local.FuTitle,
            //                           MenuName = local == null ? key.MenuName : local.MeTitle
            //                       };
            //        }
            //        else
            //        {
            //            authlist = _work.Repository<Model.DB.Auth_Key>().Queryable();
            //            authkeys = _work.Repository<Model.DB.Auth_Key>().Queryable();
            //        }
            //    }
            //    else
            //    {
            //        authlist = _work.Repository<Model.DB.Auth_Key>().Queryable();
            //        authkeys = _work.Repository<Model.DB.Auth_Key>().Queryable();
            //    }
            //}
            //else
            //{

                authlist = _work.Repository<Model.DB.Auth_Key>().Queryable();
                authkeys = _work.Repository<Model.DB.Auth_Key>().Queryable();

            //}


            var alauthkey = _work.Repository<Model.DB.Auth_RoleAuthScope>().Queryable(
                    q => q.RoleID == roleid);

            var temp = from auth in authlist
                       orderby auth.OrderIndex
                       group auth by auth.ModuleName into authg
                       select new AuthModule
                       {
                           ModuleName = authg.Key,
                           AuthMenus = from menu in authg.Where(q => !string.IsNullOrEmpty(q.RoutUrl) || !string.IsNullOrEmpty(q.FunctionName))
                                       orderby menu.OrderIndex
                                       group menu by menu.MenuName into menug
                                       select new AuthKeyMenu
                                       {
                                           MenuName = menug.Key,
                                           AuthFuncS = from authkey in menug.Where(q => !string.IsNullOrEmpty(q.FunctionName))
                                                       orderby authkey.OrderIndex
                                                       let check = alauthkey.Any(q => q.AuthKey == authkey.AuthKey)
                                                       let aukey = authkeys.FirstOrDefault(q => q.AuthKey == authkey.AuthKey)
                                                       select new AuthKeyFunc
                                                       {
                                                           AuthFunc = aukey,
                                                           Checked = check
                                                       }

                                       }

                       };

            return new ActionResult<IEnumerable<AuthModule>>(temp);


        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<UserView>> GetUserList(PagerQuery<UserQuery> para)
        {
            var users = _rpsuser.Queryable();
            var profiles = _rpsprofiel.Queryable();

            var retmp = from u in users.Where(q => (q.Login.Contains(para.KeyWord)
                        || string.IsNullOrEmpty(para.KeyWord)
                        ))
                        let pro = profiles.FirstOrDefault(q => q.Login == u.Login)
                        orderby pro.CNName
                        select new UserView
                        {
                            UserInfo = u,
                            UserProfile = pro,
                            StateStr = u.State == (int)PublicEnum.GenericState.Cancel ? "作废" :
                              u.State == (int)PublicEnum.GenericState.Normal ? "正常" : "未知"
                        };

            var re = new Pager<UserView>().GetCurrentPage(retmp, para.PageSize, para.PageIndex);

            return new ActionResult<Pager<UserView>>(re);

        }
        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<bool> LoginSetRole(LoginSetRole para)
        {
            var userrole = _work.Repository<Model.DB.Auth_UserRole>();

            var dbroles = new List<Model.DB.Auth_UserRole>();
            foreach (var role in para.RoleID)
            {
                var urole = new Model.DB.Auth_UserRole
                {
                    Login = para.Login,
                    RoleID = role,

                    ID = Guid.NewGuid()
                };
                dbroles.Add(urole);
            };

            userrole.Delete(q => q.Login == para.Login);

            userrole.Add(dbroles);

            _work.Commit();

            return new ActionResult<bool>(true);

        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<bool> ReSetPwd(UserReSetPwd para)
        {
            var user = _rpsuser.GetModel(q => q.ID == para.ID);
            if (user == null)
            {
                throw new Exception("用户不存在");
            }

            user.Pwd = para.Pwd;
            _rpsuser.Update(user);
            _work.Commit();
            return new ActionResult<bool>(true);

        }

        /// <summary>
        /// 设置用户Profile
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<bool> SetProfile(UserSetProfile para)
        {
            var pro = _rpsprofiel.GetModel(q => q.Login == para.Login)
                ;
            if (pro == null)
            {
                throw new Exception("用户Profile不存在");
            }
            pro = para.CopyTo<Auth_UserProfile>(pro);
            //para.Clone(pro);

            _rpsprofiel.Update(pro);
            _work.Commit();

            return new ActionResult<bool>(true);


        }
        /// <summary>
        /// 为角色设置权限
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<bool> SetRoleAuth(RoleSet para)
        {
            var authrole = _work.Repository<Model.DB.Auth_RoleAuthScope>();

            var dbrauth = new List<Model.DB.Auth_RoleAuthScope>();
            foreach (var rauth in para.AuthKeys)
            {
                var irauth = new Model.DB.Auth_RoleAuthScope
                {
                    AuthKey = rauth,

                    RoleID = para.RoleID,
                    ID = Guid.NewGuid()
                };
                dbrauth.Add(irauth);
            }

            authrole.Delete(q => q.RoleID == para.RoleID);

            authrole.Add(dbrauth);

            _work.Commit();

            return new ActionResult<bool>(true);


        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<bool> Update(UserEdit para)
        {

            var user = _rpsuser.GetModel(q => q.ID == para.ID);
            if (user == null)
            {
                throw new Exception("用户不存在");
            }
            user = para.CopyTo<Auth_User>(user);             

            _rpsuser.Update(user);
            _work.Commit();

            return new ActionResult<bool>(true);
        }
        /// <summary>
        /// 获取用户选择器
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<UserSelector>> userbin()
        {
            var em = _rpsuser.Queryable(p=>p.State==(int)PublicEnum.GenericState.Normal);
            var pro = _rpsprofiel.Queryable();

            var re = from l in em
                     let p = pro.FirstOrDefault(p => p.Login == l.Login)
                     select new UserSelector
                     {
                         Login = l.Login,
                         Name = p.CNName
                     };

            return new ActionResult<IEnumerable<UserSelector>>(re);
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public virtual ActionResult<UserView> UserSignin(UserSignin para)
        {
            var user = _rpsuser.GetModel(q => q.Login == para.Login);
            if (user == null)
            {
                throw new Exception("用户名或密码错误");
            }
            if (string.Compare(user.Pwd, para.Pwd, false) != 0)
            {
                throw new Exception("用户名或密码错误");
            }
            var profile = _rpsprofiel.GetModel(q => q.Login == para.Login);

            int vtimes = int.Parse(System.Configuration.ConfigurationManager.AppSettings["TokenValidTimes"]);


            user.TokenValidTime = DateTime.Now.AddMinutes(vtimes);

            user.Token = Command.CreateToken(64);
            _rpsuser.Update(user);
            _work.Commit();

            return new ActionResult<UserView>(new UserView
            {
                UserInfo = user,
                UserProfile = profile,
            });
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public virtual ActionResult<UserView> APPUserSignin(UserSignin para)
        {
            if (para.Login == "Admin")
            {
                throw new Exception("该用户无法登陆移动端!");
            }
            var user = _rpsuser.GetModel(q => q.Login == para.Login);
            if (user == null)
            {
                throw new Exception("用户名或密码错误");
            }
            if (string.Compare(user.Pwd, para.Pwd, false) != 0)
            {
                throw new Exception("用户名或密码错误");
            }
            var profile = _rpsprofiel.GetModel(q => q.Login == para.Login);

            int vtimes = int.Parse(System.Configuration.ConfigurationManager.AppSettings["TokenValidTimes"]);


            user.TokenValidTime = DateTime.Now.AddMinutes(vtimes);

            user.Token = Command.CreateToken(64);
            _rpsuser.Update(user);
            _work.Commit();

            return new ActionResult<UserView>(new UserView
            {
                UserInfo = user,
                UserProfile = profile,
            });
        }


        /// <summary>
        /// 用户绑定
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public virtual ActionResult<UserView> UserSigninBind(UserSigninWX para)
        {
            var user = _rpsuser.GetModel(q => q.Login == para.Login);
            if (user == null)
            {
                throw new Exception("用户名或密码错误");
            }
            if (string.Compare(user.Pwd, para.Pwd, false) != 0)
            {
                throw new Exception("用户名或密码错误");
            }
            if (string.IsNullOrEmpty(para.openID))
            {
                throw new Exception("openID不能为空");
            }
            if (!string.IsNullOrEmpty(user.openID)) //已经绑定则不允许重新绑定
            {
                throw new Exception("用户已经绑定微信");
            }
            var profile = _rpsprofiel.GetModel(q => q.Login == para.Login);

            //int vtimes = int.Parse(System.Configuration.ConfigurationManager.AppSettings["TokenValidTimes"]);


            //user.TokenValidTime = DateTime.Now.AddMinutes(vtimes);

            //user.Token = Command.CreateToken(64);
            user.Token = "";

            user.openID = para.openID;

            _rpsuser.Update(user);
            _work.Commit();

            //写入平台的绑定信息
            

            return new ActionResult<UserView>(new UserView
            {
                UserInfo = user,
                UserProfile = profile,
            });
        }

        public virtual ActionResult<UserView> UserSigninByopenID(string openid)
        {
            throw new Exception("平台不支持微信登陆");

            //var wxbind = _rpswxbinds.GetModel(q => q.openID == openid);
            //if(wxbind == null)
            //{
            //    throw new Exception("微信尚未绑定");
            //}

            //var user = _rpsuser.GetModel(q => q.Login == para.Login);
            //if (user == null)
            //{
            //    throw new Exception("用户名或密码错误");
            //}
            //if (string.Compare(user.Pwd, para.Pwd, false) != 0)
            //{
            //    throw new Exception("用户名或密码错误");
            //}
            //if (string.IsNullOrEmpty(para.openID))
            //{
            //    throw new Exception("openID不能为空");
            //}
            //var profile = _rpsprofiel.GetModel(q => q.Login == para.Login);

            ////int vtimes = int.Parse(System.Configuration.ConfigurationManager.AppSettings["TokenValidTimes"]);


            ////user.TokenValidTime = DateTime.Now.AddMinutes(vtimes);

            ////user.Token = Command.CreateToken(64);
            //user.openID = para.openID;

            //_rpsuser.Update(user);
            //_work.Commit();

            //return new ActionResult<UserView>(new UserView
            //{
            //    UserInfo = user,
            //    UserProfile = profile,
            //});
        }
        /// <summary>
        /// 用户解绑
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public virtual ActionResult<bool> UserWxUnBind(string openid)
        {
           
            try
            {
                _work.SetUserDB(null);

                //throw new NotImplementedException(); 
                var wxbind = _rpswxbinds.GetModel(q => q.openID == openid);
                if (wxbind == null)
                {
                    throw new Exception("该openid未绑定");
                }
                _rpswxbinds.Delete(wxbind);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
    }
}
