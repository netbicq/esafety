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
    public interface IDangerSetService
    {
        ActionResult<bool> AddDangerRelation(DangerRelationNew dangerRelation);
        ActionResult<bool> DelDangerRelation(Guid id);

        ActionResult<Pager<DangerRelationView>> GetDangerRelations(PagerQuery<DangerRelationQuery> para);
    }
}
