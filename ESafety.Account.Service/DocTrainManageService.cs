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
    public class DocTrainManageService : ServiceBase, IDocTrainManageService
    {
        private IUnitwork _work = null;
        private IRepository<Doc_Training> _rpsdt = null;
        private IRepository<Doc_TrainEmpoyees> _rpsdtemp = null;
        private IRepository<Core.Model.DB.Basic_Employee> _rpsemp = null;
        private IRepository<Core.Model.DB.Basic_Org> _rpsorg = null;
        public DocTrainManageService(IUnitwork work)
        {
            _work = work;
            Unitwork = work;
            _rpsdt = work.Repository<Doc_Training>();
            _rpsdtemp = work.Repository<Doc_TrainEmpoyees>();
            _rpsemp = work.Repository<Core.Model.DB.Basic_Employee>();
            _rpsorg = work.Repository<Core.Model.DB.Basic_Org>();
        }
        /// <summary>
        /// 新建训练人员模型
        /// </summary>
        /// <param name="empoyeesNew"></param>
        /// <returns></returns>
        public ActionResult<bool> AddTrainEmployee(DocTrainEmpoyeesNew empoyeesNew)
        {
            try
            {
                var check = _rpsdtemp.Any(p=>p.TrainID==empoyeesNew.TrainID);
                if (!check)
                {
                    throw new Exception("未找到该培训项");
                }
                check = _rpsdtemp.Any(p => p.TrainID == empoyeesNew.TrainID && p.EmployeeID == empoyeesNew.EmployeeID);
                if (check)
                {
                    throw new Exception("该培训项下已存在该人员");
                }
                var dbdtemp = empoyeesNew.MAPTO<Doc_TrainEmpoyees>();
                _rpsdtemp.Add(dbdtemp);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 新建训练模型
        /// </summary>
        /// <param name="trainingNew"></param>
        /// <returns></returns>
        public ActionResult<bool> AddTraining(DocTrainingNew trainingNew)
        {
            try
            {
                var check = _rpsdt.Any(p => p.Motif == trainingNew.Motif);
                if (check)
                {
                    throw new Exception("该训练项已存在");
                }
                var dbdt = trainingNew.MAPTO<Doc_Training>();
                _rpsdt.Add(dbdt);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {

                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 删除训练项人员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelTrainEmplyee(Guid id)
        {
            try
            {
                var dbdtemp = _rpsdtemp.GetModel(id);
                if (dbdtemp==null)
                {
                    throw new Exception("未找到该训练项人员");
                }
                _rpsdtemp.Delete(dbdtemp);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {

                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 删除训练项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelTraining(Guid id)
        {
            try
            {
                var dbdt = _rpsdt.GetModel(id);
                if (dbdt== null)
                {
                    throw new Exception("未找到该训练项");
                }
                _rpsdt.Delete(dbdt);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {

                return new ActionResult<bool>(ex);
            }
        }

        public ActionResult<bool> EditTraining(DocTrainingEdit trainingEdit)
        {
            throw new NotImplementedException();
        }

        public ActionResult<Pager<PostEmployeesView>> GetTrainEmployee(PagerQuery<DocTrainEmpoyeesQuery> para)
        {
            throw new NotImplementedException();
        }

        public ActionResult<DocTrainingView> GetTraining(Guid id)
        {
            throw new NotImplementedException();
        }

        public ActionResult<Pager<DocTrainingView>> GetTrainings(PagerQuery<DocTrainingQuery> para)
        {
            throw new NotImplementedException();
        }
    }
}
