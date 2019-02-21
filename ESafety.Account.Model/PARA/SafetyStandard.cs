﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    class SafetyStandard
    {
    }

    public class SafetyStandardNew
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类别ID
        /// </summary>
        public Guid DangerSortID { get; set; }
        /// <summary>
        /// 管控措施
        /// </summary>
        public string Controls { get; set; }
    }
    public class SafetyStandardEdit:SafetyStandardNew
    {
        public Guid ID { get; set; }
    }

}
