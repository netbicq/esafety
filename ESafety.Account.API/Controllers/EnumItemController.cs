using ESafety.Core;
using ESafety.Core.Model;
using ESafety.ORM;
using ESafety.Unity;
using ESafety.Web.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ESafety.Account.API.Controllers
{
    /// <summary>
    /// 枚举项
    /// </summary>
    [RoutePrefix("api/enum")]
    public class EnumItemController : ESFAPI
    {
        private IDict bll = null;

        public EnumItemController(IDict dict)
        {
            bll = dict;
            BusinessServices =new List<object>() { dict };
        }
        /// <summary>
        /// 执行频率日期类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("cycledatetype")]
        public ActionResult<IEnumerable<EnumItem>> GetCycleDateType()
        {
            var re = Command.GetItems(typeof(PublicEnum.EE_CycleDateType));
            return new ActionResult<IEnumerable<EnumItem>>(re);
        }
        /// <summary>
        /// 隐患等级计算方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("evaluatemethod")]
        public ActionResult<IEnumerable<EnumItem>> GetEvaluateMethod()
        {
            var re = Command.GetItems(typeof(PublicEnum.EE_EvaluateMethod));
            return new ActionResult<IEnumerable<EnumItem>>(re);
        }
        /// <summary>
        /// 任务检查结果
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("taskresulttype")]
        public ActionResult<IEnumerable<EnumItem>> GetTaskResultType()
        {
            var re = Command.GetItems(typeof(PublicEnum.EE_TaskResultType));
            return new ActionResult<IEnumerable<EnumItem>>(re);
        }
        /// <summary>
        /// 审批节点类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("flowpointtype")]
        public ActionResult<IEnumerable<EnumItem>> GetFlowPointType()
        {
            var re = Command.GetItems(typeof(PublicEnum.EE_FlowPointType));
            return new ActionResult<IEnumerable<EnumItem>>(re);
        }
        /// <summary>
        /// 审批业务类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("businesstype")]
        public ActionResult<IEnumerable<EnumItem>> GetBusinessType()
        {
            var re = Command.GetItems(typeof(PublicEnum.EE_BusinessType));
            return new ActionResult<IEnumerable<EnumItem>>(re);
        }

        /// <summary>
        /// 巡查主体类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("subjecttype")]
        public ActionResult<IEnumerable<EnumItem>> GetSubjectType()
        {
            var re = Command.GetItems(typeof(PublicEnum.EE_SubjectType));
            return new ActionResult<IEnumerable<EnumItem>>(re);
        }

        /// <summary>
        /// 用户自定义数据类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("userdefineddatatype")]
        public ActionResult<IEnumerable<EnumItem>> GetUserDefinedDataType()
        {
            var re = Command.GetItems(typeof(PublicEnum.EE_UserDefinedDataType));
            return new ActionResult<IEnumerable<EnumItem>>(re);
        }
        /// <summary>
        /// 账套参数数据类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("accountoptionitemtype")]
        public ActionResult<IEnumerable<EnumItem>> GetAccountOptionItemType()
        {
            var re = Command.GetItems(typeof(PublicEnum.AccountOptionItemType));
            return new ActionResult<IEnumerable<EnumItem>>(re);
        }
        /// <summary>
        /// 获取隐患等级
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("gettroublelevel")]
        public ActionResult<IEnumerable<EnumItem>> GetTroubleLevel()
        {
            var re = Command.GetItems(typeof(PublicEnum.EE_TroubleLevel));
            return new ActionResult<IEnumerable<EnumItem>>(re);
        }
        /// <summary>
        /// 获取作业申请单的节点处理类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getopreateflowresult")]
        public ActionResult<IEnumerable<EnumItem>> GetOpreateFlowResult()
        {
            var re = Command.GetItems(typeof(PublicEnum.OpreateFlowResult));
            return new ActionResult<IEnumerable<EnumItem>>(re);

        }

        /// <summary>
        ///  获取审批业务单据状态
        /// </summary>
        [HttpGet]
        [Route("getbillflowstate")]
        public ActionResult<IEnumerable<EnumItem>> GetBillFlowState()
        {
            var re = Command.GetItems(typeof(PublicEnum.BillFlowState));
            return new ActionResult<IEnumerable<EnumItem>>(re);
        }


    }
}
