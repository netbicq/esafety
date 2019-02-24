﻿using ESafety.Account.IService;
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
    public class DangerSetService : ServiceBase, IDangerSetService
    {
        private IUnitwork _work = null;
        private IRepository<Basic_DangerRelation> _rpsdangerrelation = null;
        private IRepository<Basic_Danger> _rpsdanger = null;
        private IRepository<Basic_DangerSort> _rpsdangersort = null;
        public DangerSetService(IUnitwork work)
        {
            _work = work;
            Unitwork = work;
            _rpsdangerrelation = work.Repository<Basic_DangerRelation>();
            _rpsdanger = work.Repository<Basic_Danger>();
            _rpsdangersort = work.Repository<Basic_DangerSort>();
        }
        /// <summary>
        /// 新建风险点配置
        /// </summary>
        /// <param name="dangerRelation"></param>
        /// <returns></returns>
        public ActionResult<bool> AddDangerRelation(DangerRelationNew dangerRelation)
        {
            try
            {
                var check = _rpsdangerrelation.Any(p=>p.SubjectID==dangerRelation.SubjectID&&p.DangerID==dangerRelation.DangerID);
                if (check)
                {
                    throw new Exception("已存在该风险点配置");
                }
                var dbdangerrelation=dangerRelation.MAPTO<Basic_DangerRelation>();
                _rpsdangerrelation.Add(dbdangerrelation);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 删除风险配置
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult<bool> DelDangerRelation(Guid id)
        {
            try
            {
                var check = _rpsdangerrelation.Any(p => p.ID==id);
                if (check)
                {
                    throw new Exception("未找到该风险点配置");
                }
                _rpsdangerrelation.Delete(p=>p.ID==id);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 获取风险点关系页
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<DangerRelationView>> GetDangerRelations(PagerQuery<DangerRelationQuery> para)
        {
            try
            {
                var dbdangerrelation = _rpsdangerrelation.Queryable();
                var dbdanger = _rpsdanger.Queryable();
                var redr = from s in dbdanger
                           where (from c in dbdangerrelation where c.SubjectID == para.Query.SubjectID select c.DangerID).Contains(s.ID)
                           select new DangerRelationView
                           {
                               Code = s.Code,
                               ID = _rpsdangerrelation.GetModel(p => p.SubjectID == para.Query.SubjectID).ID,
                               DangerID = s.ID,
                               DangerSortID = s.DangerSortID,
                               SubjectID = para.Query.SubjectID,
                               DangerSortName = _rpsdangersort.GetModel(p => p.ID == s.DangerSortID).SortName,
                               Name = s.Name
                           };
                var re = new Pager<DangerRelationView>().GetCurrentPage(redr, para.PageSize, para.PageIndex);
                return new ActionResult<Pager<DangerRelationView>>(re);
            }
            catch (Exception ex)
            {

                return new ActionResult<Pager<DangerRelationView>>(ex);
            }
        }
    }
}