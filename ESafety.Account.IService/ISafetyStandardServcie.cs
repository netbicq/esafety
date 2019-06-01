using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.IService
{
    public interface ISafetyStandardServcie
    {
        /// <summary>
        /// 新建执行标准
        /// </summary>
        /// <param name="safetystandard"></param>
        /// <returns></returns>
        ActionResult<bool> AddSafetyStandard(SafetyStandardNew safetystandard);
        /// <summary>
        /// 修改执行标准
        /// </summary>
        /// <param name="safetystandard"></param>
        /// <returns></returns>
        ActionResult<bool> EditSafetyStandard(SafetyStandardEdit safetystandard);
        /// <summary>
        /// 根据ID，删除执行标准
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<bool> DelSafetyStandard(Guid id);
        /// <summary>
        /// 获取执行标准列表
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<SafetyStandardView>> GetSafetyStandards();
        /// <summary>
        /// 根据风控项ID获取执行标准
        /// </summary>
        /// <param name="dangerid"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<SafetyStandardView>> GetSafetyStandards(Guid dangerid);
        /// <summary>
        /// 根据ID获取执行标准模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<SafetyStandardModel> GetSafetyStandard(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dangersortid"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<SafetyStandardView>> GetSafetyStandardsByDangerSort(Guid dangersortid);
    }
}
