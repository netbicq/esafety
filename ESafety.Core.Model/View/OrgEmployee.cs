using ESafety.Core.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.View
{
 
    public class EmployeeView
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        public Guid ID { get; set; }
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
        /// <summary>
        /// 组织ID
        /// </summary>
        public Guid OrgID { get; set; }
        
    }

    public class EmployeeModelView:EmployeeView
    {
        /// <summary>
        /// 自定义项
        /// </summary>
        public IEnumerable<UserDefinedForm> UserDefineds { get; set; }
    }

    public class OrgView
    {

        public Guid ID { get; set; }
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
