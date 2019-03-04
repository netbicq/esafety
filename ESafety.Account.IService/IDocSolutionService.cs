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
    /// 紧急预案
    /// </summary>
    public interface IDocSolutionService
    {
        ActionResult<bool> AddDocSolution(DocSolutionNew solutionNew);
        ActionResult<bool> DelDocSolution(Guid id);
        ActionResult<bool> EditDocSolution(DocSolutionEdit solutionEdit);
        ActionResult<DocSolutionView> GetDocSolution(Guid id);
        ActionResult<Pager<DocSolutionView>> GetDocSolutions(PagerQuery<DocSolutionQuery> para);
    }
}
