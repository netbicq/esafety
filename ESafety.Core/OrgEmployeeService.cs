using ESafety.Core.Model;
using ESafety.Core.Model.PARA;
using ESafety.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core
{

    /// <summary>
    /// 组织架构和人员      /// 
    /// </summary>
    public class OrgEmployeeService : ServiceBase, IOrgEmployee
    {
        private IUnitwork _work = null;


        public OrgEmployeeService(IUnitwork work)
        {
            _work = work;
            Unitwork = work;
        }

        public ActionResult<bool> AddOrg(OrgNew org)
        {
            throw new NotImplementedException();
        }

        public ActionResult<bool> EditOrg(OrgEdit org)
        {
            throw new NotImplementedException();
        }
    }
}
