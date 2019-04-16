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
        /// <summary>
        /// APP端获取预案列表
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<PhoneDocSolutionView>> GetDocSolutionList(Guid DangerLevelID);
        /// <summary>
        /// APP端获取预案模型
        /// </summary>
        /// <returns></returns>
        ActionResult<PhoneDocSolutionModelView> GetDocSolutionModel(Guid id);
    }
}
