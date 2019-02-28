using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.DB.Account;
using ESafety.Core.Model.PARA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core
{

    /// <summary>
    /// 电子附件
    /// </summary>
    public interface IAttachFile
    {

        /// <summary>
        /// 保存电子文件
        /// 不提交数据库与业务一起提交
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        ActionResult<bool> SaveFiles(AttachFileSave file);
        /// <summary>
        /// 删除方件同时删除服务器的io文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<bool> DelFile(Guid id);
        /// <summary>
        /// 根据业务id获取电子文件集合
        /// </summary>
        /// <param name="buisnessid"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<Bll_AttachFile>> GetFiles(Guid buisnessid);

        /// <summary>
        /// 根据业务id 删除文件
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        ActionResult<bool> DelFileByBusinessId(Guid guid);
    }
}
