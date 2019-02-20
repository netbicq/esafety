using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.PARA;
using ESafety.Core.Model.View;
using ESafety.ORM;
using ESafety.Unity;
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
        private IRepository<Model.DB.Basic_Org> _rpsorg = null;
        private IRepository<Basic_Employee> _rpsemployee = null;

        private IUserDefined usedefinedService = null;

        public OrgEmployeeService(IUnitwork work)
        {
            _work = work;
            Unitwork = work;
            _rpsorg = work.Repository<Basic_Org>();
            _rpsemployee = work.Repository<Basic_Employee>();
            usedefinedService = new UserDefinedService(work);

        }

        /// <summary>
        /// 新增人员
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public ActionResult<bool> AddEmployee(EmployeeNew employee)
        {
            var check = _rpsemployee.Any(q => q.Login == employee.Login);
            if (check)
            {
                throw new Exception("已经存在相同的用户 ：" + employee.Login);
            }
            var _employee = employee.MAPTO<Basic_Employee>();
            _rpsemployee.Add(_employee);
           
            _work.Commit();
            return new ActionResult<bool>(true);
        }

        /// <summary>
        /// 新建组织
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        public ActionResult<bool> AddOrg(OrgNew org)
        {
            var check = _rpsorg.Any(q => q.ParentID == org.PrentID && q.OrgName == org.OrgName);
            if (check)
            {
                throw new Exception("已经存在相同的组织名称 ：" + org.OrgName);
            }
            var _org = org.MAPTO<Basic_Org>();
            _rpsorg.Add(_org);
            _work.Commit();
            return new ActionResult<bool>(true);
        }

        public ActionResult<bool> DeleteEmployee(Guid id)
        {
            var dbemployee = _rpsemployee.Any(q => q.ID == id);
            if (!dbemployee)
            {
                throw new Exception("该人员不存在!");
            }

            //作业务检查

            _rpsemployee.Delete(p => p.ID == id);
            _work.Commit();
            return new ActionResult<bool>(true);
        }
        /// <summary>
        /// 删除组织结构
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DeleteOrg(Guid id)
        {
            var dborg = _rpsorg.Any(q => q.ID == id);
            if (!dborg)
            {
                throw new Exception("该组织结构不存在!");
            }
            var check = _rpsorg.Any(q => q.ParentID == id);
            if (check)
            {
                throw new Exception("该组织存在子组织无法删除!");
            }

            //检查人员是否存在

            _rpsorg.Delete(p => p.ID == id);
            _work.Commit();
            return new ActionResult<bool>(true);
        }

        /// <summary>
        /// 修改人员信息
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public ActionResult<bool> EditEmployee(EmployeeEdit employee)
        {
            var dbemployee = _rpsemployee.GetModel(q => q.ID == employee.ID);
            if (dbemployee == null)
            {
                throw new Exception("未找到该人员信息");
            }
            var check = _rpsemployee.Any(p => p.ID != employee.ID && p.Login == employee.Login);
            if (check)
            {
                throw new Exception("已经存在相同的用户：" + employee.Login);
            }
            dbemployee = employee.CopyTo<Basic_Employee>(dbemployee);
            _rpsemployee.Update(dbemployee);
            _work.Commit();
            return new ActionResult<bool>(true);
        }

        /// <summary>
        /// 修改组织结构
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        public ActionResult<bool> EditOrg(OrgEdit org)
        {
            var dborg = _rpsorg.GetModel(q => q.ID == org.ID);
            if (dborg == null)
            {
                throw new Exception("未找到组织");
            }
            var check = _rpsorg.Any(p => p.OrgName == org.OrgName && p.ParentID == dborg.ParentID && p.ID != org.ID);
            if (check)
            {
                throw new Exception("已经存在相同的组织名称：" + org.OrgName);
            }
            dborg = org.CopyTo<Basic_Org>(dborg);
            _rpsorg.Update(dborg);
            _work.Commit();
            return new ActionResult<bool>(true);
        }

        public ActionResult<EmployeeView> GetEmployeeModel(Guid id)
        {

            var employee = _rpsemployee.GetModel(id);
            var re = employee.MAPTO<EmployeeView>();
            return new ActionResult<EmployeeView>(re);
        }

        public ActionResult<IEnumerable<EmployeeView>> GetEmployees(Guid orgid)
        {
            var dbemployees = _rpsemployee.Queryable();
            var orgids = from ids in _rpsorg.GetList(p => p.ParentID == orgid)
                         select ids.ID;
            var re = from employee in dbemployees.ToList()
                     where orgids.Contains(employee.OrgID) || employee.ID == orgid
                     select new EmployeeView
                     {
                         CNName = employee.CNName,
                         Gender = employee.Gender,
                         IsLeader = employee.IsLeader,
                         IsLevel = employee.IsLevel,
                         HeadIMG = employee.HeadIMG,
                         Login = employee.Login
                     };
            return new ActionResult<IEnumerable<EmployeeView>>(re);
        }

        public ActionResult<IEnumerable<OrgView>> GetOrgChildren(Guid id)
        {
            var dborgs = _rpsorg.Queryable(p => p.ParentID == id);
            var re = from org in dborgs.ToList()
                     select new OrgView
                     {
                         Level = org.Level,
                         ID = org.ID,
                         OrgName = org.OrgName,
                         ParentID = org.ParentID,
                         Principal = org.Principal,
                         PrincipalTel = org.PrincipalTel
                     };
            return new ActionResult<IEnumerable<OrgView>>(re);
        }
 
    }
}
