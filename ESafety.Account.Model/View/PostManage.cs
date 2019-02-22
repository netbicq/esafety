﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    class PostManage
    {
    }

    public class PostView
    {
        /// <summary>
        /// 岗位ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        ///编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string Principal { get; set; }
        /// <summary>
        /// 负责人电话
        /// </summary>
        public string PrincipalTel { get; set; }
    }

    public class PostEmployeeView
    {
        /// <summary>
        /// 岗位ID
        /// </summary>
        public Guid PostID { get; set; }
        /// <summary>
        /// 人员ID
        /// </summary>
        public Guid EmployeeID { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string PostName { get; set; }
        /// <summary>
        /// 人员名称
        /// </summary>
        public string EmployeeName { get; set; }
    }
}
