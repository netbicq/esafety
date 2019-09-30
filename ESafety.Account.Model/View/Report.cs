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
        /// 风险等级
        /// </summary>
        public string DLevel { get; set; }
        /// <summary>
        /// 危害因素
        /// </summary>
        public IEnumerable<string> WHYSDict { get; set; }
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
    /// <summary>
    /// 岗位报表
    /// </summary>
    public class PostReport
    {
        /// <summary>
        /// 岗位名
        /// </summary>
        public string PostName { get; set; }
        /// 组织架构ID
        /// </summary>
        public string Org { get; set; }
        /// <summary>
        /// 主要任务
        /// </summary>
        public string MainTasks { get; set; }
        /// <summary>
        ///备注
        /// </summary>
        public string Memo { get; set; }
    }
    /// <summary>
    /// 企业岗位作业内容清单
    /// </summary>
    public class OpreateReport
    {
        /// <summary>
        /// 作业名称
        /// </summary>
        public string OpreateName { get; set; }
        /// <summary>
        /// 作业步骤
        /// </summary>
        public IEnumerable<string> OpreateFlow { get; set; }
        /// <summary>
        /// 作业目的
        /// </summary>
        public string Target { get; set; }
    }
    /// <summary>
    /// 管控报表
    /// </summary>
    public class CtrReport
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
        /// 风控项
        /// </summary>
        public string Danger { get; set; }
        /// <summary>
        /// 检查人
        /// </summary>
        public string BEmp { get; set; }
        /// <summary>
        /// 检查情况
        /// </summary>
        public string CheckResult { get; set; }
        /// <summary>
        /// 隐患创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 隐患等级
        /// </summary>
        public string TLevel { get; set; }
        /// <summary>
        /// 管控目标
        /// </summary>
        public string CtrTarget { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string Principal { get; set; }
        /// <summary>
        /// 验收时间
        /// </summary>
        public DateTime? AccepteDate { get; set; }
        /// <summary>
        /// 是否通过
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(EnumJsonConvert<Unity.PublicEnum.EE_TroubleState>))]
        public Unity.PublicEnum.EE_TroubleState IsAccepte { get; set; }
        /// <summary>
        /// 验收人
        /// </summary>
        public string Acceptor { get; set; }
    }
    /// <summary>
    /// 设备设施风险分级控制清单
    /// </summary>
    public class SubDReport
    {
        /// <summary>
        /// 风险点
        /// </summary>
        public string DangerPoint { get; set; }
        /// <summary>
        /// 主体
        /// </summary>
        public IEnumerable<DangerSub> DangerSubs { get; set; }
        /// <summary>
        /// 后果
        /// </summary>
        public string Consequence { get; set; }
        /// <summary>
        /// 负责单位
        /// </summary>
        public string POrg { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string Principal { get; set; }
    }
    /// <summary>
    /// 风险主体
    /// </summary>
    public class DangerSub
    {
        /// <summary>
        /// 主体类型
        /// </summary>
        public string SubType { get; set; }
        /// <summary>
        /// 主体
        /// </summary>
        public string Sub { get; set; }
        /// <summary>
        /// 风控项
        /// </summary>
        public IEnumerable<RDanger> Dangers { get; set; }



    }
    /// <summary>
    /// 风控项
    /// </summary>
    public class RDanger
    {
        /// <summary>
        /// 风控项
        /// </summary>
        public string DangerName { get; set; }
        /// <summary>
        /// 执行标准
        /// </summary>
        public IEnumerable<Standard> Standards { get; set; }
        /// <summary>
        /// 风险等级
        /// </summary>
        public string DLevel { get; set; }
    }

    /// <summary>
    /// 标准
    /// </summary>
    public class Standard
    {
        /// <summary>
        /// 工程技术
        /// </summary>
        public string Engineering { get; set; }
        /// <summary>
        /// 管理措施
        /// </summary>
        public string Controls { get; set; }
        /// <summary>
        /// 个体防护措施
        /// </summary>

        public string Individual { get; set; }
        /// <summary>
        /// 应急处理措施
        /// </summary>
        public string Accident { get; set; }
    }
}
