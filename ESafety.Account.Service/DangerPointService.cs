using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
using ESafety.Core.Model.DB.Account;
using ESafety.ORM;
using ESafety.Unity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ThoughtWorks.QRCode.Codec;

namespace ESafety.Account.Service
{
    public class DangerPointService : ServiceBase, IDangerPointService
    {
        private IUnitwork work=null;
        private IRepository<Basic_DangerPoint> rpsdp=null;
        private IRepository<Basic_DangerPointRelation> rpsdpr = null;
        public DangerPointService(IUnitwork _work)
        {
            work = _work;
            Unitwork = _work;
            rpsdp = work.Repository<Basic_DangerPoint>();
            rpsdpr = work.Repository<Basic_DangerPointRelation>();
        }
        /// <summary>
        /// 新建风险点
        /// </summary>
        /// <param name="pointNew"></param>
        /// <returns></returns>
        public ActionResult<bool> AddDangerPoint(DangerPointNew pointNew)
        {
            try
            {
                var check = rpsdp.Any(q=>q.Name==pointNew.Name);
                if (check)
                {
                    throw new Exception("该风险点已存在");
                }
                var dbdp = pointNew.MAPTO<Basic_DangerPoint>();
                dbdp.QRCoderUrl =CreateQRCoder(dbdp.ID);
                rpsdp.Add(dbdp);
                work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="pointID"></param>
        /// <returns></returns>
        private string CreateQRCoder(Guid pointID)
        {
           
            QRCodeEncoder endocder = new QRCodeEncoder();
            //二维码背景颜色
            endocder.QRCodeBackgroundColor = System.Drawing.Color.White;
            //二维码编码方式
            endocder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //每个小方格的宽度
            endocder.QRCodeScale = 10;
            //二维码版本号
            endocder.QRCodeVersion = 5;
            //纠错等级
            endocder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //将json串做成二维码
            Bitmap bitmap = endocder.Encode(JsonConvert.SerializeObject(pointID), System.Text.Encoding.UTF8);
            string strSaveDir = HttpContext.Current.Server.MapPath("/QRCoder/");
            if (!Directory.Exists(strSaveDir))
            {
                Directory.CreateDirectory(strSaveDir);
            }
            string strSavePath = Path.Combine(strSaveDir,pointID+ ".png");
            if (!System.IO.File.Exists(strSavePath))
            {
                bitmap.Save(strSavePath);
            }
            return "~/QRCoder/" + pointID + ".png";
          
        }


        /// <summary>
        /// 新建配置关系模型
        /// </summary>
        /// <param name="relationNew"></param>
        /// <returns></returns>
        public ActionResult<bool> AddDangerPointRelation(DangerPointRelationNew relationNew)
        {
            try
            {
                var check = rpsdp.Any(q => q.ID== relationNew.DangerPointID);
                if (check)
                {
                    throw new Exception("未找到该风险点");
                }
                if (relationNew.SubjectID == Guid.Empty || relationNew.SubjectType == 0)
                {
                    throw new Exception("主体与主体类型不能为空!");
                }

                var dbdpr = relationNew.MAPTO<Basic_DangerPointRelation>();
                rpsdpr.Add(dbdpr);
                work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 删除风险点
        /// </summary>
        /// <param name="pointID"></param>
        /// <returns></returns>
        public ActionResult<bool> DelDangerPoint(Guid pointID)
        {
            try
            {
                var dbdp = rpsdp.GetModel(pointID);
                if (dbdp == null)
                {
                    throw new Exception("未找到改风险点");
                }
                var check = work.Repository<Basic_DangerPointRelation>().Any(p=>p.DangerPointID==pointID);
                if (check)
                {
                    throw new Exception("该风险点已配置,无法删除!");
                }
                //删除二维码文件
                var filepath = HttpContext.Current.Server.MapPath(dbdp.QRCoderUrl);
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                rpsdp.Delete(dbdp);
                work.Commit();
                return new ActionResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 删除风险配置
        /// </summary>
        /// <param name="relationID"></param>
        /// <returns></returns>
        public ActionResult<bool> DelDangerPointRelation(Guid relationID)
        {
            try
            {
                var dbdpr = rpsdpr.GetModel(relationID);
                if (dbdpr == null)
                {
                    throw new Exception("未找到所需删除的配置模型!");
                }
                rpsdpr.Delete(dbdpr);
                work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 修改风险点
        /// </summary>
        /// <param name="pointEdit"></param>
        /// <returns></returns>
        public ActionResult<bool> EditDangerPoint(DangerPointEdit pointEdit)
        {
            try
            {
                var dbdp = rpsdp.GetModel(pointEdit.ID);
                if(dbdp==null)
                {
                    throw new Exception("未找到所需修改的风险点!");
                }
                var check = rpsdp.Any(p => p.Name == pointEdit.Name&&p.ID!=pointEdit.ID);
                if (check)
                {
                    throw new Exception("该风险点名已存在!");
                }
                dbdp =pointEdit.CopyTo<Basic_DangerPoint>(dbdp);
                rpsdp.Add(dbdp);
                work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 获取风险点模型
        /// </summary>
        /// <param name="pointID"></param>
        /// <returns></returns>
        public ActionResult<DangerPointView> GetDangerPointModel(Guid pointID)
        {
            try
            {
                var dbdp = rpsdp.GetModel(pointID);
                if (dbdp == null)
                {
                    throw new Exception("未找到该风险点模型!");
                }
                var re = dbdp.MAPTO<DangerPointView>();
                return new ActionResult<DangerPointView>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<DangerPointView>(ex);
            }
        }

        /// <summary>
        /// 分页获取风险点模型
        /// </summary>
        /// <param name="pointName"></param>
        /// <returns></returns>
        public ActionResult<Pager<DangerPointView>> GetDangerPointPages(PagerQuery<string> pointName)
        {
            try
            {
                var page = rpsdp.Queryable(p=>pointName.Query.Contains(p.Name)||pointName.Query==string.Empty);
                var retemp = page.MAPTO<DangerPointView>();
                var re = new Pager<DangerPointView>().GetCurrentPage(retemp,pointName.PageSize,pointName.PageIndex);
                return new ActionResult<Pager<DangerPointView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<DangerPointView>>(ex);
            }
        }
        /// <summary>
        /// 风险点配置关系
        /// </summary>
        /// <param name="pointID"></param>
        /// <returns></returns>
        public ActionResult<Pager<DangerPointRelationView>> GetDangerPointRelationPages(PagerQuery<Guid> pointID)
        {
            try
            {
                var page = rpsdpr.Queryable(p => p.DangerPointID==pointID.Query);
                var subids = page.Select(s => s.SubjectID);
                var devs = work.Repository<Basic_Facilities>().Queryable(q => subids.Contains(q.ID));
                var posts = work.Repository<Basic_Post>().Queryable(q => subids.Contains(q.ID));
                var opres = work.Repository<Basic_Opreation>().Queryable(q => subids.Contains(q.ID));

                var retemp = from pg in page
                             let dev = devs.FirstOrDefault(q => q.ID == pg.SubjectID)
                             let ppst = posts.FirstOrDefault(q => q.ID == pg.SubjectID)
                             let opr = opres.FirstOrDefault(q => q.ID == pg.SubjectID)
                             select new DangerPointRelationView
                             {
                                 ID=pg.ID,
                                 SubjectType=Command.GetItems(typeof(PublicEnum.EE_SubjectType)).FirstOrDefault(p=>p.Value==pg.SubjectType).Caption,
                                 SubjectName= dev != null ? dev.Name : ppst != null ? ppst.Name : opr != null ? opr.Name : default(string)
                             };
                var re = new Pager<DangerPointRelationView>().GetCurrentPage(retemp, pointID.PageSize, pointID.PageIndex);
                return new ActionResult<Pager<DangerPointRelationView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<DangerPointRelationView>>(ex);
            }
        }
        /// <summary>
        /// 主体选择器
        /// </summary>
        /// <param name="select"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<DangerPointRelationSelector>> GetDangerPointRelationSelector(DangerPointRelationSelect select)
        {
            try
            {
                var page = rpsdpr.Queryable(p => p.DangerPointID == select.DangerPointID&&p.SubjectType==select.SubjectType);
                var re= from pg in page
                             select new DangerPointRelationSelector
                             {
                                 SubjectID= pg.SubjectID,
                                 SubjectName =pg.SubjectName
                             };
 
                return new ActionResult<IEnumerable<DangerPointRelationSelector>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<DangerPointRelationSelector>>(ex);
            }
        }
        /// <summary>
        /// 风险点选择器
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<DangerPointSelector>> GetDangerPointSelector()
        {
            try
            {
                var page = rpsdp.Queryable();
                var re = from pg in page
                         select new DangerPointSelector
                         {
                             ID = pg.ID,
                             Name=pg.Name
                         }; 
                return new ActionResult<IEnumerable<DangerPointSelector>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<DangerPointSelector>>(ex);
            }
        }
        /// <summary>
        /// 批量获取二维码
        /// </summary>
        /// <param name="pointIds"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<QRCoder>> GetQRCoders(IEnumerable<Guid> pointIds)
        {
            try
            {
                var dbdps = rpsdp.Queryable(p=>pointIds.Contains(p.ID));
                var re = from code in dbdps
                         select new QRCoder
                         {
                             ID = code.ID,
                             Name = code.Name,
                             QRCoderUrl = code.QRCoderUrl
                         };
                return new ActionResult<IEnumerable<QRCoder>>(re);

            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<QRCoder>>(ex);
            }
        }
    }
}
