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
    /// 培训管理
    /// </summary>
    [RoutePrefix("api/doctrainmanage")]
    public class DocTrainManageController : ESFAPI
    {
        private IDocTrainManageService bll = null;
        public DocTrainManageController(IDocTrainManageService dtm)
        {
            bll = dtm;
            BusinessService = dtm;
        }
        /// <summary>
        /// 新建训练项与人员之间的关系模型
        /// </summary>
        /// <param name="empoyeesNew"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("adddtemp")]
        public ActionResult<bool> AddTrainEmployee(DocTrainEmpoyeesNew empoyeesNew)
        {
            LogContent = "新建了训练项与人员之间的关系模型,参数源:" + JsonConvert.SerializeObject(empoyeesNew);
            return bll.AddTrainEmployee(empoyeesNew);
        }
        /// <summary>
        /// 新建训练项模型
        /// </summary>
        /// <param name="trainingNew"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("adddt")]
        public ActionResult<bool> AddTraining(DocTrainingNew trainingNew)
        {
            LogContent = "新建了训练项模型，参数源:" + JsonConvert.SerializeObject(trainingNew);
            return bll.AddTraining(trainingNew);
        }
        /// <summary>
        /// 删除训练项人员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("deldtemp/{id:Guid}")]
        public ActionResult<bool> DelTrainEmplyee(Guid id)
        {
            LogContent = "删除了训练项人员，ID:" + JsonConvert.SerializeObject(id);
            return bll.DelTrainEmplyee(id);
        }
        /// <summary>
        /// 删除训练项模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("deldt/{id:Guid}")]
        public ActionResult<bool> DelTraining(Guid id)
        {
            LogContent = "删除了训练项模型，ID:" + JsonConvert.SerializeObject(id);
            return bll.DelTraining(id);
        }
        /// <summary>
        /// 修改训练项模型
        /// </summary>
        /// <param name="trainingEdit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editdt")]
        public ActionResult<bool> EditTraining(DocTrainingEdit trainingEdit)
        {
            LogContent = "修改了训练项模型，参数源:" + JsonConvert.SerializeObject(trainingEdit);
            return bll.EditTraining(trainingEdit);
        }
        /// <summary>
        /// 分页获取参加训练的人员
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getdtemps")]
        public ActionResult<Pager<DocTrainEmpoyeesView>> GetTrainEmployee(PagerQuery<DocTrainEmpoyeesQuery> para)
        {
            return bll.GetTrainEmployee(para);
        }
        /// <summary>
        /// 根据ID，获取训练项模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getdt/{id:Guid}")]
        public ActionResult<DocTrainingView> GetTraining(Guid id)
        {
            return bll.GetTraining(id);
        }
        /// <summary>
        /// 分页获取训练项模型，可根据训练项主题查询
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getdts")]
        public ActionResult<Pager<DocTrainingView>> GetTrainings(PagerQuery<DocTrainingQuery> para)
        {
            return bll.GetTrainings(para);
        }
    }
}
