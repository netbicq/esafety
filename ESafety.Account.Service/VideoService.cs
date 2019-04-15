using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
using ESafety.Core.Model.DB.Account;
using ESafety.ORM;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Service
{
    public class VideoService : ServiceBase, IVideoService
    {
        private IUnitwork _work = null;
        private IRepository<Basic_Vedio> _rpsv = null;
        private IRepository<Basic_VedioSubject> _rpsvs = null;

        public VideoService(IUnitwork work)
        {
            _work = work;
            Unitwork = work;
            _rpsv = work.Repository<Basic_Vedio>();
            _rpsvs = work.Repository<Basic_VedioSubject>();
        }
        /// <summary>
        /// 新建视频监控
        /// </summary>
        /// <param name="videoNew"></param>
        /// <returns></returns>
        public ActionResult<bool> AddVideo(VideoNew videoNew)
        {
            try
            {
                if (videoNew == null)
                {
                    throw new Exception("参数类型错误");
                }
                var check = _rpsv.Any(p=>p.Code==videoNew.Code);
                if (check)
                {
                    throw new Exception("该摄像头已存在");
                }
                var dbv = videoNew.MAPTO<Basic_Vedio>();
                var lsvs = (from vs in videoNew.Subjects
                            select new Basic_VedioSubject
                            {
                                ID = Guid.NewGuid(),
                                SubjectID = vs.SubjectID,
                                SubjectType = vs.SubjectType,
                                VedioID = dbv.ID
                            }).ToList();
                _rpsv.Add(dbv);
                _rpsvs.Add(lsvs);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 删除摄像头信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelVideo(Guid id)
        {
            try
            {
                var dbv = _rpsv.GetModel(id);
                if (dbv == null)
                {
                    throw new Exception("未找到所需删除的摄像头信息");
                }
                _rpsv.Delete(dbv);
                _rpsvs.Delete(p => p.VedioID == id);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {

                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 修改摄像头信息
        /// </summary>
        /// <param name="videoEdit"></param>
        /// <returns></returns>
        public ActionResult<bool> EditVideo(VideoEdit videoEdit)
        {
            try
            {
                var dbv = _rpsv.GetModel(videoEdit.ID);
                if (dbv == null)
                {
                    throw new Exception("未找到所需修改的摄像头信息");
                }
                var check= _rpsv.Any(p =>p.ID!=videoEdit.ID && p.Code == videoEdit.Code);
                if (check)
                {
                    throw new Exception("该摄像头信息已存在");
                }
                dbv = videoEdit.CopyTo<Basic_Vedio>(dbv);

                _rpsvs.Delete(p => p.VedioID == videoEdit.ID);
                var lsvs = (from vs in videoEdit.Subjects
                            select new Basic_VedioSubject
                            {
                                ID = Guid.NewGuid(),
                                SubjectID = vs.SubjectID,
                                SubjectType = vs.SubjectType,
                                VedioID = dbv.ID
                            }).ToList();
                _rpsv.Update(dbv);
                _rpsvs.Add(lsvs);
                _work.Commit();
                return new ActionResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 获取摄像头的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<VideoModelView> GetVideo(Guid id)
        {
            try
            {
                var dbv = _rpsv.GetModel(id);
                if (dbv == null)
                {
                    throw new Exception("摄像头未找到");
                }
                var rev = dbv.MAPTO<VideoModelView>();

                var subs = _rpsvs.Queryable(q => q.VedioID== id).ToList();
                var subids = subs.Select(s => s.SubjectID);

                var devs = _work.Repository<Basic_Facilities>().Queryable(q => subids.Contains(q.ID)).ToList();
                var posts = _work.Repository<Basic_Post>().Queryable(q => subids.Contains(q.ID)).ToList();
                var opres = _work.Repository<Basic_Opreation>().Queryable(q => subids.Contains(q.ID)).ToList();

                rev.Subjects = from s in subs
                                   let dev = devs.FirstOrDefault(q => q.ID == s.SubjectID)
                                   let ppst = posts.FirstOrDefault(q => q.ID == s.SubjectID)
                                   let opr = opres.FirstOrDefault(q => q.ID == s.SubjectID)
                                   select new VideoSubjectView
                                   {
                                       ID = s.ID,
                                       VedioID = s.VedioID,
                                       SubjectID = s.SubjectID,
                                       SubjectType = s.SubjectType,
                                       SubjectTypeName = Command.GetItems(typeof(PublicEnum.EE_SubjectType)).FirstOrDefault(q => q.Value == s.SubjectType).Caption,
                                       SubjectName =
                                          dev != null ? dev.Name : ppst != null ? ppst.Name : opr != null ? opr.Name : default(string)
                                   };
                return new ActionResult<VideoModelView>(rev);
            }
            catch (Exception ex)
            {
                return new ActionResult<VideoModelView>(ex);
            }
        }
        /// <summary>
        /// 移动端获取摄像头信息
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<VideoView>> GetVideoList()
        {
            try
            {
                var rev = _rpsv.Queryable().ToList();
                var re = rev.MAPTO<VideoView>();
                return new ActionResult<IEnumerable<VideoView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<VideoView>>(ex);
            }
        }

        /// <summary>
        /// 分页获取摄像头信息
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<VideoView>> GetVideos(PagerQuery<VideoQuery> para)
        {
            try
            {
                var rev = _rpsv.Queryable(q => q.Code.Contains(para.Query.Key) || q.Site.Contains(para.Query.Key) || para.Query.Key == "" ).ToList();
                var re = rev.MAPTO<VideoView>();
                var result = new Pager<VideoView>().GetCurrentPage(re, para.PageSize, para.PageIndex);
                return new ActionResult<Pager<VideoView>>(result);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<VideoView>>(ex);
            }
        }
        /// <summary>
        /// 根据摄像头ID，获取监控主体明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<VideoSubjectView>> GetVideoSubjects(Guid id)
        {
            try
            {
                var subs = _rpsvs.Queryable(q => q.VedioID == id).ToList();
                var subids = subs.Select(s => s.SubjectID);

                var devs = _work.Repository<Basic_Facilities>().Queryable(q => subids.Contains(q.ID)).ToList();
                var posts = _work.Repository<Basic_Post>().Queryable(q => subids.Contains(q.ID)).ToList();
                var opres = _work.Repository<Basic_Opreation>().Queryable(q => subids.Contains(q.ID)).ToList();

                var re = from s in subs
                         let subinfo =
                         (PublicEnum.EE_SubjectType)s.SubjectType == PublicEnum.EE_SubjectType.Device ? devs.FirstOrDefault(q => q.ID == s.SubjectID).Name :
                         (PublicEnum.EE_SubjectType)s.SubjectType == PublicEnum.EE_SubjectType.Opreate ? opres.FirstOrDefault(q => q.ID == s.SubjectID).Name :
                         (PublicEnum.EE_SubjectType)s.SubjectType == PublicEnum.EE_SubjectType.Post ? posts.FirstOrDefault(q => q.ID == s.SubjectID).Name : ""
                         select new VideoSubjectView
                         {
                             ID = s.ID,
                             VedioID = s.VedioID,
                             SubjectID = s.SubjectID,
                             SubjectType = s.SubjectType,
                             SubjectTypeName = Command.GetItems(typeof(PublicEnum.EE_SubjectType)).FirstOrDefault(q => q.Value == s.SubjectType).Caption,
                             SubjectName =subinfo
                         };
                return new ActionResult<IEnumerable<VideoSubjectView>>(re);
            }
            catch (Exception ex)
            {

                return new ActionResult<IEnumerable<VideoSubjectView>>(ex);
            }
          
        }
    }
}
