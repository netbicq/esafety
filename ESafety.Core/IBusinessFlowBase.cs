using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core
{
    /// <summary>
    /// 业务单据审批基类
    /// </summary>
    public interface IBusinessFlowBase
    {
        /// <summary>
        /// 发起业务单据审批流程
        /// </summary>
        /// <param name="businessid"></param>
        /// <returns></returns>
        ActionResult<bool> FlowStart<T> (T rps, Guid businessid)where T: IRepository<ModelBase>;

        /// <summary>
        /// 审核业务单据
        /// </summary>
        /// <param name="businessid"></param>
        /// <returns></returns>
        ActionResult<bool> ApproveBill<T>(T rps, Guid businessid) where T : IRepository<ModelBase>;


    }
}
