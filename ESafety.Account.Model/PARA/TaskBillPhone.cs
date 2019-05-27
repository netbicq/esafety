using ESafety.Core.Model.PARA;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    /// <summary>
    /// 新建任务
    /// </summary>
    public class TaskBillNew
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        public Guid TaskID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 预计完成时间
        /// </summary>
        public DateTime EndTime { get; set; }
    }

    /// <summary>
    /// 新建任务单中的主体检查结果
    /// </summary>
    public class TaskBillSubjectNew
    {
        /// <summary>
        /// 任务单据ID
        /// </summary>
        public Guid BillID { get; set; }
        /// <summary>
        /// 主体类型
        /// </summary>
        public PublicEnum.EE_SubjectType SubjectType { get; set; }
        /// <summary>
        /// 主体ID
        /// </summary>
        public Guid SubjectID { get; set; }
        /// <summary>
        /// 检查结果
        /// </summary>
        public PublicEnum.EE_TaskResultType TaskResult { get; set; }
        /// <summary>
        /// 检查结果描述
        /// </summary>
        public string TaskResultMemo { get; set; }
        /// <summary>
        /// 风控项ID
        /// </summary>
        public Guid DangerID { get; set; }
        /// <summary>
        /// 附件集合
        /// </summary>
        public IEnumerable<AttachFileNew> AttachFiles { get; set; }
        /// <summary>
        /// 危害因素
        /// </summary>
        public Guid Eval_WHYS { get; set; }
        /// <summary>
        /// 事故类型
        /// </summary>
        public Guid Eval_SGLX { get; set; }
        /// <summary>
        /// 事故后果
        /// </summary>
        public Guid Eval_SGJG { get; set; }
        /// <summary>
        /// 影响范围
        /// </summary>
        public Guid Eval_YXFW { get; set; }
        /// <summary>
        /// 评测方法
        /// </summary>
        public int Eval_Method { get; set; }
        /// <summary>
        /// 隐患等级
        /// </summary>
        public Guid TroubleLevel { get; set; }
        /// <summary>
        /// 风险等级
        /// </summary>
        public Guid DangerLevel { get; set; }

        /// <summary>
        /// 管控责任人ID
        /// </summary>
        public Guid PrincipalID { get; set; }

        /// <summary>
        /// LECD计算方法的L
        /// </summary>
        public Guid LECD_L { get; set; }
        /// <summary>
        /// LECD计算方法的E
        /// </summary>
        public Guid LECD_E { get; set; }
        /// <summary>
        /// LECD计算方法的C
        /// </summary>
        public Guid LECD_C { get; set; }
        /// <summary>
        /// LSD计算方法的L
        /// </summary>
        public Guid LSD_L { get; set; }
        /// <summary>
        /// LSD计算方法的S
        /// </summary>
        public Guid LSD_S { get; set; }
        /// <summary>
        /// 通过计算得出的值，若为手动选择 则存0
        /// </summary>
        public int DValue { get; set; }
    }

}
