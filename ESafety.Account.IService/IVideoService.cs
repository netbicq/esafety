using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.IService
{
    /// <summary>
    /// 视频监控
    /// </summary>
    public interface IVideoService
    {
        ActionResult<bool> AddVideo(VideoNew videoNew);
        ActionResult<bool> EditVideo(VideoEdit videoEdit);
        ActionResult<bool> DelVideo(Guid id);
        ActionResult<VideoModelView> GetVideo(Guid id);
        ActionResult<Pager<VideoView>> GetVideos(PagerQuery<VideoQuery> para);
        ActionResult<IEnumerable<VideoSubjectView>> GetVideoSubjects(Guid id);

        /// <summary>
        /// 移动端获取视频信息
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<VideoView>> GetVideoList();


    

    }
}
