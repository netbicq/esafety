
/*---------------------------------------------------------------- 
// 版权所有。  
// 
// 文件名： IDoc_CrewService
// 文件功能描述： 
// author：DengYinFeng
// 时间：02/27/2019 10:23:54
// 创建标识： 
// 
// 修改标识： 
//  
// 修改描述：此代码由T4模板自动生成
//			 对此文件的更改可能会导致不正确的行为，并且如果
//			 重新生成代码，这些更改将会丢失。
//----------------------------------------------------------------*/

using ESafety.Account.Model.View;
using ESafety.Core.Model;
using ESafety.Core.Model.DB.Account;
using System;
using System.Collections.Generic;

namespace ESafety.Account.IService
{
	public interface IDoc_CrewService
	{
        List<TypeConfig> GetMenus();

        Pager<DocCrewDTO> GetPageCrew(PagerQuery<Doc_Crew> page, Guid id);

    }
	
}


    