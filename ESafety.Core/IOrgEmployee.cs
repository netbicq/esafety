using ESafety.Core.Model;
using ESafety.Core.Model.PARA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core
{
    /// <summary>
    /// 组织加构，人员
    /// </summary>
    public interface IOrgEmployee
    {
        /// <summary>
        /// 新建组织架构
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        ActionResult<bool> AddOrg(OrgNew org);

        ActionResult<bool> EditOrg(OrgEdit org);

        ActionResult<bool> DeleteOrg(Guid id);

        ActionResult<bool> AddEmployee(EmployeeNew employee);

        ActionResult<bool> EditEmployee(EmployeeEdit employee);

        ActionResult<bool> DeleteEmployee(Guid id);

        ActionResult<IEnumerable<Model.View.EmployeeView>> GetEmployee(Guid orgid);

        ActionResult<IEnumerable<Model.View.OrgView>> GetOrgChildren(Guid id);

        ActionResult<IEnumerable<Model.View.OrgView>> GetOrgRoot();
    }
}
