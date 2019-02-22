﻿using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
using ESafety.Core.Model.DB.Account;
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
       
        public PostManageService(IUnitwork work)
        {
            _work = work;
            Unitwork = work;
            _rpspost = work.Repository<Basic_Post>();
            _rpspostemp = work.Repository<Basic_PostEmployees>();
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
                var check = _rpspost.Any(p=>p.Name==post.Name);
                if (check)
                {
                    throw new Exception("该岗位已存在");
                }
                var dbpost = post.MAPTO<Basic_Post>();
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
        public ActionResult<bool> AddPosteEmployee(PostEmployeeNew postEmployee)
        {
            try
            {
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

        public ActionResult<Pager<PostView>> GetPosts(PagerQuery<string> para)
        {
            throw new NotImplementedException();
        }
    }
}
