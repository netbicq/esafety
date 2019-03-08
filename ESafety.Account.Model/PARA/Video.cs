using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    /// <summary>
    /// 视频监控
    /// </summary>
    public class VideoNew
    {
        /// <summary>
        /// 视频编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 视频地点
        /// </summary>
        public string Site { get; set; }
        /// <summary>
        /// 视频url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 主体明细
        /// </summary>
        public IEnumerable<VideoSubjectNew> Subjects { get; set; }
    }

    public class VideoEdit:VideoNew
    {
        /// <summary>
        /// 视频监控ID
        /// </summary>
        public Guid ID { get; set; }
    }

    public class VideoSubjectNew
    {
        /// <summary>
        /// 主体ID
        /// </summary>
        public Guid SubjectID { get; set; }
        /// <summary>
        /// 主主体类型
        /// </summary>
        public int SubjectType { get; set; }
    }
    public class VideoQuery
    {
        /// <summary>
        /// 根据关键字查询，支持摄像机编号和位置
        /// </summary>
        public string Key { get; set; }
    }
}
