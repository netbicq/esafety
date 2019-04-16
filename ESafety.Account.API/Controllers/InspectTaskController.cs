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
    /// 巡检任务
    /// </summary>
    [RoutePrefix("api/insptask")]
    public class InspectTaskController : ESFAPI
    {
        private IInspectTask bll = null;


        public InspectTaskController(IInspectTask intask)
        {

            bll = intask;
            BusinessServices =new List<object>() { intask };

        }
        /// <summary>
        /// 新建巡检任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [Route("addnew")]
        [HttpPost]
        public ActionResult<bool> AddNew(InspectTaskNew task)
        {
            LogContent = "新建巡检任务，参数源：" + JsonConvert.SerializeObject(task);
            return bll.AddNew(task);

        }

        /// <summary>
        /// 任务审核
        /// </summary>
        /// <param name="businessid"></param>
        /// <returns></returns>
        [Route("approve/{businessid:Guid}")]
        [HttpGet]
        public ActionResult<bool> Approve(Guid businessid)
        {
            LogContent = "审核了巡检任务，id:" + businessid.ToString();
            return bll.Approve(businessid);

        }

        /// <summary>
        /// 改变任务状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("changestate")]
        public ActionResult<bool> ChangeState(InspectTaskChangeState state)
        {
            LogContent = "改变任务状态，参数源：" + JsonConvert.SerializeObject(state);
            return bll.ChangeState(state);
        }
        /// <summary>
        /// 删除指定ID的巡检任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("deltask/{id:Guid}")]
        public ActionResult<bool> DelTask(Guid id)
        {
            LogContent = "删除巡检任务,id:" + id.ToString();
            return bll.DelTask(id);
        }

        /// <summary>
        /// 修改巡检任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [Route("edtittask")]
        [HttpPost]
        public ActionResult<bool> EditTask(InspectTaskEdit task)
        {
            LogContent = "修改巡检任务，参数源:" + JsonConvert.SerializeObject(task);
            return bll.EditTask(task);
        }
        /// <summary>
        /// 获取巡检任务模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getmodel/{id:Guid}")]
        public ActionResult<InspectTaskModelView> GetModel(Guid id)
        {
            return bll.GetModel(id);
        }

        /// <summary>
        /// 获取巡检任务列表
        /// </summary>
        /// <param name="qurey"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("gettasks")]
        public ActionResult<Pager<InspectTaskView>> GetTasks(PagerQuery<InspectTaskQuery> qurey)
        {

            return bll.GetTasks(qurey);

        }
        /// <summary>
        /// 获取临时任务列表
        /// </summary>
        /// <param name="qurey"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("gettemptasks")]
        public ActionResult<Pager<InspectTempTaskView>> GetTempTasks(PagerQuery<InspectTaskQuery> qurey)
        {
            return bll.GetTempTasks(qurey);
        }
        /// <summary>
        /// 根据巡检任务id获取任务主体明细集合
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getsubjects/{taskid:Guid}")]
        public ActionResult<IEnumerable<InspectTaskSubjectView>> GetTaskSubjects(Guid taskid)
        {
            return bll.GetTaskSubjects(taskid);
        }

        /// <summary>
        /// 发起流程
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("startflow/{taskid:Guid}")]
        public ActionResult<bool> StartFlow(Guid taskid)
        {
            LogContent = "发起审批，业务id:" + taskid.ToString();
            return bll.StartBillFlow(taskid);
        }
        /// <summary>
        /// 为临时任务分配执行人员
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("allottempemp")]
        public ActionResult<bool> AllotTempTaskEmp(AllotTempTaskEmp emp)
        {
            LogContent = "为临时任务分配了执行人员，参数源:" + JsonConvert.SerializeObject(emp);
            return bll.AllotTempTaskEmp(emp);
        }
    }
}
