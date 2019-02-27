

/*---------------------------------------------------------------- 
// 版权所有。  
// 
// 文件名： Doc_TrainPeopleService
// 文件功能描述： 
// author：DengYinFeng
// 时间：02/27/2019 21:22:10
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
using System.Collections.Generic;
using ESafety.Account.Model.View;
using System;
using ESafety.Core.Model.DB;
using System.Linq;

namespace ESafety.Account.Service
{
	public  class  Doc_TrainPeopleService:ServiceBase,IDoc_TrainPeopleService
	{
		private IUnitwork _work = null;

        /// <summary>
        /// 培训
        /// </summary>
        private IRepository<Doc_TrainPeople> _idocTrain = null;

        /// <summary>
        /// 组织架构
        /// </summary>
        private IRepository<Basic_Org> _rpsorg = null;
        /// <summary>
        /// 人员
        /// </summary>
        private IRepository<Basic_Employee> _rpsemployee = null;


        public Doc_TrainPeopleService(IUnitwork work){
			_work = work;
            _idocTrain = _work.Repository<Doc_TrainPeople>();
            _rpsorg = _work.Repository<Basic_Org>();
            _rpsemployee = _work.Repository<Basic_Employee>();
        }

        /// <summary>
        /// 根据培训id获取培训人员
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ActionResult<List<DocTranPeopleView>> GetPeopleAll(Guid guid)
        {
            List<Doc_TrainPeople> doc_Trains = _idocTrain.GetList(r=>r.TPId == guid).ToList();
            List<DocTranPeopleView> dataes = (from Item in doc_Trains
                                                 //组织
                                             let zObj = _rpsorg.GetModel(Item.TTid)
                                             //人员
                                             let pObj = _rpsemployee.GetModel(Item.TPId)
                                             where Item.TTid == zObj.ID && Item.TPId == pObj.ID
                                             select new DocTranPeopleView()
                                             {
                                                 Id = Item.Id,
                                                 BName = zObj.OrgName,
                                                 RName = pObj.CNName
                                             }).ToList();
            return new ActionResult<List<DocTranPeopleView>>(new List<DocTranPeopleView>());
        }
	}
}



    