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


    public class DangerService : ServiceBase, IDangerService
    {
        private IUnitwork _work = null;
        private IRepository<Core.Model.DB.Account.Basic_Danger> _rpsdanger = null;
        private IRepository<Basic_DangerSort> _rpsdangersort = null;

        public DangerService(IUnitwork work)
        {
            _work = work;
            Unitwork = work;
            _rpsdanger = work.Repository<Basic_Danger>();
            _rpsdangersort = work.Repository<Basic_DangerSort>();
        }


        /// <summary>
        /// 新建风险信息
        /// </summary>
        /// <param name="danger"></param>
        /// <returns></returns>
        public ActionResult<bool> AddDanger(DangerNew danger)
        {
            var check = _rpsdanger.Any(p=>p.DangerSortID==danger.DangerSortID&&p.Name==danger.Name&&p.Code==danger.Code);
            if (check)
            {
                throw new Exception("改风险信息已存在！");
            }
            var _danger = danger.MAPTO<Basic_Danger>();
            _rpsdanger.Add(_danger);
            _work.Commit();
            return new ActionResult<bool>(true);
        }

        /// <summary>
        /// 新建风险类别
        /// </summary>
        /// <param name="dangersort"></param>
        /// <returns></returns>
        public ActionResult<bool> AddDangerSort(DangerSortNew dangersort)
        {
            var check = _rpsdangersort.Any(p => p.ParetID == dangersort.ParetID && p.SortName == dangersort.SortName);
            if (check)
            {
                throw new Exception("当前节点下已存在该类别名称:"+dangersort.SortName);
            }

            var _dangersort = dangersort.MAPTO<Basic_DangerSort>();
            _rpsdangersort.Add(_dangersort);
            _work.Commit();
            return new ActionResult<bool>(true);
        }
        /// <summary>
        /// 删除风险信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelDanger(Guid id)
        {
            var dbdanger = _rpsdanger.GetModel(id);
            if (dbdanger == null)
            {
                throw new Exception("未找到该风险信息");
            }
            _rpsdanger.Delete(dbdanger);
            _work.Commit();
            return new ActionResult<bool>(true);
             
        }
        /// <summary>
        ///删除风险类别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelDangerSort(Guid id)
        {
            var dbdangersort = _rpsdangersort.GetModel(id);
            if (dbdangersort == null)
            {
                throw new Exception("未找到该风险类别");
            }
            var check = _rpsdangersort.Any(p=>p.ParetID==id);
            if (check)
            {
                throw new Exception("该类别下存在子类别，无法删除");
            }
            _rpsdangersort.Delete(dbdangersort);
            _work.Commit();
            return new ActionResult<bool>(true);
        }

        /// <summary>
        /// 修改风险信息
        /// </summary>
        /// <param name="danger"></param>
        /// <returns></returns>
        public ActionResult<bool> EditDanger(DangerEdit danger)
        {
            var dbdanger = _rpsdanger.GetModel(danger.Id);
            if (dbdanger == null)
            {
                throw new Exception("未找到所需修改项");
            }
            var check = _rpsdanger.Any(p => p.ID != danger.Id && p.DangerSortID == danger.DangerSortID && p.Name == danger.Name && p.Code == danger.Code);
            if (check)
            {
                throw new Exception("当前类别下已存在该风险信息");
            }
            var _dbdanger = danger.CopyTo<Basic_Danger>(dbdanger);
            _rpsdanger.Update(_dbdanger);
            return new ActionResult<bool>(true);
        }
        /// <summary>
        /// 更具指定Id获取风险项
        /// </summary>
        /// <param name="dangerid"></param>
        /// <returns></returns>
        public ActionResult<DangerView> GetDanger(Guid dangerid)
        {
            var dbdanger = _rpsdanger.GetModel(dangerid);
            if (dbdanger == null)
            {
                throw new Exception("未找到该风险信息");
            }
            var re = dbdanger.MAPTO<DangerView>();
            return new ActionResult<DangerView>(re);
        }
        /// <summary>
        /// 根据风险类别获取风险信息
        /// </summary>
        /// <param name="dangersortid"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<DangerView>> GetDangers(Guid dangersortid)
        {
            var dbdangers = _rpsdanger.Queryable();
            var dangersortids = from dangersort in _rpsdangersort.GetList(p => p.ParetID == dangersortid)
                                select dangersort.ID;
            var dangers = from danger in dbdangers.ToList()
                          where dangersortids.Contains(danger.DangerSortID) || danger.ID == dangersortid
                          select new DangerView
                          {
                              Code = danger.Code,
                              DangerSortID = danger.DangerSortID,
                              Name = danger.Name,
                              ID = danger.ID
                          };
            return new ActionResult<IEnumerable<DangerView>>(dangers);
        }

        /// <summary>
        /// 获取风险类别树节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<DangerSortView>> GetDangerSorts(Guid id)
        {
            var dbdangersorts = _rpsdangersort.Queryable(p=>p.ParetID==id);
            var re = from s in dbdangersorts.ToList()
                     select new DangerSortView
                     {
                         ID=s.ID,
                         ParetID=s.ParetID,
                         Level=s.Level,
                         SortName=s.SortName
                     };
            return new ActionResult<IEnumerable<DangerSortView>>(re);
        }
    }
}
