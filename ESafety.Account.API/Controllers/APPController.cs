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
        private IOpreateBill opreatebll;
        private IDocInstitutionService docinsbll;
        private IDocSolutionService docssbll;
        private IVideoService videobll;
        private ITroubleCtrService ctrbll;
        private IDangerPointService dpbll;

        public APPController(IInspectTask spectask, ITaskBillService taskbill,
                               IOpreateBill opreatebill,IDocInstitutionService docins,
                               IDocSolutionService docss,IVideoService video,
                               ITroubleCtrService ctrService,IDangerPointService dangerPoint)
        {

            spectbll = spectask;
            billbll = taskbill;
            opreatebll = opreatebill;
            docinsbll = docins;
            docssbll = docss;
            videobll = video;
            ctrbll = ctrService;
            dpbll = dangerPoint;
            BusinessServices = new List<object> { taskbill, spectask,opreatebill, docins, docss,video,ctrService,dangerPoint};            
            
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
        /// 获取当前用户临时任务列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("gettemptask")]
        public ActionResult<IEnumerable<InsepctTempTaskByEmployee>> GetTempTaskListByEmployee()
        {
            return spectbll.GetTempTaskListByEmployee();
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
        /// <summary>
        /// 下载数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("downloaddata")]
        public ActionResult<DownloadData> DownloadData()
        {
            return billbll.DownloadData();
        }

        /// <summary>
        /// 根据作业单ID，获取带处理节点的单据模型
        /// </summary>
        /// <param name="opreateid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getopreateflowmodel/{opreateid:Guid}")]
        public ActionResult<OpreateBillFlowModel> GetBillFlowModel(Guid opreateid)
        {
            return opreatebll.GetBillFlowModel(opreateid);
        }
        /// <summary>
        /// 获取当前人能做的作业单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getcurrentlist")]
        public ActionResult<IEnumerable<OpreateBillByEmp>> GetCurrentList()
        {
            return opreatebll.GetCurrentList();
        }

        /// <summary>
        /// 获取当前人已做的作业单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getoverlist")]
        public ActionResult<IEnumerable<OpreateBillByEmp>> GetOverList()
        {
            return opreatebll.GetOverList();
        }
        /// <summary>
        /// 处理作业单流程节点
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("billflowset")]
        public ActionResult<bool> FlowResult(OpreateBillFlowResult flow)
        {
            LogContent = "处理了作业节点，参数源：" + JsonConvert.SerializeObject(flow);
            return opreatebll.FlowResult(flow);
        }
        /// <summary>
        /// 获取所有制度
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getdocinslist")]
        public ActionResult<IEnumerable<PhoneDocInstitutionView>> GetDocInstitutionsList()
        {
            return docinsbll.GetDocInstitutionsList();
        }
        /// <summary>
        /// 根据制度ID获取制度
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getdocins/{insid:Guid}")]
        public ActionResult<PhoneDocInstitutionModelView> GetDocInstitution(Guid insid)
        {
            return docinsbll.GetDocInstitution(insid);
        }

        ///// <summary>
        ///// 根据预案ID获取预案详情
        ///// </summary>
        ///// <param name="docsolutionid"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("getdocsolutionmodel/{docsolutionid:Guid}")]
        //public ActionResult<PhoneDocSolutionModelView> GetDocSolutionModel(Guid docsolutionid)
        //{
        //    return docssbll.GetDocSolutionModel(docsolutionid);
        //}
        ///// <summary>
        ///// 根据风险等级ID获取预案列表,默认值为00000000-0000-0000-0000-000000000000
        ///// </summary>
        ///// <param name="dangerlevelid"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("getdocsolutionlist/{dangerlevelid:Guid}")]
        //public ActionResult<IEnumerable<PhoneDocSolutionView>> GetDocSolutionList(Guid dangerlevelid)
        //{
        //    return docssbll.GetDocSolutionList(dangerlevelid);
        //}
        /// <summary>
        /// 获取摄像头列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getvideolist")]
        public ActionResult<IEnumerable<VideoView>> GetVideoList()
        {
            return videobll.GetVideoList();
        }
        /// <summary>
        /// 移动端新建临时任务
        /// </summary>
        /// <param name="temptask"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addtemptask")]
        public ActionResult<bool> AddTempTask(AddTempTask temptask)
        {
            return spectbll.AddTempTask(temptask);
        }

        /// <summary>
        /// 获取选择器信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getselector")]
        public ActionResult<TempTaskSelector> GetTempTaskSelector()
        {
            return spectbll.GetTempTaskSelector();
        }
        /// <summary>
        /// 获取风控项选择器信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("getdangerselector")]
        public ActionResult<IEnumerable<Sub>> GetTempTaskDangerSelector(TempTaskDangerSelect select)
        {
            return spectbll.GetTempTaskDangerSelector(select);
        }

        /// <summary>
        /// 根据二维码获取任务
        /// </summary>
        /// <param name="dangerPointID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getEmpTaskByQRCoder/{dangerPointID:Guid}")]
        public ActionResult<IEnumerable<InsepctTaskByEmployee>> GetEmpTaskByQRCoder(Guid dangerPointID)
        {
            return spectbll.GetEmpTaskByQRCoder(dangerPointID);
        }
        ///// <summary>
        ///// 根据二维码获取超时任务
        ///// </summary>
        ///// <param name="dangerPointID"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("getEmpTaskTimeOutByQRCoder/{dangerPointID:Guid}")]
        //public ActionResult<IEnumerable<InsepctTaskByEmployee>> GetEmpTimeOutTaskByQRCoder(Guid dangerPointID)
        //{
        //    return spectbll.GetEmpTimeOutTaskByQRCoder(dangerPointID);
        //}
        /// <summary>
        /// 根据二维码获取历史
        /// </summary>
        /// <param name="pointID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getTaskBillMastersOverByQRCoder/{pointID:Guid}")]
        public ActionResult<IEnumerable<TaskBillModel>> GetTaskBillMastersOverByQRCoder(Guid pointID)
        {
            return billbll.GetTaskBillMastersOverByQRCoder(pointID);
        }
        /// <summary>
        /// 获取管控信息详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getTroubleCtr")]
        public ActionResult<IEnumerable<APPTroubleCtrView>> GetTroubleCtr()
        {
            return ctrbll.GetTroubleCtr();
        }

        /// <summary>
        /// 延长管控完成时间
        /// </summary>
        /// <param name="finishTime"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delayFinishTime")]
        public ActionResult<bool> DelayFinishTime(DelayFinishTime finishTime)
        {
            return ctrbll.DelayFinishTime(finishTime);
        }
        /// <summary>
        /// 改变隐患等级
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("changeLevel")]
        public ActionResult<bool> ChangeLevel(ChangeLevel level)
        {
            LogContent = "改变隐患等级，参数源:" + JsonConvert.SerializeObject(level);
            return ctrbll.ChangeLevel(level);
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
            return ctrbll.ChangeDangerLevel(level);
        }
        /// <summary>
        /// 新建申请/验收
        /// </summary>
        /// <param name="flowNew"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addTroubleCtrFlow")]
        public ActionResult<bool> AddTroubleCtrFlow(TroubleCtrFlowNew flowNew)
        {
            LogContent = "新建了验收/申请，参数源:" + JsonConvert.SerializeObject(flowNew);
            return ctrbll.AddTroubleCtrFlow(flowNew);
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
            return ctrbll.Filed(ctrID);
        }

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
            return ctrbll.HandleCtr(handleTrouble);
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
            return ctrbll.TransferPrincipal(transferTrouble);
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
            return ctrbll.QuickHandleCtr(quickHandleTrouble);
        }

        /// <summary>
        /// APP 端获取风险等级
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getDangerLevels")]
        public ActionResult<IEnumerable<DangerLevel>> GetDangerLevels()
        {
            return dpbll.GetDangerLevels();
        }
        /// <summary>
        /// APP 根据风险点ID 端获取风险点
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getDangerPointsPage")]
        public ActionResult<Pager<APPDangerPointView>> GetDangerPointsPage(PagerQuery<Guid> query)
        {
            return dpbll.GetDangerPointsPage(query);
        }

        /// <summary>
        /// APP端获取统计管控菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getCtrMenu")]
        public ActionResult<IEnumerable<TroubleCtrMenu>> GetCtrMenu()
        {
            return ctrbll.GetCtrMenu();
        }

        /// <summary>
        /// APP 统计 分页获取管控项
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getTroubleCtrsPage")]
        public ActionResult<Pager<TroubleCtrsPage>> GetTroubleCtrsPage(PagerQuery<int> query)
        {
            return ctrbll.GetTroubleCtrsPage(query);
        }

        /// <summary>
        /// APP 统计 当前人组织架构下 获取所有超期任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("getTimeOutTask")]
        public ActionResult<Pager<TimeOutTask>> GetTimeOutTask(PagerQuery<string> query)
        {
            return spectbll.GetTimeOutTask(query);
        }
    }
}
