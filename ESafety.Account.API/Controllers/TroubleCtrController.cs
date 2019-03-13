using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
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
    /// 隐患管控
    /// </summary>
    [RoutePrefix("api/troublectr")]
    public class TroubleCtrController : ESFAPI, ITroubleCtrService
    {
        private ITroubleCtrService bll = null;
        public TroubleCtrController(ITroubleCtrService ctr)
        {
            bll = ctr;
            BusinessService = ctr;
        }
        /// <summary>
        /// 新建隐患管控模型
        /// </summary>
        /// <param name="ctrNew"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addtc")]
        public ActionResult<bool> AddTroubleCtr(TroubleCtrNew ctrNew)
        {
            LogContent = "新建了管控模型，参数源：" + JsonConvert.SerializeObject(ctrNew);
            return bll.AddTroubleCtr(ctrNew);
        }
        /// <summary>
        /// 新建隐患管控日志模型
        /// </summary>
        /// <param name="flowNew"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addtcf")]
        public ActionResult<bool> AddTroubleCtrFlow(TroubleCtrFlowNew flowNew)
        {
            LogContent = "新建了管控日志模型，参数源:" + JsonConvert.SerializeObject(flowNew);
            return bll.AddTroubleCtrFlow(flowNew);
        }
        /// <summary>
        /// 隐患管控审核
        /// </summary>
        /// <param name="businessid"></param>
        /// <returns></returns>
        [Route("approve/{businessid:Guid}")]
        [HttpGet]
        public ActionResult<bool> Approve(Guid businessid)
        {
            LogContent = "审核了隐患管控，id:" + businessid.ToString();
            return bll.Approve(businessid);
        }
        /// <summary>
        /// 改变隐患等级,根据ID
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("changelevel")]
        public ActionResult<bool> ChangeLevel(ChangeLevel level)
        {
            LogContent = "修改了隐患管控的隐患等级，参数源：" + JsonConvert.SerializeObject(level);
            return bll.ChangeLevel(level);
        }
        /// <summary>
        /// 改变隐患管控状态,根据ID
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("changestate")]
        public ActionResult<bool> ChangeState(TroubleCtrChangeState state)
        {
            LogContent = "修改了隐患管控的隐患状态，参数源:" + JsonConvert.SerializeObject(state);
            return bll.ChangeState(state);
        }
        /// <summary>
        /// 延长隐患管控的完成时间
        /// </summary>
        /// <param name="finishTime"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delaytime")]
        public ActionResult<bool> DelayFinishTime(DelayFinishTime finishTime)
        {
            LogContent = "延长了隐患管控的完成时间，参数源:" + JsonConvert.SerializeObject(finishTime);
            return bll.DelayFinishTime(finishTime);
        }
        /// <summary>
        /// 删除隐患管控模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("deltc/{id:Guid}")]
        public ActionResult<bool> DelTroubleCtr(Guid id)
        {
            LogContent = "删除了隐患管控模型，ID为：" + id.ToString();
            return bll.DelTroubleCtr(id);
        }
        /// <summary>
        /// 获取隐患管控模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("gettc/{id:Guid}")]
        public ActionResult<TroubleCtrView> GetTroubleCtr(Guid id)
        {
            return bll.GetTroubleCtr(id);
        }
        /// <summary>
        /// 获取隐患管控详情模型，根据ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("gettcdm/{id:Guid}")]
        public ActionResult<TroubleCtrDetailView> GetTroubleCtrDetailModel(Guid id)
        {
            return bll.GetTroubleCtrDetailModel(id);
        }
        /// <summary>
        /// 分页获取管控详情
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("gettcds")]
        public ActionResult<Pager<TroubleCtrDetailView>> GetTroubleCtrDetails(PagerQuery<TroubleControlDetailQuery> para)
        {
            return bll.GetTroubleCtrDetails(para);
        }
        /// <summary>
        /// 根据隐患管控ID,获取隐患管控流程日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("gettcfs/{id:Guid}")]
        public ActionResult<IEnumerable<TroubleCtrFlowView>> GetTroubleCtrFlows(Guid id)
        {
            return bll.GetTroubleCtrFlows(id);
        }
        /// <summary>
        /// 分页获取隐患管控，关键字可用管控名称查询
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("gettcp")]
        public ActionResult<Pager<TroubleCtrView>> GetTroubleCtrs(PagerQuery<TroubleCtrQuery> para)
        {
            return bll.GetTroubleCtrs(para);
        }
        /// <summary>
        /// 发起流程
        /// </summary>
        /// <param name="billid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("startf/{billid:Guid}")]
        public ActionResult<bool> StartBillFlow(Guid billid)
        {
            LogContent = "发起审批，业务id:" + billid.ToString();
            return bll.StartBillFlow(billid);
        }
    }
}
