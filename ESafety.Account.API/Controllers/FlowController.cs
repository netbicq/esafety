using ESafety.Core;
using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.PARA;
using ESafety.Core.Model.View;
using ESafety.ORM;
using ESafety.Unity;
using ESafety.Web.Unity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ESafety.Account.API.Controllers
{
    /// <summary>
    /// 审批流程
    /// </summary>
    [RoutePrefix("api/flow")]
    public class FlowController : ESFAPI
    {
        private IFlow bll = null;
         
        public FlowController(IFlow flow)
        {

            bll = flow;
            BusinessServices =new List<object>() { flow };
        }
        /// <summary>
        /// 新建审批流程节点
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("newpoint")]
        public ActionResult<bool> AddFlowPoint(Flow_PointsNew point)
        {
            LogContent = "新建审批节点，参数源：" + JsonConvert.SerializeObject(point);
            return bll.AddFlowPoint(point);
        }
        /// <summary>
        /// 新建审批用户
        /// </summary>
        /// <param name="pointuser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("newpointuser")]
        public ActionResult<bool> AddPointUser(Flow_PointUsersNew pointuser)
        {
            LogContent = "新建审批用户，参数源:" + JsonConvert.SerializeObject(pointuser);
            return bll.AddPointUser(pointuser);
        }
        /// <summary>
        /// 业务单据审批
        /// </summary>
        /// <param name="approve"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("approve")]
        public ActionResult<PublicEnum.EE_FlowApproveResult> Approve(Approve approve)
        {
            LogContent = "审批了业务单据，参数源：" + JsonConvert.SerializeObject(approve);
            
            return bll.Approve(approve);
        }
         
        /// <summary>
        /// 删除指定ID的审批节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delpoint/{id:Guid}")]
        public ActionResult<bool> DelFlowPoint(Guid id)
        {
            LogContent = "删除了审批节点，节点id" + id.ToString();
            return bll.DelFlowPoint(id);
        }
        /// <summary>
        /// 删除指定id的审批用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delpointuser/{id:Guid}")]
        public ActionResult<bool> DelPointUser(Guid id)
        {
            LogContent = "删除了审批用户，ID" + id.ToString();
            return bll.DelPointUser(id);
        }
        /// <summary>
        /// 修改审批节点
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editpoint")]
        public ActionResult<bool> EditFlowPoint(Flow_PointsEdit point)
        {
            LogContent = "修改了审批节点，参数源：" + JsonConvert.SerializeObject(point);

            return bll.EditFlowPoint(point);
        }
        /// <summary>
        /// 修改审批用户
        /// </summary>
        /// <param name="pointuser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editpointuser")]
        public ActionResult<bool> EditPointUser(Flow_PointUserEdit pointuser)
        {
            LogContent = "修改了审批用户，参数源：" + JsonConvert.SerializeObject(pointuser);

            return bll.EditPointUser(pointuser);
        }
        /// <summary>
        /// 撤消正在审批的单据
        /// </summary>
        /// <param name="recall"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("recall")]
        public ActionResult<bool> FlowRecall(FlowRecall recall)
        {
            LogContent = "撤回了正在审批的业务单据，参数源：" + JsonConvert.SerializeObject(recall);
            return bll.FlowRecall(recall);
        }
        /// <summary>
        /// 获取所有审批业务类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getbusinesstype")]
        public ActionResult<IEnumerable<EnumItem>> GetBusinessTypes()
        {
            return bll.GetBusinessTypes();
        }
        /// <summary>
        /// 获取审批日志
        /// </summary>
        /// <param name="businessid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getlogs/{businessid:Guid}")]
        public ActionResult<IEnumerable<FlowLogView>> GetFlowLog(Guid businessid)
        {
            return bll.GetFlowLog(businessid);
        }
        /// <summary>
        /// 获取我的审批
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("myresult")]
        public ActionResult<Pager<Flow_ResultView>> GetMyResult(PagerQuery<string> para)
        {
            return bll.GetMyResult(para);
        }

        /// <summary>
        /// 获取我的申请
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("mystart")]
        public ActionResult<Pager<Flow_ResultView>> GetMyStart(PagerQuery<string> para)
        {
            return bll.GetMyStart(para);
        }
        /// <summary>
        /// 获取待审批
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("mytask")]
        public ActionResult<Pager<Flow_TaskView>> GetMyTask(PagerQuery<string> para)
        {
            return bll.GetMyTask(para);
        }

        /// <summary>
        /// 获取指定id的审批节点模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getpointmodel/{id:Guid}")]
        public ActionResult<Flow_PointView> GetPointModel(Guid id)
        {
            return bll.GetPointModel(id);

        }
        /// <summary>
        /// 根据业务类型获取和MasterID，分页获取审批节点集合
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getpoints")]
        public ActionResult<Pager<Flow_PointView>> GetPointsByBusinessType(PagerQuery<FlowPointQuery> query)
        {
            return bll.GetPointsByBusinessType(query);
        }
        /// <summary>
        /// 获取指定id的审批用户模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getpointuser/{id:Guid}")]
        public ActionResult<Point_UsersView> GetPointUser(Guid id)
        {
            return bll.GetPointUser(id);

        }
        /// <summary>
        /// 根据审批节点id获取该节点下所有的审批用户
        /// </summary>
        /// <param name="pointid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getpointusers/{pointid:Guid}")]
        public ActionResult<IEnumerable<Point_UsersView>> GetPointUsers(Guid pointid)
        {
            return bll.GetPointUsers(pointid);
        }
        /// <summary>
        /// 新建流程Master
        /// </summary>
        /// <param name="flowMasterNew"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addFlowMaster")]
        public ActionResult<bool> AddFlowMaster(FlowMasterNew flowMasterNew)
        {
            LogContent = "新建Master，参数源:" + JsonConvert.SerializeObject(flowMasterNew);
            return bll.AddFlowMaster(flowMasterNew);
        }
        /// <summary>
        /// 修改流程Master
        /// </summary>
        /// <param name="flowMasterEdit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editFlowMaster")]
        public ActionResult<bool> EditFlowMaster(FlowMasterEdit flowMasterEdit)
        {
            LogContent = "修改Master，参数源:" + JsonConvert.SerializeObject(flowMasterEdit);
            return bll.EditFlowMaster(flowMasterEdit);
        }
        /// <summary>
        /// 删除流程Master
        /// </summary>
        /// <param name="masterID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delFlowMaster/{masterID:Guid}")]
        public ActionResult<bool> DelFlowMaster(Guid masterID)
        {
            LogContent = "删除了Master，ID:" + JsonConvert.SerializeObject(masterID);
            return bll.DelFlowMaster(masterID);
        }
        /// <summary>
        /// 根据业务类型分页获取Master,默认为0
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getFlowMastersPage")]
        public ActionResult<Pager<FlowMasterView>> GetFlowMastersPage(PagerQuery<int> query)
        {
            return bll.GetFlowMastersPage(query);
        }
        /// <summary>
        /// 根据业务类型获取Master选择器
        /// </summary>
        /// <param name="businessType"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getFlowMasterSelector/{businessType:Int}")]
        public ActionResult<IEnumerable<FlowMasterSelector>> GetFlowMasterSelector(PublicEnum.EE_BusinessType businessType)
        {
            return bll.GetFlowMasterSelector(businessType);
        }
    }
}
