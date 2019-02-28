using ESafety.Core.Model;
using ESafety.Core.Model.DB.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    public class DocTranPara
    {
        public PagerQuery<DocTranPara> Request { get; set; }

        public Guid Id { get; set; }

        public string Keyword { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public Doc_Train tb { get; set; }

        public List<Doc_TrainPeople> dtDB { get; set; }
    }
}
