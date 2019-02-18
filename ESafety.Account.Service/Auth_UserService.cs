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

            //IEnumerable<OptionItemSet> options = null;
            //OptionItemSet optlang = null;
            //int langValue;
            //if (!string.IsNullOrEmpty(account.AccountOptions))
            //{
            //    options = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<OptionItemSet>>(account.AccountOptions);
            //    optlang = options.FirstOrDefault(q => q.OptionKey == OptionConst.OC_CONST_Language);
            //    if (optlang != null)
            //    {
            //        int.TryParse(string.IsNullOrEmpty(optlang.ItemValue) ? "1" : optlang.ItemValue, out langValue);
            //    }
            //    else
            //    {
            //        langValue = 1;
            //    }

            //}
            //else
            //{
            //    langValue = 1;
            //}

            //处理usb登陆
            //if (options.Any())
            //{
            //    var optusb = options.FirstOrDefault(q => q.OptionKey == OptionConst.OC_CONST_IsUSBKey);
            //    bool isUSB;
            //    bool.TryParse(optusb == null ? "false" : string.IsNullOrEmpty(optusb.ItemValue) ? "false" : optusb.ItemValue, out isUSB);
            //    if (isUSB)
            //    {
            //        if (string.IsNullOrEmpty(para.USBKey))
            //        {
            //            throw new Exception("请插入USBKey登陆");
            //        }

            //        var optkeys = options.FirstOrDefault(q => q.OptionKey == OptionConst.OC_CONST_USBKeys);
            //        if (!optkeys.ListValue.Any())
            //        {
            //            throw new Exception("USBKey非法");
            //        }
            //        if (!optkeys.ListValue.Any(q => q == para.USBKey))
            //        {
            //            throw new Exception("USBKey非法");
            //        }
            //    }
            //}

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
    }
}
