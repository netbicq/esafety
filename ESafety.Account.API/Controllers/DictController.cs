using ESafety.Core;
using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.PARA;
using ESafety.Core.Model.View;
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
    /// 词典管理
    /// </summary>
    [RoutePrefix("api/dict")]
    public class DictController : ESFAPI
    {
        private IDict bll = null;

        public DictController(IDict dict)
        {
            bll = dict;
            BusinessServices = new List<object>() { dict };

        }
        /// <summary>
        /// 新建词典
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("adddict")]
        public ActionResult<bool> AddDict(DictNew dict)
        {
            try
            {
                LogContent = "新建词典，参数源：" + JsonConvert.SerializeObject(dict);
                return bll.AddDict(dict);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 新建词典类型
        /// </summary>
        /// <param name="dicttype"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("adddicttype")]
        public ActionResult<bool> AddDictType(DictTypeNew dicttype)
        {
            LogContent = "新建词典类型，参数源：" + JsonConvert.SerializeObject(dicttype);
            return bll.AddDictType(dicttype);

        }

        /// <summary>
        /// 删除指定id的词典
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("deldict/{id:Guid}")]
        public ActionResult<bool> DelDict(Guid id)
        {
            LogContent = "删除词典，id：" + id.ToString();
            return bll.DelDict(id);

        }
        /// <summary>
        /// 删除指定id的词典类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("deldicttype/{id:Guid}")]
        public ActionResult<bool> DelDictType(Guid id)
        {
            LogContent = "删除词典类型，id:" + id.ToString();
            return bll.DelDictType(id);
        }

        /// <summary>
        /// 修改词典
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editdict")]
        public ActionResult<bool> EditDict(DictEidt dict)
        {
            LogContent = "修改诩典，参数源：" + JsonConvert.SerializeObject(dict);
            return bll.EditDict(dict);
        }
        /// <summary>
        ///获取风险等级词典
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getdangerdict")]
        public ActionResult<IEnumerable<Basic_Dict>> GetDangerLevel()
        {
            return bll.GetDangerLevel();
        }

        /// <summary>
        /// 获取指定id的词典模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getdictmodel/{id:Guid}")]
        public ActionResult<Basic_Dict> GetDictModel(Guid id)
        {
            return bll.GetDictModel(id);

        }
        /// <summary>
        /// 获取诩典列表，支持分页
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getdicts")]
        public ActionResult<Pager<Basic_Dict>> GetDictsByType(PagerQuery<Guid> para)
        {
            return bll.GetDictsByType(para);
        }
        /// <summary>
        /// 获取所有词典类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getdicttypes")]
        public ActionResult<IEnumerable<DictTypeView>> GetDictTypes()
        {
            return bll.GetDictTypes();
        }

        /// <summary>
        /// 获取资质词典
        /// </summary>
        /// <returns></returns>
        [Route("getdoclicense")]
        [HttpGet]
        public ActionResult<IEnumerable<Basic_Dict>> GetDocLicense()
        {
            return bll.GetDocLicense();
        }

        /// <summary>
        /// 获取制度类型词典
        /// </summary>
        /// <returns></returns>
        [Route("getdocregime")]
        [HttpGet]
        public ActionResult<IEnumerable<Basic_Dict>> GetDocRegime()
        {
            return bll.GetDocRegime();
        }
        /// <summary>
        /// 获取应急预案词典
        /// </summary>
        /// <returns></returns>
        [Route("getdocslution")]
        [HttpGet]
        public ActionResult<IEnumerable<Basic_Dict>> GetDocSlution()
        {
            return bll.GetDocSlution();
        }
        /// <summary>
        /// 获取事故后果词典
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getevealsyhg")]
        public ActionResult<IEnumerable<Basic_Dict>> GetEval_SGJG()
        {
            return bll.GetEval_SGJG();
        }
        /// <summary>
        ///获取事故类型词典
        /// </summary>
        /// <returns></returns>
        [Route("getevealsglx")]
        [HttpGet]
        public ActionResult<IEnumerable<Basic_Dict>> GetEval_SGLX()
        {
            return bll.GetEval_SGLX();
        }
        /// <summary>
        /// 危害因素
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getevealwhys")]
        public ActionResult<IEnumerable<Basic_Dict>> GetEval_WHYS()
        {
            return bll.GetEval_WHYS();
        }
        /// <summary>
        /// 获取影响范围词典
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getevealyxfw")]
        public ActionResult<IEnumerable<Basic_Dict>> GetEval_YXFW()
        {
            return bll.GetEval_YXFW();
        }

        /// <summary>
        /// 获取LECD_L词典
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getEval_LECD_L")]
        public ActionResult<IEnumerable<Basic_Dict>> GetEval_LECD_L()
        {
            return bll.GetEval_LECD_L();
        }
        /// <summary>
        /// 获取LECD_E词典
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getEval_LECD_E")]
        public ActionResult<IEnumerable<Basic_Dict>> GetEval_LECD_E()
        {
            return bll.GetEval_LECD_E();
        }
        /// <summary>
        /// 获取LECD_C词典
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getEval_LECD_C")]
        public ActionResult<IEnumerable<Basic_Dict>> GetEval_LECD_C()
        {
            return bll.GetEval_LECD_C();
        }
        /// <summary>
        /// 获取LSD_L词典
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getEval_LSD_L")]
        public ActionResult<IEnumerable<Basic_Dict>> GetEval_LSD_L()
        {
            return bll.GetEval_LSD_L();
        }
        /// <summary>
        /// 获取LSD_S词典
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getEval_LSD_S")]
        public ActionResult<IEnumerable<Basic_Dict>> GetEval_LSD_S()
        {
            return bll.GetEval_LSD_S();
        }
        /// <summary>
        /// 获取隐患等级
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getTroubleLevel")]
        public ActionResult<IEnumerable<Basic_Dict>> GetTroubleLevel()
        {
            return bll.GetTroubleLevel();
        }
    }
}
