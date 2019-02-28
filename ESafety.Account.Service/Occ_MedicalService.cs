

/*---------------------------------------------------------------- 
// 版权所有。  
// 
// 文件名： Occ_MedicalService
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
using ESafety.Core.Model.DB;
using System.Linq;
using ESafety.Account.Model.View;
using ESafety.Account.Model.PARA;
using System;

namespace ESafety.Account.Service
{
	public  class  Occ_MedicalService:ServiceBase,IOcc_MedicalService
	{
		private IUnitwork _work = null;

        private IRepository<Bll_AttachFile> _rpsFile = null;
        private IRepository<Occ_Medical> _ioccMedical = null;
        private IRepository<Basic_Employee> _iemp = null;
        private IRepository<Basic_Org> _rpsorg = null;

        public Occ_MedicalService(IUnitwork work){
			_work = work;
            _ioccMedical = _work.Repository<Occ_Medical>();
            _iemp = _work.Repository<Basic_Employee>();
            _rpsorg = _work.Repository<Basic_Org>();
            _rpsFile = _work.Repository<Bll_AttachFile>();
        }


        /// <summary>
        /// 添加或者修改体检数据
        /// </summary>
        /// <param name="occFile"></param>
        /// <returns></returns>
        public ActionResult<bool> AddOrUpdateMedical(OccMedicalPara occFile)
        {
            if (_ioccMedical.GetModel(r => r.Id == occFile.Id) != null)
            {
                _ioccMedical.Update(r => r.Id == occFile.Id, (V) => new Occ_Medical()
                {
                    MEmpId = V.MEmpId,
                    MTime = V.MTime,
                    MContent = V.MContent,
                    MAge = V.MAge,
                });
                _rpsFile.Delete(r => r.BusinessID == occFile.tb.Id);
                _rpsFile.Add(occFile.files);
            }
            else
            {
                occFile.tb.CreateTime = DateTime.Now;
                _ioccMedical.Add(occFile.tb);
                _rpsFile.Add(occFile.files);
            }
            _work.Commit();
            return new ActionResult<bool>();

        }


        /// <summary>
        /// 根据id删除体检数据
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ActionResult<bool> DeleteMedicalById(Guid guid)
        {
            if (_ioccMedical.Delete(r => r.Id == guid) == 0)
                throw new Exception("当前数据不存在");
            _rpsFile.Delete(r=>r.BusinessID == guid);
            _work.Commit();
            return new ActionResult<bool>(true);
        }


        /// <summary>
        /// 分页获取体检数据
        /// </summary>
        /// <param name="occFile"></param>
        /// <returns></returns>
        public ActionResult<Pager<OccMedicalView>> GetMedicalData(OccMedicalPara occFile)
        {
            var baseHealth = _ioccMedical.GetList().ToList();
            var resultData = (from Item in baseHealth
                              let Ry = _iemp.GetModel(r => r.ID == Item.MEmpId)
                              let Zz = _rpsorg.GetModel(r => r.ID == Ry.OrgID)
                              select new OccMedicalView()
                              {
                                  Id = Item.Id,
                                  MEmpId = Ry.ID,
                                  MEmpName = Ry.CNName,
                                  ZzId = Zz.ID,
                                  ZzName = Zz.OrgName,
                                  MAge = Item.MAge,
                                  MContent = Item.MContent,
                                  Sex = Ry.Gender,
                                  MTime = Item.MTime,
                                  
                              }).ToList();

            if (occFile.ZzId != Guid.Empty)
                resultData.Where(r => r.ZzId == occFile.ZzId).ToList();
            if (!string.IsNullOrWhiteSpace(occFile.Keyword))
                resultData.Where(r => r.MEmpName.Contains(occFile.Keyword)).ToList();

            return new ActionResult<Pager<OccMedicalView>>(new Pager<OccMedicalView>()
                .GetCurrentPage(resultData, occFile.PageSize, occFile.PageIndex));
        }

    }
}



    