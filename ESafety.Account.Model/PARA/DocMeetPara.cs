using ESafety.Core.Model.DB;
using ESafety.Core.Model.DB.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    public class DocMeetPara
    {
        public Doc_Meeting meet { get; set; }

        public List<Doc_MeetPeople> meet_data { get; set; }

    }

    public class DocMeetPara1
    {

        /// <summary>
        /// 
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// String:
        /// </summary>        				 
        public String MTheme { get; set; }

        /// <summary>
        /// DateTime:
        /// </summary>        				 
        public DateTime MTime { get; set; }



        public String MContent { get; set; }
    }

    public class DocMeetPara2
    {
        /// <summary>
        /// 人员id
        /// </summary>
        public Guid MPId { get; set; }
        /// <summary>
        /// 0.参与人员 1.主持人员
        /// </summary>
        public int MState { get; set; }
        /// <summary>
        /// 会议id
        /// </summary>
        public Guid MMId { get; set; }
    }
}
