

/*---------------------------------------------------------------- 
// 版权所有。  
// 
// 文件名： Doc_QualificationService
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
using ESafety.Core.Model.DB;
using ESafety.Core.Model.DB.Account;
using ESafety.Account.Model.PARA;
using System.Collections.Generic;
using System.Linq;
using ESafety.Account.Model.View;
using ESafety.Core.Model.DB.Platform;
using System;

namespace ESafety.Account.Service
{
	public  class  Doc_QualificationService:ServiceBase,IDoc_QualificationService
	{
		private IUnitwork _work = null;
        /// <summary>
        /// 词典
        /// </summary>
        private IRepository<Basic_Dict> _rpsDict = null;
        /// <summary>
        /// 资质
        /// </summary>
        private IRepository<Doc_Qualification> _docQual = null;
        /// <summary>
        /// 电子文档
        /// </summary>
        private IRepository<Bll_AttachFile> rpsFile = null;

        public Doc_QualificationService(IUnitwork work){

			_work = work;

            _rpsDict = _work.Repository<Basic_Dict>();

            _docQual = _work.Repository<Doc_Qualification>();

            rpsFile = _work.Repository<Bll_AttachFile>();

        }

        /// <summary>
        /// 获取资质数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult<Pager<DocQualView>> GetQualData(DocQualPara request)
        { 
            var rpsaccount = _work.Repository<AccountInfo>();
            List<Doc_Qualification> doc_s = _docQual.GetList(r => r.QTypeId == request.Id)
                .ToList();
            if (!string.IsNullOrWhiteSpace(request.Keyword))
                doc_s = doc_s.Where(r=>r.QName.Contains(request.Keyword)).ToList();
            var doc_s_Dto = from Item in doc_s
                            let Obj = rpsaccount.GetModel(Item.QPeopleId)
                            select new DocQualView()
                            {
                                Id = Item.Id,
                                QEndTime = Item.QEndTime,
                                QAudit = Item.QAudit,
                                QInstitutions = Item.QInstitutions,
                                QPeople = Obj.AccountName,
                                QTypeId = Item.QTypeId,
                                QName = Item.QName,
                                CreateTime = Item.CreateTime,
                                QPeopleId = Obj.ID,
                            };
            return new ActionResult<Pager<DocQualView>>(new Pager<DocQualView>()
                .GetCurrentPage(doc_s_Dto,request.PageSize,request.PageIndex));
        }

        /// <summary>
        /// 添加或者修改资质
        /// </summary>
        /// <param name="doc_"></param>
        /// <returns></returns>
        public ActionResult<bool> AddOrUpdateQual(Doc_Qualification doc_)
        {
            if (_rpsDict.GetModel(doc_.QTypeId) == null)
                throw new Exception("非法请求");
            Doc_Qualification IsQual = _docQual.GetModel(doc_.ID);
            if( IsQual != null)
            {
                _docQual.Update(r => r.ID == doc_.ID, (V) => new Doc_Qualification() {
                    QName = V.QName,
                    QEndTime = V.QEndTime,
                    QInstitutions = V.QInstitutions,
                    QTypeId = V.QTypeId,
                    QAudit = V.QAudit
                });
                return new ActionResult<bool>(true);
            }
            doc_.CreateTime = DateTime.Now;
            _docQual.Add(doc_);
            return new ActionResult<bool>(true);
        }

        /// <summary>
        /// 删除资质
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ActionResult<bool> DeleteQualById(Guid guid)
        {
            int state = _docQual.Delete(r => r.ID == guid);
            if (state == 0)
                throw new Exception("当前数据不存在");
            rpsFile.Delete(r => r.BusinessID == guid);
            return new ActionResult<bool>(true);
        }
	}
}



    