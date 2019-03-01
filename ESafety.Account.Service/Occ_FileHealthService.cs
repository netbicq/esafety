

/*---------------------------------------------------------------- 
// 版权所有。  
// 
// 文件名： Occ_FileHealthService
// 文件功能描述： 
// author：DengYinFeng
// 时间：02/28/2019 10:42:15
// 创建标识： 
// 
// 修改标识： 
//  
// 修改描述：此代码由T4模板自动生成
//			 对此文件的更改可能会导致不正确的行为，并且如果
//			 重新生成代码，这些更改将会丢失。
//----------------------------------------------------------------*/

using ESafety.Core.Model;
using ESafety.Account.IService;
using ESafety.ORM;
using ESafety.Core.Model.DB.Account;
using ESafety.Account.Model.View;
using ESafety.Account.Model.PARA;
using System.Linq;
using ESafety.Core.Model.DB;
using System;
using ESafety.Core;
using ESafety.Unity;

namespace ESafety.Account.Service
{
    /// <summary>
    /// 职业健康
    /// </summary>
	public  class  Occ_FileHealthService:ServiceBase,IOcc_FileHealthService
	{
		private IUnitwork _work = null;

        private IRepository<Bll_AttachFile> _rpsFile = null;
        private IRepository<Occ_FileHealth> _iocchealth = null;
        private IRepository<Basic_Employee> _iemp = null;
        private IRepository<Occ_Medical> _ioccMedical = null;
        private IRepository<Basic_Org> _rpsorg = null;
        /// <summary>
        /// 电子文档service
        /// </summary>
        private IAttachFile attach = null;
        public Occ_FileHealthService(IUnitwork work,AttachFileService a){
			_work = work;
            _iocchealth = _work.Repository<Occ_FileHealth>();
            _iemp = _work.Repository<Basic_Employee>();
            _rpsorg = _work.Repository<Basic_Org>();
            _rpsFile = _work.Repository<Bll_AttachFile>();
            _ioccMedical = _work.Repository<Occ_Medical>();
            attach = a;
        }





        /// <summary>
        /// 添加健康档案
        /// </summary>
        /// <param name="aOcc"></param>
        /// <returns></returns>
        public ActionResult<bool> InceaseHealth(AOccFileHealthPara aOcc)
        {
            try
            {
                Occ_FileHealth occ_File = _iocchealth.GetModel(r => r.FEmpId == aOcc.FEmpId);
                if (occ_File != null)
                    throw new Exception("已存在当前人员的健康档案");
                Occ_FileHealth Dto = aOcc.MAPTO<Occ_FileHealth>();
                _iocchealth.Add(Dto);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 修改健康档案
        /// </summary>
        /// <param name="aOcc"></param>
        /// <returns></returns>
        public ActionResult<bool> AmendHealth(AOccFileHealthPara aOcc)
        {
            try
            {
                Occ_FileHealth occ_File = _iocchealth.GetModel(r => r.ID == aOcc.ID);
                if (occ_File == null)
                    throw new Exception("未找到当前档案");
                Occ_FileHealth Dto = aOcc.CopyTo(occ_File);
                _iocchealth.Update(Dto);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }



        /// <summary>
        /// 分页获取健康档案
        /// </summary>
        /// <param name="occFile"></param>
        /// <returns></returns>
        public ActionResult<Pager<OccFileHealthView>> GetHealthData(PagerQuery<OccFileHealthPara> occFile)
        {
            try
            {
                var baseHealth = _iocchealth.GetList();
                var resultData = (from Item in baseHealth
                                  let Ry = _iemp.GetModel(r => r.ID == Item.FEmpId)
                                  let Zz = _rpsorg.GetModel(r => r.ID == Ry.OrgID)
                                  select new OccFileHealthView()
                                  {
                                      Id = Item.ID,
                                      FBornTime = Item.FBornTime,
                                      FContent = Item.FContent,
                                      FDisease = Item.FDisease,
                                      FEmpId = Ry.ID,
                                      FEmpName = Ry.CNName,
                                      FGenetic = Item.FGenetic,
                                      FSurgery = Item.FSurgery,
                                      FTypeName = Item.FTypeName,
                                      ZzId = Zz.ID,
                                      ZzName = Zz.OrgName,
                                      Sex = Ry.Gender
                                  });

                if (occFile.Query.ZzId != Guid.Empty)
                    resultData.Where(r => r.ZzId == occFile.Query.ZzId);
                if (!string.IsNullOrWhiteSpace(occFile.KeyWord))
                    resultData.Where(r => r.FEmpName.Contains(occFile.KeyWord));
                var page = new Pager<OccFileHealthView>()
                    .GetCurrentPage(resultData, occFile.PageSize, occFile.PageIndex);
                return new ActionResult<Pager<OccFileHealthView>>(page);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<OccFileHealthView>>(ex);
            }
        }

        /// <summary>
        /// 根据id删除健康档案
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ActionResult<bool> DeleteFileHealthById(Guid guid)
        {
            try
            {
                if (_iocchealth.Delete(r => r.ID == guid) == 0)
                    throw new Exception("当前数据不存在");
                attach.DelFileByBusinessId(guid);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }



        /// <summary>
        /// 分页获取体检数据
        /// </summary>
        /// <param name="occFile"></param>
        /// <returns></returns>
        public ActionResult<Pager<OccMedicalView>> GetMedicalData(PagerQuery<OccMedicalPara> occFile)
        {
            try
            {
                var baseHealth = _ioccMedical.GetList();
                var resultData = from Item in baseHealth
                                 let Ry = _iemp.GetModel(r => r.ID == Item.MEmpId)
                                 let Zz = _rpsorg.GetModel(r => r.ID == Ry.OrgID)
                                 select new OccMedicalView()
                                 {
                                     Id = Item.ID,
                                     MEmpId = Ry.ID,
                                     MEmpName = Ry.CNName,
                                     ZzId = Zz.ID,
                                     ZzName = Zz.OrgName,
                                     MAge = Item.MAge,
                                     MContent = Item.MContent,
                                     Sex = Ry.Gender,
                                     MTime = Item.MTime,

                                 };

                if (occFile.Query.ZzId != Guid.Empty)
                    resultData.Where(r => r.ZzId == occFile.Query.ZzId);
                if (!string.IsNullOrWhiteSpace(occFile.KeyWord))
                    resultData.Where(r => r.MEmpName.Contains(occFile.KeyWord));
                var page = new Pager<OccMedicalView>()
                    .GetCurrentPage(resultData, occFile.PageSize, occFile.PageIndex);
                return new ActionResult<Pager<OccMedicalView>>(page);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<OccMedicalView>>(ex);
            }
        }



        /// <summary>
        /// 根据id删除体检数据
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ActionResult<bool> DeleteMedicalById(Guid ID)
        {
            try
            {
                if (_ioccMedical.Delete(r => r.ID == ID) == 0)
                    throw new Exception("当前数据不存在");
                attach.DelFileByBusinessId(ID);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 添加体检报告
        /// </summary>
        /// <param name="occMedical"></param>
        /// <returns></returns>
        public ActionResult<bool> IncreaseMedical(Occ_Medical occMedical)
        {
            try
            {
                Occ_Medical occ = _ioccMedical.GetModel(r => r.MEmpId == occMedical.MEmpId);
                if (occ != null)
                    throw new Exception("已存在当前人员的体检报告");
                _ioccMedical.Add(occMedical);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 修改体检报告
        /// </summary>
        /// <param name="occMedical"></param>
        /// <returns></returns>
        public ActionResult<bool> AmendMedical(AOccMedicalPara occMedical)
        {
            try
            {
                Occ_Medical occ = _ioccMedical.GetModel(occMedical.ID);
                if (occ == null)
                    throw new Exception("体检报告不存在");
                _ioccMedical.Update(occMedical.CopyTo(occ));
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

    }
}



    