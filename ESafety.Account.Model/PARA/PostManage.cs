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
        public Guid Principal { get; set; }
        /// <summary>
        /// 组织架构ID
        /// </summary>
        public Guid Org { get; set; }
        /// <summary>
        /// 主要任务
        /// </summary>
        public string MainTasks { get; set; }
        /// <summary>
        /// 自定义类项
        /// </summary>
        public IEnumerable<UserDefinedValue> UserDefineds { get; set; }

        /// <summary>
        /// 文件列表
        /// </summary>
        public IEnumerable<AttachFileNew> fileNews { get; set; }
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
        public IEnumerable<Guid> EmployeeID { get; set; }
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
