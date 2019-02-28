
//----------Doc_Crew开始----------



using System.Web.Http;
using ESafety.Web.Unity;
using ESafety.Account.IService;
using ESafety.Core.Model;
using System.Collections.Generic;
using ESafety.Core.Model.DB.Account;
using System;
using System.Data.SqlClient;
using ESafety.Account.Model.View;
using ESafety.Account.Model.PARA;
using Newtonsoft.Json;

namespace ESafety.Account.API.Controllers
{
	[System.Web.Http.RoutePrefix("api/doc_crew")]
    public class Doc_CrewController : ESFAPI
    {
        private IDoc_CrewService bll = null;
        public Doc_CrewController(IDoc_CrewService user)
        {
            bll = user;
            BusinessService = user;
        }

        [HttpGet,Route("GetRegimeData")]
        /// <summary>
        /// 获取制度数据
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<DocCrewView>> GetRegimeData([FromUri]DocCrewPara para)
        {
            try
            {
                return bll.GetRegimeData(para);
            }
            catch(Exception ex)
            {
                return new ActionResult<Pager<DocCrewView>>(ex);
            }
        }

        [HttpPost,Route("DeleteDocCrewById")]
        /// <summary>
        /// 删除一条制度数据
        /// </summary>
        /// <param name="guid">主键</param>
        /// <returns></returns>
        public ActionResult<bool> DeleteDocCrewById([FromUri]Guid guid)
        {
            try
            {
                return bll.DeleteDocCrewById(guid);
            }
            catch(Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        [HttpPost,Route("AddOrUpdateDocCrew")]
        /// <summary>
        /// 添加或修改制度数据
        /// </summary>
        /// <param name="doc_"></param>
        /// <returns></returns>
        public ActionResult<bool> AddOrUpdateDocCrew([FromBody]Doc_CrewPara doc_)
        {
            Doc_Crew crew = JsonConvert.DeserializeObject<Doc_Crew>(JsonConvert.SerializeObject(doc_));
            try
            {
                return bll.AddOrUpdateDocCrew(crew);
            }
            catch(Exception ex)
            {
                return new ActionResult<bool>(true);
            } 
        }
    }
}

//----------Doc_Crew结束----------

    