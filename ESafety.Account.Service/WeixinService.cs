using ESafety.Account.IService;
using ESafety.ORM;
using Quick.WXHelper;
using Quick.WXHelper.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Service
{
    public class WeixinService : ServiceBase, IWeixinService
    {
        public APIResult<UserInfo> GetUserInfo(string openid)
        {
            try
            {
                return Quick.WXHelper.WxService.UserInfo(openid);
            }
            catch (Exception ex)
            {
                return new APIResult<UserInfo>(ex);
            }
        }

        public APIResult<JSConfig> GetWXJSConfig(string url)
        {
            return WxService.GetJSWxConfig(url);
        }
    }
}
