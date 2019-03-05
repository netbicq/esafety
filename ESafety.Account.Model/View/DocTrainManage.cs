using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    public class DocTrainingView
    {
        /// <summary>
        /// 培训ID
        /// </summary>
        public Guid ID { get; set; }
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
    }

    public class DocTrainEmpoyeesView
    {
        /// <summary>
        /// 人员与培训项的关系模型ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 培训ID
        /// </summary>
        public Guid TrainID { get; set; }
        /// <summary>
        /// 人员ID
        /// </summary>
        public Guid EmployeeID { get; set; }
        /// <summary>
        /// 人员名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 人员部门
        /// </summary>
        public string Department { get; set; }
    }

}
