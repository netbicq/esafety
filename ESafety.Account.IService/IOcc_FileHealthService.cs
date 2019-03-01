

/*---------------------------------------------------------------- 
// 版权所有。  
// 
// 文件名： IOcc_FileHealthService
// 文件功能描述： 
// author：DengYinFeng
// 时间：02/28/2019 10:42:08
// 创建标识： 
// 
// 修改标识： 
//  
// 修改描述：此代码由T4模板自动生成
//			 对此文件的更改可能会导致不正确的行为，并且如果
//			 重新生成代码，这些更改将会丢失。
//----------------------------------------------------------------*/


using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
using ESafety.Core.Model.DB.Account;
using System;

namespace ESafety.Account.IService
{
    /// <summary>
    /// 职业健康[8]
    /// </summary>
	public interface IOcc_FileHealthService
	{
        /// <summary>
        /// 添加健康档案
        /// </summary>
        /// <param name="aOcc"></param>
        /// <returns></returns>
        ActionResult<bool> InceaseHealth(AOccFileHealthPara aOcc);

        /// <summary>
        /// 修改健康档案
        /// </summary>
        /// <param name="aOcc"></param>
        /// <returns></returns>
        ActionResult<bool> AmendHealth(AOccFileHealthPara aOcc);

        /// <summary>
        /// 分页获取健康档案
        /// </summary>
        /// <param name="occFile"></param>
        /// <returns></returns>
        ActionResult<Pager<OccFileHealthView>> GetHealthData(PagerQuery<OccFileHealthPara> occFile);

        /// <summary>
        /// 根据id删除健康档案
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        ActionResult<bool> DeleteFileHealthById(Guid guid);

        /// <summary>
        /// 分页获取体检数据
        /// </summary>
        /// <param name="occFile"></param>
        /// <returns></returns>
        ActionResult<Pager<OccMedicalView>> GetMedicalData(PagerQuery<OccMedicalPara> occFile);

        /// <summary>
        /// 根据id删除体检数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        ActionResult<bool> DeleteMedicalById(Guid ID);

        /// <summary>
        /// 添加体检报告
        /// </summary>
        /// <param name="occMedical"></param>
        /// <returns></returns>
        ActionResult<bool> IncreaseMedical(Occ_Medical occMedical);

        /// <summary>
        /// 修改体检报告
        /// </summary>
        /// <param name="occMedical"></param>
        /// <returns></returns>
        ActionResult<bool> AmendMedical(AOccMedicalPara occMedical);
    }
}


    