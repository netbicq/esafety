using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core;
using ESafety.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.IService
{
    public interface ITroubleCtrService:IBusinessFlowBase
    {
        ActionResult<bool> AddTroubleCtr(TroubleCtrNew ctrNew);

        ActionResult<bool>  DelayFinishTime(DelayFinishTime finishTime);

        ActionResult<bool> ChangeLevel(ChangeLevel level);

        ActionResult<bool> DelTroubleCtr(Guid id);

        ActionResult<TroubleCtrView> GetTroubleCtr(Guid id);

        ActionResult<Pager<TroubleCtrView>> GetTroubleCtrs(PagerQuery<TroubleCtrQuery> para);

        ActionResult<Pager<TroubleCtrDetailView>> GetTroubleCtrDetails(PagerQuery<TroubleControlDetailQuery> para);

        ActionResult<bool> AddTroubleCtrFlow(TroubleCtrFlowNew flowNew);
        ActionResult<IEnumerable<TroubleCtrFlowView>> GetTroubleCtrFlows(Guid id);

        ActionResult<TroubleCtrDetailView> GetTroubleCtrDetailModel(Guid id);

        /// <summary>
        /// 改变状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        ActionResult<bool> ChangeState(TroubleCtrChangeState state);


    }
}
