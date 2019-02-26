﻿using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
using ESafety.Core.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.IService
{
    public interface IPostManageService
    {
        ActionResult<bool> AddPost(PostNew post);

        ActionResult<bool> DelPost(Guid id);

        ActionResult<bool> EditPost(PostEdit post);

        ActionResult<PostView> GetPost(Guid id);
        ActionResult<IEnumerable<PostView>> GetPosts();
        ActionResult<Pager<PostView>> GetPostsPage(PagerQuery<PostQuery> para);

        ActionResult<Pager<PostEmployeesView>> GetEmployeesByPostID(PagerQuery<PostEmployeeQuery> para);

        ActionResult<bool> DelPostEmployee(Guid id);
    }
}
