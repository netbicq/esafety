

/*---------------------------------------------------------------- 
// 版权所有。  
// 
// 文件名： IDoc_QualificationService
// 文件功能描述： 
// author：DengYinFeng
// 时间：02/27/2019 21:29:24
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

namespace ESafety.Account.IService
{
	public interface IDoc_QualificationService
	{
        ActionResult<Pager<DocQualView>> GetQualData(DocQualPara request);

    }
}


    