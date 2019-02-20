using ESafety.Core;
using ESafety.Core.Model;
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
    /// 组织架构 人员
    /// </summary>
    [RoutePrefix("api/orgemp")]
    public class OrgEmployeeController : ESFAPI
    {

        private IOrgEmployee bll = null;


        public OrgEmployeeController(IOrgEmployee orgemp)
        {

            bll = orgemp;
            BusinessService = orgemp;

        }
       
        /// <summary>
        /// 新建人员
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addemp")]
        public ActionResult<bool> AddEmployee(EmployeeNew employee)
        {
            LogContent = "新增人员，参数源：" + JsonConvert.SerializeObject(employee);
            return bll.AddEmployee(employee);
        }
        /// <summary>
        /// 新建组织
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addorg")]
        public ActionResult<bool> AddOrg(OrgNew org)
        {
            LogContent = "新建组织，参数源：" + JsonConvert.SerializeObject(org);
            return bll.AddOrg(org);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delemp/{id:Guid}")]
        public ActionResult<bool> DeleteEmployee(Guid id)
        {
            LogContent = "删除人员，id:" + id.ToString();
            return bll.DeleteEmployee(id);
        }
        /// <summary>
        /// 删除指定ID的组织
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delorg/{id:Guid}")]
        public ActionResult<bool> DeleteOrg(Guid id)
        {
            LogContent = "删除了组织，id:" + id.ToString();
            return bll.DeleteOrg(id);
        }
         
        public ActionResult<bool> EditEmployee(EmployeeEdit employee)
        {
            throw new NotImplementedException();
        }

        public ActionResult<bool> EditOrg(OrgEdit org)
        {
            throw new NotImplementedException();
        }

        public ActionResult<EmployeeView> GetEmployeeModel(Guid id)
        {
            throw new NotImplementedException();
        }

        public ActionResult<IEnumerable<EmployeeView>> GetEmployees(Guid orgid)
        {
            throw new NotImplementedException();
        }

        public ActionResult<IEnumerable<OrgView>> GetOrgChildren(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
