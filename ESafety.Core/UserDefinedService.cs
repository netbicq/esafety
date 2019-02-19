using ESafety.Core.Model;
using ESafety.Core.Model.PARA;
using ESafety.Core.Model.View;
using ESafety.ORM;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core
{
    /// <summary>
    /// 用户自定义项
    /// </summary>
    public class UserDefinedService : ServiceBase, IUserDefined
    {

        /// <summary>
        /// 新建自定义项
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult<bool> AddUserDefined(UserDefinedNew entity)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 修改自定义项
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult<bool> EditUserDefined(UserDefinedEdit entity)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取自定义项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<UserDefinedView> GetUserDefined(Guid id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 根据自定义类型获取自定义基集合
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<UserDefinedView>> GetUserDefineds(PublicEnum.EE_UserDefinedType type)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取自定义项类型集合
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<EnumItem>> GetUserDefinedTypes()
        {
            try
            {
                var re = Command.GetItems(typeof(PublicEnum.EE_UserDefinedType));
                return new ActionResult<IEnumerable<EnumItem>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<EnumItem>>(ex);
            }
        }
        /// <summary>
        /// 获取用户自定义项，用于业务表单
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<UserDefinedForm>> GetUserDefineItems(UserDefinedBusiness para)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 保存自定义项
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public ActionResult<bool> SaveBuisnessValue(BusinessValue values)
        {
            throw new NotImplementedException();
        }
    }
}
