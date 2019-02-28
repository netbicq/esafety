

/*---------------------------------------------------------------- 
// 版权所有。  
// 
// 文件名： Doc_MeetingService
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
using ESafety.Account.Model.PARA;
using System;
using ESafety.Core.Model.DB;
using ESafety.Account.Model.View;
using System.Linq;
using System.Collections.Generic;

namespace ESafety.Account.Service
{
	public  class  Doc_MeetingService:ServiceBase,IDoc_MeetingService
	{
		private IUnitwork _work = null;

        private IRepository<Doc_Meeting> _imeet = null;

        private IRepository<Basic_Employee> _iemp = null;


        private IRepository<Bll_AttachFile> _rpsFile = null;

        private IRepository<Doc_MeetPeople> _imeetPeople = null;

        public Doc_MeetingService(IUnitwork work){
			_work = work;
            _imeet = _work.Repository<Doc_Meeting>();
            _iemp = _work.Repository<Basic_Employee>();
            _rpsFile = _work.Repository<Bll_AttachFile>();

        }




        /// <summary>
        /// 获取参与会议人员
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ActionResult<List<Basic_Employee>> GetEmpAll(Guid guid)
        {
            return new ActionResult<List<Basic_Employee>>((from item in _imeetPeople.GetList(r => r.MMId == guid)
                                                           select _iemp.GetModel(r => r.ID == item.MPId)).ToList());
        }


        /// <summary>
        /// 根据会议id删会议
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ActionResult<bool> DeleteMeetById(Guid guid)
        {
            ///删除会议
            int state = _imeet.Delete(r => r.Id == guid);
            if (state == 0)
                throw new Exception("当前会议不存在");
            ///删除参会人员 主持人员 
            _imeetPeople.Delete(r => r.MMId == guid);
            //删除电子邮件
            _rpsFile.Delete(r => r.BusinessID == guid);

            //提交
            _work.Commit();
            return new ActionResult<bool>(true);
        }


        /// <summary>
        /// 分页获取会议
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public ActionResult<Pager<DocMeetView>> GetMeetData(DocMeetPara doc)
        {
            var data = _imeet.GetList().ToList();
            var linq = from Item in data
                       let Obj = _imeetPeople.GetModel(Item.Id)
                       let Obj1 = _imeetPeople.GetModel(r => r.MMId == Item.Id && r.MState == 1)
                       select new DocMeetView()
                       {
                           Id = Item.Id,
                           CreateTime = Item.CreateTime,
                           HostId = Obj1 == null ? Guid.Empty : Obj1.MPId,
                           HostName = Obj1 == null ? "" : _iemp.GetModel(r => r.ID == Obj1.MPId).CNName,
                           MContent = Item.MContent,
                           MTheme = Item.MTheme,
                           MTime = Item.MTime,
                       };
            if (!string.IsNullOrWhiteSpace(doc.Keyword))
            {
                
                return new ActionResult<Pager<DocMeetView>>(new Pager<DocMeetView>().GetCurrentPage(linq.Where(r=>r.MTheme.Contains(doc.Keyword)), doc.PageSize, doc.PageIndex));
            }
            else
            {
                return new ActionResult<Pager<DocMeetView>>(new Pager<DocMeetView>().GetCurrentPage(linq, doc.PageSize, doc.PageIndex));
            }
        }
        




        /// <summary>
        /// 添加或修改会议
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public ActionResult<bool> AddOrUpdateMeet(DocMeetPara doc)
        {
            if(!(doc.meet.Id == null || doc.meet.Id == Guid.Empty))
            {
                if (_imeet.GetModel(doc.meet.Id) == null)
                    throw new Exception("当前会议不存在");
                _imeet.Update(r => r.Id == doc.meet.Id, (V) => new Doc_Meeting() {
                    MTheme = doc.meet.MTheme,
                    MTime = doc.meet.MTime,
                    MContent = doc.meet.MContent
                });
                //删除所有会员和参与人员关联记录
                _imeetPeople.Delete(r => r.MMId == doc.meet.Id);
                //删除所以电子附件
                _rpsFile.Delete(r => r.BusinessID == doc.meet.Id);
                ///添加主持和参与人员
                _imeetPeople.Add(doc.meet_data);
                //添加电子附件
                _rpsFile.Add(doc.meet_file);

                _work.Commit();
                return new ActionResult<bool>(true);
            }



            if (_imeet.GetModel(r => r.MTheme == doc.meet.MTheme) != null)
                throw new Exception("已存在当前会议");
            ///添加会议
            _imeet.Add(doc.meet);
            ///添加主持和参与人员
            _imeetPeople.Add(doc.meet_data);
            //添加电子附件
            _rpsFile.Add(doc.meet_file);

            _work.Commit();
            return new ActionResult<bool>(true);

        }

	}
}



    