﻿using ESafety.Account.IService;
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
    [RoutePrefix("api/taskbill")]
    public class TaskBillController : ESFAPI
    {
        private ITaskBillService bll = null;
        public TaskBillController(ITaskBillService tb)
        {
            bll = tb;
            BusinessService = tb;
        }
        /// <summary>
        /// 处理任务单据
        /// </summary>
        /// <param name="subjectBillEdit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("edittbs")]
        public ActionResult<bool> EditTaskBillSubjects(TaskSubjectBillEdit subjectBillEdit)
        {
            LogContent = "更新任务明细,参数源：" + JsonConvert.SerializeObject(subjectBillEdit);
            return bll.EditTaskBillSubjects(subjectBillEdit);
        }
        /// <summary>
        /// 获取任务主体模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("gettbm/{id:Guid}")]
        public ActionResult<TaskBillModelView> GetTaskBillModel(Guid id)
        {
            return bll.GetTaskBillModel(id);
        }
        /// <summary>
        /// 分页获取任务单据信息
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("gettbp")]
        public ActionResult<Pager<TaskBillView>> GetTaskBillPage(PagerQuery<TaskBillQuery> para)
        {
            return bll.GetTaskBillPage(para);
        }
        /// <summary>
        /// 分页获取任务主体详情，根据主体
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<TaskSubjectBillView>> GetTaskBillSubjects(PagerQuery<TaskBillSubjectsQuery> para)
        {
            return bll.GetTaskBillSubjects(para);
        }
    }
}
