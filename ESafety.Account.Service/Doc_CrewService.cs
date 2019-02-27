

/*---------------------------------------------------------------- 
// 版权所有。  
// 
// 文件名： Doc_CrewService
// 文件功能描述： 
// author：DengYinFeng
// 时间：02/27/2019 10:46:08
// 创建标识： 
// 
// 修改标识： 
//  
// 修改描述：此代码由T4模板自动生成
//			 对此文件的更改可能会导致不正确的行为，并且如果
//			 重新生成代码，这些更改将会丢失。
//----------------------------------------------------------------*/

using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
using ESafety.Core.Model.DB.Account;
using ESafety.ORM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESafety.Account.Service
{
	public  class  Doc_CrewService:ServiceBase,IDoc_CrewService
	{
		private IUnitwork _work = null;
        private IRepository<TypeConfig> _rpsdanger = null;
        private IRepository<Doc_Crew> _doccrew = null;
        public Doc_CrewService(IUnitwork work){
			_work = work;
            Unitwork = work;
            _rpsdanger = work.Repository<TypeConfig>();
            _doccrew = work.Repository<Doc_Crew>();

        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public List<TypeConfig> GetMenus()


        {
                var menus = _rpsdanger.GetList().ToList();
                return menus;
        }

        public Pager<DocCrewDTO> GetPageCrew(PagerQuery<Doc_Crew> page,Guid id)
        {
                var data = from Item in _doccrew.GetList(r => r.CType == id).OrderBy(r => r.ID)
                           let Obj = _rpsdanger.GetModel(r => r.ID == Item.CType)
                           select new DocCrewDTO()
                           {
                               Id = Item.ID,
                               CContent = Item.CContent,
                               CFontSize = Item.CFontSize,
                               CName = Item.CName,
                               CreateTime = Item.CreateTime,
                               TId = Obj.ID,
                               TName = Obj.TName
                           }; 
                return new Pager<DocCrewDTO>().GetCurrentPage(data, page.PageSize, page.PageIndex);
            }
    }
}



    