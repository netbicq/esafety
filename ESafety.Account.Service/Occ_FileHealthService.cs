

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

namespace ESafety.Account.Service
{
	public  class  Occ_FileHealthService:ServiceBase,IOcc_FileHealthService
	{
		private IUnitwork _work = null;

        private IRepository<Bll_AttachFile> _rpsFile = null;
        private IRepository<Occ_FileHealth> _iocchealth = null;
        private IRepository<Basic_Employee> _iemp = null;
        private IRepository<Basic_Org> _rpsorg = null;

        public Occ_FileHealthService(IUnitwork work){
			_work = work;
            _iocchealth = _work.Repository<Occ_FileHealth>();
            _iemp = _work.Repository<Basic_Employee>();
            _rpsorg = _work.Repository<Basic_Org>();
            _rpsFile = _work.Repository<Bll_AttachFile>();
        }


        /// <summary>
        /// 分页获取健康档案
        /// </summary>
        /// <param name="occFile"></param>
        /// <returns></returns>
        public ActionResult<Pager<OccFileHealthView>> GetHealthData(OccFileHealthPara occFile)
        {
            var baseHealth = _iocchealth.GetList().ToList();
            var resultData = (from Item in baseHealth
                             let Ry = _iemp.GetModel(r => r.ID == Item.FEmpId)
                             let Zz = _rpsorg.GetModel(r => r.ID == Ry.OrgID)
                             select new OccFileHealthView()
                             {
                                 Id = Item.Id,
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
                             }).ToList();

            if (occFile.ZzId != Guid.Empty)
                resultData.Where(r => r.ZzId == occFile.ZzId).ToList();
            if (!string.IsNullOrWhiteSpace(occFile.Keyword))
                resultData.Where(r => r.FEmpName.Contains(occFile.Keyword)).ToList();

            return new ActionResult<Pager<OccFileHealthView>>(new Pager<OccFileHealthView>()
                .GetCurrentPage(resultData, occFile.PageSize, occFile.PageIndex));
        }

        /// <summary>
        /// 根据id删除健康档案
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ActionResult<bool> DeleteFileHealthById(Guid guid)
        {
            if (_iocchealth.Delete(r => r.Id == guid) == 0)
                throw new Exception("当前数据不存在");
            _rpsFile.Delete(r => r.BusinessID == guid);
            _work.Commit();
            return new ActionResult<bool>(true);
        }


        /// <summary>
        /// 添加或修改健康档案
        /// </summary>
        /// <param name="occFile"></param>
        /// <returns></returns>
        public ActionResult<bool> AddOrUpdateEmp(OccFileHealthPara occFile)
        {
            if(_iocchealth.GetModel(r=>r.Id == occFile.Id) != null)
            {
                _iocchealth.Update(r => r.Id == occFile.Id, (V) => new Occ_FileHealth()
                {
                    FEmpId = V.FEmpId,
                    FTypeName = V.FTypeName,
                    FDisease = V.FDisease,
                    FBornTime = V.FBornTime,
                    FContent = V.FContent,
                    FGenetic = V.FGenetic,
                    FSurgery = V.FSurgery,
                });
                _rpsFile.Delete(r => r.BusinessID == occFile.tb.Id);
                _rpsFile.Add(occFile.files);
            }
            else
            {
                occFile.tb.CreateTime = DateTime.Now;
                _iocchealth.Add(occFile.tb);
                _rpsFile.Add(occFile.files);
            }
            _work.Commit();
            return new ActionResult<bool>();
        }
	}
}



    