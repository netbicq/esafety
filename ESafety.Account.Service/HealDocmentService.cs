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
        private IRepository<Core.Model.DB.Basic_Employee> _rpsemp = null;
        private IAttachFile srvFile = null;
        public HealDocmentService(IUnitwork work, IAttachFile file)
        {
            _work = work;
            Unitwork = work;
            _rpshd = work.Repository<Heal_Docment>();
            _rpsemp = work.Repository<Core.Model.DB.Basic_Employee>();
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
                if (docmentNew == null)
                {
                    throw new Exception("参数有误");
                }
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

                _rpshd.Update(dbhd);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 获取健康档案列表
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<HealDocmentView>> GetHealDocList()
        {
            try
            {
                var dbhds = _rpshd.Queryable();
                var empsid = dbhds.Select(p => p.EmployeeID);
                var emps = _rpsemp.Queryable(p =>empsid.Contains(p.ID));
                var rehds = from s in dbhds.ToList()
                            let emp = emps.FirstOrDefault(p => p.ID == s.EmployeeID)
                            select new HealDocmentView
                            {
                                ID = s.ID,
                                BirthDay = s.BirthDay,
                                Age = DateTime.Now.Year - s.BirthDay.Year,
                                CNName =emp.CNName,
                                EmployeeID = s.EmployeeID,
                                Gender = emp.Gender,
                                HeredityRec = s.HeredityRec,
                                IllnessRec = s.IllnessRec,
                                Nation = s.Nation,
                                OpreatRec = s.OpreatRec,
                                OrgID =emp.OrgID
                            };
               
                return new ActionResult<IEnumerable<HealDocmentView>>(rehds);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<HealDocmentView>>(ex);
            }
        }

        /// <summary>
        /// 获取健康档案 模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<HealDocmentView> GetHealDocment(Guid id)
        {
            try
            {
                var dbhd = _rpshd.GetModel(id);
                var emp = _rpsemp.GetModel(dbhd.EmployeeID);
                var re = dbhd.MAPTO<HealDocmentView>();
                re.Age =DateTime.Now.Year-dbhd.BirthDay.Year;
                re.CNName = emp.CNName;
                re.Gender = emp.Gender;
                re.OrgID = emp.OrgID;
                return new ActionResult<HealDocmentView>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<HealDocmentView>(ex);
            }
        }
        /// <summary>
        /// 分页获取健康档案
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<HealDocmentView>> GetHealDocments(PagerQuery<HealDocmentQuery> para)
        {
            try
            {
                var emps = _rpsemp.Queryable(p=>(p.OrgID== para.Query.OrgID || para.Query.OrgID==Guid.Empty)&&(p.CNName.Contains(para.Query.Key)||para.Query.Key==string.Empty)).ToList();
                var empsid = emps.Select(p=>p.ID);

                var dbhds = _rpshd.Queryable(p=>empsid.Contains(p.EmployeeID));
                var rehds = from s in dbhds.ToList()
                            let emp=emps.FirstOrDefault(p=>p.ID==s.EmployeeID)
                            select new HealDocmentView
                            {
                                ID = s.ID,
                                BirthDay=s.BirthDay,
                                Age=DateTime.Now.Year-s.BirthDay.Year,
                                CNName=emp.CNName,
                                EmployeeID=s.EmployeeID,
                                Gender=emp.Gender,
                                HeredityRec=s.HeredityRec,
                                IllnessRec=s.IllnessRec,
                                Nation=s.Nation,
                                OpreatRec=s.OpreatRec,
                                OrgID=emp.OrgID
                            };
                var re = new Pager<HealDocmentView>().GetCurrentPage(rehds, para.PageSize, para.PageIndex);
                return new ActionResult<Pager<HealDocmentView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<HealDocmentView>>(ex);
            }
        }

        
    }
}
