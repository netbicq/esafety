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
    /// <summary>
    /// 安全制度
    /// </summary>
    public class DocInstitutionService :ServiceBase, IDocInstitutionService
    {
        private IUnitwork _work = null;

        private IRepository<Doc_Institution> _rpsin = null;
        private IUserDefined srvUserDefined = null;
        public DocInstitutionService(IUnitwork work,IUserDefined defined)
        {
            _work = work;
            Unitwork = work;
            _rpsin = work.Repository<Doc_Institution>();
            srvUserDefined = defined;
        }

        /// <summary>
        /// 新建安全制度模型
        /// </summary>
        /// <param name="institutionNew"></param>
        /// <returns></returns>
        public ActionResult<bool> AddDocInstitution(DocInstitutionNew institutionNew)
        {
            try
            {
                if (institutionNew == null)
                {
                    throw new Exception("参数有误");
                }
                var check = _rpsin.Any(p=>p.Name==institutionNew.Name&&p.TypeID==institutionNew.TypeID);
                if (check)
                {
                    throw new Exception("该类型下已存在该安全制度模型");
                }
                var dbin = institutionNew.MAPTO<Doc_Institution>();
                //自定义项
                var definedvalue = new UserDefinedBusinessValue
                {
                    BusinessID = dbin.ID,
                    Values = institutionNew.UserDefineds
                };
                var defined = srvUserDefined.SaveBuisnessValue(definedvalue);
                if (defined.state != 200)
                {
                    throw new Exception(defined.msg);
                }
                _rpsin.Add(dbin);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {

                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 删除安全制度模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelDocInstitution(Guid id)
        {
            try
            {
                var dbin = _rpsin.GetModel(id);
                if (dbin == null)
                {
                    throw new Exception("未找到该安全制度模型");
                   
                }
                srvUserDefined.DeleteBusinessValue(id);
                _rpsin.Delete(dbin);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 修改安全制度模型
        /// </summary>
        /// <param name="institutionEdit"></param>
        /// <returns></returns>
        public ActionResult<bool> EditDocInstitution(DocInstitutionEdit institutionEdit)
        {
            try
            {
                var dbin = _rpsin.GetModel(p=>p.ID==institutionEdit.ID);
                if (dbin==null)
                {
                    throw new Exception("未找到所需修改的安全制度模型");
                }
                var check = _rpsin.Any(p => p.Name == institutionEdit.Name && p.TypeID == institutionEdit.TypeID&&p.ID!=institutionEdit.ID);
                if (check)
                {
                    throw new Exception("该类型下已存在该安全制度模型");
                }
                dbin = institutionEdit.CopyTo<Doc_Institution>(dbin);
                //自定义项
                srvUserDefined.DeleteBusinessValue(dbin.ID);
                var definedvalue = new UserDefinedBusinessValue
                {
                    BusinessID = dbin.ID,
                    Values = institutionEdit.UserDefineds
                };
                var defined = srvUserDefined.SaveBuisnessValue(definedvalue);
                if (defined.state != 200)
                {
                    throw new Exception(defined.msg);
                }
                _rpsin.Update(dbin);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        public ActionResult<PhoneDocInstitutionModelView> GetDocInstitution(Guid id)
        {
            try
            {
                var dbins = _rpsin.GetModel(id);

                var dicts = _work.Repository<Core.Model.DB.Basic_Dict>().GetModel(dbins.TypeID);
                var re = new PhoneDocInstitutionModelView
                         {
                             BigCode = dbins.BigCode,
                             InstitutionID = dbins.ID,
                             IssueDate ="发布日期:"+dbins.IssueDate.ToString("yyyy-MM-dd"),
                             InstitutionType =dicts.DictName,
                             Name = dbins.Name,
                             Content=dbins.Content
                             
                         };

                return new ActionResult<PhoneDocInstitutionModelView>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<PhoneDocInstitutionModelView>(ex);
            }
        }

        /// <summary>
        /// 获取安全制度模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<DocInstitutionView> GetDocInstitutionModel(Guid id)
        {
            try
            {
                var dbin = _rpsin.GetModel(id);
                var inv = dbin.MAPTO<DocInstitutionView>();
                return new ActionResult<DocInstitutionView>(inv);
            }
            catch (Exception ex)
            {
                return new ActionResult<DocInstitutionView>(ex);
            }
        }
        /// <summary>
        /// 分页获取安全制度模型
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<DocInstitutionView>> GetDocInstitutions(PagerQuery<DocInstitutionQuery> para)
        {
            try
            {
                var dbins = _rpsin.GetList(p=>p.TypeID==para.Query.TypeID&&(p.Name.Contains(para.Query.Name)||string.IsNullOrEmpty(para.Query.Name))).OrderBy(o=>o.CreateDate);
                var revs = dbins.MAPTO<DocInstitutionView>();
                var re = new Pager<DocInstitutionView>().GetCurrentPage(revs, para.PageSize, para.PageIndex);
                return new ActionResult<Pager<DocInstitutionView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<DocInstitutionView>>(ex);
            }
        }
        /// <summary>
        /// App端获取所有安全制度
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<PhoneDocInstitutionView>> GetDocInstitutionsList()
        {
            try
            {
                var dbins = _rpsin.Queryable();
                var typeids = dbins.Select(s => s.TypeID).Distinct();
                var dicts = _work.Repository<Core.Model.DB.Basic_Dict>().Queryable(p => typeids.Contains(p.ID));
                var re = from ins in dbins.ToList()
                         let type=dicts.FirstOrDefault(p=>p.ID==ins.TypeID)
                         select new PhoneDocInstitutionView
                         {
                             BigCode="字号:"+ins.BigCode,
                             InstitutionID=ins.ID,
                             IssueDate="发布日期:"+ins.IssueDate.ToString("yyyy-MM-dd"),
                             InstitutionType="类型:"+type.DictName,
                             Name="名称:"+ins.Name
                         };
              
                return new ActionResult<IEnumerable<PhoneDocInstitutionView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<PhoneDocInstitutionView>>(ex);
            }
        }
    }
}
