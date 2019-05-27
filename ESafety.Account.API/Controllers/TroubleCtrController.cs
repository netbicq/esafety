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
    public class TroubleCtrController : ESFAPI
    {
        private ITroubleCtrService bll = null;
        public TroubleCtrController(ITroubleCtrService ctr)
        {
            bll = ctr;
            BusinessServices =new List<object>() { ctr };
        }
        ///// <summary>
        ///// 新建隐患管控模型
        ///// </summary>
        ///// <param name="ctrNew"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("addtc")]
        //public ActionResult<bool> AddTroubleCtr(TroubleCtrNew ctrNew)
        //{
        //    LogContent = "新建了管控模型，参数源：" + JsonConvert.SerializeObject(ctrNew);
        //    return bll.AddTroubleCtr(ctrNew);
        //}
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
        ///// <summary>
        ///// 隐患管控审核
        ///// </summary>
        ///// <param name="businessid"></param>
        ///// <returns></returns>
        //[Route("approve/{businessid:Guid}")]
        //[HttpGet]
        //public ActionResult<bool> Approve(Guid businessid)
        //{
        //    LogContent = "审核了隐患管控，id:" + businessid.ToString();
        //    return bll.Approve(businessid);
        //}
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
        ///  归档
        /// </summary>
        /// <param name="ctrID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("filed/{ctrID:Guid}")]
        public ActionResult<bool> Filed(Guid ctrID)
        {
            LogContent = "隐患管控归档，参数源:" + JsonConvert.SerializeObject(ctrID);
            return bll.Filed(ctrID);
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
        ///// <summary>
        ///// 删除隐患管控模型
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("deltc/{id:Guid}")]
        //public ActionResult<bool> DelTroubleCtr(Guid id)
        //{
        //    LogContent = "删除了隐患管控模型，ID为：" + id.ToString();
        //    return bll.DelTroubleCtr(id);
        //}
        /// <summary>
        /// 获取隐患管控模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("gettc/{id:Guid}")]
        public ActionResult<TroubleCtrModel> GetTroubleCtr(Guid id)
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
        [HttpPost]
        [Route("gettcp")]
        public ActionResult<Pager<TroubleCtrView>> GetTroubleCtrs(PagerQuery<TroubleCtrQuery> para)
        {
            return bll.GetTroubleCtrs(para);
        }
        ///// <summary>
        ///// 发起流程
        ///// </summary>
        ///// <param name="billid"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("startf/{billid:Guid}")]
        //public ActionResult<bool> StartBillFlow(Guid billid)
        //{
        //    LogContent = "发起审批，业务id:" + billid.ToString();
        //    return bll.StartBillFlow(billid);
        //}
        /// <summary>
        /// 处理管控项
        /// </summary>
        /// <param name="handleTrouble"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("handleCtr")]
        public ActionResult<bool> HandleCtr(HandleTroubleCtr handleTrouble)
        {
            LogContent = "处理管控项，参数源:" + JsonConvert.SerializeObject(handleTrouble);
            return bll.HandleCtr(handleTrouble);
        }

        /// <summary>
        /// 转让责任人
        /// </summary>
        /// <param name="transferTrouble"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("transferPrincipal")]
        public ActionResult<bool> TransferPrincipal(TransferTroublePrincipal transferTrouble)
        {
            LogContent = "转让责任人，参数源:" + JsonConvert.SerializeObject(transferTrouble);
            return bll.TransferPrincipal(transferTrouble);
        }

        /// <summary>
        /// 改变风险等级
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("changeDangerLevel")]
        public ActionResult<bool> ChangeDangerLevel(ChangeDangerLevel level)
        {
            LogContent = "改变风险等级，参数源:" + JsonConvert.SerializeObject(level);
            return bll.ChangeDangerLevel(level);
        }

        /// <summary>
        /// 快速处理
        /// </summary>
        /// <param name="quickHandleTrouble"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("quickHandleCtr")]
        public ActionResult<bool> QuickHandleCtr(QuickHandleTroubleCtr quickHandleTrouble)
        {
            LogContent = "快速处理，参数源:" + JsonConvert.SerializeObject(quickHandleTrouble);
            return bll.QuickHandleCtr(quickHandleTrouble);
        }

    }
}
