using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    public class TroubleCtrView
    {
        /// <summary>
        ///隐患管控编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 隐患管控ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 管控名称
        /// </summary>
        public string ControlName { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime FinishTime { get; set; }
        /// <summary>
        /// 责任人id
        /// </summary>
        public Guid PrincipalID { get; set; }
        /// <summary>
        /// 负责人名
        /// </summary>
        public string  PrincipalName { get; set; }
        /// <summary>
        /// 巡检人名
        /// </summary>
        public string BillEmpName { get; set; }
        /// <summary>
        /// 责任人电话
        /// </summary>
        public string PrincipalTEL { get; set; }
        /// <summary>
        /// 责任部门id
        /// </summary>
        public Guid OrgID { get; set; }
        /// <summary>
        /// 负责人部门名
        /// </summary>
        public String OrgName { get; set; }
        /// <summary>
        /// 管控描述
        /// </summary>
        public string ControlDescription { get; set; }
        /// <summary>
        /// 隐患级别
        /// </summary>
        public int TroubleLevel { get; set; }
        /// <summary>
        /// 隐患等级描述
        /// </summary>
        public string TroubleLevelDesc { get; set; }
        /// <summary>
        /// 发现日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 状态名
        /// </summary>
        public string StateName { get; set; }

        /// <summary>
        /// 验收人
        /// </summary>
        public string FlowEmp { get; set; }
        /// <summary>
        /// 验收时间
        /// </summary>
        public DateTime? FlowTime { get; set; }
    }

    public class TroubleCtrDetailView
    {
        /// <summary>
        /// 隐患详情ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 主体类型
        /// </summary>
        public string SubjectTypeName { get; set; }
        /// <summary>
        /// 主体名
        /// </summary>
        public string SubjectName { get; set; }
        /// <summary>
        ///危险源名称 
        /// </summary>
        public string DangerName { get; set; }
        /// <summary>
        /// 任务结果名
        /// </summary>
        public string TaskResultName { get; set; }
        /// <summary>
        /// 任务巡检描述
        /// </summary>
        public string TaskResultMemo { get; set; }
        /// <summary>
        /// 危害因素名
        /// </summary>
        public string WHYSDic { get; set; }
        /// <summary>
        /// 事故类型名
        /// </summary>
        public string SGLXDic { get; set; }
        /// <summary>
        /// 事故后果名
        /// </summary>
        public string SGJGDic { get; set; }
        /// <summary>
        /// 影响范围名
        /// </summary>
        public string YXFWDic { get; set; }
        /// <summary>
        /// 评测方法名
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 隐患等级名
        /// </summary>
        public string TroubleLevelName { get; set; }
    }

    public class TroubleCtrFlowView
    {
        /// <summary>
        /// 隐患控制ID
        /// </summary>
        public Guid ControlID { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime FlowDate { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public int FlowResult { get; set; }
        /// <summary>
        /// 结果名
        /// </summary>
        public string FlowResultName { get; set; }
        /// <summary>
        /// 操作人员id
        /// </summary>
        public Guid FlowEmployeeID { get; set; }
        /// <summary>
        /// 人员名
        /// </summary>
        public string EmpName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string FlowMemo { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int FlowType { get; set; }
        /// <summary>
        /// 流程类型名
        /// </summary>
        public string FlowTypeName { get; set; }
    }

}
