using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    public class OccMedicalView
    {

        [KeyAttribute]

        /// <summary>
        /// Guid:
        /// </summary>        				 
        public Guid Id { get; set; }

        //
        /// <summary>
        /// Guid:
        /// </summary>        				 
        public Guid MEmpId { get; set; }

        public string MEmpName { get; set; }


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

        public Guid ZzId { get; set; }
        public string ZzName { get; set; }

        public string Sex { get;set; }
        
        public string dtStr { get { return MTime.ToString("g"); } }
        
    }
}
