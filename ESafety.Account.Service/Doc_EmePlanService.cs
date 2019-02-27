

/*---------------------------------------------------------------- 
// 版权所有。  
// 
// 文件名： Doc_EmePlanService
// 文件功能描述： 
// author：DengYinFeng
// 时间：02/27/2019 21:22:10
// 创建标识： 
// 
// 修改标识： 
//  
// 修改描述：此代码由T4模板自动生成
//			 对此文件的更改可能会导致不正确的行为，并且如果
//			 重新生成代码，这些更改将会丢失。
//----------------------------------------------------------------*/

using ESafety.Core.Model;
using ESafety.Account.IService;
using ESafety.ORM;
using ESafety.Core.Model.DB.Account;

namespace ESafety.Account.Service
{
	public  class  Doc_EmePlanService:ServiceBase,IDoc_EmePlanService
	{
		private IUnitwork _work = null;
        private IRepository<Doc_EmePlan> idocEme = null;
		public Doc_EmePlanService(IUnitwork work){
			_work = work;
            idocEme = _work.Repository<Doc_EmePlan>();
		}
	}
}



    