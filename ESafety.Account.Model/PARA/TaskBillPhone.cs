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
        /// 附件集合
        /// </summary>
        public IEnumerable<AttachFileNew> AttachFiles { get; set; }
    }

}
