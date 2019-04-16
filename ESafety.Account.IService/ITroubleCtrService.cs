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
    public interface ITroubleCtrService/*:IBusinessFlowBase*/
    {
        /// <summary>
        /// 新建管控项
        /// </summary>
        /// <param name="ctrNew"></param>
        /// <returns></returns>
        ActionResult<bool> AddTroubleCtr(TroubleCtrNew ctrNew);
        /// <summary>
        /// 延长验收时间
        /// </summary>
        /// <param name="finishTime"></param>
        /// <returns></returns>
        ActionResult<bool>  DelayFinishTime(DelayFinishTime finishTime);
        /// <summary>
        /// 改变隐患等级
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        ActionResult<bool> ChangeLevel(ChangeLevel level);
        /// <summary>
        /// 删除管控项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<bool> DelTroubleCtr(Guid id);
        /// <summary>
        /// 根据ID获取管控模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<TroubleCtrView> GetTroubleCtr(Guid id);
        /// <summary>
        /// 分页获取管控模型
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<Pager<TroubleCtrView>> GetTroubleCtrs(PagerQuery<TroubleCtrQuery> para);
        /// <summary>
        /// 分页获取管控明细
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<Pager<TroubleCtrDetailView>> GetTroubleCtrDetails(PagerQuery<TroubleControlDetailQuery> para);
        /// <summary>
        /// 新建管控流程模型
        /// </summary>
        /// <param name="flowNew"></param>
        /// <returns></returns>
        ActionResult<bool> AddTroubleCtrFlow(TroubleCtrFlowNew flowNew);
        /// <summary>
        /// 根据管控ID获取管控流程列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<TroubleCtrFlowView>> GetTroubleCtrFlows(Guid id);
        /// <summary>
        /// 根据ID获取管控明细模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<TroubleCtrDetailView> GetTroubleCtrDetailModel(Guid id);

        /// <summary>
        /// 改变状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        ActionResult<bool> ChangeState(TroubleCtrChangeState state);


    }
}
