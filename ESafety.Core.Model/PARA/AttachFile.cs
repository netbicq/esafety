using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.PARA
{

    /// <summary>
    /// 新建电子文档
    /// </summary>
    public class AttachFileNew
    {

        /// <summary>
        /// 文件标题
        /// </summary>
        public string FileTitle { get; set; }
        /// <summary>
        /// 文件地址
        /// </summary>
        public string FileUrl { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType { get; set; }

    }
    /// <summary>
    /// 保存电子文件
    /// </summary>
    public class AttachFileSave
    {
        /// <summary>
        /// 业务id
        /// </summary>
        public Guid BusinessID { get; set; }
        /// <summary>
        /// 文件集合
        /// </summary>
        public IEnumerable<AttachFileNew> files { get; set; }
    }
}
