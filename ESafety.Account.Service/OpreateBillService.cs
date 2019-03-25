using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core;
using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.DB.Account;
using ESafety.ORM;
using ESafety.Unity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Service
{
    /// <summary>
    /// 作业申请单
    /// </summary>
    public class OpreateBillService : FlowBusinessService, IOpreateBill
    {

        private IUnitwork _work = null;

        private IRepository<Bll_OpreationBill> rpsOpreateBill = null;
        private IRepository<Bll_OpreateionBillFlow> rpsBillFlow = null;


        public OpreateBillService(IUnitwork work, IFlow flow) : base(work, flow)
        {

            _work = work;
            rpsOpreateBill = work.Repository<Bll_OpreationBill>();
            rpsBillFlow = work.Repository<Bll_OpreateionBillFlow>();
            Unitwork = work;

        }
        /// <summary>
        /// 新建作业申请单
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        public ActionResult<bool> AddNew(OperateBillNew bill)
        {
            try
            {
                var opreationmodel = _work.Repository<Core.Model.DB.Account.Basic_Opreation>().GetModel(bill.OpreationID);
                if (opreationmodel == null)
                {
                    throw new Exception("作业流程未找到");
                }

                var flows = _work.Repository<Core.Model.DB.Account.Basic_OpreationFlow>().Queryable(q => q.OpreationID == opreationmodel.ID).OrderBy(o => o.PointIndex);


                var billdb = bill.MAPTO<Core.Model.DB.Account.Bll_OpreationBill>();
                billdb.BillCode = Command.CreateCode();
                billdb.FlowJson = JsonConvert.SerializeObject(flows);

                _work.Repository<Core.Model.DB.Account.Bll_OpreationBill>().Add(billdb);
                _work.Commit();

                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 修改作业申请单
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        public ActionResult<bool> EditBill(OpreateBillEdit bill)
        {
            try
            {
                var dbbill = rpsOpreateBill.GetModel(bill.ID);
                if (dbbill == null)
                {
                    throw new Exception("作业申请单不存在");
                }
                if (dbbill.State != (int)PublicEnum.BillFlowState.normal)
                {
                    throw new Exception("作业申请单状态不允许修改");
                }
                var eddbbill = bill.CopyTo<Core.Model.DB.Account.Bll_OpreationBill>(dbbill);

                rpsOpreateBill.Update(eddbbill);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        public override ActionResult<bool> StartBillFlow(Guid businessid)
        {
            return base.StartBillFlow(businessid);
        }
        public override ActionResult<bool> Approve(Guid businessid)
        {
            return base.Approve(businessid);
        }
        /// <summary>
        /// 获取作业单模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<OpreateBillModel> GetModel(Guid id)
        {
            try
            {
                var dbmodel = rpsOpreateBill.GetModel(id);
                if (dbmodel == null)
                {
                    throw new Exception("作业单据不存在");
                }
                var re = dbmodel.MAPTO<OpreateBillModel>();
                var opreation = _work.Repository<Basic_Opreation>().GetModel(dbmodel.OpreationID);
                re.OpreationName = opreation == null ? "" : opreation.Name;
                re.StateName = Command.GetItems(typeof(PublicEnum.BillFlowState)).FirstOrDefault(q => q.Value == dbmodel.State).Caption;

                return new ActionResult<OpreateBillModel>(re);

            }
            catch (Exception ex)
            {
                return new ActionResult<OpreateBillModel>(ex);
            }
        }
        /// <summary>
        /// 获取作业单表列表
        /// </summary>
        /// <param name="Para"></param>
        /// <returns></returns>
        public ActionResult<Pager<OpreateBillModel>> GetList(PagerQuery<string> Para)
        {
            try
            {
                var bills = rpsOpreateBill.Queryable(q => q.BillName.Contains(Para.KeyWord) || Para.KeyWord == "");
                var opretions = _work.Repository<Basic_Opreation>().Queryable(q => bills.Select(s => s.OpreationID).Contains(q.ID));
                var emps = _work.Repository<Basic_Employee>().Queryable(q => bills.Select(s => s.PrincipalEmployeeID).Contains(q.ID));

                var retemp = from bill in bills
                             let opreation = opretions.FirstOrDefault(q => q.ID == bill.OpreationID)
                             let emp = emps.FirstOrDefault(q => q.ID == bill.PrincipalEmployeeID)
                             select new OpreateBillModel
                             {
                                 BillCode = bill.BillCode,
                                 OpreationID = bill.OpreationID,
                                 ID = bill.ID,
                                 BillLong = bill.BillLong,
                                 BillName = bill.BillName,
                                 CreateDate = bill.CreateDate,
                                 CreateMan = bill.CreateMan,
                                 EndTime = bill.EndTime,
                                 OpreationName = opreation == null ? "" : opreation.Name,
                                 PrincipalEmployeeID = bill.PrincipalEmployeeID,
                                 StartTime = bill.StartTime,
                                 State = (PublicEnum.BillFlowState)bill.State,
                                 StateName =
                                  bill.State == (int)PublicEnum.BillFlowState.approved ? "审批通过" :
                                  bill.State == (int)PublicEnum.BillFlowState.audited ? "已审核" :
                                  bill.State == (int)PublicEnum.BillFlowState.cancel ? "已作废" :
                                  bill.State == (int)PublicEnum.BillFlowState.check ? "已验收" :
                                  bill.State == (int)PublicEnum.BillFlowState.deny ? "已拒绝" :
                                  bill.State == (int)PublicEnum.BillFlowState.normal ? "待审批" :
                                  bill.State == (int)PublicEnum.BillFlowState.pending ? "审批中" :
                                  bill.State == (int)PublicEnum.BillFlowState.recalled ? "已撤回" : "未知",
                                 PrincipalEmployeeName = emp == null ? "" : emp.CNName
                             };

                var re = new Pager<OpreateBillModel>().GetCurrentPage(retemp, Para.PageSize, Para.PageIndex);
                return new ActionResult<Pager<OpreateBillModel>>(re);

            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<OpreateBillModel>>(ex);
            }
        }
        /// <summary>
        /// 删除指定id的作业单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelBill(Guid id)
        {
            try
            {
                var dbmodel = rpsOpreateBill.GetModel(id);
                if (dbmodel == null)
                {
                    throw new Exception("作业单据不存在");
                }
                if (dbmodel.State != (int)PublicEnum.BillFlowState.normal)
                {
                    throw new Exception("单据状态不允许操作");
                }

                rpsOpreateBill.Delete(dbmodel);
                _work.Repository<Flow_Result>().Delete(q => q.BusinessID == dbmodel.ID);

                _work.Commit();

                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        public ActionResult<bool> FlowResult(OpreateBillFlowResult flow)
        {
            try
            {
                if(flow == null)
                {
                    throw new Exception("参数有误");
                }
                if ((int)flow.FlowResult == 0)
                {
                    throw new Exception("处理结果数据有误");
                }
                return new ActionResult<bool>(new Exception("未处理完"));

            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        public ActionResult<OpreateBillFlowModel> GetBillFlowModel(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
