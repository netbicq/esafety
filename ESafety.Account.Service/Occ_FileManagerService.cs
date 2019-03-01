

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
using ESafety.Core.Model.View;
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
        private IDict _rpsDict = null;
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

        /// <summary>
        /// 用户
        /// </summary>
        private IOrgEmployee rpsaccount = null;

        /// <summary>
        /// 应急预案
        /// </summary>
        private IRepository<Doc_EmePlan> _rpseme = null;

        /// <summary>
        /// 安全会议
        /// </summary>
        private IRepository<Doc_Meeting> _imeet = null;

        /// <summary>
        /// 会议 && 人员
        /// </summary>
        private IRepository<Doc_MeetPeople> _imeetPeople = null;

        public Occ_FileManagerService(IUnitwork work, IAttachFile _attach,IDict dict, IOrgEmployee i)
        {
			_work = work;
            attach = _attach;
            _doccrew = _work.Repository<Doc_Crew>();
            _rpsDict = dict;
            docTrain = _work.Repository<Doc_Train>();
            _idocTrain = _work.Repository<Doc_TrainPeople>();
            _docQual = _work.Repository<Doc_Qualification>();
            rpsaccount = i;
            _rpseme = _work.Repository<Doc_EmePlan>();
        }

        #region "风险公示" && “资质管理”
        /// <summary>
        /// 根据制度id获取风险公示数据
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<DocCrewView>> GetRegimeData(PagerQuery<Guid> para)
        {
            try
            {
                Basic_Dict dict = _rpsDict.GetDictModel(para.Query).data;
                if (dict == null)
                    throw new Exception("当前节点未定义");
                var crew_Data = _doccrew.GetList(r => r.CType == dict.ID);
                if (!string.IsNullOrWhiteSpace(para.KeyWord))
                    crew_Data = crew_Data.Where(r => r.CName.Contains(para.KeyWord));
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
                var data = new Pager<DocCrewView>()
                    .GetCurrentPage(crew_Data_Dto, para.PageSize, para.PageIndex);
                return new ActionResult<Pager<DocCrewView>>(data);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<DocCrewView>>(ex);
            }
        }

        /// <summary>
        /// 根据id删除风险公示数据
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ActionResult<bool> DeleteDocCrewById(Guid guid)
        {
            try
            {
                int state = _doccrew.Delete(r => r.ID == guid);
                if (state == 0)
                    throw new Exception("数据未定义");
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 新增风险公示数据
        /// </summary>
        /// <param name="doc_"></param>
        /// <returns></returns>
        public ActionResult<bool> IncreaseCrew(Doc_Crew doc_)
        {
            try
            {
                Doc_Crew crew = _doccrew.GetModel(r => r.CName == doc_.CName);
                if (crew != null)
                    throw new Exception("当前数据已存在");
                doc_.CreateTime = DateTime.Now;
                _doccrew.Add(doc_);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
}

        /// <summary>
        /// 修改风险公示数据
        /// </summary>
        /// <param name="amend"></param>
        /// <returns></returns>
        public ActionResult<bool> AmendCrew(AmendCrew amend)
        {
            try
            {
                Doc_Crew one = _doccrew.GetModel(r => r.ID == amend.ID);
                if (one == null)
                    throw new Exception("未找到此数据");
                Doc_Crew func = amend.CopyTo(one);
                _doccrew.Update(func);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 获取资质数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult<Pager<DocQualView>> GetQualData(PagerQuery<Guid> request)
        {
            try
            {
                var doc_s = _docQual.GetList(r => r.QTypeId == request.Query);
                if (!string.IsNullOrWhiteSpace(request.KeyWord))
                {
                    doc_s = doc_s.Where(r => r.QName.Contains(request.KeyWord));
                }
                var doc_s_Dto = from Item in doc_s
                                let Obj = rpsaccount.GetEmployeeModel(Item.QPeopleId)
                                select new DocQualView()
                                {
                                    Id = Item.ID,
                                    QEndTime = Item.QEndTime,
                                    QAudit = Item.QAudit,
                                    QInstitutions = Item.QInstitutions,
                                    QPeople = Obj.data.CNName,
                                    QTypeId = Item.QTypeId,
                                    QName = Item.QName,
                                    CreateTime = Item.CreateTime,
                                    QPeopleId = Obj.data.ID,
                                };
                var data = new Pager<DocQualView>()
                    .GetCurrentPage(doc_s_Dto, request.PageSize, request.PageIndex);
                return new ActionResult<Pager<DocQualView>>(data);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<DocQualView>>(ex);
            }
        }

        /// <summary>
        /// 删除资质
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ActionResult<bool> DeleteQualById(Guid guid)
        {
            try
            {
                int state = _docQual.Delete(r => r.ID == guid);
                if (state == 0)
                    throw new Exception("当前数据不存在");
                attach.DelFileByBusinessId(guid);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 新增资质
        /// </summary>
        /// <param name="qual"></param>
        /// <returns></returns>
        public ActionResult<bool> IncreaseQual(Doc_Qualification qual)
        {
            try
            {
                Doc_Qualification _qual = _docQual.GetModel(r => r.QName == qual.QName);
                if (_qual != null)
                    throw new Exception("当前数据已存在");
                _docQual.Add(qual);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
}

        /// <summary>
        /// 修改资质
        /// </summary>
        /// <param name="amend"></param>
        /// <returns></returns>
        public ActionResult<bool> AmendQual(AmendQual amend)
        {
            try
            {
                Doc_Qualification doc_ = _docQual.GetModel(r => r.ID == amend.ID);
                if (doc_ == null)
                    throw new Exception("当前数据不存在");
                Doc_Qualification func_ = amend.CopyTo(doc_);
                _docQual.Update(func_);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        #endregion

        #region "培训"
        /// <summary>
        /// 获取培训数据
        /// </summary>
        /// <param name="docTran"></param>
        /// <returns></returns>
        public ActionResult<Pager<DocTranView>> GetTranData(PagerQuery<Doc_Train> docTran)
        {
            try
            {
                //获取人员 &&培训 数据
                var doc_s = docTrain.GetList();
                if (!string.IsNullOrWhiteSpace(docTran.KeyWord))
                    doc_s = doc_s.Where(r => r.TTheme.Contains(docTran.KeyWord));
                var views = from Item in doc_s
                            let Peo = _idocTrain.GetModel(r => r.TTid == Item.ID)
                            select new DocTranView()
                            {
                                ID = Item.ID,
                                TContent = Item.TContent,
                                TEndTime = Item.TEndTime,
                                Trainer = rpsaccount.GetEmployeeModel(Peo.TPId).data.CNName,
                                TrainerId = Peo.TPId,
                                TTheme = Item.TTheme,
                                TTime = Item.TTime,
                            };
                var data = new Pager<DocTranView>()
                    .GetCurrentPage(views, docTran.PageSize, docTran.PageIndex);
                return new ActionResult<Pager<DocTranView>>(data);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<DocTranView>>(ex);
            }
        }

        /// <summary>
        /// 根据培训id 获取培训人员
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<EmployeeModelView>> GetEmpData(Guid guid)
        {
            try
            {
                var _trains = _idocTrain.GetList(r => r.TTid == guid);
                var data = from Item in _trains
                            let Obj = rpsaccount.GetEmployeeModel(Item.TPId).data
                            select Obj;
                return new ActionResult<IEnumerable<EmployeeModelView>>(data);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<EmployeeModelView>>(ex);
            }
        }


        /// <summary>
        /// 添加培训
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ActionResult<bool> IncreaseTran(DocTranPara dto)
        {
            try
            {
                Doc_Train _Train = docTrain.GetModel(r => r.TTheme == dto.TTheme);
                if (_Train == null)
                    throw new Exception("当前数据已存在");
                docTrain.Add(dto.CopyTo(_Train));
                List<Doc_TrainPeople> tps = new List<Doc_TrainPeople>();
                dto.AIds.Split(',').ToList().ForEach(r =>
                {
                    tps.Add(new Doc_TrainPeople()
                    {
                        CreateTime = DateTime.Now,
                        IsDeal = 1,
                        TPId = Guid.Parse(r),
                        TTid = _Train.ID,
                    });
                });
                _idocTrain.Add(tps);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 修改培训
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ActionResult<bool> AmendTran(DocTranPara dto)
        {
            try
            {
                Guid ID = Guid.Empty;
                bool IsTry = Guid.TryParse(dto.AIds, out ID);
                if (!IsTry)
                    throw new Exception("非法传参");
                Doc_Train _Train = docTrain.GetModel(r => r.TTheme == dto.TTheme);
                if (_Train == null)
                    throw new Exception("重复的主题");
                docTrain.Update(r => r.ID == ID, (V) => dto.CopyTo(new Doc_Train()));
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 移除当前培训人员
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ActionResult<bool> DeleteDocTranpeopleById(Guid guid)
        {
            try
            {
                int state = _idocTrain.Delete(r => r.ID == guid);
                if (state == 0)
                    throw new Exception("没有当前数据");
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 添加培训人员
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ActionResult<bool> IncreaseAccount(DocTranPara dto)
        {
            try
            {
                Doc_TrainPeople _Train = _idocTrain.GetModel(r => r.TPId == Guid.Parse(dto.AIds));
                if (_Train != null)
                    throw new Exception("当前参培人员已存在");
                _Train = new Doc_TrainPeople();
                _Train = dto.CopyTo(_Train);
                _idocTrain.Add(_Train);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }


        /// <summary>
        /// 删除培训
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ActionResult<bool> DeleteDocTranById(Guid guid)
        {
            try
            {
                int state = docTrain.Delete(r => r.ID == guid);
                if (state == 0)
                    throw new Exception("当前数据不存在");
                _idocTrain.Delete(r => r.TPId == guid);
                attach.DelFileByBusinessId(guid);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        #endregion

        #region "应急预案"

        /// <summary>
        /// 分页获取应急预案
        /// </summary>
        /// <param name="_pager"></param>
        /// <returns></returns>
        public ActionResult<Pager<DocEmePlanView>> GetEmeData(PagerQuery<Doc_EmePlan> _pager)
        {
            try
            {
                var data = _rpseme.GetList(r => r.ETypeId1 == _pager.Query.ID);
                //风险等级
                if (!(_pager.Query.ELvId == Guid.Empty || _pager.Query.ELvId == null))
                    data.Where(r => r.ELvId == _pager.Query.ELvId);
                //模糊查询
                if (!string.IsNullOrWhiteSpace(_pager.KeyWord))
                    data.Where(r => r.EName.Contains(_pager.KeyWord));
                var views = from Item in data
                            let Zd = _rpsDict.GetDictModel(Item.ETypeId)
                            let Fx = _rpsDict.GetDictModel(Item.ELvId)
                            let Dto = Item.CopyTo(new DocEmePlanView()
                            {
                                EveName = Fx.data.DictName,
                                ETypeName = Zd.data.DictName
                            })
                            select Dto;
                var page = new Pager<DocEmePlanView>().GetCurrentPage(views, _pager.PageSize, _pager.PageIndex);
                return new ActionResult<Pager<DocEmePlanView>>(page);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<DocEmePlanView>>(ex);
            }
        }


        /// <summary>
        /// 修改预案维护
        /// </summary>
        /// <param name="_body"></param>
        /// <returns></returns>
        public ActionResult<bool> AmendEmeplan(DocEmePlanPara _body)
        {
            try
            {
                Doc_EmePlan one = _rpseme.GetModel(r => r.ID == _body.ID);
                if (one == null)
                    throw new Exception("未定义的数据");
                var copy = _body.CopyTo(one);
                _rpseme.Update(copy);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 删除预案维护
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ActionResult<bool> DeleteEmeplanById(Guid guid)
        {
            try
            {
                Doc_EmePlan one = _rpseme.GetModel(r => r.ID == guid);
                if (one == null)
                    throw new Exception("未定义的数据");
                _rpseme.Delete(r => r.ID == guid);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }


        /// <summary>
        /// 添加预案维护
        /// </summary>
        /// <param name="doc_"></param>
        /// <returns></returns>
        public ActionResult<bool> InscreaseEmeplan(Doc_EmePlan doc_)
        {
            try
            {
                Doc_EmePlan one = _rpseme.GetModel(r => r.EName == doc_.EName);
                if (one != null)
                    throw new Exception("当前数据已存在");
                _rpseme.Add(doc_);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }


        #endregion

        #region "安全会议"

        /// <summary>
        /// 分页获取会议
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public ActionResult<Pager<DocMeetView>> GetMeetData(PagerQuery<Guid> doc)
        {
            try
            {
                var data = _imeet.GetList();
                var linq = from Item in data
                           let Obj = _imeetPeople.GetModel(Item.ID)
                           let Obj1 = _imeetPeople.GetModel(r => r.MMId == Item.ID && r.MState == 1)
                           select new DocMeetView()
                           {
                               Id = Item.ID,
                               CreateTime = Item.CreateTime,
                               HostId = Obj1 == null ? Guid.Empty : Obj1.MPId,
                               HostName = Obj1 == null ? "" : rpsaccount.GetEmployeeModel(Obj1.MPId).data.CNName,
                               MContent = Item.MContent,
                               MTheme = Item.MTheme,
                               MTime = Item.MTime,
                           };
                if (!string.IsNullOrWhiteSpace(doc.KeyWord))
                {
                    linq = linq.Where(r => r.MTheme.Contains(doc.KeyWord));
                }

                var page = new Pager<DocMeetView>().GetCurrentPage(linq, doc.PageSize, doc.PageIndex);
                return new ActionResult<Pager<DocMeetView>>(page);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<DocMeetView>>(ex);
            }
        }



        /// <summary>
        /// 根据会议id删会议
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ActionResult<bool> DeleteMeetById(Guid guid)
        {
            try
            {
                ///删除会议
                int state = _imeet.Delete(r => r.ID == guid);
                if (state == 0)
                    throw new Exception("当前会议不存在");
                ///删除参会人员 主持人员 
                _imeetPeople.Delete(r => r.MMId == guid);
                //删除电子邮件
                attach.DelFileByBusinessId(guid);
                //提交
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 添加单一的人员
        /// </summary>
        /// <param name="d2"></param>
        /// <returns></returns>
        public ActionResult<bool> IncreaseMeetByEme(DocMeetPara2 d2)
        {
            try
            {
                EmployeeModelView emp = rpsaccount.GetEmployeeModel(d2.MPId).data;
                if (emp != null && d2.MState==1)
                {
                    var ip = _imeetPeople.GetModel(emp.ID);
                    ip.MPId = d2.MPId;
                    _imeetPeople.Update(ip);
                    _work.Commit();
                    return new ActionResult<bool>(true);
                }
                Doc_MeetPeople p = d2.CopyTo(new Doc_MeetPeople());
                _imeetPeople.Add(p);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }


        /// <summary>
        /// 删除一个参会人员
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult<bool> DelMeetByEme(Guid ID)
        {
            try
            {
                int state = _imeetPeople.Delete(R => R.ID == ID && R.MState == 0);
                if (state == 0)
                    throw new Exception("非法操作");
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }


        /// <summary>
        /// 获取参与会议人员
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<EmployeeModelView>> GetEmpAll(Guid guid)
        {
            try
            {
                var data = from item in _imeetPeople.GetList(r => r.MMId == guid)
                           select rpsaccount.GetEmployeeModel(item.MPId).data;
                return new ActionResult<IEnumerable<EmployeeModelView>>(data);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<EmployeeModelView>>(ex);
            }
        }



        /// <summary>
        /// 添加会议
        /// </summary>
        /// <param name="doc_"></param>
        /// <returns></returns>
        public ActionResult<bool> InceaseMeet(DocMeetPara doc_)
        {
            try
            {
                Doc_Meeting one = _imeet.GetModel(r => r.MTheme == doc_.meet.MTheme);
                if (one != null)
                    throw new Exception("已存在的数据");
                doc_.meet.CreateTime = DateTime.Now;
                Doc_Meeting s1 = _imeet.Add(doc_.meet);

                doc_.meet_data.ForEach(r => { r.MMId = s1.ID; });
                _imeetPeople.Add(doc_.meet_data);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 修改会议
        /// </summary>
        /// <param name="doc_"></param>
        /// <returns></returns>
        public ActionResult<bool> AmendMeet(DocMeetPara1 doc_)
        {
            try
            {
                Doc_Meeting one = _imeet.GetModel(r => r.ID == doc_.ID);
                if (one == null)
                    throw new Exception("未定义的数据");
                one = _imeet.GetModel(r => r.MTheme == doc_.MTheme);
                if (one != null)
                    throw new Exception("已存在的数据");
                _imeet.Update(doc_.CopyTo(one));
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }



            #endregion
        }
}



    