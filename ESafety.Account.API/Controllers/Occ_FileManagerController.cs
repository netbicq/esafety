using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.DB.Account;
using ESafety.Web.Unity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ESafety.Account.API.Controllers
{
    /// <summary>
    /// 档案管理
    /// </summary>
    [System.Web.Http.RoutePrefix("api/fileManager")]
    public class Occ_FileManagerController : ESFAPI
    {

        /// <summary>
        /// 风险公示业务接口类
        /// </summary>
        private IOcc_FileManagerService bll = null;
        public Occ_FileManagerController(IOcc_FileManagerService user)
        {
            bll = user;
            BusinessService = user;
        }

        /// <summary>
        /// 根据制度id获取风险公示数据
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost,Route("GetRegimeData")]
        public ActionResult<Pager<DocCrewView>> GetRegimeData(PagerQuery<Guid> para)
        {
            return bll.GetRegimeData(para);
        }

        /// <summary>
        /// 删除一条制度数据
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns></returns>
        [HttpGet, Route("DeleteDocCrewById/{ID:Guid}")]
        public ActionResult<bool> DeleteDocCrewById(Guid ID)
        {
            LogContent = "删除一条制度数据，数据源:" + ID.ToString();
            return bll.DeleteDocCrewById(ID);
        }

        /// <summary>
        /// 新增制度数据
        /// </summary>
        /// <param name="doc_"></param>
        /// <returns></returns>
        [HttpPost, Route("IncreaseCrew")]
        public ActionResult<bool> IncreaseCrew(Doc_Crew doc_)
        {
            LogContent = "新增制度数据,参数源:" + JsonConvert.SerializeObject(doc_);
            return bll.IncreaseCrew(doc_);
        }

        /// <summary>
        /// 修改制度数据
        /// </summary>
        /// <param name="amend"></param>
        /// <returns></returns>
        [HttpPost, Route("AmendCrew")]
        public ActionResult<bool> AmendCrew(AmendCrew amend)
        {
            LogContent = "修改制度数据,参数源:" + JsonConvert.SerializeObject(amend);
            return bll.AmendCrew(amend);
        }

        /// <summary>
        /// 获取资质数据[分页]
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost,Route("GetQualData")]
        public ActionResult<Pager<DocQualView>> GetQualData(PagerQuery<Guid> request)
        {
            return bll.GetQualData(request);
        }

        /// <summary>
        /// 删除资质
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet,Route("DeleteQualById/{ID:Guid}")]
        public ActionResult<bool> DeleteQualById(Guid ID)
        {
            LogContent = "删除资质,参数源:" + ID.ToString();
            return bll.DeleteQualById(ID);
        }

        /// <summary>
        /// 新增资质
        /// </summary>
        /// <param name="qual"></param>
        /// <returns></returns>
        [HttpPost,Route("IncreaseQual")]
        public ActionResult<bool> IncreaseQual(Doc_Qualification qual)
        {
            LogContent = "新增资质,参数源:" + JsonConvert.SerializeObject(qual);
            return bll.IncreaseQual(qual);
        }

        /// <summary>
        /// 修改资质
        /// </summary>
        /// <param name="amend"></param>
        /// <returns></returns>
        [HttpPost,Route("AmendQual")]
        public ActionResult<bool> AmendQual(AmendQual amend)
        {
            LogContent = "修改资质,参数源:" + JsonConvert.SerializeObject(amend);
            return bll.AmendQual(amend);
        }

        /// <summary>
        /// 获取培训数据 -- 分页
        /// </summary>
        /// <param name="docTran"></param>
        /// <returns></returns>
        [HttpPost,Route("GetTranData")]
        public ActionResult<Pager<DocTranView>> GetTranData(PagerQuery<Doc_Train> docTran)
        {
            return bll.GetTranData(docTran);
        }

        /// <summary>
        /// 根据培训id 获取培训人员
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet,Route("GetEmpData/{ID:Guid}")]
        public ActionResult<IEnumerable<Basic_Employee>> GetEmpData(Guid ID)
        {
            return bll.GetEmpData(ID);
        }

        /// <summary>
        /// 添加培训
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost,Route("IncreaseTran")]
        public ActionResult<bool> IncreaseTran(DocTranPara dto)
        {
            LogContent = "添加培训,参数源:" + JsonConvert.SerializeObject(dto);
            return bll.IncreaseTran(dto);
        }

        /// <summary>
        /// 修改培训
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost, Route("AmendTran")]
        public ActionResult<bool> AmendTran(DocTranPara dto)
        {
            LogContent = "修改培训,参数源:" + JsonConvert.SerializeObject(dto);
            return bll.AmendTran(dto);
        }

        /// <summary>
        /// 移除当前培训人员
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet, Route("DeleteDocTranpeopleById/{ID:Guid}")]
        public ActionResult<bool> DeleteDocTranpeopleById(Guid ID)
        {
            LogContent = "移除当前培训人员,参数源:" + ID.ToString();
            return bll.DeleteDocTranpeopleById(ID);
        }
        /// <summary>
        /// 添加培训人员
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>        
        [HttpPost, Route("IncreaseAccount")]
        public ActionResult<bool> IncreaseAccount(DocTranPara dto)
        {
            LogContent = "添加培训人员,参数源:" + JsonConvert.SerializeObject(dto);
            return bll.IncreaseAccount(dto);
        }

        /// <summary>
        /// 删除培训
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet, Route("DeleteDocTranById/{ID:Guid}")]
        public ActionResult<bool> DeleteDocTranById(Guid ID)
        {
            LogContent = "删除培训,参数源:" + ID.ToString();
            return bll.DeleteDocTranById(ID);
        }
        /// <summary>
        /// 分页获取应急预案
        /// </summary>
        /// <param name="_pager"></param>
        /// <returns></returns>
        [HttpPost, Route("GetEmeData")]
        public ActionResult<Pager<DocEmePlanView>> GetEmeData(PagerQuery<Doc_EmePlan> _pager)
        {
            return bll.GetEmeData(_pager);
        }

        /// <summary>
        /// 修改预案维护
        /// </summary>
        /// <param name="_body"></param>
        /// <returns></returns>
        [HttpPost, Route("AmendEmeplan")]
        public ActionResult<bool> AmendEmeplan(DocEmePlanPara _body)
        {
            LogContent = "修改预案维护,参数源:" + JsonConvert.SerializeObject(_body);
            return bll.AmendEmeplan(_body);
        }

        /// <summary>
        /// 删除预案维护
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>        
        [HttpGet, Route("DeleteEmeplanById/{ID:Guid}")]
        public ActionResult<bool> DeleteEmeplanById(Guid ID)
        {
            LogContent = "删除预案维护,参数源:" + ID.ToString();
            return bll.DeleteEmeplanById(ID);
        }

        /// <summary>
        /// 添加预案维护
        /// </summary>
        /// <param name="doc_"></param>
        /// <returns></returns>
        [HttpPost, Route("InscreaseEmeplan")]
        public ActionResult<bool> InscreaseEmeplan(Doc_EmePlan doc_)
        {
            LogContent = "添加预案维护,参数源:" + JsonConvert.SerializeObject(doc_);
            return bll.InscreaseEmeplan(doc_);
        }

        /// <summary>
        /// 根据会议id删会议
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet, Route("DeleteMeetById/{ID:Guid}")]
        public ActionResult<bool> DeleteMeetById(Guid ID)
        {
            LogContent = "根据会议id删会议,参数源:" + ID.ToString();
            return bll.DeleteMeetById(ID);
        }

        /// <summary>
        /// 添加单一的人员
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost, Route("IncreaseMeetByEme")]
        public ActionResult<bool> IncreaseMeetByEme(DocMeetPara2 dto)
        {
            LogContent = "添加单一的人员,参数源:" + JsonConvert.SerializeObject(dto);
            return bll.IncreaseMeetByEme(dto);
        }

        /// <summary>
        /// 删除一个参会人员
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet,Route("DelMeetByEme/{ID:Guid}")]
        public ActionResult<bool> DelMeetByEme(Guid ID)
        {
            LogContent = "删除一个参会人员,参数源:" + ID.ToString();
            return bll.DelMeetByEme(ID);
        }

        /// <summary>
        /// 获取参与会议人员
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet, Route("GetEmpAll/{ID:Guid}")]
        public ActionResult<IEnumerable<Basic_Employee>> GetEmpAll(Guid ID)
        {
            return bll.GetEmpAll(ID);
        }

        /// <summary>
        /// 添加会议
        /// </summary>
        /// <param name="doc_"></param>
        /// <returns></returns>
        [HttpPost, Route("InceaseMeet")]
        public ActionResult<bool> InceaseMeet(DocMeetPara doc_)
        {
            LogContent = "添加会议,参数源:" + JsonConvert.SerializeObject(doc_);
            return bll.InceaseMeet(doc_);
        }

        /// <summary>
        /// 修改会议
        /// </summary>
        /// <param name="doc_"></param>
        /// <returns></returns>
        [HttpPost, Route("AmendMeet")]
        public ActionResult<bool> AmendMeet(DocMeetPara1 doc_)
        {
            LogContent = "修改会议,参数源:" + JsonConvert.SerializeObject(doc_);
            return bll.AmendMeet(doc_);
        }

    }
}
