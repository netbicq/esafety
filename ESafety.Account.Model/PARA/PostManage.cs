using ESafety.Core.Model.PARA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    class PostManage
    {
    }

    public class PostNew
    {
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
        /// <summary>
        /// 自定义类项
        /// </summary>
        public IEnumerable<UserDefinedValue> UserDefineds { get; set; }
    }

    public class PostEdit:PostNew
    {
        /// <summary>
        /// 岗位ID
        /// </summary>
        public Guid ID { get; set; }
    }

    public class PostEmployeeNew
    {
        /// <summary>
        /// 岗位ID
        /// </summary>
        public Guid PostID { get; set; }
        /// <summary>
        /// 人员ID
        /// </summary>
        public Guid EmployeeID { get; set; }
    }

    public class PostQuery
    {
        /// <summary>
        ///编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string Name { get; set; }
    }

    public class PostEmployeeQuery
    {
        /// <summary>
        /// 岗位ID
        /// </summary>
        public Guid PostID { get; set; }
    }

}
