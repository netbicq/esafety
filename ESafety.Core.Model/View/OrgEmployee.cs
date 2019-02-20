using ESafety.Core.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.View
{
    public class OrgEmployee
    {
     

        

    }
    public class EmployeeView
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string CNName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// Leader
        /// </summary>
        public bool IsLeader { get; set; }
        /// <summary>
        /// 接受平级
        /// </summary>
        public bool IsLevel { get; set; }
        /// <summary>
        /// 操作用户
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// 头像IMG
        /// </summary>
        public string HeadIMG { get; set; }
    }

    public class OrgView
    {
        public Guid ParentID { get; set; }
        /// <summary>
        /// 级次
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string OrgName { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string Principal { get; set; }
        /// <summary>
        /// 负责人电话
        /// </summary>
        public string PrincipalTel { get; set; }

    }
}
