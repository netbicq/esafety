using ESafety.Core;
using ESafety.Core.Model;
using ESafety.Core.Model.ReportManager;
using ESafety.Core.Model.View;
using ESafety.Unity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ESafety.Platform.API.Controllers
{
    /// <summary>
    /// 报表管理器
    /// </summary>
    [RoutePrefix("api/platrpt")]
    public class ReportController : Web.Unity.ESFAPI
    {

        private IReportManager bll = null;
        private IReport rbll = null;


        public ReportController( IReportManager report,  IReport rreport)
        {
            bll = report;
            rbll = rreport;
            BusinessService = bll;

        }
        /// <summary>
        /// 新建报表列
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("colnew")]
        public ActionResult<bool> AddColumn(ReportColumnNew column)
        {
            LogContent = "新建报表列，参数源：" + JsonConvert.SerializeObject(column);

            return bll.AddColumn(column);
        }

        /// <summary>
        /// 新建报表参数
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("paranew")]
        public ActionResult<bool> AddParameter(ParameterNew parameter)
        {
            LogContent = "新建报表参数，参数源：" + JsonConvert.SerializeObject(parameter);

            return bll.AddParameter(parameter);
        }
        /// <summary>
        /// 新建报表
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        [Route("reportnew")]
        [HttpPost]
        public ActionResult<bool> AddReportInfo(ReportInfoNew report)
        {
            LogContent = "新建报表，参数源：" + JsonConvert.SerializeObject(report);

            return bll.AddReportInfo(report);
        }
        /// <summary>
        /// 新建报表子表
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("tablenew")]
        public ActionResult<bool> AddTable(ChildeTableNew table)
        {
            LogContent = "新建报表子表，参数源：" + JsonConvert.SerializeObject(table);

            return bll.AddTable(table);
        }
        /// <summary>
        /// 新建子表列
        /// </summary>
        /// <param name="tablecolumn"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("tcolnew")]
        public ActionResult<bool> AddTableColumn(ChildeTableColumnNew tablecolumn)
        {
            LogContent = "新建子表列，参数源：" + JsonConvert.SerializeObject(tablecolumn);

            return bll.AddTableColumn(tablecolumn);
        }
        /// <summary>
        /// 删除指定ID的报表列
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delcol/{id:Guid}")]
        public ActionResult<bool> DelColumn(Guid id)
        {
            LogContent = "删除指定ID的报表列";

            return bll.DelColumn(id);
        }

        /// <summary>
        /// 删除指定ID的报表参数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("delpara/{id:Guid}")]
        [HttpGet]
        public ActionResult<bool> DelParameter(Guid id)
        {
            LogContent = "删除指定ID的报表参数";
            return bll.DelParameter(id);
        }

        /// <summary>
        /// 删除指定ID的报表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("delrpt/{id:Guid}")]
        [HttpGet]
        public ActionResult<bool> DelReportInfo(Guid id)
        {
            LogContent = "删除指定ID的报表";

            return bll.DelReportInfo(id);
        }
        /// <summary>
        /// 删除指定ID的报表作用域
        /// </summary>
        /// <param name="scopeid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delscope/{scopeid:Guid}")]
        public ActionResult<bool> DelReportScope(Guid scopeid)
        {
            LogContent = "删除指定ID的报表作用域";

            return rbll.DelReportScope(scopeid);
        }
        /// <summary>
        /// 获取指定报表ID的作用域列表
        /// </summary>
        /// <param name="reportid"></param>
        /// <returns></returns>
        [Route("getscopelist/{reportid:Guid}")]
        [HttpGet]
        public ActionResult<IEnumerable< ReportScope>> GetReportScopeList(Guid reportid)
        {
            return rbll.GetReportScopes(reportid);
        }
        /// <summary>
        /// 删除指定ID的报表子表
        /// </summary>
        /// <param name="tableid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("deltable/{tableid:Guid}")]
        public ActionResult<bool> DelTable(Guid tableid)
        {
            LogContent = "删除指定ID的报表子表";

            return bll.DelTable(tableid);
        }
        /// <summary>
        /// /删除指定ID的子表列
        /// </summary>
        /// <param name="tablecolumnid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("deltablecol/{tablecolumnid:Guid}")]
        public ActionResult<bool> DelTableColumn(Guid tablecolumnid)
        {
            LogContent = "删除指定ID的子表列";

            return bll.DelTableColumn(tablecolumnid);
        }
        /// <summary>
        /// 修改报表列
        /// </summary>
        /// <param name="updater"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editcol")]
        public ActionResult<bool> EditColumn(ReportColumnEdit updater)
        {
            LogContent = "修改报表列，参数源：" + JsonConvert.SerializeObject(updater);

            return bll.EditColumn(updater);
        }
        /// <summary>
        /// 修改报表参数
        /// </summary>
        /// <param name="updater"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editpara")]
        public ActionResult<bool> EditParameter(ParemeterEdit updater)
        {
            LogContent = "修改报表参数，参数源：" + JsonConvert.SerializeObject(updater);

            return bll.EditParameter(updater);
        }
        /// <summary>
        /// 修改报表信息
        /// </summary>
        /// <param name="updater"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editreport")]
        public ActionResult<bool> EditReportInfo(ReportInfoEdit updater)
        {
            LogContent = "修改报表信息，参数源：" + JsonConvert.SerializeObject(updater);

            return bll.EditReportInfo(updater);
        }
        /// <summary>
        /// 修改报表子表
        /// </summary>
        /// <param name="updater"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("edittable")]
        public ActionResult<bool> EditTable(ChildeTableEdit updater)
        {
            LogContent = "修改报表子表，参数源：" + JsonConvert.SerializeObject(updater);

            return bll.EditTable(updater);
        }
        /// <summary>
        /// 修改子表列
        /// </summary>
        /// <param name="updater"></param>
        /// <returns></returns>
        [Route("edittcolum")]
        [HttpPost]
        public ActionResult<bool> EditTableColumn(ChildeTableColumnEdit updater)
        {
            LogContent = "修改子表列，参数源：" + JsonConvert.SerializeObject(updater);

            return bll.EditTableColumn(updater);
        }
        /// <summary>
        /// 获取指定报表的列列表
        /// </summary>
        /// <param name="reportid"></param>
        /// <returns></returns>
        [Route("getcols/{reportid:Guid}")]
        [HttpGet]
        public ActionResult<IEnumerable<ReportColumnListView>> GetColumnList(Guid reportid)
        {
            return bll.GetColumnList(reportid);
        }
        /// <summary>
        /// 获取指定报表的参数列表
        /// </summary>
        /// <param name="reportid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getparas/{reportid:Guid}")]
        public ActionResult<IEnumerable<ParameterList>> GetParameterList(Guid reportid)
        {
            return bll.GetParameterList(reportid);
        }
        /// <summary>
        /// 报表选择器
        /// </summary>
        /// <returns></returns>
        [Route("reportselector")]
        [HttpGet]
        public ActionResult<IEnumerable<ReportInfoListView>> GetReportList()
        {
            return bll.ReportSelector();
        }
        /// <summary>
        /// 获取报表列表
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [Route("getreorts")]
        [HttpPost]
        public ActionResult<Pager<ReportInfoListView>> GetReportList(PagerQuery<string> para)
        {
            return bll.GetReportList(para);
        }
        /// <summary>
        /// 获取指定子表的列集合
        /// </summary>
        /// <param name="tableid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("gettcols/{tableid:Guid}")]
        public ActionResult<IEnumerable<ChildeTableColumnList>> GetTableColumnList(Guid tableid)
        {
            return bll.GetTableColumnList(tableid);
        }
        /// <summary>
        /// 获取指定报表的子表集合
        /// </summary>
        /// <param name="reportid"></param>
        /// <returns></returns>
        [Route("gettables/{reportid:Guid}")]
        [HttpGet]
        public ActionResult<IEnumerable<ChildeTableList>> GetTableList(Guid reportid)
        {
            return bll.GetTableList(reportid);
        }
        /// <summary>
        /// 设置报表作用域
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [Route("setscope")]
        [HttpPost]
        public ActionResult<bool> SetReportScope(SetReportScope para)
        {
            LogContent = "设置报表作用域，参数源：" + JsonConvert.SerializeObject(para);

            return bll.SetReportScope(para);
        }
        /// <summary>
        /// 获取作用域列表
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [Route("getsouce")]
        [HttpPost]
        public ActionResult<IEnumerable<reptlist>> reptlist()
        {
            return bll.reptlist();
        }
        /// <summary>
        /// 报表状态管理
        /// </summary>
        /// <param name="state"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        [Route("stateset/{state}/{ID:Guid}")]
        [HttpGet]
        public ActionResult<bool> CardStateSet(PublicEnum.GenericState state, Guid ID)
        {
            LogContent = "更改报表状态";

            return bll.StateSet(state, ID);
        }
    }
}
