using ESafety.Core;
using ESafety.Core.Model;
using ESafety.Core.Model.DB.Platform;
using ESafety.Core.Model.PARA;
using ESafety.Core.Model.View;
using ESafety.Unity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ESafety.Platform.API.Controllers
{
    /// <summary>
    /// 账套管理
    /// </summary>
    [RoutePrefix("api/account")]
    public class AccountController : Web.Unity.ESFAPI
    {
        private IAccount bll = null;

        public AccountController(IAccount account)
        {
            bll = account;

            BusinessService = bll;

        }
        /// <summary>
        /// 报表选择器数据源，根据参数提供数据
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("repselector")]
        public ActionResult<IEnumerable<AccountInfo>> ReportSelector(AccountSelectorQuery para)
        {

            return bll.GetSelector(para);

        }

        /// <summary>
        /// 新建账套
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [Route("addnew")]
        [HttpPost]
        public ActionResult<bool> AddAccount( AccountInfoNew account)
        {
            LogContent = "新建账套，参数源：" + JsonConvert.SerializeObject(account);

            return bll.AddAccount(account);

        }
        /// <summary>
        /// 创建账套的数据库
        /// </summary>
        /// <param name="accountid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("createdb/{accountid:Guid}")]
        public ActionResult< CreateDBResult> CreateDB(Guid accountid)
        {
            LogContent = "创建账套的数据库，账套ID：" + accountid;

            return bll.CreateDB(accountid);

        }

        /// <summary>
        /// 删除指定ID的账套信息
        /// </summary>
        /// <param name="accountid"></param>
        /// <returns></returns>        
        [HttpGet]
        [Route("delmodel/{accountid:Guid}")]
        public ActionResult<bool> DelAccount(Guid accountid)
        {
            LogContent = "删除指定ID的账套信息，账套ID：" + accountid;

            return bll.DelAccount(accountid);
        }
        /// <summary>
        /// 获取指定ID的账套信息
        /// </summary>
        /// <param name="accountid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getmodel/{accountid:Guid}")]
        public ActionResult<AccountInfo> GetAccountInfo(Guid accountid)
        {
            return bll.GetAccountInfo(accountid);
        }

        /// <summary>
        /// 获取账套信息列表
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [Route("getlist")]
        [HttpPost]
        public ActionResult<Pager<AccountInfoList>> GetList(PagerQuery<AccountListQuery> para)
        {
            return bll.GetList(para);
        }

        /// <summary>
        /// 账套信息选择器
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getselector")]
        public ActionResult<IEnumerable<AccountInfo>> GetSelector()
        {
            return bll.GetSelector();
        }

        /// <summary>
        /// 设置账套信息
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [Route("setaccountinfo")]
        [HttpPost]
        public ActionResult<bool> SetAccountInfo(AccountSetInfo para)
        {
            LogContent = "设置账套信息，参数源：" + JsonConvert.SerializeObject(para);

            return bll.SetAccountInfo(para);
        }
        /// <summary>
        /// 获取企业账套参数
        /// </summary>
        /// <param name="accountid"></param>
        /// <returns></returns>
        [Route("getoptions/{accountid:Guid}")]
        [HttpGet]
        public ActionResult<IEnumerable<OptionsItemView>> GetOptionItems(Guid accountid)
        {
            return bll.GetAccountOptionItems(accountid);

        }
        /// <summary>
        /// 设置企业账套参数
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        [Route("setoptions")]
        [HttpPost]
        public ActionResult<bool> SetOptions(SetOptoin option)
        {
            return bll.SetAccountOptionItems(option);
        }
        /// <summary>
        /// 设置账套配置
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("setdbserver")]
        public ActionResult<bool> SetDBServer(AccountSetDBServer para)
        {
            LogContent = "设置账套配置";

            return bll.SetDBServer(para);
        }

        /// <summary>
        /// 设备账套MQTT配置
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("setmqtt")]
        public ActionResult<bool> SetMQTTServer(AccoutSetMQTTServer para)
        {
            LogContent = "设备账套MQTT配置";

            return bll.SetMQTTServer(para);
        }
        /// <summary>
        /// 账套状态管理
        /// </summary>
        /// <param name="state"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        [Route("stateac/{state}/{ID:Guid}")]
        [HttpGet]
        public ActionResult<bool> StateSet(PublicEnum.AccountState state, Guid ID)
        {
            LogContent = "更改账套状态";

            return bll.StateSet(state, ID);
        }

    }
}
