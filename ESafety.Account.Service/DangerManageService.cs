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


    public class DangerManageService : ServiceBase, IDangerManageService
    {
        private IUnitwork _work = null;
        private IRepository<Core.Model.DB.Account.Basic_Danger> _rpsdanger = null;
        private IRepository<Basic_DangerSort> _rpsdangersort = null;
        private IRepository<Basic_DangerSafetyStandards> _rpsdangersafetystandards = null;
        private IReadOnlyCollection<Basic_DangerRelation> _rpsdr = null;

        public DangerManageService(IUnitwork work)
        {
            _work = work;
            Unitwork = work;
            _rpsdanger = work.Repository<Basic_Danger>();
            _rpsdangersort = work.Repository<Basic_DangerSort>();
            _rpsdangersafetystandards = work.Repository<Basic_DangerSafetyStandards>();
        }


        /// <summary>
        /// 新建风险信息
        /// </summary>
        /// <param name="danger"></param>
        /// <returns></returns>
        public ActionResult<bool> AddDanger(DangerNew danger)
        {
            try
            {
                var check = _rpsdanger.Any(p => p.DangerSortID == danger.DangerSortID && p.Name == danger.Name && p.Code == danger.Code);
                if (check)
                {
                    throw new Exception("该风险信息已存在！");
                }
                var _danger = danger.MAPTO<Basic_Danger>();
                _rpsdanger.Add(_danger);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {

                return new ActionResult<bool>(ex);
            }
           
        }

        /// <summary>
        /// 为风险点添加安全标准
        /// </summary>
        /// <param name="safetyStandards"></param>
        /// <returns></returns>
        public ActionResult<bool> AddDangerSafetyStandard(DangerSafetyStandards safetyStandards)
        {
            try
            {
                var check = _rpsdanger.Any(p => p.ID == safetyStandards.DangerID);
                if (!check)
                {
                    throw new Exception("未找到该风险点");
                }
                check = _rpsdangersafetystandards.Any(p => p.DangerID == safetyStandards.DangerID && p.SafetyStandardID == safetyStandards.SafetyStandardID);
                if (check)
                {
                    throw new Exception("已为该风险点添加了该风险项");
                }
                var _dangersafetystandards = safetyStandards.MAPTO<Basic_DangerSafetyStandards>();
                _rpsdangersafetystandards.Add(_dangersafetystandards);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {

                return new ActionResult<bool>(ex);
            }
           
        }

        /// <summary>
        /// 新建风险类别
        /// </summary>
        /// <param name="dangersort"></param>
        /// <returns></returns>
        public ActionResult<bool> AddDangerSort(DangerSortNew dangersort)
        {
            try
            {
                var check = _rpsdangersort.Any(p => p.ParetID == dangersort.ParetID && p.SortName == dangersort.SortName);
                if (check)
                {
                    throw new Exception("当前节点下已存在该类别名称:" + dangersort.SortName);
                }

                var _dangersort = dangersort.MAPTO<Basic_DangerSort>();
                _rpsdangersort.Add(_dangersort);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {

                return new ActionResult<bool>(ex);
            }
           
        }

        /// <summary>
        /// 删除风险信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelDanger(Guid id)
        {
            try
            {
                var dbdanger = _rpsdanger.GetModel(id);
                if (dbdanger == null)
                {
                    throw new Exception("未找到该风险信息");
                }
                var check = _rpsdangersafetystandards.Any(p => p.DangerID == id);
                if (check)
                {
                    throw new Exception("该风险点已配置安全标准，无法删除！");
                }
                check = _rpsdr.Any(p=>p.DangerID==id);
                if (check)
                {
                    throw new Exception("已存在风险点配置，无法删除！");
                }
                _rpsdanger.Delete(dbdanger);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {

                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 删除风险点与安全准则的联系
        /// </summary>
        /// <param name="safetyStandard"></param>
        /// <returns></returns>
        public ActionResult<bool> DelDangerSafetyStandard(DangerSafetyStandards safetyStandard)
        {
            try
            {
                var dbdangerafetystandards = _rpsdangersafetystandards.GetModel(p => p.SafetyStandardID == safetyStandard.SafetyStandardID && p.DangerID == safetyStandard.DangerID);
                if (dbdangerafetystandards == null)
                {
                    throw new Exception("未找到该风险点所需删除的安全标准ID");
                }
                _rpsdangersafetystandards.Delete(dbdangerafetystandards);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {

                return new ActionResult<bool>(ex);
            }
           
        }

        /// <summary>
        ///删除风险类别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelDangerSort(Guid id)
        {
            try
            {
                var dbdangersort = _rpsdangersort.GetModel(id);
                if (dbdangersort == null)
                {
                    throw new Exception("未找到该风险类别");
                }
                var check = _rpsdangersort.Any(p => p.ParetID == id);
                if (check)
                {
                    throw new Exception("该类别下存在子类别，无法删除");
                }
                check = _rpsdanger.Any(p => p.DangerSortID == dbdangersort.ID);
                if (check)
                {
                    throw new Exception("该类别下存在风险点,无法删除");
                }
                _rpsdangersort.Delete(dbdangersort);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {

                return new ActionResult<bool>(ex);
            }
          
        }
 
        /// <summary>
        /// 修改风险信息
        /// </summary>
        /// <param name="danger"></param>
        /// <returns></returns>
        public ActionResult<bool> EditDanger(DangerEdit danger)
        {
            try
            {
                var dbdanger = _rpsdanger.GetModel(danger.ID);
                if (dbdanger == null)
                {
                    throw new Exception("未找到所需修改项");
                }
                var check = _rpsdanger.Any(p => p.ID != danger.ID && p.DangerSortID == danger.DangerSortID && p.Name == danger.Name && p.Code == danger.Code);
                if (check)
                {
                    throw new Exception("当前类别下已存在该风险信息");
                }
                var _dbdanger = danger.CopyTo<Basic_Danger>(dbdanger);
                _rpsdanger.Update(_dbdanger);
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {

                return new ActionResult<bool>(ex);
            }
           
        }

        /// <summary>
        /// 根据指定Id获取风险项
        /// </summary>
        /// <param name="dangerid"></param>
        /// <returns></returns>
        public ActionResult<DangerView> GetDanger(Guid dangerid)
        {
            try
            {
                var dbdanger = _rpsdanger.GetModel(dangerid);
                if (dbdanger == null)
                {
                    throw new Exception("未找到该风险信息");
                }
                var re = dbdanger.MAPTO<DangerView>();
                re.DangerSortName = _rpsdangersort.GetModel(dbdanger.DangerSortID).SortName;
                return new ActionResult<DangerView>(re);
            }
            catch (Exception ex)
            {

                return new ActionResult<DangerView>(ex);
            }
           
        }
        /// <summary>
        /// 根据风险类别获取风险信息
        /// </summary>
        /// <param name="dangersortid"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<DangerView>> GetDangers(Guid dangersortid)
        {
            try
            {
                var dbdangers = _rpsdanger.Queryable(p => p.DangerSortID == dangersortid);
                var dangers = from danger in dbdangers.ToList()
                              select new DangerView
                              {
                                  Code = danger.Code,
                                  DangerSortID = danger.DangerSortID,
                                  Name = danger.Name,
                                  ID = danger.ID,
                                  DangerSortName = _rpsdangersort.GetModel(p => p.ID == danger.DangerSortID || p.ID == dangersortid).SortName
                              };
                return new ActionResult<IEnumerable<DangerView>>(dangers);
            }
            catch (Exception ex)
            {

                return new ActionResult<IEnumerable<DangerView>>(ex);
            }
        }
        /// <summary>
        /// 获取风险点集合
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<DangerView>> GetDangerslist()
        {
            try
            {
                var dbdangers = _rpsdanger.Queryable();
                var dangers = from danger in dbdangers.ToList()
                              let o = _rpsdangersort.GetModel(danger.DangerSortID)
                              select new DangerView
                              {
                                  Code = danger.Code,
                                  DangerSortID = danger.DangerSortID,
                                  Name = danger.Name,
                                  ID = danger.ID,
                                  DangerSortName = o==null?"":o.SortName
                              };
                return new ActionResult<IEnumerable<DangerView>>(dangers);
            }
            catch (Exception ex)
            {

                return new ActionResult<IEnumerable<DangerView>>(ex);
            }
        }

        /// <summary>
        /// 获取风险类别树节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<DangerSortView>> GetDangerSorts(Guid id)
        {
            try
            {
                var dbdangersorts = _rpsdangersort.Queryable(p => p.ParetID == id);
                var re = from s in dbdangersorts.ToList()
                         select new DangerSortView
                         {
                             ID = s.ID,
                             ParetID = s.ParetID,
                             Level = s.Level,
                             SortName = s.SortName
                         };
                return new ActionResult<IEnumerable<DangerSortView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<DangerSortView>>(ex);
            }
           
        }

    }
}
