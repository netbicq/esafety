using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    /// <summary>
    /// 处理管控项
    /// </summary>
    public class HandleTroubleCtr
    {
        /// <summary>
        /// 新建管控项
        /// </summary>
        public Guid CtrID { get; set; }
        /// <summary>
        ///隐患管控执行人
        /// </summary>
        public Guid ExecutorID { get; set; }
        /// <summary>
        /// 隐患管控验收人
        /// </summary>
        public Guid AcceptorID { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime FinishTime { get; set; }
        /// <summary>
        /// 管控目标
        /// </summary>
        public string ControlDescription { get; set; }
    }

    public class QuickHandleTroubleCtr
    {
        /// <summary>
        /// 管控ID
        /// </summary>
        public Guid CtrID { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }

    public class TransferTroublePrincipal
    {
        /// <summary>
        /// 管控ID
        /// </summary>
        public Guid CtrID { get; set; }
        /// <summary>
        /// 责任人
        /// </summary>
        public Guid PrincipalID { get; set; }
    }
}
