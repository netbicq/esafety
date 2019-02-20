using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.PARA
{
    /// <summary>
    /// 新建组织架构
    /// </summary>
    public class OrgNew
    {

        public Guid PrentID { get; set; }

        public string OrgName { get; set; }

        public string Principal { get; set; }

        public string PrincipalTel { get; set; }
    }
    /// <summary>
    /// 修改组织架构
    /// </summary>
    public class OrgEdit
    {

        public Guid ID { get; set; }

        public string OrgName { get; set; }

        public string Principal { get; set; }

        public string PrincipalTel { get; set; }

    }

    public class EmployeeNew
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
        //组织ID
        public Guid OrdId { get; set; }
    }


    public class EmployeeEdit:EmployeeNew
    {
        public Guid ID { get; set; }
    }


}
