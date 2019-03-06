using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Core;
using ESafety.Core.Model;
using ESafety.Core.Model.DB.Account;
using ESafety.ORM;
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
        public  ActionResult<bool> AddNew(OperateBillNew bill)
        {
            throw new NotImplementedException();
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
    }
}
