using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
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
        /// <summary>
        /// 流程节点处理
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        ActionResult<bool> FlowResult(Model.PARA.OpreateBillFlowResult flow);

        /// <summary>
        /// 获取带节点处理信息的表单模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<OpreateBillFlowModel> GetBillFlowModel(Guid id);

        /// <summary>
        /// APP端获取当前人的所有待完成作业申请单
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<OpreateBillByEmp>> GetCurrentList();

        /// <summary>
        /// APP端获取当前人的所有已完成作业申请单
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<OpreateBillByEmp>> GetOverList();

    }
}
