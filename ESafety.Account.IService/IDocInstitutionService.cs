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
    /// <summary>
    /// 安全制度
    /// </summary>
    public interface IDocInstitutionService
    {
        ActionResult<bool> AddDocInstitution(DocInstitutionNew  institutionNew);
        ActionResult<bool> DelDocInstitution(Guid id);
        ActionResult<bool> EditDocInstitution(DocInstitutionEdit institutionEdit);
        ActionResult<DocInstitutionView> GetDocInstitutionModel(Guid id);
        ActionResult<Pager<DocInstitutionView>> GetDocInstitutions(PagerQuery<DocInstitutionQuery> para);
    }
}
