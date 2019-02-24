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
    public interface IOpreationFlowService
    {
        ActionResult<bool> AddOpreation(OpreationNew opreation);
        ActionResult<bool> DelOpreation(Guid id);
        ActionResult<bool> EditOpreation(OpreationEdit opreation);
        ActionResult<IEnumerable<OpreationView>> GetOpreations();
        ActionResult<Pager<OpreationView>> GetOpreationPage(PagerQuery<OpreationQuery> para);

        ActionResult<OpreationView> GetOpreation(Guid id);
 
        ActionResult<bool> AddOpreationFlow(OpreationFlowNew flowNew);

        ActionResult<bool> DelOpreationFlow(Guid id);

        ActionResult<IEnumerable<OpreationFlowView>> GetOpreationFlows(Guid id);
    }
}
