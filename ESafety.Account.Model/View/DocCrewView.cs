using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    public class DocCrewView
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
        public String CName { get; set; }

        /// <summary>
        /// String:
        /// </summary>        				 
        public String CFontSize { get; set; }

        /// <summary>
        /// String:
        /// </summary>        				 
        public String CContent { get; set; }

        /// <summary>
        /// Guid:
        /// </summary>        				 
        public Guid CType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dtStr { get { return CreateTime.ToString("g"); } }

        /// <summary>
        /// 
        /// </summary>
        public string CType_Name { get; set; }
    }
}
