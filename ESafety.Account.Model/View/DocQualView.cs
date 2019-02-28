using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    public class DocQualView
    {
        [KeyAttribute]

        /// <summary>
        /// Guid:
        /// </summary>        				 
        public Guid Id { get; set; }

        /// <summary>
        /// DateTime:
        /// </summary>        				 
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// String:
        /// </summary>        				 
        public String QName { get; set; }

        /// <summary>
        /// DateTime:
        /// </summary>        				 
        public DateTime QEndTime { get; set; }

        /// <summary>
        /// DateTime:
        /// </summary>        				 
        public DateTime QAudit { get; set; }

        /// <summary>
        /// String:
        /// </summary>        				 
        public String QInstitutions { get; set; }

        /// <summary>
        /// String:
        /// </summary>        				 
        public String QPeople { get; set; }

        /// <summary>
        /// Guid:
        /// </summary>        				 
        public Guid QPeopleId { get; set; }

        /// <summary>
        /// Guid:
        /// </summary>        				 
        public Guid QTypeId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string dtStr { get { return CreateTime.ToString("g"); } }
    }
}
