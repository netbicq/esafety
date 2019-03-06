﻿using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core;
using ESafety.Core.Model;
using ESafety.Core.Model.DB.Account;
using ESafety.Core.Model.PARA;
using ESafety.ORM;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Service
{
    /// <summary>
    /// 安全会议
    /// </summary>
    public class DocMeetingService : ServiceBase, IDocMeetingService
    {
        private IUnitwork _work = null;
        private IRepository<Doc_Meeting> _rpsdm = null;
        private IAttachFile srvFile = null;

        public DocMeetingService(IUnitwork work, IAttachFile file)
        {
            _work = work;
            Unitwork = work;
            _rpsdm = work.Repository<Doc_Meeting>();
            srvFile = file;

        }
        /// <summary>
        /// 新建安全会议模型
        /// </summary>
        /// <param name="meetingNew"></param>
        /// <returns></returns>
        public ActionResult<bool> AddDocMeeting(DocMeetingNew meetingNew)
        {
            try
            {
                var check = _rpsdm.Any(p => p.Motif == meetingNew.Motif);
                if (check)
                {
                    throw new Exception("该会议主题已存在！");
                }
                var dbdm = meetingNew.MAPTO<Doc_Meeting>();

                //电子文档
                var files = new AttachFileSave
                {
                    BusinessID = dbdm.ID,
                    files = from f in meetingNew.AttachFiles
                            select new AttachFileNew
                            {
                                FileTitle = f.FileTitle,
                                FileType = f.FileType,
                                FileUrl = f.FileUrl
                            }
                };

                var fileresult = srvFile.SaveFiles(files);
                if (fileresult.state != 200)
                {
                    throw new Exception(fileresult.msg);
                }

                _rpsdm.Add(dbdm);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 删除安全会议模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelDocMeeting(Guid id)
        {
            try
            {
                var dbdm = _rpsdm.GetModel(id);
                if (dbdm == null)
                {
                    throw new Exception("未找到所要删除的会议！");
                }
                _rpsdm.Delete(dbdm);
                //删除电子文档
                srvFile.DelFileByBusinessId(id);

                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 修改安全会议模型
        /// </summary>
        /// <param name="meetingEdit"></param>
        /// <returns></returns>
        public ActionResult<bool> EditDocMeeting(DocMeetingEdit meetingEdit)
        {
            try
            {
                var dbdm = _rpsdm.GetModel(meetingEdit.ID);
                if (dbdm == null)
                {
                    throw new Exception("未找到所要修改的会议！");
                }
                var check = _rpsdm.Any(p => p.ID != meetingEdit.ID && p.Motif == meetingEdit.Motif);
                if (check)
                {
                    throw new Exception("该会议主题已存在！");
                }
                dbdm = meetingEdit.CopyTo<Doc_Meeting>(dbdm);
                //电子文档 
                srvFile.DelFileByBusinessId(dbdm.ID);
                var files = new AttachFileSave
                {
                    BusinessID = dbdm.ID,
                    files = from f in meetingEdit.AttachFiles
                            select f.CopyTo<AttachFileNew>(f)
                };

                srvFile.SaveFiles(files);

                _rpsdm.Update(dbdm);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 获取安全会议模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<DocMeetingView> GetDocMeeting(Guid id)
        {
            try
            {
                var dbdm = _rpsdm.GetModel(id);
                var re = dbdm.MAPTO<DocMeetingView>();
                return new ActionResult<DocMeetingView>(re);
            }
            catch (Exception ex)
            {

                return new ActionResult<DocMeetingView>(ex);

            }
        }
        /// <summary>
        /// 分页获取安全会议模型
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<DocMeetingView>> GetDocMeetings(PagerQuery<DocMeetingQuery> para)
        {
            try
            {
                var dbdms = _rpsdm.Queryable(p => p.Motif.Contains(para.Query.Motif) || string.IsNullOrEmpty(para.Query.Motif));
                var redms = from s in dbdms
                            select new DocMeetingView
                            {
                                ID = s.ID,
                                Content = s.Content,
                                Site = s.Site,
                                EmployeeS = s.EmployeeS,
                                MeetingDate = s.MeetingDate,
                                MeetingMaster = s.MeetingMaster,
                                Motif = s.Motif
                            };
                var re = new Pager<DocMeetingView>().GetCurrentPage(redms, para.PageSize, para.PageIndex);
                return new ActionResult<Pager<DocMeetingView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<DocMeetingView>>(ex);
            }
        }
    }
}
