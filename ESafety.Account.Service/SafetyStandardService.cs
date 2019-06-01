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
    class SafetyStandardService : ServiceBase, ISafetyStandardServcie
    {
        private IUnitwork _work = null;
        private IRepository<Basic_SafetyStandard> _rpssafetystandard = null;
        private IRepository<Basic_DangerSort> _rpsdangersort = null;
        private IRepository<Basic_DangerSafetyStandards> _rpsdssd = null;
        public SafetyStandardService(IUnitwork work)
        {
            _work = work;
            Unitwork = work;
            _rpssafetystandard = work.Repository<Basic_SafetyStandard>();
            _rpsdangersort= work.Repository<Basic_DangerSort>();
            _rpsdssd = work.Repository<Basic_DangerSafetyStandards>();
        }
        /// <summary>
        /// 添加安全标准
        /// </summary>
        /// <param name="safetystandard"></param>
        /// <returns></returns>
        public ActionResult<bool> AddSafetyStandard(SafetyStandardNew safetystandard)
        {
            try
            {
                if (safetystandard == null)
                {
                    throw new Exception("参数有误");
                }
                if (safetystandard.DangerSortID==Guid.Empty)
                {
                    throw new Exception("请选择则风控项类别！");
                }
                var check = _rpssafetystandard.Any(p => p.DangerSortID == safetystandard.DangerSortID && p.Code == safetystandard.Code);
                if (check)
                {
                    throw new Exception("该风险点下已存在该编号的风险安全标准");
                }
                var dbsafetystandard = safetystandard.MAPTO<Basic_SafetyStandard>();
                _rpssafetystandard.Add(dbsafetystandard);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }

        }
        /// <summary>
        /// 删除安全标准
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelSafetyStandard(Guid id)
        {
            try
            {
                var dbsafetystandard = _rpssafetystandard.GetModel(id);
                if (dbsafetystandard == null)
                {
                    throw new Exception("未找到该安全标准");
                }
                var check = _rpsdssd.Any(p=>p.SafetyStandardID==id);
                if (check)
                {
                    throw new Exception("该安全点已经配置在风险点下，无法删除！");
                }
                _rpssafetystandard.Delete(dbsafetystandard);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {

                return new ActionResult<bool>(ex);
            }
       
        }
        /// <summary>
        /// 修改安全标准
        /// </summary>
        /// <param name="safetystandard"></param>
        /// <returns></returns>
        public ActionResult<bool> EditSafetyStandard(SafetyStandardEdit safetystandard)
        {
            try
            {
                var dbsafetystandard = _rpssafetystandard.GetModel(safetystandard.ID);
                if (dbsafetystandard == null)
                {
                    throw new Exception("未找到所需修改安全标准");
                }
                var check =_rpsdssd.Any(p=>p.SafetyStandardID==safetystandard.ID);
                if (check)
                {
                    throw new Exception("该安全标准已配置在风险点下,无法修改!");
                }
                var _dbsafetystandard = safetystandard.CopyTo<Basic_SafetyStandard>(dbsafetystandard);
                _rpssafetystandard.Update(dbsafetystandard);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
   
        }

        /// <summary>
        /// 根据ID获取安全标准
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<SafetyStandardModel> GetSafetyStandard(Guid id)
        {
            try
            {
                var safetystandard = _rpssafetystandard.GetModel(id);
                if (safetystandard == null)
                {
                    throw new Exception("未找到该安全标准");
                }
                var re = safetystandard.MAPTO<SafetyStandardModel>();
                return new ActionResult<SafetyStandardModel>(re);
            }
            catch (Exception ex)
            { 
                return new ActionResult<SafetyStandardModel>(ex);
            }
           
        }

        /// <summary>
        /// 获取安全标准
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<SafetyStandardView>> GetSafetyStandards()
        {
            try
            {
                var SafetyStandards = _rpssafetystandard.Queryable();
                var re = from s in SafetyStandards.ToList()
                         select new SafetyStandardView
                         {
                             ID = s.ID,
                             Code = s.Code,
                             Controls = s.Controls,
                             DangerSort = _rpsdangersort.GetModel(s.DangerSortID).SortName,
                             DangerSortID = s.DangerSortID,
                             Name = s.Name
                         };

                return new ActionResult<IEnumerable<SafetyStandardView>>(re);
            }
            catch (Exception ex)
            {

                return new ActionResult<IEnumerable<SafetyStandardView>>(ex);
            }
            
        }


        
        /// <summary>
        /// 根据风险类别ID获取所有安全准则
        /// </summary>
        /// <param name="dangerid"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<SafetyStandardView>> GetSafetyStandards(Guid dangerid)
        {
            try
            {
                var SafetyStandardIds = _rpsdssd.Queryable(p => p.DangerID == dangerid).Select(s=>s.SafetyStandardID);
                var ss = _rpssafetystandard.Queryable(p => SafetyStandardIds.Contains(p.ID));
                var danger = _work.Repository<Basic_Danger>().GetModel(dangerid);
                var ds = _rpsdangersort.GetModel(p => p.ID == danger.DangerSortID);
          
                var re = from o in ss
                         select new SafetyStandardView
                         {
                             ID = o.ID,
                             Code = o.Code,
                             Controls = o.Controls,
                             DangerSort = ds.SortName,
                             DangerSortID = o.DangerSortID,
                             Name = o.Name,
                             DangerName=danger.Name

                         };
                return new ActionResult<IEnumerable<SafetyStandardView>>(re);
            }
            catch (Exception ex)
            {

                return new ActionResult<IEnumerable<SafetyStandardView>>(ex);
            }
           
        }
        /// <summary>
        /// 根据风控项类别获取执行标准
        /// </summary>
        /// <param name="dangersortid"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<SafetyStandardView>> GetSafetyStandardsByDangerSort(Guid dangersortid)
        {
            try
            {
                var SafetyStandards = _rpssafetystandard.Queryable(p=>p.DangerSortID==dangersortid);
                var sort = _rpsdangersort.GetModel(dangersortid);
                var re = from s in SafetyStandards
                         select new SafetyStandardView
                         {
                             ID = s.ID,
                             Code = s.Code,
                             Controls = s.Controls,
                             DangerSort =sort.SortName,
                             DangerSortID = sort.ID,
                             Name = s.Name
                         };

                return new ActionResult<IEnumerable<SafetyStandardView>>(re);
            }
            catch (Exception ex)
            {

                return new ActionResult<IEnumerable<SafetyStandardView>>(ex);
            }

        }
    }
}
