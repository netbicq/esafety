

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
using ESafety.Core;
using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.DB.Account;
using ESafety.Core.Model.DB.Platform;
using ESafety.ORM;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESafety.Account.Service
{
    /// <summary>
    /// 档案管理
    /// </summary>
	public  class Occ_FileManagerService : ServiceBase,IOcc_FileManagerService
	{
		private IUnitwork _work = null;
        /// <summary>
        /// 制度
        /// </summary>
        private IRepository<Doc_Crew> _doccrew = null;
        /// <summary>
        /// 词典
        /// </summary>
        private IRepository<Basic_Dict> _rpsDict = null;
        /// <summary>
        /// 资质
        /// </summary>
        private IRepository<Doc_Qualification> _docQual = null;

        /// <summary>
        /// 培训
        /// </summary>
        private IRepository<Doc_Train> docTrain = null;
        /// <summary>
        /// 培训 && 人员
        /// </summary>
        private IRepository<Doc_TrainPeople> _idocTrain = null;

        /// <summary>
        /// 电子文档service
        /// </summary>
        private IAttachFile attach =null;

        public Occ_FileManagerService(IUnitwork work, IAttachFile _attach)
        {
			_work = work;
            attach = _attach;
            _doccrew = _work.Repository<Doc_Crew>();
            _rpsDict = _work.Repository<Basic_Dict>();
            docTrain = _work.Repository<Doc_Train>();
            _idocTrain = _work.Repository<Doc_TrainPeople>();
            _docQual = _work.Repository<Doc_Qualification>();

        }

        #region "风险公示" && “资质管理”
        /// <summary>
        /// 获取制度数据
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<DocCrewView>> GetRegimeData(PagerQuery<Guid> para)
        {
            Basic_Dict dict = _rpsDict.GetModel(para.Query);
            if (dict == null)
                throw new Exception("当前节点未定义");
            List<Doc_Crew> crew_Data = _doccrew.GetList(r => r.CType == dict.ID)
                .ToList();
            if (!string.IsNullOrWhiteSpace(para.KeyWord))
                crew_Data = crew_Data.Where(r => r.CName.Contains(para.KeyWord)).ToList();
            var crew_Data_Dto = from Item in crew_Data
                                              select new DocCrewView()
                                              {
                                                  Id = Item.ID,
                                                  CName = Item.CName,
                                                  CContent = Item.CContent,
                                                  CFontSize = Item.CFontSize,
                                                  CreateTime = Item.CreateTime,
                                                  CType = Item.CType,
                                                  CType_Name = dict.DictName
                                              };
            return new ActionResult<Pager<DocCrewView>>(new Pager<DocCrewView>()
                .GetCurrentPage(crew_Data_Dto,para.PageSize,para.PageIndex));
        }

        /// <summary>
        /// 删除制度数据
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ActionResult<bool> DeleteDocCrewById(Guid guid)
        {
            int state = _doccrew.Delete(r => r.ID == guid);
            if (state == 0)
                throw new Exception("数据未定义");
            return new ActionResult<bool>(true);
        }

        /// <summary>
        /// 新增制度数据
        /// </summary>
        /// <param name="doc_"></param>
        /// <returns></returns>
        public ActionResult<bool> IncreaseCrew(Doc_Crew doc_)
        {
            Doc_Crew crew = _doccrew.GetModel(r=>r.CName == doc_.CName);
            if (crew == null)
                throw new Exception("当前数据已存在");
            _doccrew.Add(crew);
            _work.Commit();
            return new ActionResult<bool>(true);
        }

        /// <summary>
        /// 修改制度数据
        /// </summary>
        /// <param name="amend"></param>
        /// <returns></returns>
        public ActionResult<bool> AmendCrew(AmendCrew amend)
        {
            Doc_Crew one = _doccrew.GetModel(r=>r.ID == amend.ID);
            if (one == null)
                throw new Exception("未找到此数据");
            Doc_Crew func = amend.CopyTo(one);
            _doccrew.Update(func);
            _work.Commit();
            return new ActionResult<bool>(true);
        }

        /// <summary>
        /// 获取资质数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult<Pager<DocQualView>> GetQualData(PagerQuery<Guid> request)
        {
            var rpsaccount = _work.Repository<AccountInfo>();
            List<Doc_Qualification> doc_s = _docQual.GetList(r => r.QTypeId == request.Query)
                .ToList();
            if (!string.IsNullOrWhiteSpace(request.KeyWord))
                doc_s = doc_s.Where(r => r.QName.Contains(request.KeyWord)).ToList();
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
                .GetCurrentPage(doc_s_Dto, request.PageSize, request.PageIndex));
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
            attach.GetFiles(guid).data.ToList().ForEach(r=>attach.DelFile(r.ID)); 
            _work.Commit();
            return new ActionResult<bool>(true);
        }

        /// <summary>
        /// 新增资质
        /// </summary>
        /// <param name="qual"></param>
        /// <returns></returns>
        public ActionResult<bool> IncreaseQual(Doc_Qualification qual)
        {
            Doc_Qualification _qual = _docQual.GetModel(r=>r.QName == qual.QName);
            if (_qual != null)
                throw new Exception("当前数据已存在");
            _docQual.Add(qual);
            _work.Commit();
            return new ActionResult<bool>(true);
        }

        /// <summary>
        /// 修改资质
        /// </summary>
        /// <param name="amend"></param>
        /// <returns></returns>
        public ActionResult<bool> AmendQual(AmendQual amend)
        {
            Doc_Qualification doc_ = _docQual.GetModel(r => r.ID == amend.ID);
            if (doc_ == null)
                throw new Exception("当前数据不存在");
            Doc_Qualification func_ = amend.CopyTo(doc_);
            _docQual.Update(func_);
            _work.Commit();
            return new ActionResult<bool>(true);
        }
        #endregion



    }
}



    