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
    public class DocTrainManageService : ServiceBase, IDocTrainManageService
    {
        private IUnitwork _work = null;
        private IRepository<Doc_Training> _rpsdt = null;
        private IRepository<Doc_TrainEmpoyees> _rpsdtemp = null;
        private IRepository<Core.Model.DB.Basic_Employee> _rpsemp = null;
        private IRepository<Core.Model.DB.Basic_Org> _rpsorg = null;
        private IAttachFile srvFile = null;
        public DocTrainManageService(IUnitwork work, IAttachFile file)
        {
            _work = work;
            Unitwork = work;
            _rpsdt = work.Repository<Doc_Training>();
            _rpsdtemp = work.Repository<Doc_TrainEmpoyees>();
            _rpsemp = work.Repository<Core.Model.DB.Basic_Employee>();
            _rpsorg = work.Repository<Core.Model.DB.Basic_Org>();
            srvFile = file;
        }
        ///// <summary>
        ///// 新建训练人员模型
        ///// </summary>
        ///// <param name="empoyeesNew"></param>
        ///// <returns></returns>
        //public ActionResult<bool> AddTrainEmployee(DocTrainEmpoyeesNew empoyeesNew)
        //{
        //    try
        //    {
        //        var check = _rpsdtemp.Any(p=>p.TrainID==empoyeesNew.TrainID);
        //        if (!check)
        //        {
        //            throw new Exception("未找到该培训项");
        //        }
        //        check = _rpsdtemp.Any(p => p.TrainID == empoyeesNew.TrainID && p.EmployeeID == empoyeesNew.EmployeeID);
        //        if (check)
        //        {
        //            throw new Exception("该培训项下已存在该人员");
        //        }
        //        var dbdtemp = empoyeesNew.MAPTO<Doc_TrainEmpoyees>();
        //        _rpsdtemp.Add(dbdtemp);
        //        _work.Commit();
        //        return new ActionResult<bool>(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ActionResult<bool>(ex);
        //    }
        //}
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
                //电子文档
                var files = new AttachFileSave
                {
                    BusinessID = dbdt.ID,
                    files = from f in trainingNew.AttachFiles
                            select new AttachFileNew
                            {
                                FileTitle = f.FileTitle,
                                FileType = f.FileType,
                                FileUrl = f.FileUrl
                            }
                };
                
                var fileresult= srvFile.SaveFiles(files);
                if (fileresult.state != 200)
                {
                    throw new Exception(fileresult.msg);
                }
                //添加培训人员
                List<DocTrainEmpoyeesNew> emps = (from empid in trainingNew.EmployeeIDs
                                                  select new DocTrainEmpoyeesNew
                                                  {
                                                     EmployeeID=empid,
                                                     TrainID=dbdt.ID
                                                  }).ToList();
               var dbdtemps = emps.MAPTO<Doc_TrainEmpoyees>();
                _rpsdtemp.Add(dbdtemps);
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
                //删除电子文档
                srvFile.DelFileByBusinessId(id);
                //删除培训人员
                _rpsdtemp.Delete(p=>p.TrainID==id);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {

                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 修改训练模型
        /// </summary>
        /// <param name="trainingEdit"></param>
        /// <returns></returns>
        public ActionResult<bool> EditTraining(DocTrainingEdit trainingEdit)
        {
            try
            {
                var dbdt = _rpsdt.GetModel(trainingEdit.ID);
                if (dbdt == null)
                {
                    throw new Exception("未找到所需修改的训练项模型");
                }
                var check = _rpsdt.Any(p => p.ID != trainingEdit.ID && p.Motif == trainingEdit.Motif);
                if (check)
                {
                    throw new Exception("该训练项已存在");
                }
                dbdt = trainingEdit.CopyTo<Doc_Training>(dbdt);

                //电子文档 
                srvFile.DelFileByBusinessId(dbdt.ID);
                var files = new AttachFileSave
                {
                    BusinessID = dbdt.ID,
                    files = from f in trainingEdit.AttachFiles
                            select new AttachFileNew
                            {
                                FileTitle = f.FileTitle,
                                FileType = f.FileType,
                                FileUrl = f.FileUrl
                            }
                };

                var fileresult = srvFile.SaveFiles(files);
                if (fileresult.state != 200)
                {
                    throw new Exception(fileresult.msg);
                }

                //培训人员先删除后添加
                _rpsdtemp.Delete(p=>p.TrainID==trainingEdit.ID);
                //添加培训人员
                List<DocTrainEmpoyeesNew> emps = (from empid in trainingEdit.EmployeeIDs
                                                  select new DocTrainEmpoyeesNew
                                                  {
                                                      EmployeeID = empid,
                                                      TrainID = dbdt.ID
                                                  }).ToList();
                var dbdtemps = emps.MAPTO<Doc_TrainEmpoyees>();
                _rpsdtemp.Add(dbdtemps);

                _rpsdt.Update(dbdt);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
           

        }

        /// <summary>
        /// 分页获取当前训练项下的人员
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<DocTrainEmpoyeesView>> GetTrainEmployee(PagerQuery<DocTrainEmpoyeesQuery> para)
        {
            try
            {
                var dbdtemps = _rpsdtemp.Queryable(p=>p.TrainID==para.Query.TrainID);
                var reemps = from s in dbdtemps.ToList()
                         let emp= _rpsemp.GetModel(s.EmployeeID)
                         select new DocTrainEmpoyeesView
                         {
                             ID=s.ID,
                             TrainID=s.TrainID,
                             EmployeeID=s.EmployeeID,
                             Name=emp.CNName,
                             Department=_rpsorg.GetModel(emp.OrgID).OrgName
                         };
                var re = new Pager<DocTrainEmpoyeesView>().GetCurrentPage(reemps,para.PageSize,para.PageIndex);
                return new ActionResult<Pager<DocTrainEmpoyeesView>>(re);

            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<DocTrainEmpoyeesView>>(ex);
            }
        }
        /// <summary>
        /// 获取训练项模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<DocTrainingView> GetTraining(Guid id)
        {
            try
            {
                var dbdt = _rpsdt.GetModel(id);
                var re = dbdt.MAPTO<DocTrainingView>();
                return new ActionResult<DocTrainingView>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<DocTrainingView>(ex);
            }
        }
        /// <summary>
        /// 分页获取训练项模型
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<DocTrainingView>> GetTrainings(PagerQuery<DocTrainingQuery> para)
        {
            try
            {
                var dbdts = _rpsdt.Queryable(p => p.Motif.Contains(para.Query.Motif)||string.IsNullOrEmpty(para.Query.Motif));
                var redts = from s in dbdts
                            select new DocTrainingView
                            {
                                ID = s.ID,
                                Content = s.Content,
                                TrainDate=s.TrainDate,
                                Trainer=s.Trainer,
                                TrainLong=s.TrainLong,
                                Motif = s.Motif
                            };
                var re = new Pager<DocTrainingView>().GetCurrentPage(redts, para.PageSize, para.PageIndex);
                return new ActionResult<Pager<DocTrainingView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<DocTrainingView>>(ex);
            }
        }
    }
}
