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
        private IRepository<Auth_User> rpsUser = null;
        private IRepository<Auth_UserProfile> rpsUserProfile = null;


        private IUserDefined usedefinedService = null;
        private ITree srvTree = null;

        public OrgEmployeeService(IUnitwork work, IUserDefined udf, ITree tree)
        {
            _work = work;
            Unitwork = work;
            _rpsorg = work.Repository<Basic_Org>();
            _rpsemployee = work.Repository<Basic_Employee>();
            usedefinedService = udf;
            srvTree = tree;
            rpsUser = work.Repository<Auth_User>();
            rpsUserProfile = work.Repository<Auth_UserProfile>();

        }

        /// <summary>
        /// 新增人员
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public ActionResult<bool> AddEmployee(EmployeeNew employee)
        {
            try
            {
                if (employee == null)
                {
                    throw new Exception("参数有误");
                }
                var check = _rpsemployee.Any(q => q.Jobno == employee.Jobno);
                if (check)
                {
                    throw new Exception("已存在用户工号 ：" + employee.Jobno);
                }
                var _employee = employee.MAPTO<Basic_Employee>();
                var definedvalue = new UserDefinedBusinessValue
                {
                    BusinessID = _employee.ID,
                    Values = employee.UserDefineds
                };
                var defined = usedefinedService.SaveBuisnessValue(definedvalue);
                if (defined.state != 200)
                {
                    throw new Exception(defined.msg);
                }
                //新建账号
                if (employee.IsCreate)
                {
                    check = rpsUser.Any(p => p.Login == employee.Jobno);
                    if (check)
                    {
                        throw new Exception("已存在用户：" + employee.Jobno);
                    }
                    //登录账号
                    var dbuser = new Model.DB.Auth_User()
                    {
                        Login = employee.Jobno,
                        OtherEdit = true,
                        OtherView = true,
                        Pwd = "123456",
                        TokenValidTime = DateTime.Now,
                        State = 1,
                        Token = "",
                    };
                    //个人资料
                    var profile = new Model.DB.Auth_UserProfile()
                    {
                        CNName = employee.CNName,
                        Login = employee.Jobno,
                        HeadIMG = "",
                        Tel = employee.Tel
                    };
                    //角色
                    var userrole = _work.Repository<Model.DB.Auth_UserRole>();
                    var dbroles = new List<Model.DB.Auth_UserRole>();
                    foreach (var role in employee.RoleIDs)
                    {
                        var urole = new Model.DB.Auth_UserRole
                        {
                            Login = employee.Jobno,
                            RoleID = role,
                            ID = Guid.NewGuid()
                        };
                        dbroles.Add(urole);
                    };
                    userrole.Add(dbroles);
                    rpsUser.Add(dbuser);
                    rpsUserProfile.Add(profile);
                }
                _employee.Login = employee.Jobno;
                _employee.IsQuit = false;
                _rpsemployee.Add(_employee);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }

        }

        /// <summary>
        /// 新建组织
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        public ActionResult<bool> AddOrg(OrgNew org)
        {
            var check = _rpsorg.Any(q => q.ParentID == org.ParentID && q.OrgName == org.OrgName);
            if (check)
            {
                throw new Exception("已经存在相同的组织名称 ：" + org.OrgName);
            }
            var parent = _rpsorg.GetModel(org.ParentID);

            var _org = org.MAPTO<Basic_Org>();
            //根据上级设置Level
            _org.Level = parent == null ? 1 : parent.Level + 1;

            _rpsorg.Add(_org);
            _work.Commit();
            return new ActionResult<bool>(true);
        }

        public ActionResult<bool> DeleteEmployee(Guid id)
        {
            try
            {
                var dbemployee = _rpsemployee.GetModel(id);
                if (dbemployee == null)
                {
                    throw new Exception("该人员不存在!");
                }
                //作业务检查
                var check = _work.Repository<Core.Model.DB.Account.Bll_InspectTask>().Any(p => p.EmployeeID == id);
                if (check)
                {
                    throw new Exception("该人员已分配任务，无法删除!");
                }
                check = _work.Repository<Core.Model.DB.Account.Bll_TaskBill>().Any(p => p.EmployeeID == id);
                if (check)
                {
                    throw new Exception("该人员已存在任务单据，无法删除!");
                }
                check = _work.Repository<Core.Model.DB.Account.Bll_OpreateionBillFlow>().Any(p => p.FlowEmployeeID == id);
                if (check)
                {
                    throw new Exception("该人员已存在作业流程单，无法删除!");
                }

                _rpsemployee.Delete(p => p.ID == id);
                //删除自定义项
                usedefinedService.DeleteBusinessValue(id);


                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }

        }
        /// <summary>
        /// 删除组织结构
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DeleteOrg(Guid id)
        {
            try
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
                var empcheck = _rpsemployee.Any(q => q.OrgID == id);
                if (empcheck)
                {
                    throw new Exception("组织架构下存在有人员数据，不允许删除");
                }

                _rpsorg.Delete(p => p.ID == id);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }

        }

        /// <summary>
        /// 修改人员信息
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public ActionResult<bool> EditEmployee(EmployeeEdit employee)
        {
            try
            {
                var dbemployee = _rpsemployee.GetModel(q => q.ID == employee.ID);
                if (dbemployee == null)
                {
                    throw new Exception("未找到该人员信息");
                }
                var check = _rpsemployee.Any(p => p.ID != employee.ID && p.Jobno == employee.Jobno);
                if (check)
                {
                    throw new Exception("已经存在相同的工号：" + employee.Jobno);
                }
                dbemployee = employee.CopyTo<Basic_Employee>(dbemployee);

                //角色
                var userrole = _work.Repository<Model.DB.Auth_UserRole>();
                var dbroles = new List<Model.DB.Auth_UserRole>();
                foreach (var role in employee.RoleIDs)
                {
                    var urole = new Model.DB.Auth_UserRole
                    {
                        Login = dbemployee.Login,
                        RoleID = role,
                        ID = Guid.NewGuid()
                    };
                    dbroles.Add(urole);
                };
                userrole.Delete(p => p.Login == dbemployee.Login);
                userrole.Add(dbroles);

                //自定义项
                var definevalue = new UserDefinedBusinessValue
                {
                    BusinessID = dbemployee.ID,
                    Values = employee.UserDefineds
                };
                var defined = usedefinedService.SaveBuisnessValue(definevalue);
                if (defined.state != 200)
                {
                    throw new Exception(defined.msg);
                }
                _rpsemployee.Update(dbemployee);


                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }

        }


        ///// <summary>
        ///// 递归算树
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public IEnumerable<OrgTree> GetOrgTree(Guid id)
        //{
        //    try
        //    {
        //        List<OrgTree> re = new List<OrgTree>();


        //        var orgall = from org in _rpsorg.Queryable(q => q.ParentID == id)
        //                     select org;

        //        foreach (var org in orgall)
        //        {
        //            var orgtree = org.MAPTO<OrgTree>();
        //            orgtree.Children.AddRange(GetOrgTree(org.ID));
        //            re.Add(orgtree);
        //        }


        //        return re;


        //    }
        //    catch (Exception ex)
        //    {
        //        return new List<OrgTree>();
        //    }
        //}

        //public IEnumerable<Guid> GetChildIds(Guid id)
        //{

        //    List<Guid> re = new List<Guid>();
        //    re.Add(id);
        //    var orgall = from org in _rpsorg.Queryable(q => q.ParentID == id)
        //                 select org;

        //    foreach (var t in orgall)
        //    {
        //        var ids = GetChildIds(t.ID);
        //        re.AddRange(ids);
        //    }

        //    return re;
        //}

        //public IEnumerable<Basic_Org> GetParentIds(Guid id)
        //{
        //    List<Basic_Org> re = new List<Basic_Org>();

        //    var cnode = _rpsorg.GetModel(id);
        //    if(cnode != null)
        //    { 
        //        var parent = _rpsorg.GetModel(q => q.ID == cnode.ParentID);
        //        if (parent != null)
        //        {

        //            re.Add(parent);
        //            re.AddRange(GetParentIds(parent.ID));
        //        }
        //    }

        //    return re;

        //}
        /// <summary>
        /// 修改组织结构
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        public ActionResult<bool> EditOrg(OrgEdit org)
        {
            try
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
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }

        }

        /// <summary>
        /// 根据组织ID，返回人员列表
        /// </summary>
        /// <param name="orgid"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<EmployeeView>> GetEmployeelist(Guid orgid)
        {
            var emps = _rpsemployee.Queryable(q => q.OrgID == orgid && q.IsQuit == false);
            var org = _rpsorg.GetModel(orgid);
            var reemps = from em in emps
                         select new EmployeeView
                         {
                             ID = em.ID,
                             CNName = em.CNName,
                             Gender = em.Gender,
                             IsLeader = em.IsLeader,
                             IsLevel = em.IsLevel,
                             HeadIMG = em.HeadIMG,
                             Login = em.Login,
                             OrgID = em.OrgID,
                             OrgName = org.OrgName
                         };
            return new ActionResult<IEnumerable<EmployeeView>>(reemps);
        }

        /// <summary>
        /// 获取职员模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<EmployeeModelView> GetEmployeeModel(Guid id)
        {
            try
            {
                var employee = _rpsemployee.GetModel(id);
                var re = employee.MAPTO<EmployeeModelView>();
                re.OrgName = _rpsorg.GetModel(employee.OrgID).OrgName;
                //获取业务数据的自定义
                //var defines = usedefinedService.GetUserDefineItems(new UserDefinedBusiness
                //{
                //    BusinessID = employee.ID,
                //    DefinedType = PublicEnum.EE_UserDefinedType.Employee
                //});
                //if (defines.state == 200)
                //{
                //    re.UserDefineds = defines.data;
                //}

                return new ActionResult<EmployeeModelView>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<EmployeeModelView>(ex);
            }
        }

        public ActionResult<Pager<EmployeeView>> GetEmployees(PagerQuery<EmployeeQuery> para)
        {
            try
            {
                //var dbemployees = _rpsemployee.Queryable();
                //var orgids = from ids in _rpsorg.GetList(p => p.ParentID == orgid)
                //             select ids.ID;


                //var re = from employee in dbemployees.ToList()
                //         where orgids.Contains(employee.OrgID) || employee.ID == orgid
                //         select new EmployeeView
                //         {
                //             CNName = employee.CNName,
                //             Gender = employee.Gender,
                //             IsLeader = employee.IsLeader,
                //             IsLevel = employee.IsLevel,
                //             HeadIMG = employee.HeadIMG,
                //             Login = employee.Login
                //         };

                var emps = _rpsemployee.Queryable(q => q.OrgID == para.Query.ID);
                var orgs = _rpsorg.Queryable();
                var reemps = from em in emps
                             let or = orgs.FirstOrDefault(p=>p.ID==em.OrgID)
                             orderby em.CNName
                             select new EmployeeView
                             {
                                 ID = em.ID,
                                 CNName = em.CNName,
                                 Gender = em.Gender,
                                 IsLeader = em.IsLeader,
                                 IsLevel = em.IsLevel,
                                 HeadIMG = em.HeadIMG,
                                 Login = em.Login,
                                 OrgID = em.OrgID,
                                 OrgName = or.OrgName
                             };

                var re = new Pager<EmployeeView>().GetCurrentPage(reemps, para.PageSize, para.PageIndex);
                return new ActionResult<Pager<EmployeeView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<EmployeeView>>(ex);
            }

        }



        public ActionResult<IEnumerable<OrgView>> GetOrgChildren(Guid id)
        {
            try
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
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<OrgView>>(ex);
            }

        }

        public ActionResult<IEnumerable<OrgTree>> GetTree(Guid id)
        {
            try
            {
                (srvTree as TreeService).AppUser = AppUser;

                var re = srvTree.GetTree<Basic_Org, OrgTree>(id);

                return new ActionResult<IEnumerable<OrgTree>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<OrgTree>>(ex);
            }
        }
        /// <summary>
        /// 获取父级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<Basic_Org>> GetParents(Guid id)
        {
            try
            {
                (srvTree as TreeService).AppUser = AppUser;

                var re = srvTree.GetParents<Basic_Org>(id);

                return new ActionResult<IEnumerable<Basic_Org>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<Basic_Org>>(ex);
            }
        }
        /// <summary>
        /// 获取子级ID集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<Guid>> GetChildren(Guid id)
        {
            try
            {
                (srvTree as TreeService).AppUser = AppUser;

                var re = srvTree.GetChildrenIds<Basic_Org>(id);

                return new ActionResult<IEnumerable<Guid>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<Guid>>(ex);
            }
        }
        /// <summary>
        /// 人员离职
        /// </summary>
        /// <param name="employeeQuit"></param>
        /// <returns></returns>
        public ActionResult<bool> EmployeeQuit(EmployeeQuit employeeQuit)
        {
            try
            {
                if (employeeQuit == null)
                {
                    throw new Exception("参数错误!");
                }
                var dbemp = _rpsemployee.GetModel(employeeQuit.ID);
                if (dbemp == null)
                {
                    throw new Exception("未找到该人员!");
                }
                dbemp.QuitDate = employeeQuit.QuitDate;
                dbemp.IsQuit = true;
                _rpsemployee.Update(dbemp);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
    }
}
