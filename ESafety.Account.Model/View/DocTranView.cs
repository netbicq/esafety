﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    public class DocTranView
    {

        public Guid ID { get; set; }

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
        /// String:
        /// </summary>        				 
        public Guid TrainerId { get; set; }

        /// <summary>
        /// 主持人姓名
        /// </summary>
        public string Trainer { get; set; }
    }
}
