using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Core;
using ESafety.Core.Model;
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

        public OpreateBillService(IUnitwork work,IFlow flow):base(work,flow)
        {

            _work = work;
            rpsOpreateBill = work.Repository<Bll_OpreationBill>();
            Unitwork = work;

        }
        /// <summary>
        /// 新建作业申请单
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        public  ActionResult<bool> AddNew(OperateBillNew bill)
        {
            try
            {
                var opreationmodel = _work.Repository<Core.Model.DB.Account.Basic_Opreation>().GetModel(bill.OpreationID);
                if(opreationmodel == null)
                {
                    throw new Exception("作业流程未找到");
                }
                
                var flows = _work.Repository<Core.Model.DB.Account.Basic_OpreationFlow>().Queryable(q => q.OpreationID == opreationmodel.ID).OrderBy(o=>o.PointIndex);


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
         

        public  ActionResult<bool> EditBill(OpreateBillEdit bill)
        {
            throw new NotImplementedException();
        }

        public override ActionResult<bool> StartBillFlow(Guid businessid)
        {
            return base.StartBillFlow(businessid);
        }
        public override ActionResult<bool> Approve(Guid businessid)
        {
            return base.Approve(businessid);
        }

        public ActionResult<OpreateBillModel> GetModel(Guid id)
        {
            throw new NotImplementedException();
        }

        public ActionResult<Pager<OpreateBillModel>> GetList(PagerQuery<string> Para)
        {
            throw new NotImplementedException();
        }

        public ActionResult<bool> DelBill(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
