using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
using ESafety.ORM;
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
            BusinessServices = new List<object> { taskbill, spectask };            
            
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
        public ActionResult<IEnumerable<TaskSubjectView>> GetTaskSubjectByTask(Guid billid)
        {
            return billbll.GetTaskSubjects(billid);
        }
        /// <summary>
        /// 完成任务单据
        /// </summary>
        /// <param name="billid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("taskbillover/{billid:Guid}")]
        public ActionResult<bool> TaskBillOver(Guid billid)
        {
            LogContent = "任务单据已完成检查，ID为：" + JsonConvert.SerializeObject(billid);
            return billbll.TaskBillOver(billid);
        }
        /// <summary>
        /// 删除任务单据
        /// </summary>
        /// <param name="billid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("deltaskbill/{billid:Guid}")]
        public ActionResult<bool> DelTaskBillMaster(Guid billid)
        {
            LogContent = "删除了任务单据，ID为：" + JsonConvert.SerializeObject(billid);
            return billbll.DelTaskBillMaster(billid);
        }
        /// <summary>
        /// 获取当前任务单据列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("gettaskbills")]
        public ActionResult<IEnumerable<TaskBillModel>> GetTaskBillMasters()
        {
            return billbll.GetTaskBillMasters();
        }

        /// <summary>
        /// 获取历史任务单据列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("gettaskbillsover")]
        public ActionResult<IEnumerable<TaskBillModel>> GetTaskBillMastersOver()
        {
            return billbll.GetTaskBillMastersOver();
        }

        /// <summary>
        /// 根据任务单id获取已检查了的主体的集合
        /// </summary>
        /// <param name="taskbillid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("gettasksubover/{taskbillid:Guid}")]
        public ActionResult<IEnumerable<TaskSubjectOverView>> GetTaskSubjectsOver(Guid taskbillid)
        {
            return billbll.GetTaskSubjectsOver(taskbillid);
        }
        /// <summary>
        /// 根据结果ID，删除检查结果
        /// </summary>
        /// <param name="subresultid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delsubresult/{subresultid:Guid}")]
        public ActionResult<bool> DelSubResult(Guid subresultid)
        {
            LogContent = "删除了主体检查结果，ID为：" + JsonConvert.SerializeObject(subresultid);
            return billbll.DelSubResult(subresultid);
        }

        /// <summary>
        /// 获取检查结果模型
        /// </summary>
        /// <param name="subresultid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getsubresult/{subresultid:Guid}")]
        public ActionResult<SubResultView> GetSubResultModel(Guid subresultid)
        {
            return billbll.GetSubResultModel(subresultid);
        }
    }
}
