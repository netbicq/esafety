

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
using ESafety.Core.Model.DB;
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
        /// <summary>
        /// 制度
        /// </summary>
        private IRepository<Doc_Crew> _doccrew = null;
        /// <summary>
        /// 词典
        /// </summary>
        private IRepository<Basic_Dict> _rpsDict = null;
        public Doc_CrewService(IUnitwork work){
			_work = work;
            _doccrew = work.Repository<Doc_Crew>();
            _rpsDict = work.Repository<Basic_Dict>();
        }

        /// <summary>
        /// 获取制度数据
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<DocCrewView>> GetRegimeData(DocCrewPara para)
        {
            Basic_Dict dict = _rpsDict.GetModel(para.Id);
            if (dict == null)
                throw new Exception("当前节点未定义");
            List<Doc_Crew> crew_Data = _doccrew.GetList(r => r.CType == dict.ID)
                .ToList();
            if (!string.IsNullOrWhiteSpace(para.Keyword))
                crew_Data = crew_Data.Where(r => r.CName.Contains(para.Keyword)).ToList();
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
        /// 添加或修改制度数据
        /// </summary>
        /// <param name="doc_"></param>
        /// <returns></returns>
        public ActionResult<bool> AddOrUpdateDocCrew(Doc_Crew doc_)
        {
            Doc_Crew isDoc = _doccrew.GetModel(doc_.ID);
            if (doc_.CType == null)
                throw new Exception("制度Id不能为空");
            if (isDoc != null)
            {
                isDoc.CName = doc_.CName;
                isDoc.CType = doc_.CType;
                //isDoc.CreateTime = doc_.CreateTime
                isDoc.CContent = doc_.CContent;
                int state =  _doccrew.Update(r=>r.ID == doc_.ID,(V)=>new Doc_Crew {
                    CType = V.CType,
                    CFontSize = V.CFontSize,
                    CName = V.CName,
                    CContent = V.CContent,                    
                });
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            if (_doccrew.GetModel(r => r.CName == doc_.CName) != null)
                throw new Exception("当前记录已存在");
            doc_.CreateTime = DateTime.Now;
            _doccrew.Add(doc_);
            _work.Commit();
            return new ActionResult<bool>(true);
        }


    }
}



    