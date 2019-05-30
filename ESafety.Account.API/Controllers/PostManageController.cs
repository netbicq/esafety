using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
using ESafety.Web.Unity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ESafety.Account.API.Controllers
{
    /// <summary>
    /// 岗位管理
    /// </summary>
    [RoutePrefix("api/postmanage")]
    public class PostManageController : ESFAPI
    {
        private IPostManageService bll = null;

        public PostManageController(IPostManageService pm)
        {
            bll = pm;
            BusinessServices =new List<object>() { pm };
        }
        /// <summary>
        /// 新建岗位模型
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addpost")]
        public ActionResult<bool> AddPost(PostNew post)
        {
            LogContent = "新建岗位模型，参数源：" + JsonConvert.SerializeObject(post);
            return bll.AddPost(post);
        }

        /// <summary>
        /// 新建岗位与人员的关系
        /// </summary>
        /// <param name="postEmployee"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addpostemp")]
        public ActionResult<bool> AddPostEmployee(PostEmployeeNew postEmployee)
        {
            LogContent = "新建岗位与人员的关系模型,参数源:" + JsonConvert.SerializeObject(postEmployee);
            return bll.AddPostEmployee(postEmployee);
        }

        /// <summary>
        /// 删除岗位模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delpost/{id:Guid}")]
        public ActionResult<bool> DelPost(Guid id)
        {
            LogContent = "删除岗位模型，ID：" + JsonConvert.SerializeObject(id);
            return bll.DelPost(id);
        }
        /// <summary>
        /// 根据ID，删除岗位与人员的关系模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delpostemp/{id:Guid}")]
        public ActionResult<bool> DelPostEmployee(Guid id)
        {
            LogContent = "删除了岗位与人员的关系模型,ID为:" + JsonConvert.SerializeObject(id);
            return bll.DelPostEmployee(id);
        }

        /// <summary>
        /// 修改岗位模型
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editpost")]
        public ActionResult<bool> EditPost(PostEdit post)
        {
            LogContent = "修改岗位模型，参数源：" + JsonConvert.SerializeObject(post);
            return bll.EditPost(post);

        }
        /// <summary>
        /// 根据岗位ID，分页获取人员信息
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getempbypostid")]
        public ActionResult<Pager<PostEmployeesView>> GetEmployeesByPostID(PagerQuery<PostEmployeeQuery> para)
        {
            return bll.GetEmployeesByPostID(para);
        }
        /// <summary>
        /// 根据岗位ID,获取人员选择器
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getempbypost/{postid:Guid}")]
        public ActionResult<IEnumerable<PostEmpSelect>> GetEmpByPostID(Guid postid)
        {
            return bll.GetEmpByPostID(postid);
        }

        /// <summary>
        /// 根据岗位ID，获取岗位模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getpost/{id:Guid}")]
        public ActionResult<PostModel> GetPost(Guid id)
        {
            return bll.GetPost(id);
        }
        /// <summary>
        /// 获取岗位模型列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getposts")]
        public ActionResult<IEnumerable<PostModel>> GetPosts()
        {
            return bll.GetPosts();
        }
        /// <summary>
        /// 分页获取岗位
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getpostpage")]
        public ActionResult<Pager<PostView>> GetPostsPage(PagerQuery<PostQuery> para)
        {
            return bll.GetPostsPage(para);
        }
    }
}
