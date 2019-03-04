using ESafety.Core.Model;
using ESafety.Core.Model.DB.Account;
using ESafety.Core.Model.PARA;
using ESafety.ORM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ESafety.Unity;
using ESafety.Core.Model.DB;

namespace ESafety.Core
{

    /// <summary>
    /// 电子附件
    /// </summary>
    public class AttachFileService : ServiceBase, IAttachFile
    {

        private IUnitwork _work = null;

        private IRepository<Bll_AttachFile> rpsFile = null;


        public AttachFileService(IUnitwork work)
        {
            _work = work;
            Unitwork = work;
            rpsFile = work.Repository<Bll_AttachFile>();

        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelFile(Guid id)
        {
            try
            {

                var file = rpsFile.GetModel(id);
                if (file == null)
                {
                    throw new Exception("文件不存在");
                }
                rpsFile.Delete(q => q.ID == id);

                var filepath = HttpContext.Current.Server.MapPath(file.FileUrl);
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }

        }

        /// <summary>
        /// 根据业务id 删除文件
        /// 在业务处统一提交
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ActionResult<bool> DelFileByBusinessId(Guid guid)
        {
            try
            {
                var file = rpsFile.GetList(r => r.BusinessID == guid);
                
                rpsFile.Delete(q => q.BusinessID == guid);
                var delfiles = file.Select(r => {
                    var filepath = HttpContext.Current.Server.MapPath(r.FileUrl);
                    if (File.Exists(filepath))
                    {
                        File.Delete(filepath);
                    }
                    return r.ID;
                });

                rpsFile.Delete(q => delfiles.Contains(q.ID));

                return new ActionResult<bool>(true);

            }
            catch(Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
            
        }




        /// <summary>
        /// 根据业务id获取电子文件集合
        /// </summary>
        /// <param name="buisnessid"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<Bll_AttachFile>> GetFiles(Guid buisnessid)
        {
            try
            {
                var files = rpsFile.Queryable(q => q.BusinessID == buisnessid);
                return new ActionResult<IEnumerable<Bll_AttachFile>>(files);

            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<Bll_AttachFile>>(ex);
            }
        }
        /// <summary>
        /// 保存电子文档
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public ActionResult<bool> SaveFiles(AttachFileSave file)
        {
            try
            {
                var dbfiles = file.files.MAPTO<Bll_AttachFile>();
                rpsFile.Delete(q => q.BusinessID == file.BusinessID);
                rpsFile.Add(dbfiles);
                //不提交，与业务一起提交
                return new ActionResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
    }
}
