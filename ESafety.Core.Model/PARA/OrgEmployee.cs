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
        /// <summary>
        /// 上级ID
        /// </summary>
        public Guid ParentID { get; set; }
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
    /// <summary>
    /// 修改组织架构
    /// </summary>
    public class OrgEdit
    {
        /// <summary>
        /// ID
        /// </summary>

        public Guid ID { get; set; }
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
        public Guid OrgId { get; set; }
        /// <summary>
        /// 自定义类项
        /// </summary>
        public IEnumerable<UserDefinedValue> UserDefineds { get; set; }
    }


    public class EmployeeEdit:EmployeeNew
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid ID { get; set; }
    }

    public class EmployeeQuery
    {
        /// <summary>
        /// 组织ID
        /// </summary>
        public Guid ID { get; set; }
    }
}
