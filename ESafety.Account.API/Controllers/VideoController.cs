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
    /// 视频监控
    /// </summary>
    [RoutePrefix("api/video")]
    public class VideoController : ESFAPI 
    {
        private IVideoService bll = null;
        public VideoController(IVideoService v)
        {
            bll = v;
            BusinessServices =new List<object>() { v };
        }
        /// <summary>
        /// 新建监控模型
        /// </summary>
        /// <param name="videoNew"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addv")]
        public ActionResult<bool> AddVideo(VideoNew videoNew)
        {
            LogContent = "新建了监控模型，参数源:" + JsonConvert.SerializeObject(videoNew);
            return bll.AddVideo(videoNew);
        }
        /// <summary>
        /// 删除监控模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delv/{id:Guid}")]
        public ActionResult<bool> DelVideo(Guid id)
        {
            LogContent = "删除了监控模型，ID：" + JsonConvert.SerializeObject(id);
            return bll.DelVideo(id);
        }
        /// <summary>
        /// 修改监控模型
        /// </summary>
        /// <param name="videoEdit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editv")]
        public ActionResult<bool> EditVideo(VideoEdit videoEdit)
        {
            LogContent = "修改了监控模型，参数源:" + JsonConvert.SerializeObject(videoEdit);
            return bll.EditVideo(videoEdit);
        }
        /// <summary>
        /// 获取监控模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getv/{id:Guid}")]
        public ActionResult<VideoModelView> GetVideo(Guid id)
        {
            return bll.GetVideo(id);
        }
        /// <summary>
        /// 分页获取，监控参数模型，支持摄像机编号和位置
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getvs")]
        public ActionResult<Pager<VideoView>> GetVideos(PagerQuery<VideoQuery> para)
        {
            return bll.GetVideos(para);
        }
        /// <summary>
        /// 根据摄像头ID获取监控主体明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getvsls/{id:Guid}")]
        public ActionResult<IEnumerable<VideoSubjectView>> GetVideoSubjects(Guid id)
        {
            return bll.GetVideoSubjects(id);
        }
    }
}
