using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.DB
{
    public class Bll_AttachFile:ModelBase
    {
        /// <summary>
        /// 业务id
        /// </summary>
        public Guid BusinessID { get; set; }
        /// <summary>
        /// 文件标题
        /// </summary>
        public string FileTitle { get; set; }
        /// <summary>
        /// 文件url
        /// </summary>
        public string FileUrl { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType { get; set; }
    }
}
