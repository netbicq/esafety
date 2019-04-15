using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    /// <summary>
    /// 视频监控
    /// </summary>
    public class VideoView
    {
        /// <summary>
        /// 摄像头ID
        /// </summary>
        public Guid ID { get; set; }
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

    }





    public class VideoModelView : VideoView
    {
        /// <summary>
        ///主体明细
        /// </summary>
        public IEnumerable<VideoSubjectView> Subjects { get; set; }
      
    }

    public class VideoSubjectView
    {
        /// <summary>
        /// 摄像头和主体关系ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 视频id
        /// </summary>
        public Guid VedioID { get; set; }
        /// <summary>
        /// 主体id
        /// </summary>
        public Guid SubjectID { get; set; }
        /// <summary>
        /// 主体类型
        /// </summary>
        public int SubjectType { get; set; }
        /// <summary>
        /// 主体类型名
        /// </summary>
        public string SubjectTypeName { get; set; }
        /// <summary>
        /// 主题名称
        /// </summary>
        public string SubjectName { get; set; }
    }
}
