using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core
{
    /// <summary>
    /// 账套
    /// </summary>
    public interface IAccount
    {
        /// <summary>
        /// 获取账套选项
        /// </summary>
        /// <param name="accountid"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<OptionsItemView>> GetAccountOptionItems(Guid accountid);
        /// <summary>
        /// 设备账套选项
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        ActionResult<bool> SetAccountOptionItems(SetOptoin options);
        /// <summary>
        /// 新建账套
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        ActionResult<bool> AddAccount(Model.PARA.AccountInfoNew account);
        /// <summary>
        /// 删除指定ID的账套
        /// </summary>
        /// <param name="accountid"></param>
        /// <returns></returns>
        ActionResult<bool> DelAccount(Guid accountid);
        /// <summary>
        /// 账套状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        ActionResult<bool> StateSet(PublicEnum.AccountState state, Guid ID);
        /// <summary>
        /// 获取指定ID的账套信息
        /// </summary>
        /// <param name="accountid"></param>
        /// <returns></returns>
        ActionResult<AccountInfo> GetAccountInfo(Guid accountid);
        /// <summary>
        /// 设置账套基本信息
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<bool> SetAccountInfo(Model.PARA.AccountSetInfo para);
        /// <summary>
        /// 设备账套数据库服务器
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<bool> SetDBServer(Model.PARA.AccountSetDBServer para);
        /// <summary>
        /// 设置账套MQTT服务器
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<bool> SetMQTTServer(Model.PARA.AccoutSetMQTTServer para);
        /// <summary>
        /// 为指定ID的账套创建数据库
        /// </summary>
        /// <param name="accountid"></param>
        /// <returns></returns>
        ActionResult<Model.View.CreateDBResult> CreateDB(Guid accountid);
        /// <summary>
        /// 获取账套信息列表
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<Pager<Model.View.AccountInfoList>> GetList(PagerQuery<Model.PARA.AccountListQuery> para);

        /// <summary>
        /// 账套选择器数据源
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<AccountInfo>> GetSelector();

        /// <summary>
        /// 账套选择器数据源
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<AccountInfo>> GetSelector(Model.PARA.AccountSelectorQuery para);
    }
}
