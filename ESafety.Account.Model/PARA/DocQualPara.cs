using ESafety.Core.Model;
using ESafety.Core.Model.DB.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    public class DocQualPara
    {
    }

    public class AmendQual
    {
        public Guid ID { get; set; }

        
        /// <summary>
        /// String:名称
        /// </summary>        				 
        public String QName { get; set; }

        /// <summary>
        /// DateTime:结束时间
        /// </summary>        				 
        public DateTime QEndTime { get; set; }

        /// <summary>
        /// DateTime:审核日期
        /// </summary>        				 
        public DateTime QAudit { get; set; }


        /// <summary>
        /// Guid:颁发机构id
        /// </summary>        				 
        public Guid QInsId { get; set; }


        /// <summary>
        /// Guid:持有人id
        /// </summary>        				 
        public Guid QPeopleId { get; set; }


        /// <summary>
        /// Guid:资质id【词典】
        /// </summary>        				 
        public Guid QTypeId { get; set; }
    }
}
