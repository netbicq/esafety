using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.PARA;
using ESafety.Core.Model.View;
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
        /// <summary>
        /// 修改组织架构
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        ActionResult<bool> EditOrg(OrgEdit org);
        /// <summary>
        /// 删除组织架构
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<bool> DeleteOrg(Guid id);
        /// <summary>
        /// 新建人员
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        ActionResult<bool> AddEmployee(EmployeeNew employee);
        /// <summary>
        /// 修改人员
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        ActionResult<bool> EditEmployee(EmployeeEdit employee);
        /// <summary>
        /// 获取人员模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<EmployeeModelView> GetEmployeeModel(Guid id);
        /// <summary>
        /// 删除人员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<bool> DeleteEmployee(Guid id);
        /// <summary>
        /// 分页获取人员
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<Pager<Model.View.EmployeeView>> GetEmployees(PagerQuery<EmployeeQuery> para);
        /// <summary>
        /// 根据组织架构id获取人员
        /// </summary>
        /// <param name="orgid"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<EmployeeView>> GetEmployeelist(Guid orgid);
        /// <summary>
        /// 获取组织架构子集
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<Model.View.OrgView>> GetOrgChildren(Guid id);
        /// <summary>
        /// 获取组织架构树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<OrgTree>> GetTree(Guid id);
        /// <summary>
        /// 获取组织架构父级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<Basic_Org>> GetParents(Guid id);
        /// <summary>
        /// 获取子集id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<Guid>> GetChildren(Guid id);
        /// <summary>
        /// 人员离职
        /// </summary>
        /// <param name="employeeQuit"></param>
        /// <returns></returns>
        ActionResult<bool> EmployeeQuit(EmployeeQuit employeeQuit);



    }
}
