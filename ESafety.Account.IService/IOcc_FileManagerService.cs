

using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
using ESafety.Core.Model.DB.Account;
using System;
using System.Collections.Generic;

namespace ESafety.Account.IService
{
    /// <summary>
    /// 档案管理
    /// </summary>
	public interface IOcc_FileManagerService
	{

        ActionResult<Pager<DocCrewView>> GetRegimeData(PagerQuery<Guid> pagerQuery);

        ActionResult<bool> DeleteDocCrewById(Guid guid);

        ActionResult<bool> IncreaseCrew(Doc_Crew doc_);

        ActionResult<bool> AmendCrew(AmendCrew amend);

        ActionResult<Pager<DocQualView>> GetQualData(PagerQuery<Guid> request);

        ActionResult<bool> DeleteQualById(Guid guid);

        ActionResult<bool> IncreaseQual(Doc_Qualification qual);

        ActionResult<bool> AmendQual(AmendQual amend);
    }
	
}


    