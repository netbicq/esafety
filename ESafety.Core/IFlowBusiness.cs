using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.PARA;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core
{
    /// <summary>
    /// 业务审批基类
    /// </summary>
    public interface IFlowBusiness
    {

        /// <summary>
        /// 发起审批
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<Flow_Task> StartFlow(BusinessAprovePara para);
        /// <summary>
        /// 业务审核
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<bool> BusinessAprove(BusinessAprovePara para);
         
    }
}
