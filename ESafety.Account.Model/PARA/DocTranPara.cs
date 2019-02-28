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
        /// <summary>
        /// 培训人员Id 逗号分隔
        /// </summary>
        public string AIds { get; set; }

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
        public String TTheme { get; set; }

        /// <summary>
        /// Int32:
        /// </summary>        				 
        public Int32 TTime { get; set; }

        /// <summary>
        /// DateTime:
        /// </summary>        				 
        public DateTime TEndTime { get; set; }

        /// <summary>
        /// String:
        /// </summary>        				 
        public String TContent { get; set; }

        /// <summary>
        /// String:培训人id
        /// </summary>        				 
        public Guid TrainerId { get; set; }
    }
}
