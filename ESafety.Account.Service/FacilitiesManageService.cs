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
    public class FacilitiesManageService :ServiceBase, IFacilitiesManageService
    {
        private IUnitwork _work = null;
        private IRepository<Basic_FacilitiesSort> _rpsfacilitiessort = null;
        private IRepository<Basic_Facilities> _rpsfacilities = null;

        private IUserDefined usedefinedService = null;
        public FacilitiesManageService(IUnitwork work, IUserDefined usf)
        {
            _work = work;
            Unitwork = work;
            _rpsfacilitiessort = work.Repository<Basic_FacilitiesSort>();
            _rpsfacilities = work.Repository<Basic_Facilities>();
            usedefinedService = usf;
        }
        /// <summary>
        /// 新建设备设施类别
        /// </summary>
        /// <param name="facilitiesSort"></param>
        /// <returns></returns>
        public ActionResult<bool> AddFacilitiesSort(FacilitiesSortNew facilitiesSort)
        {
            try
            {
                var check = _rpsfacilitiessort.Any(p=>p.ParentID==facilitiesSort.ParentID&&p.SortName==facilitiesSort.SortName);
                if (check)
                {
                    throw new Exception("该设备设施类别已存在！");
                }
                var dbfacilitiessort = facilitiesSort.MAPTO<Basic_FacilitiesSort>();
                _rpsfacilitiessort.Add(dbfacilitiessort);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 新建设备设施模型
        /// </summary>
        /// <param name="facility"></param>
        /// <returns></returns>
        public ActionResult<bool> AddFacility(FacilityNew facility)
        {
            try
            {
                if (facility == null)
                {
                    throw new Exception("参数错误");
                }
                var check = _rpsfacilities.Any(p=>p.SortID==facility.SortID&&p.Name==facility.Name);
                if (check)
                {
                    throw new Exception("该设备设施已存在");
                }
                var dbfacility = facility.MAPTO<Basic_Facilities>();

                var definedvalue = new UserDefinedBusinessValue
                {
                    BusinessID = dbfacility.ID,
                    Values = facility.UserDefineds
                };
                var defined = usedefinedService.SaveBuisnessValue(definedvalue);
                if (defined.state != 200)
                {
                    throw new Exception(defined.msg);
                }

                _rpsfacilities.Add(dbfacility);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 删除设备设施类别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelFacilitiesSort(Guid id)
        {
            try
            {
                var check = _rpsfacilitiessort.Any(p => p.ID == id);
                if (!check)
                {
                    throw new Exception("未找到该类别");
                }
                check = _rpsfacilitiessort.Any(p=>p.ParentID==id);
                if (check)
                {
                    throw new Exception("该类别存在子类别，无法删除");
                }
                check = _rpsfacilities.Any(p => p.SortID == id);
                if (check)
                {
                    throw new Exception("该类别下存在设备设施，无法删除");
                }
                _rpsfacilitiessort.Delete(p=>p.ID==id);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
           
        }
        /// <summary>
        /// 删除设备设施
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelFacility(Guid id)
        {
            try
            {
                var check = _rpsfacilities.Any(p => p.ID == id);
                if (check)
                {
                    throw new Exception("该设备设施不存在");
                }
                _rpsfacilities.Delete(p=>p.ID==id);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 修改设备设施
        /// </summary>
        /// <param name="facility"></param>
        /// <returns></returns>
        public ActionResult<bool> EditFacility(FacilityEdit facility)
        {
            try
            {
                var dbfacility = _rpsfacilities.GetModel(p=>p.ID==facility.ID);
                if (dbfacility == null)
                {
                    throw new Exception("未找到所需修改的设备设施项");
                }
                var _dbfacility = facility.CopyTo<Basic_Facilities>(dbfacility);

                var definedvalue = new UserDefinedBusinessValue
                {
                    BusinessID = _dbfacility.ID,
                    Values = facility.UserDefineds
                };
                var defined = usedefinedService.SaveBuisnessValue(definedvalue);
                if (defined.state != 200)
                {
                    throw new Exception(defined.msg);
                }
                _rpsfacilities.Update(_dbfacility);
                _work.Commit();
                return new ActionResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 根据类别ID分页获取设备设施
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<FacilityView>> GetFacilities(PagerQuery<FacilitiesQuery> para)
        {
            try
            {
               
                var dbfacilities = _rpsfacilities.Queryable(p=>p.SortID==para.Query.ID/*&& (p.Name.Contains(para.Query.Name) || p.Code.Contains(para.Query.Code) || string.IsNullOrEmpty(para.Query.Name) || string.IsNullOrEmpty(para.Query.Code))*/);
                var sortname = _rpsfacilitiessort.GetModel(p => p.ID == para.Query.ID).SortName;
                var refclty = from f in dbfacilities
                              select new FacilityView
                              {
                                  ID = f.ID,
                                  Code = f.Code,
                                  Name = f.Name,
                                  Principal = f.Principal,
                                  PrincipalTel = f.PrincipalTel,
                                  SortID = f.SortID,
                                  SortName = sortname
                              };
                var re = new Pager<FacilityView>().GetCurrentPage(refclty, para.PageSize, para.PageIndex);
                return new ActionResult<Pager<FacilityView>>(re);
            }
            catch (Exception ex)
            {

                return new ActionResult<Pager<FacilityView>>(ex);
            }
        }
        /// <summary>
        /// 获取设备设施类别树节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<FacilitiesSortView>> GetFacilitiesSorts(Guid id)
        {
            try
            {
                var dbfacilitiessorts = _rpsfacilitiessort.Queryable(p=>p.ParentID==id);
                var re = from s in dbfacilitiessorts
                         select new FacilitiesSortView
                         {
                             ID = s.ID,
                             ParentID = s.ParentID,
                             Level = s.Level,
                             SortName = s.SortName
                         };
                return new ActionResult<IEnumerable<FacilitiesSortView>>(re);
            }
            catch (Exception ex)
            {

                return new ActionResult<IEnumerable<FacilitiesSortView>>(ex);
            }
        }
    }
}
