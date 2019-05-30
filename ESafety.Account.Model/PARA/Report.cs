using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    /// <summary>
    /// 企业安全风险三清单报表查询条件
    /// </summary>
    public class DPReportQuery
    {
        /// <summary>
        /// 风险等级
        /// </summary>
        public Guid DLevel { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }
    }
    /// <summary>
    /// 企业安全风险分级管控表查询条件
    /// </summary>
    public class DSReportQuery
    {
        ///// <summary>
        ///// 风险等级
        ///// </summary>
        //public Guid DLevel { get; set; }
        /// <summary>
        /// 关键字(主体名、负责人名)
        /// </summary>
        public string KeyWord { get; set; }
    }
    /// <summary>
    /// 隐患整改情况查询条件
    /// </summary>
    public class CtrReportQuery {   }
    /// <summary>
    /// 设备设施风险分级控制清单
    /// </summary>
    public class SubDReportQuery { }
}
