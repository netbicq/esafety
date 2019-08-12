using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESafety.Core;
using ESafety.Core.Model;
using ESafety.Core.Model.DB.Platform;
using ESafety.Core.Model.PARA;
using ESafety.Core.Model.View;
using ESafety.ORM;
using ESafety.Unity;

namespace ESafety.Account.Service
{
    /// <summary>
    /// 用户类
    /// </summary>
    public class Auth_UserService:Core.Auth_UserService
    {
        private IUnitwork _work = null;

        public Auth_UserService(IUnitwork work) : base(work)
        {
            _work = work;
            Unitwork = work;
        }
        /// <summary>
        /// 微信解绑
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public override ActionResult<bool> UserWxUnBind(string openid)
        {
            var rpsbinds = _work.Repository<Core.Model.DB.Platform.Auth_WxBinds>();
            var wxbind = rpsbinds.GetModel(q => q.openID == openid);
            if(wxbind == null)
            {
                throw new Exception("该用户尚未绑定微信");
            }
            var rpsact = _work.Repository<AccountInfo>();

            var act = rpsact.GetModel(q => q.AccountCode == wxbind.AccountCode);
            if(act == null)
            {
                throw new Exception("账套数据有误");
            }
            if (act.State == (int)PublicEnum.AccountState.Closed || act.ValidDate <= DateTime.Now)
            {
                throw new Exception("账套已经过期或已关闭");
            }
            rpsbinds.Delete(wxbind);
           
            Core.Model.AppUserDB userdb = new AppUserDB()
            {
                DBName = act.DBName,
                DBPwd = act.DBPwd,
                DBServer = act.DBServer,
                DBUid = act.DBUid
            };
            Unitwork.SetUserDB(userdb);

            var rpsuser = _work.Repository<Core.Model.DB.Auth_User>();
            var user = rpsuser.GetModel(q => q.openID == openid);
            if(user == null)
            {
                throw new Exception("用户未绑定微信");
            }
            rpsuser.Delete(user);
            _work.Commit();

            return base.UserWxUnBind(openid);
        }
        /// <summary>
        /// 微信openid登陆
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public override ActionResult<UserView> UserSigninByopenID(string openid)
        {
            var rpswxbinds = _work.Repository<Core.Model.DB.Platform.Auth_WxBinds>();
            var wxbd = rpswxbinds.GetModel(q => q.openID == openid);
            if(wxbd == null)
            {
                throw new Exception("微信号尚未绑写绑号");
            }
            var rpsact = _work.Repository<AccountInfo>();
            var act = rpsact.GetModel(q => q.AccountCode == wxbd.AccountCode);
            if(act == null)
            {
                throw new Exception("账套数据异常");
            }
            if (act.State == (int)PublicEnum.AccountState.Closed || act.ValidDate <= DateTime.Now)
            {
                throw new Exception("账套已经过期或已关闭");
            }
            Core.Model.AppUserDB userdb = new AppUserDB()
            {
                DBName = act.DBName,
                DBPwd = act.DBPwd,
                DBServer = act.DBServer,
                DBUid = act.DBUid
            };
            Unitwork.SetUserDB(userdb);

            var rpsuser = _work.Repository<Core.Model.DB.Auth_User>();
            var user = rpsuser.GetModel(q => q.openID == openid);
            if(user == null)
            {
                throw new Exception("微信未绑定账号");
            }
            var _rpsprofiel = _work.Repository<Core.Model.DB.Auth_UserProfile>();

            var profile = _rpsprofiel.GetModel(q => q.Login == user.Login);

            int vtimes = int.Parse(System.Configuration.ConfigurationManager.AppSettings["TokenValidTimes"]);


            user.TokenValidTime = DateTime.Now.AddMinutes(vtimes);

            user.Token = Command.CreateToken(64);
            rpsuser.Update(user);
            _work.Commit();

            return new ActionResult<UserView>(new UserView
            {
                AccountID=act.ID,
                UserInfo = user,
                UserProfile = profile
            }); 
        }
        /// <summary>
        /// 账套绑定
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public override ActionResult<UserView> UserSigninBind(UserSigninWX para)
        {
            var rpsact = _work.Repository<AccountInfo>();

            var account = rpsact.GetModel(q => q.AccountCode == para.AccountCode);
            if(account == null)
            {
                throw new Exception("用户名密码或账套号错误");
            }
            if(account.State ==(int)PublicEnum.AccountState.Closed || account.ValidDate <= DateTime.Now)
            {
                throw new Exception("账套已经过期或已关闭");
            }
            var rpswxbind = _work.Repository<Core.Model.DB.Platform.Auth_WxBinds>();
            var bindcheck  = rpswxbind.Any(q => q.openID == para.openID);
            if(bindcheck)
            {
                throw new Exception("微信号已经绑定账号");
            }
            rpswxbind.Add(new Auth_WxBinds
            {
                AccountCode = para.AccountCode,
                ID = Guid.NewGuid(),
                openID = para.openID
            });
            _work.Commit();//写入平台绑定

            Core.Model.AppUserDB userdb = new AppUserDB()
            {
                DBName = account.DBName,
                DBPwd = account.DBPwd,
                DBServer = account.DBServer,
                DBUid = account.DBUid
            };
            Unitwork.SetUserDB(userdb); 
            var sresult = base.UserSigninBind(para);
            if (sresult.data != null && sresult.state == 200)
            {
                var user = sresult.data.UserInfo;
                //user.TokenValidTime = DateTime.Now.AddMinutes(account.TokenValidTimes);
                //_work.Repository<Core.Model.DB.Auth_User>().Update(user);
                //_work.Commit();

                sresult.data.AccountID = account.ID;
                sresult.data.AccountCode = account.AccountCode;
                sresult.data.AccountName = account.AccountName;
                sresult.data.Principal = account.Principal;
                sresult.data.ShortName = account.ShortName;
                sresult.data.Tel = account.Tel;

            }
            return sresult; 
        }

        public override ActionResult<UserView> UserSignin(UserSignin para)
        {
            var rpsaccount = _work.Repository<AccountInfo>();
            var account = rpsaccount.GetModel(q => q.AccountCode == para.AccountCode);

            if (account == null)
            {
                throw new Exception("用户名密码或账套号错误");
            }
            if (account.State == (int)PublicEnum.AccountState.Closed || account.ValidDate <= DateTime.Now)
            {
                throw new Exception("账套已过期或已关闭");
            }

            

            Core.Model.AppUserDB userdb = new AppUserDB()
            {
                DBName = account.DBName,
                DBPwd = account.DBPwd,
                DBServer = account.DBServer,
                DBUid = account.DBUid
            };
            Unitwork.SetUserDB(userdb);

            var sresult = base.UserSignin(para);
            if (sresult.data != null && sresult.state == 200)
            {
                var user = sresult.data.UserInfo;
                user.TokenValidTime = DateTime.Now.AddMinutes(account.TokenValidTimes);
                _work.Repository<Core.Model.DB.Auth_User>().Update(user);
                _work.Commit();

                sresult.data.AccountID = account.ID;
                sresult.data.AccountCode = account.AccountCode;
                sresult.data.AccountName = account.AccountName;
                sresult.data.Principal = account.Principal;
                sresult.data.ShortName = account.ShortName;
                sresult.data.Tel = account.Tel;

            }
            return sresult;
        }
        /// <summary>
        /// 移动端登陆
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public override ActionResult<UserView> APPUserSignin(UserSignin para)
        {
            var rpsaccount = _work.Repository<AccountInfo>();
            var account = rpsaccount.GetModel(q => q.AccountCode == para.AccountCode);

            if (account == null)
            {
                throw new Exception("用户名密码或账套号错误");
            }
            if (account.State == (int)PublicEnum.AccountState.Closed || account.ValidDate <= DateTime.Now)
            {
                throw new Exception("账套已过期或已关闭");
            }



            Core.Model.AppUserDB userdb = new AppUserDB()
            {
                DBName = account.DBName,
                DBPwd = account.DBPwd,
                DBServer = account.DBServer,
                DBUid = account.DBUid
            };
            Unitwork.SetUserDB(userdb);

            var sresult = base.APPUserSignin(para);
            if (sresult.data != null && sresult.state == 200)
            {
                var user = sresult.data.UserInfo;
                user.TokenValidTime = DateTime.Now.AddMinutes(account.TokenValidTimes);
                _work.Repository<Core.Model.DB.Auth_User>().Update(user);
                _work.Commit();

                sresult.data.AccountID = account.ID;
                sresult.data.AccountCode = account.AccountCode;
                sresult.data.AccountName = account.AccountName;
                sresult.data.Principal = account.Principal;
                sresult.data.ShortName = account.ShortName;
                sresult.data.Tel = account.Tel;

            }
            return sresult;
        }
    }
}
