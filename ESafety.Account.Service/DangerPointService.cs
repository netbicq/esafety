using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core;
using ESafety.Core.Model;
using ESafety.Core.Model.DB.Account;
using ESafety.Core.Model.PARA;
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
        private IUnitwork work = null;
        private IRepository<Basic_DangerPoint> rpsdp = null;
        private IRepository<Basic_DangerPointRelation> rpsdpr = null;
        private IAttachFile srvFile = null;
        private ITree srvTree;
        public DangerPointService(IUnitwork _work, IAttachFile file, ITree orgTree)
        {
            work = _work;
            Unitwork = _work;
            rpsdp = work.Repository<Basic_DangerPoint>();
            rpsdpr = work.Repository<Basic_DangerPointRelation>();
            srvFile = file;
            srvTree = orgTree;
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
                var check = rpsdp.Any(q => q.Name == pointNew.Name);
                if (check)
                {
                    throw new Exception("该风险点已存在");
                }
                if (pointNew.WXYSDictIDs.Count() == 0)
                {
                    throw new Exception("请选择危险因素!");
                }
                if (pointNew.OrgID == Guid.Empty)
                {
                    throw new Exception("请选责任部门!");
                }
                if (pointNew.Principal == Guid.Empty)
                {
                    throw new Exception("请选责任人!");
                }
                var dbdp = pointNew.MAPTO<Basic_DangerPoint>();
                dbdp.WXYSJson = JsonConvert.SerializeObject(pointNew.WXYSDictIDs);
                dbdp.Code = Command.CreateCode();
                dbdp.QRCoderUrl = CreateQRCoder(dbdp.ID);
                //文件
                var files = new AttachFileSave
                {
                    BusinessID = dbdp.ID,
                    files = pointNew.fileNews
                };
                var file = srvFile.SaveFiles(files);
                if (file.state != 200)
                {
                    throw new Exception(file.msg);
                }
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
            //  Bitmap bitmap = endocder.Encode(JsonConvert.SerializeObject(pointID), System.Text.Encoding.UTF8);
            Bitmap bitmap = endocder.Encode(pointID.ToString(), System.Text.Encoding.UTF8);
            string strSaveDir = HttpContext.Current.Server.MapPath("/QRCoder/");
            if (!Directory.Exists(strSaveDir))
            {
                Directory.CreateDirectory(strSaveDir);
            }
            string strSavePath = Path.Combine(strSaveDir, pointID + ".png");
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
                var dbdp = rpsdp.GetModel(q => q.ID == relationNew.DangerPointID);
                if (dbdp == null)
                {
                    throw new Exception("未找到该风险点");
                }
                if (relationNew.SubjectID == Guid.Empty || relationNew.SubjectType == 0)
                {
                    throw new Exception("主体与主体类型不能为空!");
                }
                var check = rpsdpr.Any(p => p.SubjectID == relationNew.SubjectID && p.DangerPointID == relationNew.DangerPointID);
                if (check)
                {
                    throw new Exception("该风险点下已存在, 主体" + relationNew.SubjectName);
                }
                check = work.Repository<Basic_DangerRelation>().Any(p => p.SubjectID == relationNew.SubjectID);
                if (!check)
                {
                    throw new Exception("该主体下没有风控项!");
                }
                //所有风控项ID
                var dangerids = work.Repository<Basic_DangerRelation>().Queryable(p => p.SubjectID == relationNew.SubjectID).Select(s => s.DangerID);
                //所有风险等级ID
                var lvids = work.Repository<Basic_Danger>().Queryable(p => dangerids.Contains(p.ID)).Select(s => s.DangerLevel);
                //最大的风险等级具体项
                var lv = work.Repository<Core.Model.DB.Basic_Dict>().Queryable(p => lvids.Contains(p.ID)).OrderByDescending(o => o.MinValue).FirstOrDefault();

                var dplv = work.Repository<Core.Model.DB.Basic_Dict>().GetModel(p => p.ID == dbdp.DangerLevel);
                if (dplv.MinValue < lv.MinValue)
                {
                    //如果主体的风空项的风险等级大于预设的风险点的风险等级则更新预设的风险等级
                    dbdp.DangerLevel = lv.ID;
                    rpsdp.Update(dbdp);
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
                var check = work.Repository<Basic_DangerPointRelation>().Any(p => p.DangerPointID == pointID);
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
                //风险点图片
                var dImgPath = HttpContext.Current.Server.MapPath(dbdp.DangerPointImg);
                if (File.Exists(dImgPath))
                {
                    File.Delete(dImgPath);
                }
                //警示牌
                var wImgPath = HttpContext.Current.Server.MapPath(dbdp.WarningSign);
                if (File.Exists(wImgPath))
                {
                    File.Delete(wImgPath);
                }


                //文件
                var file = srvFile.DelFileByBusinessId(pointID);

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
                if (dbdp == null)
                {
                    throw new Exception("未找到所需修改的风险点!");
                }
                if (pointEdit.WXYSDictIDs.Count() == 0)
                {
                    throw new Exception("请选择危险因素!");
                }
                if (pointEdit.OrgID == Guid.Empty)
                {
                    throw new Exception("请选责任部门!");
                }
                if (pointEdit.Principal == Guid.Empty)
                {
                    throw new Exception("请选责任人!");
                }
                var check = rpsdp.Any(p => p.Name == pointEdit.Name && p.ID != pointEdit.ID);
                if (check)
                {
                    throw new Exception("该风险点名已存在!");
                }
                //风险点图片
                if (!string.Equals(pointEdit.DangerPointImg, dbdp.DangerPointImg))
                {
                    var dPointImg = HttpContext.Current.Server.MapPath(dbdp.DangerPointImg);
                    if (File.Exists(dPointImg))
                    {
                        File.Delete(dPointImg);
                    }
                }
                //警示图标
                if (!string.Equals(pointEdit.WarningSign, dbdp.WarningSign))
                {
                    var wPointImg = HttpContext.Current.Server.MapPath(dbdp.WarningSign);
                    if (File.Exists(wPointImg))
                    {
                        File.Delete(wPointImg);
                    }
                }
                dbdp = pointEdit.CopyTo<Basic_DangerPoint>(dbdp);
                dbdp.WXYSJson = JsonConvert.SerializeObject(pointEdit.WXYSDictIDs);
                //文件
                srvFile.DelFileByBusinessId(pointEdit.ID);
                var files = new AttachFileSave
                {
                    BusinessID = dbdp.ID,
                    files = pointEdit.fileNews
                };
                var file = srvFile.SaveFiles(files);
                if (file.state != 200)
                {
                    throw new Exception(file.msg);
                }
                rpsdp.Update(dbdp);
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
        public ActionResult<DangerPointModel> GetDangerPointModel(Guid pointID)
        {
            try
            {
                var dbdp = rpsdp.GetModel(pointID);
                if (dbdp == null)
                {
                    throw new Exception("未找到该风险点模型!");
                }
                var re = dbdp.MAPTO<DangerPointModel>();
                var user = work.Repository<Core.Model.DB.Basic_Employee>().GetModel(dbdp.Principal);
                re.OrgID = user.OrgID;
                re.WXYSIDs = JsonConvert.DeserializeObject<IEnumerable<Guid>>(dbdp.WXYSJson);
                return new ActionResult<DangerPointModel>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<DangerPointModel>(ex);
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
                var page = rpsdp.Queryable(p => pointName.Query.Contains(p.Name) || pointName.Query == string.Empty);
                //风险等级
                var lvids = page.Select(s => s.DangerLevel);
                var lvs = work.Repository<Core.Model.DB.Basic_Dict>().Queryable(p => lvids.Contains(p.ID));
                //负责人员
                var empids = page.Select(s => s.Principal);
                var emps = work.Repository<Core.Model.DB.Basic_Employee>().Queryable(p => empids.Contains(p.ID));

                var org = work.Repository<Core.Model.DB.Basic_Org>().Queryable();

                var retemp = from p in page
                             let lv = lvs.FirstOrDefault(q => q.ID == p.DangerLevel)
                             let emp = emps.FirstOrDefault(q => q.ID == p.Principal)
                             let porg = org.FirstOrDefault(p => p.ID == emp.OrgID)
                             orderby p.Code ascending
                             select new DangerPointView
                             {
                                 ID = p.ID,
                                 QRCoderUrl = p.QRCoderUrl,
                                 Name = p.Name,
                                 Memo = p.Memo,
                                 EmergencyMeasure = p.EmergencyMeasure,
                                 ControlMeasure = p.ControlMeasure,
                                 Code = p.Code,
                                 DangerLevelName = lv.DictName,
                                 PrincipalName = emp.CNName,
                                 WarningSign = p.WarningSign,
                                 DangerPointImg = p.DangerPointImg,
                                 Consequence = p.Consequence,
                                 OrgName = porg.OrgName
                             };
                var re = new Pager<DangerPointView>().GetCurrentPage(retemp, pointName.PageSize, pointName.PageIndex);
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
                var page = rpsdpr.Queryable(p => p.DangerPointID == pointID.Query);
                var retemp = from pg in page.ToList()
                             orderby pg.SubjectName descending
                             select new DangerPointRelationView
                             {
                                 ID = pg.ID,
                                 SubjectType = Command.GetItems(typeof(PublicEnum.EE_SubjectType)).FirstOrDefault(p => p.Value == pg.SubjectType).Caption,
                                 SubjectName = pg.SubjectName
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
                var page = rpsdpr.Queryable(p => p.DangerPointID == select.DangerPointID && p.SubjectType == select.SubjectType);
                var re = from pg in page
                         select new DangerPointRelationSelector
                         {
                             SubjectID = pg.SubjectID,
                             SubjectName = pg.SubjectName
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
                             Name = pg.Name
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
                var dbdps = rpsdp.Queryable(p => pointIds.Contains(p.ID));

                var WXYSs = work.Repository<Core.Model.DB.Basic_Dict>();

                var re = from code in dbdps.ToList()
                         let WXYSIds = JsonConvert.DeserializeObject<List<Guid>>(code.WXYSJson)
                         select new QRCoder
                         {
                             ID = code.ID,
                             Name = code.Name,
                             QRCoderUrl = code.QRCoderUrl,
                             Consequence = code.Consequence,
                             ControlMeasure = code.ControlMeasure,
                             DangerPointImg = code.DangerPointImg,
                             EmergencyMeasure = code.EmergencyMeasure,
                             WarningSign = code.WarningSign,
                             WXYSDicts =from w in WXYSs.Queryable(p=>WXYSIds.Contains(p.ID))
                                        select new WXYSSelector
                                        {
                                            ID=w.ID,
                                            WXYSDictName=w.DictName
                                        }

                         };
                return new ActionResult<IEnumerable<QRCoder>>(re);

            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<QRCoder>>(ex);
            }
        }
        /// <summary>
        /// 根据风险点ID获取危险因素
        /// </summary>
        /// <param name="pointID"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<WXYSSelector>> GetWXYSSelectorByDangerPointId(Guid pointID)
        {
            try
            {
                var dp = rpsdp.GetModel(pointID);
                if (dp == null)
                {
                    throw new Exception("未找到该风险点!");
                }
                List<Guid> WXYSIds = JsonConvert.DeserializeObject<List<Guid>>(dp.WXYSJson);
                var retemp = work.Repository<Core.Model.DB.Basic_Dict>().Queryable(p => WXYSIds.Contains(p.ID));
                var re = from wxys in retemp
                         select new WXYSSelector
                         {
                             ID = wxys.ID,
                             WXYSDictName = wxys.DictName
                         };
                return new ActionResult<IEnumerable<WXYSSelector>>(re);

            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<WXYSSelector>>(ex);
            }
        }

        /// <summary>
        /// APP 端获取风险等级
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<DangerLevel>> GetDangerLevels()
        {
            try
            {
                var user = AppUser.EmployeeInfo;
                (srvTree as TreeService).AppUser = AppUser;
                var orgIDs = srvTree.GetChildrenIds<Core.Model.DB.Basic_Org>(user.OrgID);
                var dangerPoints = rpsdp.Queryable(p => orgIDs.Contains(p.OrgID));
                var dicts = work.Repository<Core.Model.DB.Basic_Dict>().Queryable(p=>p.ParentID==OptionConst.DangerLevel);
                var re = from lv in dicts
                         let count = dangerPoints.Count(p => p.DangerLevel == lv.ID)
                         orderby lv.MinValue descending
                         select new DangerLevel
                         {
                             LevelID = lv.ID,
                             Count = count,
                             LevelName = lv.DictName
                         };
                return new ActionResult<IEnumerable<DangerLevel>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<DangerLevel>>(ex);
            }
        }
        /// <summary>
        /// APP 根据风险点ID 端获取风险点
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult<Pager<APPDangerPointView>> GetDangerPointsPage(PagerQuery<Guid> query)
        {
            try
            {
                var user = AppUser.EmployeeInfo;
                (srvTree as TreeService).AppUser = AppUser;
                var orgIDs = srvTree.GetChildrenIds<Core.Model.DB.Basic_Org>(user.OrgID);
                var dangerPoints = rpsdp.Queryable(p => orgIDs.Contains(p.OrgID)&&p.DangerLevel==query.Query);
                
                var dicts = work.Repository<Core.Model.DB.Basic_Dict>().Queryable(p => p.ParentID == OptionConst.DangerLevel);
                var empids = dangerPoints.Select(s => s.Principal).Distinct();
                var emps = work.Repository<Core.Model.DB.Basic_Employee>().Queryable(p => empids.Contains(p.ID));


                var retemp = from dp in dangerPoints
                             let lv = dicts.FirstOrDefault(p => p.ID == dp.DangerLevel)
                             let emp = emps.FirstOrDefault(p => p.ID == dp.Principal)
                             orderby lv.MinValue descending
                             select new APPDangerPointView
                             {
                                 DangerLevel ="风险等级:"+lv.DictName,
                                 DangerPoint ="风险点:"+dp.Name,
                                 Principal = "责任人:"+emp.CNName
                             };
                var re = new Pager<APPDangerPointView>().GetCurrentPage(retemp, query.PageSize, query.PageIndex);
                return new ActionResult<Pager<APPDangerPointView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<APPDangerPointView>>(ex);
            }
        }
    }
}
