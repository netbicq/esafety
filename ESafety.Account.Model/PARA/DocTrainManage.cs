using ESafety.Core.Model.PARA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    /// <summary>
    /// 培训
    /// 需要电子文档
    /// </summary>
    public class DocTrainingNew
    {
        /// <summary>
        /// 培训主题
        /// </summary>
        public string Motif { get; set; }
        /// <summary>
        /// 培训日期
        /// </summary>
        public DateTime TrainDate { get; set; }
        /// <summary>
        /// 培训时长
        /// </summary>
        public int TrainLong { get; set; }
        /// <summary>
        /// 培训人
        /// </summary>
        public string Trainer { get; set; }
        /// <summary>
        /// 培训内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 电子文档
        /// </summary>
        public IEnumerable<AttachFileNew> AttachFiles { get; set; }
        /// <summary>
        /// 人员ID集合
        /// </summary>
        public IEnumerable<Guid> EmployeeIDs { get; set; }
    }

    public class DocTrainingEdit: DocTrainingNew
    {
        /// <summary>
        /// 培训ID
        /// </summary>
        public Guid ID { get; set; }
    }
    /// <summary>
    /// 培训人员
    /// </summary>
    public class DocTrainEmpoyeesNew
    {
        /// <summary>
        /// 培训ID
        /// </summary>
        public Guid TrainID { get; set; }
        /// <summary>
        /// 人员ID
        /// </summary>
        public Guid EmployeeID { get; set; }
    }

    public class DocTrainingQuery
    {
        /// <summary>
        /// 培训主题
        /// </summary>
        public string Motif{ get; set; }
    }

    public class DocTrainEmpoyeesQuery
    {
        /// <summary>
        /// 培训ID
        /// </summary>
        public Guid TrainID { get; set; }
    }


}
