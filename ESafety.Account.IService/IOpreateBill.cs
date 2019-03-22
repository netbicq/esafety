using ESafety.Account.Model.PARA;
using ESafety.Core;
using ESafety.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.IService
{
    /// <summary>
    /// 作业申请单
    /// </summary>
    public interface IOpreateBill:IBusinessFlowBase
    {
        /// <summary>
        /// 新建作业申表
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        ActionResult<bool> AddNew(OperateBillNew bill);
        /// <summary>
        /// 修改作业申请
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        ActionResult<bool> EditBill(OpreateBillEdit bill);

        /// <summary>
        /// 获取我作业单模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<OpreateBillModel> GetModel(Guid id);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="Para"></param>
        /// <returns></returns>
        ActionResult<Pager<OpreateBillModel>> GetList(PagerQuery<string> Para);
        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<bool> DelBill(Guid id);
    }
}
