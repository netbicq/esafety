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
    /// 移动端api
    /// </summary>
    [RoutePrefix("api/app")]
    public class APPController : ESFAPI
    {

        private IInspectTask spectbll;
        private ITaskBillService billbll;


        public APPController(IInspectTask spectask, ITaskBillService taskbill)
        {

            spectbll = spectask;
            billbll = taskbill;
            BusinessService = taskbill;

        }
        /// <summary>
        /// 新建任务单
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addbill")]
        public ActionResult<bool> AddTaskBill(TaskBillNew bill)
        {
            LogContent = "新建任务单，参数源：" + JsonConvert.SerializeObject(bill);
            return billbll.AddTaskBillMaster(bill);
        }
        /// <summary>
        /// 获取当前用户的任务列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("gettasklist")]
        public ActionResult<IEnumerable<InsepctTaskByEmployee>> GetTaskList()
        {
            return spectbll.GetTaskListByEmployee();
        }
        /// <summary>
        /// 获取当前用户超期任务列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("gettimetask")]
        public ActionResult<IEnumerable<InsepctTaskByEmployee>> GetTimeOutTaskList()
        {
            return spectbll.GetTaskListByTimeOut();
        }

        /// <summary>
        /// 新建任务主体检查结果
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addtasksubject")]
        public ActionResult<bool> AddTaskBillSubject(TaskBillSubjectNew subject)
        {
            LogContent = "新建任务的主体检查，参数源：" + JsonConvert.SerializeObject(subject);
            return billbll.AddTaskSubject(subject);
        }
        /// <summary>
        /// 根据任务单id获取待检查主体
        /// </summary>
        /// <param name="billid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getsubjects/{billid:Guid}")]
        public ActionResult<IEnumerable<TaskSubjectsByTask>> GetTaskSubjectByTask(Guid billid)
        {
            return billbll.GetTaskSubjects(billid);
        }

        
    }
}
