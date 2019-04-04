using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core;
using ESafety.Core.Model;
using ESafety.Core.Model.DB.Account;
using ESafety.Core.Model.PARA;
using ESafety.Core.Model.View;
using ESafety.ORM;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Service
{
    public class PostManageService : ServiceBase,IPostManageService
    {
        private IUnitwork _work = null;
        private IRepository<Basic_Post> _rpspost = null;
        private IRepository<Basic_PostEmployees> _rpspostemp=null;
        private IRepository<Core.Model.DB.Basic_Employee> _rpsemp = null;
        private IRepository<Core.Model.DB.Basic_Org> _rpsorg = null;
        private IUserDefined usedefinedService = null;

        public PostManageService(IUnitwork work,IUserDefined udf)
        {
            _work = work;
            Unitwork = work;
            _rpspost = work.Repository<Basic_Post>();
            _rpspostemp = work.Repository<Basic_PostEmployees>();
            _rpsemp = work.Repository<Core.Model.DB.Basic_Employee>();
            _rpsorg = work.Repository<Core.Model.DB.Basic_Org>();
            usedefinedService = udf;
        }
        /// <summary>
        /// 添加岗位
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public ActionResult<bool> AddPost(PostNew post)
        {

           
            try
            {
                if (post== null)
                {
                    throw new Exception("参数有误");
                }
                var check = _rpspost.Any(p=>p.Name==post.Name);
                if (check)
                {
                    throw new Exception("该岗位已存在");
                }
                var dbpost = post.MAPTO<Basic_Post>();
                var definedvalue = new UserDefinedBusinessValue
                {
                    BusinessID = dbpost.ID,
                    Values = post.UserDefineds
                };
                var defined = usedefinedService.SaveBuisnessValue(definedvalue);
                if (defined.state != 200)
                {
                    throw new Exception(defined.msg);
                }
                _rpspost.Add(dbpost);
                _work.Commit();
                return new ActionResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 为岗位分配人员
        /// </summary>
        /// <param name="postEmployee"></param>
        /// <returns></returns>
        public ActionResult<bool> AddPostEmployee(PostEmployeeNew postEmployee)
        {
            try
            {
                if (postEmployee.EmployeeID==Guid.Empty)
                {
                    throw new Exception("参数有误");
                }
                var check = _rpspostemp.Any(p => p.PostID == postEmployee.PostID && p.EmployeeID == postEmployee.EmployeeID);
                if (check)
                {
                    throw new Exception("该人员已在该岗位下");
                }
                var dbpostemp = postEmployee.MAPTO<Basic_PostEmployees>();
                _rpspostemp.Add(dbpostemp);
                _work.Commit();
                return new ActionResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 删除岗位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelPost(Guid id)
        {
            try
            {
                var dbpost = _rpspost.GetModel(p => p.ID == id);
                if (dbpost==null)
                {
                    throw new Exception("未找到所要删除的岗位");
                }
                var check = _rpspostemp.Any(p=>p.PostID==id);
                if (check)
                {
                    throw new Exception("该岗位已分配人员，无法删除");
                }
                
                _rpspost.Delete(dbpost);
                _work.Commit();
                return new ActionResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 删除岗位与人员的关系
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelPostEmployee(Guid id)
        {
            try
            {
                var check = _rpspostemp.Any(id);
                if (!check)
                {
                    throw new Exception("该岗位未找到该人员");
                }
                _rpspostemp.Delete(p=>p.ID==id);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 修改岗位
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public ActionResult<bool> EditPost(PostEdit post)
        {
            try
            {
                var dbpost = _rpspost.GetModel(p => p.ID == post.ID);
                if (dbpost==null)
                {
                    throw new Exception("未找到所需修改的岗位");
                }
                var check = _rpspost.Any(p => p.Name == post.Name&&p.ID!=post.ID);
                if (check)
                {
                    throw new Exception("该岗位名已存在");
                }
                var _dbpost = post.CopyTo<Basic_Post>(dbpost);

                var definedvalue = new UserDefinedBusinessValue
                {
                    BusinessID = _dbpost.ID,
                    Values = post.UserDefineds
                };
                var defined = usedefinedService.SaveBuisnessValue(definedvalue);
                if (defined.state != 200)
                {
                    throw new Exception(defined.msg);
                }
                _rpspost.Update(_dbpost);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {

                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 根据岗位ID获取人员选择器
        /// </summary>
        /// <param name="postid"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<PostEmpSelect>> GetEmpByPostID(Guid postid)
        {
            var posts = _rpspostemp.Queryable(p => p.PostID==postid);
            var empids = posts.Select(s=>s.EmployeeID);
            var emps = _rpsemp.Queryable(p=>empids.Contains(p.ID));
            var re = from emp in emps
                     select new PostEmpSelect
                     {
                       EmpID=emp.ID,
                       EmpName=emp.CNName
                     };
            return new ActionResult<IEnumerable<PostEmpSelect>>(re);
        }

        /// <summary>
        /// 根据岗位获取所有人员信息
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<PostEmployeesView>> GetEmployeesByPostID(PagerQuery<PostEmployeeQuery> para)
        {
            var posts = _rpspostemp.Queryable(p=>p.PostID==para.Query.PostID);

            var repostemp = from ac in posts.ToList()
                            let o = _rpsemp.GetModel(ac.EmployeeID)
                            select new PostEmployeesView
                            {
                                ID=ac.ID,
                                CNName = o.CNName,
                                Gender = o.Gender,
                                HeadIMG = o.HeadIMG,
                                IsLeader = o.IsLeader,
                                IsLevel = o.IsLevel,
                                Login = o.Login,
                                OrgID = o.OrgID,
                                EmployeeID = o.ID,
                                OrgName = _rpsorg.GetModel(o.OrgID).OrgName
                            };
            var re = new Pager<PostEmployeesView>().GetCurrentPage(repostemp, para.PageSize, para.PageIndex);

            return new ActionResult<Pager<PostEmployeesView>>(re);
        }

     

        /// <summary>
        /// 获取岗位信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<PostView> GetPost(Guid id)
        {
            try
            {
                var post = _rpspost.GetModel(id);
                var re = post.MAPTO<PostView>();
                return new ActionResult<PostView>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<PostView>(ex);
            }
        }
        /// <summary>
        /// 获取岗位列表
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<PostView>> GetPosts()
        {
            try
            {
                var dbposts = _rpspost.Queryable();
                var re = dbposts.MAPTO<PostView>();
                return new ActionResult<IEnumerable<PostView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<PostView>>(ex);
            }
        }

        /// <summary>
        /// 获取岗位页
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<PostView>> GetPostsPage(PagerQuery<PostQuery> para)
        {
            
            var posts = _rpspost.Queryable(q => q.Name.Contains(para.Query.Name)||q.Code.Contains(para.Query.Code) || string.IsNullOrEmpty(para.Query.Name)||string.IsNullOrEmpty(para.Query.Code));

            var repost = from ac in posts
                        orderby ac.Code descending
                        select new PostView
                        {
                            Code=ac.Code,
                            ID=ac.ID,
                            Name=ac.Name,
                            Principal=ac.Principal,
                            PrincipalTel=ac.PrincipalTel
                        };

            var re = new Pager<PostView>().GetCurrentPage(repost, para.PageSize, para.PageIndex);

            return new ActionResult<Pager<PostView>>(re);
        }
    }
}
