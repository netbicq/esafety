
//----------Occ_FileHealth开始----------



using System.Web.Http;
using ESafety.Web.Unity;
using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Core.Model;
using ESafety.Account.Model.View;
using System;
using ESafety.Core.Model.DB.Account;
using Newtonsoft.Json;

namespace ESafety.Account.API.Controllers
{
    /// <summary>
    /// 职业健康
    /// </summary>
	[RoutePrefix("api/occ_filehealth")]
    public class Occ_FileHealthController : ESFAPI
    {
        private IOcc_FileHealthService bll = null;
        public Occ_FileHealthController(IOcc_FileHealthService user)
        {

            bll = user;
            BusinessService = user;

        }

        /// <summary>
        /// 添加健康档案
        /// </summary>
        /// <param name="aOcc"></param>
        /// <returns></returns>
        [HttpPost,Route("InceaseHealth")]
        public ActionResult<bool> InceaseHealth(AOccFileHealthPara aOcc)
        {
            LogContent = "添加健康档案,参数源:" + JsonConvert.SerializeObject(aOcc);
            return bll.InceaseHealth(aOcc);
        }

        /// <summary>
        /// 修改健康档案
        /// </summary>
        /// <param name="aOcc"></param>
        /// <returns></returns>
        [HttpPost, Route("AmendHealth")]
        public ActionResult<bool> AmendHealth(AOccFileHealthPara aOcc)
        {
            LogContent = "修改健康档案,参数源:" + JsonConvert.SerializeObject(aOcc);
            return bll.AmendHealth(aOcc);
        }

        /// <summary>
        /// 分页获取健康档案
        /// </summary>
        /// <param name="occFile"></param>
        /// <returns></returns>
        [HttpPost, Route("GetHealthData")]
        public ActionResult<Pager<OccFileHealthView>> GetHealthData(PagerQuery<OccFileHealthPara> occFile)
        {
            return bll.GetHealthData(occFile);
        }

        /// <summary>
        /// 根据id删除健康档案
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet,Route("DeleteFileHealthById/{ID:Guid}")]
        public ActionResult<bool> DeleteFileHealthById(Guid ID)
        {
            LogContent = "根据id删除健康档案,参数源:" + ID.ToString();
            return bll.DeleteFileHealthById(ID);
        }

        /// <summary>
        /// 分页获取体检数据
        /// </summary>
        /// <param name="occFile"></param>
        /// <returns></returns>
        [HttpPost, Route("GetMedicalData")]
        public ActionResult<Pager<OccMedicalView>> GetMedicalData(PagerQuery<OccMedicalPara> occFile)
        {
            return bll.GetMedicalData(occFile);
        }

        /// <summary>
        /// 根据id删除体检数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet, Route("DeleteMedicalById/{ID:Guid}")]
        public ActionResult<bool> DeleteMedicalById(Guid ID)
        {
            LogContent = "根据id删除体检数据,参数源:" + ID.ToString();
            return bll.DeleteMedicalById(ID);
        }

        /// <summary>
        /// 添加体检报告
        /// </summary>
        /// <param name="occMedical"></param>
        /// <returns></returns>
        [HttpPost, Route("IncreaseMedical")]
        public ActionResult<bool> IncreaseMedical(Occ_Medical occMedical)
        {
            LogContent = "添加体检报告,参数源:" + JsonConvert.SerializeObject(occMedical);
            return bll.IncreaseMedical(occMedical);
        }

        /// <summary>
        /// 修改体检报告
        /// </summary>
        /// <param name="occMedical"></param>
        /// <returns></returns>
        [HttpPost, Route("AmendMedical")]
        public ActionResult<bool> AmendMedical(AOccMedicalPara occMedical)
        {
            LogContent = "修改体检报告,参数源:" + JsonConvert.SerializeObject(occMedical);
            return bll.AmendMedical(occMedical);
        }

    }
}

//----------Occ_FileHealth结束----------

    