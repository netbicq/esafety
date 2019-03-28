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
    /// 作业申请单
    /// </summary>
    [RoutePrefix("api/opreatebill")]
    public class OpreateBillController : ESFAPI
    {

        private IOpreateBill bll = null;

        public OpreateBillController(IOpreateBill bill)
        {
            bll = bill;
            BusinessService = bill;
        }
        /// <summary>
        /// 新建作业申请单
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addnew")]
        public ActionResult<bool> AddNew(OperateBillNew bill)
        {
            LogContent = "新建作业申请单，参数源：" + JsonConvert.SerializeObject(bill);
            return bll.AddNew(bill);
        }
        /// <summary>
        /// 业务单据审核
        /// </summary>
        /// <param name="businessid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("approve/{businessid:Guid}")]
        public ActionResult<bool> Approve(Guid businessid)
        {
            LogContent = "审核业务单据，单据id:" + businessid.ToString();
            return bll.Approve(businessid);

        }
        /// <summary>
        /// 删除业务单据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route ("delbill/{id:Guid}")]
        public ActionResult<bool> DelBill(Guid id)
        {
            LogContent = "删除作业申请单，单据id:" + id.ToString();
            return bll.DelBill(id);
        }
        /// <summary>
        /// 修改作业申请单
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editbill")]
        public ActionResult<bool> EditBill(OpreateBillEdit bill)
        {
            LogContent = "修改业务单据,参数源：" + JsonConvert.SerializeObject(bill);
            return bll.EditBill(bill);
        }
        /// <summary>
        /// 获取作业申请单列表
        /// </summary>
        /// <param name="Para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getlist")]
        public ActionResult<Pager<OpreateBillModel>> GetList(PagerQuery<string> Para)
        {
            return bll.GetList(Para);
        }
        /// <summary>
        /// 获取作业申请单模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getmodel/{id:Guid}")]
        public ActionResult<OpreateBillModel> GetModel(Guid id)
        {
            return bll.GetModel(id);
        }
        /// <summary>
        /// 发起作业申请单的审批流程
        /// </summary>
        /// <param name="businessid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("startflow/{businessid:Guid}")]
        public ActionResult<bool> StartBillFlow(Guid businessid)
        {
            LogContent = "发起作业申请单审批，单据id:" + businessid.ToString();
            return bll.StartBillFlow(businessid);
            
        }
        /// <summary>
        /// 获取带作业节点的作业单模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getbillflowmodel/{id:Guid}")]
        public ActionResult<OpreateBillFlowModel> GetBillFlowModel(Guid id)
        {
            return bll.GetBillFlowModel(id);
        }
        /// <summary>
        /// 处理作业单流程节点
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("billflowset")]
        public ActionResult<bool> BillFlowSet(Model.PARA.OpreateBillFlowResult flow)
        {
            LogContent = "处理了作业节点，参数源：" + JsonConvert.SerializeObject(flow);
            return bll.FlowResult(flow);
        }
    }
}
