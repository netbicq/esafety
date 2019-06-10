using Quick.WXHelper;
using Quick.WXHelper.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.IService
{
    public interface IWeixinService
    {

        APIResult<Quick.WXHelper.Dto.UserInfo> GetUserInfo(string openid);

        APIResult<JSConfig> GetWXJSConfig(string url);
    }
}
