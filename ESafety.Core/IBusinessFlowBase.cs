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
        /// 审核
        /// </summary>
        /// <param name="businessid"></param>
        /// <returns></returns>
        ActionResult<bool> Approve(Guid businessid);
        /// <summary>
        /// 发起审批
        /// </summary>
        /// <param name="businessid"></param>
        /// <returns></returns>
        ActionResult<bool> StartBillFlow(Guid businessid);

    }
}
