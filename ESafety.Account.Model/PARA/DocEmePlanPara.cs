using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    public class DocEmePlanPara
    {
     
        public Guid ID { get; set; }

        /// <summary>
        /// String:
        /// </summary>        				 
        public String EName { get; set; }

        /// <summary>
        /// Guid:类别id[制度]
        /// </summary>        				 
        public Guid ETypeId { get; set; }


        /// <summary>
        /// DateTime:发布时间
        /// </summary>        				 
        public DateTime EReleaseTime { get; set; }

        /// <summary>
        /// Guid:风险等级
        /// </summary>        				 
        public Guid ELvId { get; set; }

        ///// <summary>
        ///// String:
        ///// </summary>        				 
        //public String EContent { get; set; }
    }
}
