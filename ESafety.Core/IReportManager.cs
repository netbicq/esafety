using ESafety.Core.Model;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core
{
    public interface IReportManager
    {
        /// <summary>
        /// 新建报表
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        ActionResult<bool> AddReportInfo(Model.ReportManager.ReportInfoNew report);
        /// <summary>
        /// 修改报表
        /// </summary>
        /// <param name="updater"></param>
        /// <returns></returns>
        ActionResult<bool> EditReportInfo(Model.ReportManager.ReportInfoEdit updater);
        /// <summary>
        /// 获取报表列表
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<Model.ReportManager.ReportInfoListView>> ReportSelector();

        ActionResult<Pager<Model.ReportManager.ReportInfoListView>> GetReportList(PagerQuery<string> para);

        /// <summary>
        /// 删除报表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<bool> DelReportInfo(Guid id);


        /// <summary>
        /// 新建报表参数
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        ActionResult<bool> AddParameter(Model.ReportManager.ParameterNew parameter);
        /// <summary>
        /// 修改参数
        /// </summary>
        /// <param name="updater"></param>
        /// <returns></returns>
        ActionResult<bool> EditParameter(Model.ReportManager.ParemeterEdit updater);
        /// <summary>
        /// 删除指定参数ID的参数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<bool> DelParameter(Guid id);
        /// <summary>
        /// 获取指定报表ID的报表参数列表
        /// </summary>
        /// <param name="reportid"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<Model.ReportManager.ParameterList>> GetParameterList(Guid reportid);




        /// <summary>
        /// 新建列
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        ActionResult<bool> AddColumn(Model.ReportManager.ReportColumnNew column);
        /// <summary>
        /// 修改列
        /// </summary>
        /// <param name="updater"></param>
        /// <returns></returns>
        ActionResult<bool> EditColumn(Model.ReportManager.ReportColumnEdit updater);
        /// <summary>
        /// 删除指定ID的列
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<bool> DelColumn(Guid id);
        /// <summary>
        /// 获取指定报表ID的列集合
        /// </summary>
        /// <param name="reportid"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<Model.ReportManager.ReportColumnListView>> GetColumnList(Guid reportid);


        /// <summary>
        /// 新建报表子表
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        ActionResult<bool> AddTable(Model.ReportManager.ChildeTableNew table);
        /// <summary>
        /// 修改报表子表
        /// </summary>
        /// <param name="updater"></param>
        /// <returns></returns>
        ActionResult<bool> EditTable(Model.ReportManager.ChildeTableEdit updater);
        /// <summary>
        /// 删除指定ID的报表子表
        /// </summary>
        /// <param name="tableid"></param>
        /// <returns></returns>
        ActionResult<bool> DelTable(Guid tableid);

        /// <summary>
        /// 获取指定报表ID的子表列表
        /// </summary>
        /// <param name="reportid"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<Model.ReportManager.ChildeTableList>> GetTableList(Guid reportid);



        /// <summary>
        /// 新建子表列
        /// </summary>
        /// <param name="tablecolumn"></param>
        /// <returns></returns>
        ActionResult<bool> AddTableColumn(Model.ReportManager.ChildeTableColumnNew tablecolumn);
        /// <summary>
        /// 修改子表列
        /// </summary>
        /// <param name="updater"></param>
        /// <returns></returns>
        ActionResult<bool> EditTableColumn(Model.ReportManager.ChildeTableColumnEdit updater);
        /// <summary>
        /// 删除子表列
        /// </summary>
        /// <param name="tablecolumnid"></param>
        /// <returns></returns>
        ActionResult<bool> DelTableColumn(Guid tablecolumnid);
        /// <summary>
        /// 获取子表列集合
        /// </summary>
        /// <param name="tableid"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<Model.ReportManager.ChildeTableColumnList>> GetTableColumnList(Guid tableid);

        /// <summary>
        /// 为报表设置作用域
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        ActionResult<bool> SetReportScope(Model.ReportManager.SetReportScope para);

        /// <summary>
        /// 删除报表作用域
        /// </summary>
        /// <param name="scopeid"></param>
        /// <returns></returns>
        ActionResult<bool> DelReportScope(Guid scopeid);


        /// <summary>
        /// 作用域列表
        /// </summary>
        /// <param name="tableid"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<Model.ReportManager.reptlist>> reptlist();
        /// <summary>
        /// 自定义报表状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        ActionResult<bool> StateSet(PublicEnum.GenericState state, Guid ID);
    }
}
