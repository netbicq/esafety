using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    /// <summary>
    /// 安全会议
    /// 需添加电子文档
    /// </summary>
    public class DocMeetingNew
    {
        /// <summary>
        /// 会议主题
        /// </summary>
        public string Motif { get; set; }
        /// <summary>
        /// 参会人员
        /// </summary>
        public string EmployeeS { get; set; }
        /// <summary>
        /// 会议日期
        /// </summary>
        public DateTime MeetingDate { get; set; }
        /// <summary>
        /// 主持人
        /// </summary>
        public string MeetingMaster { get; set; }
        /// <summary>
        /// 会义地点
        /// </summary>
        public string Site { get; set; }
        /// <summary>
        /// 会议内容
        /// </summary>
        public string Content { get; set; }
    }

    public class DocMeetingEdit : DocMeetingNew
    {
        /// <summary>
        /// 安全会议ID
        /// </summary>
        public Guid ID { get; set; }
    }

    public class DocMeetingQuery
    {
        /// <summary>
        /// 会议主题
        /// </summary>
        public string  Motif { get; set; }
    }

}
