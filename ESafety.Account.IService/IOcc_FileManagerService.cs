

using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.DB.Account;
using System;
using System.Collections.Generic;

namespace ESafety.Account.IService
{
    /// <summary>
    /// 档案管理[25]
    /// </summary>
	public interface IOcc_FileManagerService
	{
        /// <summary>
        /// 根据制度id获取风险公示数据
        /// </summary>
        /// <param name="pagerQuery"></param>
        /// <returns></returns>
        ActionResult<Pager<DocCrewView>> GetRegimeData(PagerQuery<Guid> pagerQuery);

        /// <summary>
        /// 根据id删除风险公示数据
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        ActionResult<bool> DeleteDocCrewById(Guid guid);

        /// <summary>
        /// 新增风险公示数据
        /// </summary>
        /// <param name="doc_"></param>
        /// <returns></returns>
        ActionResult<bool> IncreaseCrew(Doc_Crew doc_);

        /// <summary>
        /// 修改风险公示数据
        /// </summary>
        /// <param name="amend"></param>
        /// <returns></returns>
        ActionResult<bool> AmendCrew(AmendCrew amend);

        /// <summary>
        /// 获取资质数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ActionResult<Pager<DocQualView>> GetQualData(PagerQuery<Guid> request);

        /// <summary>
        /// 删除资质
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        ActionResult<bool> DeleteQualById(Guid guid);

        /// <summary>
        /// 新增资质
        /// </summary>
        /// <param name="qual"></param>
        /// <returns></returns>
        ActionResult<bool> IncreaseQual(Doc_Qualification qual);

        /// <summary>
        /// 修改资质
        /// </summary>
        /// <param name="amend"></param>
        /// <returns></returns>
        ActionResult<bool> AmendQual(AmendQual amend);




        /// <summary>
        /// 获取培训数据
        /// </summary>
        /// <param name="docTran"></param>
        /// <returns></returns>
        ActionResult<Pager<DocTranView>> GetTranData(PagerQuery<Doc_Train> docTran);

        /// <summary>
        /// 根据培训id 获取培训人员
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<Basic_Employee>> GetEmpData(Guid guid);

        /// <summary>
        /// 添加培训
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ActionResult<bool> IncreaseTran(DocTranPara dto);

        /// <summary>
        /// 修改培训
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ActionResult<bool> AmendTran(DocTranPara dto);

        /// <summary>
        /// 移除当前培训人员
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        ActionResult<bool> DeleteDocTranpeopleById(Guid guid);

        /// <summary>
        /// 添加培训人员
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ActionResult<bool> IncreaseAccount(DocTranPara dto);

        /// <summary>
        /// 删除培训
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        ActionResult<bool> DeleteDocTranById(Guid guid);

        /// <summary>
        /// 分页获取应急预案
        /// </summary>
        /// <param name="_pager"></param>
        /// <returns></returns>
        ActionResult<Pager<DocEmePlanView>> GetEmeData(PagerQuery<Doc_EmePlan> _pager);

        /// <summary>
        /// 修改预案维护
        /// </summary>
        /// <param name="_body"></param>
        /// <returns></returns>
        ActionResult<bool> AmendEmeplan(DocEmePlanPara _body);

        /// <summary>
        /// 删除预案维护
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        ActionResult<bool> DeleteEmeplanById(Guid guid);

        /// <summary>
        /// 添加预案维护
        /// </summary>
        /// <param name="doc_"></param>
        /// <returns></returns>
        ActionResult<bool> InscreaseEmeplan(Doc_EmePlan doc_);

        /// <summary>
        /// 分页获取会议
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        ActionResult<Pager<DocMeetView>> GetMeetData(PagerQuery<Guid> doc);

        /// <summary>
        /// 根据会议id删会议
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        ActionResult<bool> DeleteMeetById(Guid guid);

        /// <summary>
        /// 添加单一的人员
        /// </summary>
        /// <param name="d2"></param>
        /// <returns></returns>
        ActionResult<bool> IncreaseMeetByEme(DocMeetPara2 d2);

        /// <summary>
        /// 删除一个参会人员
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        ActionResult<bool> DelMeetByEme(Guid ID);

        /// <summary>
        /// 获取参与会议人员
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<Basic_Employee>> GetEmpAll(Guid guid);

        /// <summary>
        /// 添加会议
        /// </summary>
        /// <param name="doc_"></param>
        /// <returns></returns>
        ActionResult<bool> InceaseMeet(DocMeetPara doc_);

        /// <summary>
        /// 修改会议
        /// </summary>
        /// <param name="doc_"></param>
        /// <returns></returns>
        ActionResult<bool> AmendMeet(DocMeetPara1 doc_);
    }
	
}


    