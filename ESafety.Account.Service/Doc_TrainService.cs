

/*---------------------------------------------------------------- 
// 版权所有。  
// 
// 文件名： Doc_TrainService
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
using ESafety.Account.Model.View;
using ESafety.Account.Model.PARA;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ESafety.Account.Service
{
	public  class  Doc_TrainService:ServiceBase,IDoc_TrainService
	{
		private IUnitwork _work = null;

        private IRepository<Doc_Train> _docTran = null;
        /// <summary>
        /// 培训
        /// </summary>
        private IRepository<Doc_TrainPeople> _idocTrain = null;
        public Doc_TrainService(IUnitwork work){
			_work = work;

            _docTran = work.Repository<Doc_Train>();

		}

        /// <summary>
        /// 获取培训数据
        /// </summary>
        /// <param name="docTran"></param>
        /// <returns></returns>
        public ActionResult<Pager<Doc_Train>> GetTranData(DocTranPara docTran)
        {
            if (!string.IsNullOrWhiteSpace(docTran.Keyword))
            {
                return new ActionResult<Pager<Doc_Train>>(new Pager<Doc_Train>()
                .GetCurrentPage(_docTran.GetList().ToList().Where(r=>r.TTheme.Contains(docTran.Keyword)), docTran.PageSize, docTran.PageIndex));
            }
            return new ActionResult<Pager<Doc_Train>>(new Pager<Doc_Train>()
                .GetCurrentPage(_docTran.GetList().ToList(),docTran.PageSize,docTran.PageIndex));
        }


        /// <summary>
        /// 修改或添加数据
        /// </summary>
        /// <param name="doc_Train"></param>
        /// <returns></returns>
        public ActionResult<bool> AddOrUpdateTran(DocTranPara doc_Train)
        {
            if(!(doc_Train.Id == Guid.Empty || doc_Train.Id == null))
            {
                _docTran.Update(r => r.ID == doc_Train.tb.Id, (V) => new Doc_Train() {
                    TTheme = doc_Train.tb.TTheme,
                    TTime = doc_Train.tb.TTime,
                    TEndTime = doc_Train.tb.TEndTime,
                    Trainer = doc_Train.tb.Trainer,
                    TContent = doc_Train.tb.TContent,                    
                });
                _idocTrain.Delete(r=>r.Id == doc_Train.tb.Id);
                doc_Train.dtDB.ForEach(r => r.TTid = doc_Train.tb.Id);
                _idocTrain.Add(doc_Train.dtDB);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            _docTran.Add(doc_Train.tb);
            doc_Train.dtDB.ForEach(r => r.TTid = doc_Train.tb.ID);
            _idocTrain.Add(doc_Train.dtDB);
            _work.Commit();
            return new ActionResult<bool>(true);
        }

        /// <summary>
        /// 删除培训
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ActionResult<bool> DeleteDocTranById(Guid guid)
        {
            int state = _docTran.Delete(r => r.ID == guid);
            if (state == 0)
                throw new Exception("当前数据不存在");
            _idocTrain.Delete(r => r.TPId == guid);
            _work.Commit();
            return new ActionResult<bool>(true);
        }

        /// <summary>
        /// 移除当前培训人员
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ActionResult<bool> DeleteDocTranpeopleById(Guid guid)
        {
            int state = _idocTrain.Delete(r=>r.ID == guid);
            if (state == 0)
                throw new Exception("没有当前数据");
            return new ActionResult<bool>(true);
        }



    }
}



    