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
    /// 安全会议
    /// </summary>
    [RoutePrefix("api/docmeeting")]
    public class DocMeetingController :ESFAPI
    {
        private IDocMeetingService bll = null;
        public DocMeetingController(IDocMeetingService dm)
        {
            bll = dm;
            BusinessService = dm;
        }
        /// <summary>
        /// 新建安全会议模型
        /// </summary>
        /// <param name="meetingNew"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("adddm")]
        public ActionResult<bool> AddDocMeeting(DocMeetingNew meetingNew)
        {
            LogContent = "新建安全会议，数据源:" + JsonConvert.SerializeObject(meetingNew);
            return bll.AddDocMeeting(meetingNew);
        }
        /// <summary>
        /// 删除安全会议模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("deldm/{id:Guid}")]
        public ActionResult<bool> DelDocMeeting(Guid id)
        {
            LogContent = "删除了安全会议模型，ID:" + JsonConvert.SerializeObject(id);
            return bll.DelDocMeeting(id);
        }
        /// <summary>
        /// 修改了安全会议模型
        /// </summary>
        /// <param name="meetingEdit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editdm")]
        public ActionResult<bool> EditDocMeeting(DocMeetingEdit meetingEdit)
        {
            LogContent = "修改了安全会议模型，数据源:" + JsonConvert.SerializeObject(meetingEdit);
            return bll.EditDocMeeting(meetingEdit);
        }
        /// <summary>
        ///根据ID，获取安全会议模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getdm/{id:Guid}")]
        public ActionResult<DocMeetingView> GetDocMeeting(Guid id)
        {
            return bll.GetDocMeeting(id);
        }
        /// <summary>
        /// 分页获取安全会议模型，可根据会议主题查询
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getdms")]
        public ActionResult<Pager<DocMeetingView>> GetDocMeetings(PagerQuery<DocMeetingQuery> para)
        {
            return bll.GetDocMeetings(para);
        }
    }
}
