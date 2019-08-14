using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.PARA;
using ESafety.Core.Model.View;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core
{
    public interface IFlow
    {
        /// <summary>
        /// 新建流程Mastor
        /// </summary>
        /// <param name="flowMasterNew"></param>
        /// <returns></returns>
        ActionResult<bool> AddFlowMaster(FlowMasterNew flowMasterNew);
        /// <summary>
        /// 修改流程Mastor
        /// </summary>
        /// <param name="flowMasterEdit"></param>
        /// <returns></returns>
        ActionResult<bool> EditFlowMaster(FlowMasterEdit flowMasterEdit);
        /// <summary>
        /// 删除流程Mastor
        /// </summary>
        /// <param name="masterID"></param>
        /// <returns></returns>
        ActionResult<bool> DelFlowMaster(Guid masterID);
        /// <summary>
        /// 分页获取流程Mastor 根据业务类型
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ActionResult<Pager<FlowMasterView>> GetFlowMastersPage(PagerQuery<int> query);
        /// <summary>
        /// 根据业务类型获取Master选择器
        /// </summary>
        /// <param name="businessType"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<FlowMasterSelector>> GetFlowMasterSelector(PublicEnum.EE_BusinessType businessType);
        /// <summary>
        /// 新建审批节点
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        ActionResult<bool> AddFlowPoint(Flow_PointsNew point);
        /// <summary>
        /// 删除审批节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<bool> DelFlowPoint(Guid id);
        /// <summary>
        /// 修改审批节点
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        ActionResult<bool> EditFlowPoint(Flow_PointsEdit point);
        /// <summary>
        /// 新建审批用户
        /// </summary>
        /// <param name="pointuser"></param>
        /// <returns></returns>
        ActionResult<bool> AddPointUser(Flow_PointUsersNew pointuser);
        /// <summary>
        /// 删除审批用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<bool> DelPointUser(Guid id);
        /// <summary>
        /// 修改审批用户
        /// </summary>
        /// <param name="pointuser"></param>
        /// <returns></returns>
        ActionResult<bool> EditPointUser(Flow_PointUserEdit pointuser);
        /// <summary>
        /// 获取业务单据类型集合
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<EnumItem>> GetBusinessTypes();
        /// <summary>
        /// 根据业务类型获取和MasterID，分页获取审批节点集合
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ActionResult<Pager<Flow_PointView>> GetPointsByBusinessType(PagerQuery<FlowPointQuery> query);
        /// <summary>
        /// 根据id获取审批节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<Flow_PointView> GetPointModel(Guid id);
        /// <summary>
        /// 根据id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<Point_UsersView> GetPointUser(Guid id);
        /// <summary>
        /// 根据审批节点获取审批用户集合
        /// </summary>
        /// <param name="pointid"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<Point_UsersView>> GetPointUsers(Guid pointid);
        /// <summary>
        /// 业务单据发起审批，返回审批版本号
        /// 如果返回 -1则表示未设置审批流程
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        ActionResult<Flow_Task> InitTask(InitTask task);
        /// <summary>
        /// 检查业务是否需要审批流程
        /// </summary>
        /// <param name="masterID"></param>
        /// <returns></returns>
        ActionResult<bool> CheckBusinessFlow(Guid masterID);
        /// <summary>
        /// 审批任务审批
        /// </summary>
        /// <param name="approve"></param>
        /// <returns></returns>
        ActionResult<PublicEnum.EE_FlowApproveResult> Approve(Approve approve);
        /// <summary>
        /// 审批撤回
        /// </summary>
        /// <param name="recall"></param>
        /// <returns></returns>
        ActionResult<bool> FlowRecall(FlowRecall recall);

        /// <summary>
        /// 获取我的待审批
        /// </summary>
        /// <returns></returns>
        ActionResult<Pager<Flow_TaskView>> GetMyTask(PagerQuery<string> para);
        /// <summary>
        /// 获取我发起的审批
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<Pager<Flow_ResultView>> GetMyStart(PagerQuery<string> para);
        /// <summary>
        /// 获取我审批过的单据
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<Pager<Flow_ResultView>> GetMyResult(PagerQuery<string> para);
        /// <summary>
        /// 获取审批日志
        /// </summary>
        /// <param name="businessid"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<FlowLogView>> GetFlowLog(Guid businessid);

      
    }
}
