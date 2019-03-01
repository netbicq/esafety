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
        public Guid ZzId { get; set; }

        public Occ_Medical tb { get; set; }

    }

    public class AOccMedicalPara
    {

        public Guid ID { get; set; }
        /// <summary>
        /// Guid:
        /// </summary>        				 
        public Guid MEmpId { get; set; }

        /// <summary>
        /// Int32:
        /// </summary>        				 
        public Int32 MAge { get; set; }

        /// <summary>
        /// DateTime:
        /// </summary>        				 
        public DateTime MTime { get; set; }

        /// <summary>
        /// String:
        /// </summary>        				 
        public String MContent { get; set; }
    }
}
