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
    /// 健康档案
    /// </summary>
    public class HealDocmentService : ServiceBase,IHealDocmentService
    {
        private IUnitwork _work = null;
        private IRepository<Heal_Docment> _rpshd = null;
        private IAttachFile srvFile = null;
        public HealDocmentService(IUnitwork work, IAttachFile file)
        {
            _work = work;
            Unitwork = work;
            _rpshd = work.Repository<Heal_Docment>();
            srvFile = file;
        }
        /// <summary>
        /// 新建健康文档
        /// </summary>
        /// <param name="docmentNew"></param>
        /// <returns></returns>
        public ActionResult<bool> AddHealDocment(HealDocmentNew docmentNew)
        {
            try
            {
                var check = _rpshd.Any(p => p.EmployeeID == docmentNew.EmployeeID);
                if (check)
                {
                    throw new Exception("该人员的健康档案已存在！");
                }
                var dbhd = docmentNew.MAPTO<Heal_Docment>();

                //电子文档
                var files = new AttachFileSave
                {
                    BusinessID = dbhd.ID,
                    files = from f in docmentNew.AttachFiles
                            select f.CopyTo<AttachFileNew>(f)
                };

                srvFile.SaveFiles(files);

                _rpshd.Add(dbhd);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 删除人员的健康档案
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelHealDocment(Guid id)
        {
            try
            {
                var dbhd = _rpshd.GetModel(id);
                if (dbhd == null)
                {
                    throw new Exception("未找到所要删除的人员健康档案");
                }
                _rpshd.Delete(dbhd);
                //删除电子文档
                srvFile.DelFileByBusinessId(id);

                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 修改人员健康档案
        /// </summary>
        /// <param name="docmentEdit"></param>
        /// <returns></returns>
        public ActionResult<bool> EditHealDocment(HealDocmentEdit docmentEdit)
        {
            try
            {
                var dbhd = _rpshd.GetModel(docmentEdit.ID);
                if (dbhd == null)
                {
                    throw new Exception("未找到所要修改的健康档案！");
                }
                var check = _rpshd.Any(p => p.ID != docmentEdit.ID && p.EmployeeID == docmentEdit.EmployeeID);
                if (check)
                {
                    throw new Exception("该人员的健康档案已存在！");
                }
                dbhd = docmentEdit.CopyTo<Heal_Docment>(dbhd);
                //电子文档 
                srvFile.DelFileByBusinessId(dbhd.ID);
                var files = new AttachFileSave
                {
                    BusinessID = dbhd.ID,
                    files = from f in docmentEdit.AttachFiles
                            select f.CopyTo<AttachFileNew>(f)
                };

                srvFile.SaveFiles(files);

                _rpshd.Update(dbhd);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        public ActionResult<HealDocmentView> GetHealDocment(Guid id)
        {
            throw new NotImplementedException();
        }

        public ActionResult<Pager<HealDocmentView>> GetHealDocments(PagerQuery<HealDocmentQuery> para)
        {
            throw new NotImplementedException();
        }
    }
}
