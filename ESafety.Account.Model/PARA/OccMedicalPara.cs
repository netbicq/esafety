using ESafety.Core.Model.DB;
using ESafety.Core.Model.DB.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    public class OccMedicalPara
    {

        public Guid Id { get; set; }

        public string Keyword { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public Guid ZzId { get; set; }

        public Occ_Medical tb { get; set; }

        public List<Bll_AttachFile> files { get; set; }
    }
}
