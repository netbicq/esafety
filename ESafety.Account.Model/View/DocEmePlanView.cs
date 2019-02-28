using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    public class DocEmePlanView
    {
        /// <summary>
        /// Int32:
        /// </summary>        				 
        public Int32 IsDeal { get; set; }

        /// <summary>
        /// DateTime:
        /// </summary>        				 
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// String:
        /// </summary>        				 
        public String EName { get; set; }

        /// <summary>
        /// Guid:
        /// </summary>        				 
        public Guid ETypeId { get; set; }

        /// <summary>
        /// 类别名
        /// </summary>
        public String ETypeName { get; set; }

        /// <summary>
        /// Guid:
        /// </summary>        				 
        public Guid ETypeId1 { get; set; }

        /// <summary>
        /// DateTime:
        /// </summary>        				 
        public DateTime EReleaseTime { get; set; }

        /// <summary>
        /// Guid:
        /// </summary>        				 
        public Guid ELvId { get; set; }

        /// <summary>
        /// 等级名
        /// </summary>
        public String EveName { get; set; }

        /// <summary>
        /// String:
        /// </summary>        				 
        public String EContent { get; set; }

    }
}
