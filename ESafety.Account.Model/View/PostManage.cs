using ESafety.Core.Model.PARA;
using System;
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

    public class PostModel
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
        public Guid Principal { get; set; }
        /// <summary>
        /// 组织架构ID
        /// </summary>
        public Guid Org { get; set; }
        /// <summary>
        /// 主要任务
        /// </summary>
        public string MainTasks { get; set; }
    }






    public class PostEmployeesView
    {
        /// <summary>
        /// 人员和岗位的关系ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 人员ID
        /// </summary>
        public Guid EmployeeID { get; set; }
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
        /// <summary>
        /// 组织名称
        /// </summary>
        public string OrgName { get; set; }
    }

    public class PostEmpSelect
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        public Guid EmpID { get; set; }
        /// <summary>
        /// 人员名
        /// </summary>
        public string EmpName { get; set; }
    }


}
