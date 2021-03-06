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
        ///// <summary>
        ///// 新建管控项
        ///// </summary>
        ///// <param name="ctrNew"></param>
        ///// <returns></returns>
        //ActionResult<bool> AddTroubleCtr(TroubleCtrNew ctrNew);
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
        /// 改变风险等级
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        ActionResult<bool> ChangeDangerLevel(ChangeDangerLevel level);
        ///// <summary>
        ///// 删除管控项
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //ActionResult<bool> DelTroubleCtr(Guid id);
        /// <summary>
        /// 根据ID获取管控模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<TroubleCtrModel> GetTroubleCtr(Guid id);
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
        /// 归档
        /// </summary>
        /// <param name="ctrID"></param>
        /// <returns></returns>
        ActionResult<bool> Filed(Guid ctrID);
        /// <summary>
        /// 手机端获取管控信息
        /// </summary>
        ActionResult<IEnumerable<APPTroubleCtrView>> GetTroubleCtr();
        /// <summary>
        /// 处理管控项
        /// </summary>
        /// <param name="handleTrouble"></param>
        /// <returns></returns>
        ActionResult<bool> HandleCtr(HandleTroubleCtr handleTrouble);
        /// <summary>
        /// 快速处理管控项
        /// </summary>
        /// <returns></returns>
        ActionResult<bool> QuickHandleCtr(QuickHandleTroubleCtr quickHandleTrouble);
        /// <summary>
        /// 转让责任人
        /// </summary>
        /// <returns></returns>
        ActionResult<bool> TransferPrincipal(TransferTroublePrincipal transferTrouble);
        /// <summary>
        /// APP获取管控菜单与数量
        /// </summary>
        ActionResult<IEnumerable<TroubleCtrMenu>> GetCtrMenu();
        /// <summary>
        /// APP 分页获取管控列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ActionResult<Pager<TroubleCtrsPage>> GetTroubleCtrsPage(PagerQuery<int> query);

    }
}
