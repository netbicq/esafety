using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
using ESafety.Core.Model.DB.Account;
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
    /// 紧急预案
    /// </summary>
    public class DocSolutionService : ServiceBase,IDocSolutionService
    {
        private IUnitwork _work = null;
        private IRepository<Doc_Solution> _rpsds = null;
        private IRepository<Core.Model.DB.Basic_Dict> _rpsdict = null;
        public DocSolutionService(IUnitwork work)
        {
            _work = work;
            Unitwork = work;
            _rpsds = work.Repository<Doc_Solution>();
            _rpsdict = work.Repository<Core.Model.DB.Basic_Dict>();
        }

        /// <summary>
        /// 新建紧急预案模型
        /// </summary>
        /// <param name="solutionNew"></param>
        /// <returns></returns>
        public ActionResult<bool> AddDocSolution(DocSolutionNew solutionNew)
        {
            try
            {
                if (solutionNew == null)
                {
                    throw new Exception("参数有误");
                }
                var check = _rpsds.Any(p=>p.Name==solutionNew.Name&&p.TypeID==solutionNew.TypeID);
                if (check)
                {
                    throw new Exception("该类型下已存在该紧急预案");
                }
                var dbds = solutionNew.MAPTO<Doc_Solution>();
                _rpsds.Add(dbds);
                _work.Commit();
                return new ActionResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 删除紧急预案模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelDocSolution(Guid id)
        {
            try
            {
                var dbds = _rpsds.GetModel(id);
                if (dbds == null)
                {
                    throw new Exception("未找到需要删除的紧急预案模型");
                }
                _rpsds.Delete(dbds);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 修改紧急预案模型
        /// </summary>
        /// <param name="solutionEdit"></param>
        /// <returns></returns>
        public ActionResult<bool> EditDocSolution(DocSolutionEdit solutionEdit)
        {
            try
            {
                var dbds = _rpsds.GetModel(solutionEdit.ID);
                if (dbds == null)
                {
                    throw new Exception("未找到所需修改的紧急预案模型");
                }
                var check = _rpsds.Any(p =>p.ID!=solutionEdit.ID &&p.Name == solutionEdit.Name && p.TypeID == solutionEdit.TypeID);
                if (check)
                {
                    throw new Exception("该类型下已存在该紧急预案");
                }
                dbds = solutionEdit.CopyTo<Doc_Solution>(dbds);
                _rpsds.Update(dbds);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 根据ID，获取紧急预案模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<DocSolutionView> GetDocSolution(Guid id)
        {
            try
            {
                var dbds = _rpsds.GetModel(id);
                var dsv = dbds.MAPTO<DocSolutionView>();
                dsv.DangerLevelName = _rpsdict.GetModel(dbds.DangerLevel).DictName;
                return new ActionResult<DocSolutionView>(dsv);
            }
            catch (Exception ex)
            {
                return new ActionResult<DocSolutionView>(ex);
            }
        }

        public ActionResult<IEnumerable<PhoneDocSolutionView>> GetDocSolutionList(Guid DangerLevelID)
        {
            try
            {
                var dbdss = _rpsds.Queryable(p => p.DangerLevel == DangerLevelID || Guid.Empty == DangerLevelID);
                var typeids = dbdss.Select(s => s.TypeID).Distinct();
                var lvs = dbdss.Select(s => s.DangerLevel).Distinct();
                var dicts = _rpsdict.Queryable(p => typeids.Contains(p.ID) || lvs.Contains(p.ID));

                var revs = from s in dbdss
                           let type=dicts.FirstOrDefault(p=>p.ID==s.TypeID)
                           let lv=dicts.FirstOrDefault(p=>p.ID==s.DangerLevel)
                           select new PhoneDocSolutionView
                           {
                               DangerLevel=lv.DictName,
                               DocSolutionID=s.ID,
                               IssueDate=s.IssueDate,
                               Name=s.Name,
                               SolutionType = type.DictName,
                           };
            
                return new ActionResult<IEnumerable<PhoneDocSolutionView>>(revs);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<PhoneDocSolutionView>>(ex);
            }
        }
        /// <summary>
        /// 根据ID获取预案模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<PhoneDocSolutionModelView> GetDocSolutionModel(Guid id)
        {
            try
            {
                var dbdss = _rpsds.GetModel(id);
  
                var dicts = _rpsdict.Queryable(p =>p.ID==dbdss.TypeID ||p.ID==dbdss.DangerLevel);

                var revs = new PhoneDocSolutionModelView
                           {
                               DangerLevel =dicts.FirstOrDefault(p=>p.ID==dbdss.DangerLevel).DictName,
                               DocSolutionID = dbdss.ID,
                               IssueDate = dbdss.IssueDate,
                               Name = dbdss.Name,
                               SolutionType = dicts.FirstOrDefault(p => p.ID == dbdss.TypeID).DictName,
                               Content=dbdss.Content
                           };

                return new ActionResult<PhoneDocSolutionModelView>(revs);
            }
            catch (Exception ex)
            {
                return new ActionResult<PhoneDocSolutionModelView>(ex);
            }
        }




        /// <summary>
        /// 分页获取紧急预案
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<DocSolutionView>> GetDocSolutions(PagerQuery<DocSolutionQuery> para)
        {
            try
            {
                var dbdss = _rpsds.Queryable(p => p.TypeID == para.Query.TypeID && (p.Name.Contains(para.Query.Name) || string.IsNullOrEmpty(para.Query.Name))&&(p.DangerLevel==para.Query.DangerLevel||Guid.Empty==para.Query.DangerLevel));
                var dicts = _rpsdict.Queryable();
                var revs = from s in dbdss
                           let dict=dicts.FirstOrDefault(p=>p.ID==s.DangerLevel)
                           orderby s.IssueDate descending
                           select new DocSolutionView
                           {
                               Content = s.Content,
                               ID = s.ID,
                               DangerLevel = s.DangerLevel,
                               IssueDate = s.IssueDate,
                               Name = s.Name,
                               TypeID = s.TypeID,
                               DangerLevelName = dict.DictName
                           };
                var re = new Pager<DocSolutionView>().GetCurrentPage(revs, para.PageSize, para.PageIndex);
                return new ActionResult<Pager<DocSolutionView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<DocSolutionView>>(ex);
            }
        }


    }
}
