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
    /// 
    /// </summary>
    public class HealRecordsService : ServiceBase,IHealRecordsService
    {
        private IUnitwork _work = null;
        private IRepository<Heal_Docment> _rpshd = null;
        private IRepository<Core.Model.DB.Basic_Employee> _rpsemp = null;
        private IRepository<Heal_Records> _rpshr = null;
        private IAttachFile srvFile = null;
        public HealRecordsService(IUnitwork work,IAttachFile file)
        {
            _work = work;
            Unitwork = work;
            _rpshd = work.Repository<Heal_Docment>();
            _rpsemp = work.Repository<Core.Model.DB.Basic_Employee>();
            _rpshr = work.Repository<Heal_Records>();
            srvFile = file;

        }
        /// <summary>
        /// 新建体检模型
        /// </summary>
        /// <param name="recordsNew"></param>
        /// <returns></returns>
        public ActionResult<bool> AddHealRecord(HealRecordsNew recordsNew)
        {
            try
            {
                if (recordsNew== null)
                {
                    throw new Exception("参数有误");
                }
                var check = _rpshr.Any(p => p.DocmentID == recordsNew.DocmentID&&p.RecDate==recordsNew.RecDate);
                if (check)
                {
                    throw new Exception("该体检信息已存在！");
                }
                var dbhr = recordsNew.MAPTO<Heal_Records>();

                //电子文档
                var files = new AttachFileSave
                {
                    BusinessID = dbhr.ID,
                    files = from f in recordsNew.AttachFiles
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

                _rpshr.Add(dbhr);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 删除体检信息模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelHealRecord(Guid id)
        {
            try
            {
                var dbhr = _rpshr.GetModel(id);
                if (dbhr == null)
                {
                    throw new Exception("未找到所要删除的体检信息");
                }
                _rpshr.Delete(dbhr);
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
        /// 修改体检模型
        /// </summary>
        /// <param name="docmentEdit"></param>
        /// <returns></returns>
        public ActionResult<bool> EditHealRecord(HealRecordsEdit recordsEdit)
        {
            try
            {
                var dbhr = _rpshr.GetModel(recordsEdit.ID);
                if (dbhr == null)
                {
                    throw new Exception("未找到所要修改的体检信息！");
                }
                var check = _rpshr.Any(p => p.ID != recordsEdit.ID && p.RecDate == recordsEdit.RecDate);
                if (check)
                {
                    throw new Exception("该人员的体检信息已存在！");
                }
                dbhr = recordsEdit.CopyTo<Heal_Records>(dbhr);
                //电子文档 
                srvFile.DelFileByBusinessId(dbhr.ID);
                var files = new AttachFileSave
                {
                    BusinessID = dbhr.ID,
                    files = from f in recordsEdit.AttachFiles
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

                _rpshr.Update(dbhr);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 获取体检信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<HealRecordsView> GetHealRecord(Guid id)
        {
            try
            {
                var dbhr = _rpshr.GetModel(id);
                var hd = _rpshd.GetModel(dbhr.DocmentID);
                var emp = _rpsemp.GetModel(hd.EmployeeID);
                var re = dbhr.MAPTO<HealRecordsView>();
                re.Age = DateTime.Now.Year - hd.BirthDay.Year;
                re.Name = emp.CNName;
                re.Gender = emp.Gender;
                return new ActionResult<HealRecordsView>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<HealRecordsView>(ex);
            }
        }
        /// <summary>
        /// 分页获取体检信息
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<HealRecordsView>> GetHealRecords(PagerQuery<HealRecordsQuery> para)
        {
            try
            {
                var dbhds = _rpshr.Queryable(p => p.DocmentID== para.Query.DocmentID || Guid.Empty == para.Query.DocmentID);
                var hds = _rpshd.Queryable();
                var emps = _rpsemp.Queryable();
                var rehds = from s in dbhds
                            let hd = hds.FirstOrDefault(p=>p.ID==s.DocmentID)
                            let emp =emps.FirstOrDefault(p=>p.ID==hd.EmployeeID)
                            orderby emp.CNName
                            select new HealRecordsView
                            {
                                ID = s.ID,
                                Age = DateTime.Now.Year - hd.BirthDay.Year,
                                Name = emp.CNName,
                                Gender = emp.Gender,
                                DocmentID=s.DocmentID,
                                RecDate=s.RecDate,
                                RecResult=s.RecResult
                            };
                var re = new Pager<HealRecordsView>().GetCurrentPage(rehds, para.PageSize, para.PageIndex);
                return new ActionResult<Pager<HealRecordsView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<HealRecordsView>>(ex);
            }
        }
    }
}
