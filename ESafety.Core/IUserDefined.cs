using ESafety.Core.Model;
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
    /// 用户自定义项
    /// </summary>
    public interface IUserDefined
    {
        /// <summary>
        /// 获取自定义类型集合
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<EnumItem>> GetUserDefinedTypes();
        /// <summary>
        /// 根据自定义类型获取自定义项集合
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<UserDefinedView>> GetUserDefineds(PublicEnum.EE_UserDefinedType type);
        /// <summary>
        /// 根据id获取自定义项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<UserDefinedView> GetUserDefined(Guid id);
        /// <summary>
        /// 新建自定义项
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ActionResult<bool> AddUserDefined(UserDefinedNew entity);

        /// <summary>
        /// 修改自定义项
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ActionResult<bool> EditUserDefined(UserDefinedEdit entity);
        /// <summary>
        /// 根据自定义类型获取表单的自定义项
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<UserDefinedForm>> GetUserDefineItems(UserDefinedBusiness para);
        /// <summary>
        /// 保存业务数据
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        ActionResult<bool> SaveBuisnessValue(BusinessValue values);
        /// <summary>
        /// 获取自定义项的数据类型
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<EnumItem>> GetUserdefinedDataType();
    }
}
