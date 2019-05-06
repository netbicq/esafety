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
        public ITree srvTree = null;
        private IAttachFile srvFile = null;

        public FacilitiesManageService(IUnitwork work, IUserDefined usf,ITree tree,IAttachFile file)
        {
            _work = work;
            Unitwork = work;
            _rpsfacilitiessort = work.Repository<Basic_FacilitiesSort>();
            _rpsfacilities = work.Repository<Basic_Facilities>();
            usedefinedService = usf;
            srvTree = tree;
            srvFile = file;

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
                if (facilitiesSort == null)
                {
                    throw new Exception("参数有误");
                }
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
                //文件
                var files = new AttachFileSave
                {
                    BusinessID=dbfacility.ID,
                    files=facility.fileNews
                };
                var file = srvFile.SaveFiles(files);
                if (file.state != 200)
                {
                    throw new Exception(file.msg);
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
                if (!check)
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
                var files = new AttachFileSave
                {
                    BusinessID = _dbfacility.ID,
                    files = facility.fileNews
                };
                var file = srvFile.SaveFiles(files);
                if (file.state != 200)
                {
                    throw new Exception(file.msg);
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
               
                var dbfacilities = _rpsfacilities.Queryable(p => p.SortID == para.Query.ID && (p.Name.Contains(para.KeyWord) || p.Code.Contains(para.KeyWord) || string.IsNullOrEmpty(para.KeyWord)));
                var sortname = _rpsfacilitiessort.GetModel(p => p.ID == para.Query.ID).SortName;
                var orgids = dbfacilities.Select(s => s.OrgID);

                var orgs = _work.Repository<Core.Model.DB.Basic_Org>().Queryable(p=>orgids.Contains(p.ID));
                var refclty = from f in dbfacilities
                              let org = orgs.FirstOrDefault(p=>p.ID==f.OrgID)
                              select new FacilityView
                              {
                                  ID = f.ID,
                                  Code = f.Code,
                                  Name = f.Name,
                                  Principal = f.Principal,
                                  PrincipalTel = f.PrincipalTel,
                                  SortID = f.SortID,
                                  SortName = sortname,
                                  OrgName=org.OrgName,
                                  OrgID=f.OrgID

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
        /// <summary>
        /// 获取设备模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<FacilityView> GetModel(Guid id)
        {
            try
            {
                var re = _rpsfacilities.GetModel(id);
                
                if(re == null)
                {
                    throw new Exception("设备未找到");
                }

                var result = re.MAPTO<FacilityView>();
                result.SortName = _rpsfacilitiessort.GetModel(re.SortID).SortName;

                return new ActionResult<FacilityView>(result);
            }
            catch (Exception ex)
            {
                return new ActionResult<FacilityView>(ex);
            }
        }
        /// <summary>
        /// 获取设备类别子级id集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<Guid>> GetSortChildrenIds(Guid id)
        {
            try
            {
                (srvTree as TreeService).AppUser = AppUser;
                var re = srvTree.GetChildrenIds<Basic_FacilitiesSort>(id);
                return new ActionResult<IEnumerable<Guid>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<Guid>>(ex);
            }
        }
        /// <summary>
        /// 获取设备类别父级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<Basic_FacilitiesSort>> GetSortParents(Guid id)
        {
            try
            {
                (srvTree as TreeService).AppUser = AppUser;
                var re = srvTree.GetParents<Basic_FacilitiesSort>(id);
                return new ActionResult<IEnumerable<Basic_FacilitiesSort>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<Basic_FacilitiesSort>>(ex);
            }
        }
        /// <summary>
        /// 获取设备类别树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<FacilitiesSortTree>> GetSortTree(Guid id)
        {
            try
            {
                (srvTree as TreeService).AppUser = AppUser;
                var re = srvTree.GetTree<Basic_FacilitiesSort, FacilitiesSortTree>(id);

                return new ActionResult<IEnumerable<FacilitiesSortTree>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<FacilitiesSortTree>>(ex);
            }
        }
        /// <summary>
        /// 获取所有设备设施
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<FacilityView>> GetFacilitiesList()
        {
            try
            {
                var dbfacilities = _rpsfacilities.Queryable();
                var sort = _rpsfacilitiessort.Queryable();
                var refclty = from f in dbfacilities
                              let s=sort.FirstOrDefault(p=>p.ID==f.SortID)
                              select new FacilityView
                              {
                                  ID = f.ID,
                                  Code = f.Code,
                                  Name = f.Name,
                                  Principal = f.Principal,
                                  PrincipalTel = f.PrincipalTel,
                                  SortID = f.SortID,
                                  SortName =s.SortName
                              };
                return new ActionResult<IEnumerable<FacilityView>>(refclty);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<FacilityView>>(ex);
            }
        }
    }
}
