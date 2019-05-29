using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    /// <summary>
    /// 安全风险三清单
    /// </summary>
    public class DPReport
    {
        /// <summary>
        /// 风险点
        /// </summary>
        public string DangerPoint { get; set; }
        /// <summary>
        /// 危害因素
        /// </summary>
        public IEnumerable<string> WHYSDict { get; set; }
        /// <summary>
        /// 后果
        /// </summary>
        public string Consequence { get; set; }
        /// <summary>
        /// 风险等级
        /// </summary>
        public string DLevel { get; set; }
        /// <summary>
        /// 管控措施
        /// </summary>
        public string ControlMeasure { get; set; }
        /// <summary>
        /// 应急处理措施
        /// </summary>
        public string EmergencyMeasure { get; set; }
        /// <summary>
        ///负责人
        /// </summary>
        public string Principal { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }
    }
    /// <summary>
    /// 企业安全风险分级管控表
    /// </summary>
    public class DSReport
    {
        /// <summary>
        /// 风险点
        /// </summary>
        public string DangerPoint { get; set; }
        /// <summary>
        /// 主体
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 危害因素
        /// </summary>
        public IEnumerable<string> WHYSDict { get; set; }
        /// <summary>
        /// 风险等级
        /// </summary>
        public string DLevel { get; set; }
        /// <summary>
        /// 负责人部门
        /// </summary>
        public string Org { get; set; }
        /// <summary>
        /// 管控措施
        /// </summary>
        public string ControlMeasure { get; set; }
        /// <summary>
        ///负责人
        /// </summary>
        public string Principal { get; set; }
    }
}
