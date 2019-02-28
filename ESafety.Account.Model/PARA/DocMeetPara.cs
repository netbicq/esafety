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

        public List<Bll_AttachFile> meet_file { get; set; }

        public Guid Id { get; set; }

        public string Keyword { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
