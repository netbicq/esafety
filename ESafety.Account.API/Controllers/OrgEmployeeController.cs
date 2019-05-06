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
    /// 组织架构 人员
    /// </summary>
    [RoutePrefix("api/orgemp")]
    public class OrgEmployeeController : ESFAPI
    {

        private IOrgEmployee bll = null;
        
        public OrgEmployeeController(IOrgEmployee orgemp)
        {

            bll = orgemp;
            BusinessServices =new List<object>() { orgemp };

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
        /// 获取组织架构的树形结构
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("gettree/{id:Guid}")]
        [HttpGet]
        public ActionResult<IEnumerable<OrgTree>> GetOrgTree(Guid id)
        {
            return bll.GetTree(id);
        }
        /// <summary>
        /// 获取指定id的的所有父级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getparents/{id:Guid}")]
        [HttpGet]
        public ActionResult<IEnumerable<Basic_Org>> GetParents(Guid id)
        {
            return bll.GetParents(id);
        }
        /// <summary>
        /// 获取指定ID的所有子级ID强集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getchildrenids/{id:Guid}")]
        [HttpGet]
        public ActionResult<IEnumerable<Guid>> GetChildrenIds(Guid id)
        {
            return bll.GetChildren(id);
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
        /// 删除人员
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
        /// <summary>
        /// 修改人员信息
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editemp")]
        public ActionResult<bool> EditEmployee(EmployeeEdit employee)
        {
            LogContent="修改了人员信息，参数源:"+ JsonConvert.SerializeObject(employee);
            return bll.EditEmployee(employee);
        }
        /// <summary>
        /// 修改组织
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editorg")]
        public ActionResult<bool> EditOrg(OrgEdit org)
        {
            LogContent = "修改了组织，参数源:" + JsonConvert.SerializeObject(org);
            return bll.EditOrg(org);
        }
        /// <summary>
        /// 根据组织ID获取人员模型列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getemplist/{id:Guid}")]
        public ActionResult<IEnumerable<EmployeeView>> GetEmployeelist(Guid id)
        {
            return bll.GetEmployeelist(id);
        }

        /// <summary>
        /// 获取职员模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getempmodel/{id:Guid}")]
        [HttpGet]
        public ActionResult<EmployeeModelView> GetEmployeeModel(Guid id)
        {
            return bll.GetEmployeeModel(id);
        }
        /// <summary>
        /// 根据组织ID，分页获取人员信息
        /// </summary>
        /// <param name="orgid"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getemps")]
        public ActionResult<Pager<EmployeeView>> GetEmployees(PagerQuery<EmployeeQuery> para)
        {
            return bll.GetEmployees(para);
        }
        /// <summary>
        /// 根据指定节点ID，获取组织的子节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getorg/{id:Guid}")]
        public ActionResult<IEnumerable<OrgView>> GetOrgChildren(Guid id)
        {
            return bll.GetOrgChildren(id);
        }
        /// <summary>
        /// 人员离职
        /// </summary>
        /// <param name="employeeQuit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("employeeQuit")]
        public ActionResult<bool> EmployeeQuit(EmployeeQuit employeeQuit)
        {
            return bll.EmployeeQuit(employeeQuit);
        }
    }
}
