using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    public class DocMeetView
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
        public String MTheme { get; set; }

        /// <summary>
        /// DateTime:
        /// </summary>        				 
        public DateTime MTime { get; set; }



        public String MContent { get; set; }

        public Guid HostId { get; set; }
        public string HostName { get; set; }
    }
}
