

/*---------------------------------------------------------------- 
// 版权所有。  
// 
// 文件名： TypeConfigService
// 文件功能描述： 
// author：DengYinFeng
// 时间：02/27/2019 10:46:08
// 创建标识： 
// 
// 修改标识： 
//  
// 修改描述：此代码由T4模板自动生成
//			 对此文件的更改可能会导致不正确的行为，并且如果
//			 重新生成代码，这些更改将会丢失。
//----------------------------------------------------------------*/

using ESafety.Account.IService;
using ESafety.ORM;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESafety.Account.Service
{
    public  class  TypeConfigService:ServiceBase,ITypeConfigService
	{
		private IUnitwork _work = null;

		public TypeConfigService(IUnitwork work){

            _work = work;

		}
	}
}



    