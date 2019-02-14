using Dapper;
using ESafety.Core.Model;
using ESafety.Core.Model.DB.Platform;
using ESafety.Core.Model.PARA;
using ESafety.Core.Model.View;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core
{
    /// <summary>
    /// 账套Service
    /// </summary>
    public class AccountService: ORM.ServiceBase, IAccount
    {
        private ORM.IUnitwork _work = null;
        private ORM.IRepository<AccountInfo> accountrps = null;

        public AccountService(ORM.IUnitwork work)
        {

            _work = work;
            Unitwork = work;
            accountrps = _work.Repository<AccountInfo>();


        }
        /// <summary>
        /// 新建账套
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public ActionResult<bool> AddAccount(Model.PARA.AccountInfoNew account)
        {
           // var dbaccount = new AccountInfo();

            var dbaccount = account.MAPTO<AccountInfo>();
             
            dbaccount.ValidDate = Convert.ToDateTime("2098-02-06");
            dbaccount.State = 1;
            dbaccount.CreateMan = AppUser.UserProfile.CNName;
            var re = _work.Repository<AccountInfo>().Add(dbaccount);
            _work.Commit();

            return new ActionResult<bool>(true);

        }


        public ActionResult<Model.View.CreateDBResult> CreateDB(Guid accountid)
        {
            var account = _work.Repository<AccountInfo>().GetModel(q => q.ID == accountid);
            string dbname = "MES_" + account.AccountCode;

            if (account == null)
            {
                throw new Exception("账套不存在");
            }
            string sql = @" DECLARE	@return_value int,
                            @errmsg nvarchar(max) 
                            Exec  @return_value =dbo.CreateAccountDB 
                            @dbName=N'" + dbname + @"',@errmsg=@errmsg output  
                            Select @errmsg as errmsg   
                            Select 'result'=@return_value ";

            SqlMapper.GridReader re = _work.ExecProcedreResult(sql) as SqlMapper.GridReader;

            string errmsg = "";
            int result = 0;

            var objerr = re.Read();
            var objresult = re.Read();

            errmsg = objerr.First().errmsg;
            result = objresult.First().result;

            string dbserver = System.Configuration.ConfigurationManager.AppSettings["dbserver"];
            string dbuid = System.Configuration.ConfigurationManager.AppSettings["dbuid"];
            string dbpwd = System.Configuration.ConfigurationManager.AppSettings["dbpwd"];

            if (result == 200)
            {
                //account.State = (int)PublicEnum.AccountState.DBed;
                account.DBName = dbname;
                account.DBPwd = dbpwd;
                account.DBServer = dbserver;
                account.DBUid = dbuid;
                account.TokenValidTimes = 30;
                _work.Commit();


                return new ActionResult<Model.View.CreateDBResult>(new CreateDBResult
                {
                    DBUid = dbuid,
                    DBServer = dbserver,
                    DBName = dbname,
                    DBPwd = dbpwd
                });
            }
            else
            {
                Exception err = new Exception(errmsg);
                return new ActionResult<CreateDBResult>(err);
            }

        }
        /// <summary>
        /// 删除指定ID的账套
        /// </summary>
        /// <param name="accountid"></param>
        /// <returns></returns>
        public ActionResult<bool> DelAccount(Guid accountid)
        {
            var account = accountrps.Delete(q => q.ID == accountid);
            return new ActionResult<bool>(account > 0);

        }
        /// <summary>
        /// 获取指定ID的账套信息
        /// </summary>
        /// <param name="accountid"></param>
        /// <returns></returns>
        public ActionResult<AccountInfo> GetAccountInfo(Guid accountid)
        {
            var re = accountrps.GetModel(accountid);
            return new ActionResult<AccountInfo>(re);

        }
        /// <summary>
        /// 获取账套选项集合
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<OptionsItemView>> GetAccountOptionItems(Guid accountid)
        {
            var re = AccountOptions.GetOptions();
            var opt = accountrps.Queryable(q => q.ID == accountid).FirstOrDefault();
            IEnumerable<OptionItemSet> optsets;
            if (!string.IsNullOrEmpty(opt.AccountOptions))
            {
                optsets = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<OptionItemSet>>(opt.AccountOptions);
                foreach (var item in re)
                {
                    switch (item.OptionType)
                    {
                        case PublicEnum.AccountOptionItemType.Bool:
                        case PublicEnum.AccountOptionItemType.Date:
                        case PublicEnum.AccountOptionItemType.Int:
                        case PublicEnum.AccountOptionItemType.Number:
                        case PublicEnum.AccountOptionItemType.String:
                            item.ItemValue = optsets.FirstOrDefault(q => q.OptionKey == item.OptionKey).ItemValue;
                            break;
                        case PublicEnum.AccountOptionItemType.Enum:
                            item.ItemValue = optsets.FirstOrDefault(q => q.OptionKey == item.OptionKey).ItemValue;
                            break;
                        case PublicEnum.AccountOptionItemType.List:
                            item.ListValue = optsets.FirstOrDefault(q => q.OptionKey == item.OptionKey).ListValue;
                            break;
                        case PublicEnum.AccountOptionItemType.MultiValue:
                            item.MultiValue = optsets.FirstOrDefault(q => q.OptionKey == item.OptionKey).MultiValue;
                            break;
                        default:
                            break;
                    }
                }
            }
            return new ActionResult<IEnumerable<OptionsItemView>>(re);
        }

        public ActionResult<Pager<AccountInfoList>> GetList(PagerQuery<AccountListQuery> para)
        {
            var retmp = from ac in accountrps.GetList(q =>
                        (q.State == (int)para.Query.State
                        || (int)para.Query.State == 0)
                        && (q.AccountName.Contains(para.KeyWord)
                        || q.ShortName.Contains(para.KeyWord)
                        || q.AccountCode.Contains(para.KeyWord)
                        || q.Tel.Contains(para.KeyWord)
                        || q.Principal.Contains(para.KeyWord)
                        || string.IsNullOrEmpty(para.KeyWord)
                        ))
                        select new Model.View.AccountInfoList
                        {
                            AccountInfo = ac,
                            StateStr = ac.State == (int)PublicEnum.AccountState.Closed ? "已关闭" :
                              ac.State == (int)PublicEnum.AccountState.Normal ? "正常" : "未知",
                        };
            string excel = "";
            if (para.ToExcel)
            {
                var sw = from obj in retmp
                         select new
                         {
                             账套编号 = obj.AccountInfo.AccountCode,
                             账套名称 = obj.AccountInfo.AccountName,
                             账套简称 = obj.AccountInfo.ShortName,
                             数据库服务器 = obj.AccountInfo.DBServer,
                             数据库名 = obj.AccountInfo.DBName,
                             用户名 = obj.AccountInfo.DBUid,
                             创建人 = obj.AccountInfo.CreateMan,
                             创建时间 = obj.AccountInfo.CreateDate,
                             状态 = obj.StateStr,
                         };
                excel = Command.CreateExcel(sw, AppUser.OutPutPaht);


            }

            var re = new Pager<AccountInfoList>().GetCurrentPage(retmp, para.PageSize, para.PageIndex);
            re.ExcelResult = excel;
            var en = re.Data.ToList();
            foreach (var item in en)
            {
                if (item.AccountInfo.ValidDate <= DateTime.Now)
                {
                    item.StateStr = "过期";
                }
            }
            re.Data = en;
            return new ActionResult<Pager<AccountInfoList>>(re);

        }
        /// <summary>
        /// 账套选择器
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<AccountInfo>> GetSelector()
        {
            var re = accountrps.GetList();

            return new ActionResult<IEnumerable<AccountInfo>>(re);

        }
        /// <summary>
        /// 根据参数获取账套选择器数据源
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<AccountInfo>> GetSelector(AccountSelectorQuery para)
        {
            var accounts = accountrps.GetList();
            var reportaccountcodes = _work.Repository< RPTAccountScope>()
                .GetList(q => q.ReportID == para.ReportID)
                .Select(s => s.AccountCode);

            var re = from ac in accounts
                     where reportaccountcodes.Contains(ac.AccountCode)
                     select ac;

            return new ActionResult<IEnumerable<AccountInfo>>(re);

        }

        /// <summary>
        /// 设置账套基本信息
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<bool> SetAccountInfo(AccountSetInfo para)
        {
            try
            {
                var account = accountrps.GetModel(q => q.ID == para.AccountID);
                if (account == null)
                {
                    throw new Exception("账套不存在");
                }

                if (accountrps.Any(q => q.AccountName == para.AccountName && q.ID != para.AccountID))
                {
                    throw new Exception("账套名称已经存在");
                }
                if (accountrps.Any(q => q.ShortName == para.ShortName && q.ID != para.AccountID))
                {
                    throw new Exception("账套简称已经存在");
                }
                account = para.MAPTO<AccountInfo>();
                 

                accountrps.Update(account);
                _work.Commit();

                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }


        }

        public ActionResult<bool> SetAccountOptionItems(SetOptoin options)
        {
            try
            {
                var accountmodel = accountrps.GetModel(options.AccountID);
                if (accountmodel == null)
                {
                    throw new Exception("账套未找到");
                }

                foreach (var opt in options.Options)
                {
                    //存在选项值则检查选项值的合法性
                    if (!string.IsNullOrEmpty(opt.ItemValue) || opt.ListValue != null || opt.MultiValue != null)
                    {
                        switch (opt.OptionType)
                        {
                            case PublicEnum.AccountOptionItemType.Bool:
                                bool checkbool;
                                if (!bool.TryParse(opt.ItemValue, out checkbool))
                                {
                                    throw new Exception("布尔类型有误");
                                }
                                break;
                            case PublicEnum.AccountOptionItemType.Date:
                                DateTime checkdate;
                                if (!DateTime.TryParse(opt.ItemValue, out checkdate))
                                {
                                    throw new Exception("日期类型有误");
                                }
                                break;
                            case PublicEnum.AccountOptionItemType.Enum:
                                Type checkenum;
                                switch (opt.OptionKey)
                                {
                                     
                                    default:
                                        checkenum = null;
                                        break;
                                }
                                if (checkenum == null)
                                {
                                    throw new Exception("Enum选项类型有误");
                                }
                                break;
                            case PublicEnum.AccountOptionItemType.Int:
                                int checkint;
                                if (!int.TryParse(opt.ItemValue, out checkint))
                                {
                                    throw new Exception("整数类型的选项值有误");
                                }
                                break;
                            case PublicEnum.AccountOptionItemType.Number:
                                decimal checkdecimal;
                                if (!decimal.TryParse(opt.ItemValue, out checkdecimal))
                                {
                                    throw new Exception("数字类型的选项值有误");
                                }
                                break;
                            default:
                                break;
                        }
                    }

                }
                var optionjson = Newtonsoft.Json.JsonConvert.SerializeObject(options.Options);
                accountmodel.AccountOptions = optionjson;
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }


        /// <summary>
        /// 设备数据库
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<bool> SetDBServer(AccountSetDBServer para)
        {

            var account = accountrps.GetModel(q => q.ID == para.AccountID);
            if (account == null)
            {
                throw new Exception("账套不存在");
            }


            if (accountrps.Any(q => q.DBName == para.DBName && q.ID != para.AccountID))
            {
                throw new Exception("数据名称已经存在");
            }
            account = para.MAPTO<AccountInfo>();
             

            accountrps.Update(account);
            _work.Commit();

            return new ActionResult<bool>(true);

        }
        /// <summary>
        /// 设置MQTT服务器
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<bool> SetMQTTServer(AccoutSetMQTTServer para)
        {
            var account = accountrps.GetModel(q => q.ID == para.AccountID);

            if (account == null)
            {
                throw new Exception("账套不存在");
            }

            account = para.MAPTO<AccountInfo>();
             

            accountrps.Update(account);
            _work.Commit();

            return new ActionResult<bool>(true);
        }
        /// <summary>
        /// 账套状态
        /// </summary>
        /// <param name="state"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult<bool> StateSet(PublicEnum.AccountState state, Guid ID)
        {
            var em = accountrps.GetModel(q => q.ID == ID);
            if (em == null)
            {
                throw new Exception("账套不存在");
            }
            em.State = (int)state;
            accountrps.Update(em);
            _work.Commit();
            return new ActionResult<bool>(true);
        }

    }
}
