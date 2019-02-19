using ESafety.Core;
using ESafety.Core.Model;
using ESafety.Core.Model.PARA;
using ESafety.Core.Model.View;
using ESafety.Unity;
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
    /// 用户自定义项
    /// </summary>
    [RoutePrefix("api/userdf")]
    public class UserDefinedController : ESFAPI
    {

        private IUserDefined bll = null;

        public UserDefinedController(IUserDefined uf)
        {
            bll = uf;
            BusinessService = uf;
        }
        /// <summary>
        /// 新建用户自定义项
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addnew")]
        public ActionResult<bool> AddUserDefined(UserDefinedNew entity)
        {
            LogContent = "新建用户自定义项，参数源：" + JsonConvert.SerializeObject(entity);
            return bll.AddUserDefined(entity);

        }
        /// <summary>
        /// 修改用户自定义项
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("edit")]
        public ActionResult<bool> EditUserDefined(UserDefinedEdit entity)
        {
            LogContent = "修改自定义项，参数源：" + JsonConvert.SerializeObject(entity);
            return bll.EditUserDefined(entity);

        }
        /// <summary>
        /// 获取用户自定义项模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getmodel/{id:Guid}")]
        public ActionResult<UserDefinedView> GetUserDefined(Guid id)
        {
            return bll.GetUserDefined(id);

        }
        /// <summary>
        /// 根据自定义类型获取自定义项集合
        /// </summary>
        /// <param name="utype"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getlist/{utype}")]
        public ActionResult<IEnumerable<UserDefinedView>> GetUserDefineds(PublicEnum.EE_UserDefinedType utype)
        {
            return bll.GetUserDefineds(utype);
        }
        /// <summary>
        /// 获取自定义类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("gettypes")]
        public ActionResult<IEnumerable<EnumItem>> GetUserDefinedTypes()
        {
            return bll.GetUserDefinedTypes();
        }

        /// <summary>
        /// 获取自定义项集合，带业务数据
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getitems")]
        public ActionResult<IEnumerable<UserDefinedForm>> GetUserDefineItems(UserDefinedBusiness para)
        {
            return bll.GetUserDefineItems(para);
        }
         
    }
}
