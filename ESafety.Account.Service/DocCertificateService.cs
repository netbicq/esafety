using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core;
using ESafety.Core.Model;
using ESafety.Core.Model.DB.Account;
using ESafety.Core.Model.PARA;
using ESafety.ORM;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Service
{
    public class DocCertificateService : ServiceBase, IDocCertificateService
    {
        private IUnitwork _work = null;
        private IRepository<Doc_Certificate> _rpsdc = null;
        private IAttachFile srvFile = null;
        public DocCertificateService(IUnitwork work, IAttachFile file)
        {
            _work = work;
            Unitwork = work;
            _rpsdc = work.Repository<Doc_Certificate>();
            srvFile = file;
        }
        /// <summary>
        /// 新建资质模型
        /// </summary>
        /// <param name="certificateNew"></param>
        /// <returns></returns>
        public ActionResult<bool> AddDocCertificate(DocCertificateNew certificateNew)
        {
            try
            {
                if (certificateNew == null)
                {
                    throw new Exception("参数有误");
                }
                var check = _rpsdc.Any(p => p.Name == certificateNew.Name && p.TypeID == certificateNew.TypeID && p.Owner == certificateNew.Owner);
                if (check)
                {
                    throw new Exception("该资质类型下已存在该资质");
                }
                var dbdc = certificateNew.MAPTO<Doc_Certificate>();
                //电子文档
                var files = new AttachFileSave
                {
                    BusinessID = dbdc.ID,
                    files = from f in certificateNew.AttachFiles
                            select new AttachFileNew
                            {
                                FileTitle = f.FileTitle,
                                FileType = f.FileType,
                                FileUrl = f.FileUrl
                            }
                };

                var fre = srvFile.SaveFiles(files);
                if (fre.state != 200)
                {
                    throw new Exception(fre.msg);
                }
                _rpsdc.Add(dbdc);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 删除资质模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelDocCertificate(Guid id)
        {
            try
            {
                var dbdc = _rpsdc.GetModel(id);
                if (dbdc == null)
                {
                    throw new Exception("未找到该资质模型");
                }
                srvFile.DelFileByBusinessId(id);
                _rpsdc.Delete(dbdc);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 修改资质模型
        /// </summary>
        /// <param name="certificateEdit"></param>
        /// <returns></returns>
        public ActionResult<bool> EditDocCertificate(DocCertificateEdit certificateEdit)
        {
            try
            {
                var dbdc = _rpsdc.GetModel(certificateEdit.ID);
                if (dbdc == null)
                {
                    throw new Exception("未找到所需修改的资质模型");
                }
                var check = _rpsdc.Any(p => p.ID != certificateEdit.ID && p.Name == certificateEdit.Name && p.TypeID == certificateEdit.TypeID && p.Owner == certificateEdit.Owner);
                if (check)
                {
                    throw new Exception("该资质类型下已存在该资质");
                }
                dbdc = certificateEdit.CopyTo<Doc_Certificate>(dbdc);

                //电子文档 
                srvFile.DelFileByBusinessId(dbdc.ID);
                var files = new AttachFileSave
                {
                    BusinessID = dbdc.ID,
                    files = from f in certificateEdit.AttachFiles
                            select new AttachFileNew
                            {
                                FileTitle = f.FileTitle,
                                FileType = f.FileType,
                                FileUrl = f.FileUrl
                            }
                };

                var fre = srvFile.SaveFiles(files);
                if (fre.state != 200)
                {
                    throw new Exception(fre.msg);
                }
                _rpsdc.Update(dbdc);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 获取资质模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<DocCertificateView> GetDocCertificate(Guid id)
        {
            try
            {
                var dbdc = _rpsdc.GetModel(id);
                var re = dbdc.MAPTO<DocCertificateView>();
                return new ActionResult<DocCertificateView>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<DocCertificateView>(ex);
            }
        }
        /// <summary>
        /// 分页获取资质模型
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<DocCertificateView>> GetDocCertificates(PagerQuery<DocCertificateQuery> para)
        {
            try
            {
                var dbdcs = _rpsdc.Queryable(p => p.TypeID == para.Query.TypeID && (p.Name.Contains(para.Query.Name) || string.IsNullOrEmpty(para.Query.Name)));
                var redcs = from s in dbdcs
                            orderby s.Name
                            select new DocCertificateView
                            {
                                ID = s.ID,
                                ApproveDate = s.ApproveDate,
                                InvalidDate = s.InvalidDate,
                                IssueOrg = s.IssueOrg,
                                Name = s.Name,
                                Owner = s.Owner,
                                TypeID = s.TypeID
                            };
                var re = new Pager<DocCertificateView>().GetCurrentPage(redcs, para.PageSize, para.PageIndex);
                return new ActionResult<Pager<DocCertificateView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<DocCertificateView>>(ex);
            }
        }
    }
}
