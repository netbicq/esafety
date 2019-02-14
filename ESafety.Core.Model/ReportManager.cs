using ESafety.Core.Model.DB;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.ReportManager
{

    /// <summary>
    /// 作用域列表
    /// </summary>
    public class reptlist
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 报表ID 属于 报表作用域
        /// </summary>
        public Guid ReportID { get; set; }
        /// <summary>
        /// 账套
        /// </summary>
        public string AccountCode { get; set; }
    }
    /// <summary>
    /// 设置报表作用域
    /// </summary>
    public class SetReportScope
    {
        /// <summary>
        /// 报表ID
        /// </summary>
        public Guid ReportID { get; set; }
        /// <summary>
        /// 账套号集合
        /// </summary>
        public string AccountCodes { get; set; }
    }
    /// <summary>
    /// 查询
    /// </summary>
    public class ReportQuery
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
    }


    #region "子表"


    /// <summary>
    /// 子表列表
    /// </summary>
    public class ChildeTableList
    {
        /// <summary>
        /// 子表信息
        /// </summary>
        public  RPTChildrenTable ChildeTableInfo { get; set; }

    }
    /// <summary>
    /// 新建子表
    /// </summary>
    public class ChildeTableNew
    {
        /// <summary>
        /// 报表ID
        /// </summary>
        public Guid ReportID { get; set; }
        /// <summary>
        /// 子表标题
        /// </summary>
        public string ChildeCaption { get; set; }
        /// <summary>
        /// 主表主键列
        /// </summary>
        public string MasterKeyColumn { get; set; }
        /// <summary>
        /// 子表主键列
        /// </summary>
        public string ChildeKeyColumn { get; set; }
        /// <summary>
        /// 子表顺序
        /// </summary>
        public int ChildeIndex { get; set; }

    }

    /// <summary>
    ///子表列编辑
    /// </summary>
    public class ChildeTableEdit : ChildeTableNew
    {
        /// <summary>
        /// 子表ID
        /// </summary>
        public Guid ID { get; set; }
    }

    #endregion


    #region"子表列"

    /// <summary>
    /// 新建子表列
    /// </summary>
    public class ChildeTableColumnNew : ReportColumnNew
    {
        /// <summary>
        /// 报表ID
        /// </summary>
        public Guid ReportID { get; set; }
        /// <summary>
        /// 子表ID
        /// </summary>
        public Guid ChildeID { get; set; }
    }

    /// <summary>
    /// 子表列编辑
    /// </summary>
    public class ChildeTableColumnEdit : ChildeTableColumnNew
    {
        /// <summary>
        /// 子表列ID
        /// </summary>
        public Guid ID { get; set; }
    }
    /// <summary>
    /// 子表列列表
    /// </summary>
    public class ChildeTableColumnList
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string DataTypeStr { get; set; }
        /// <summary>
        /// 子表列信息
        /// </summary>
        public RPTChildrenColumn ChildeColumnInfo { get; set; }
    }

    #endregion


    #region "报表列"
    /// <summary>
    /// 报表列列表
    /// </summary>
    public class ReportColumnListView
    {
        /// <summary>
        /// 报表列信息
        /// </summary>
        public  RPTColumn ColumnInfo { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataTypeStr { get; set; }

    }

    /// <summary>
    /// 新建报表列
    /// </summary>
    public class ReportColumnNew
    {
        /// <summary>
        /// 报表ID
        /// </summary>
        public Guid ReportID { get; set; }
        /// <summary>
        /// 报表名称
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 报表标题
        /// </summary>
        public string ColumnCaption { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public PublicEnum.ReportColumnDataType DataType { get; set; }
        /// <summary>
        /// 是否可见
        /// </summary>
        public bool Visiable { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderIndex { get; set; }

    }
    /// <summary>
    /// 报表列修改
    /// </summary>
    public class ReportColumnEdit
    {
        /// <summary>
        /// 列ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 报表名称
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 报表标题
        /// </summary>
        public string ColumnCaption { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public PublicEnum.ReportColumnDataType DataType { get; set; }
        /// <summary>
        /// 是否可见
        /// </summary>
        public bool Visiable { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderIndex { get; set; }
    }

    #endregion


    #region "报表信息"
    /// <summary>
    /// 新建报表
    /// </summary>
    public class ReportInfoNew
    {

        /// <summary>
        /// 报表名称
        /// </summary>
        public string ReportName { get; set; }
        /// <summary>
        /// 报表数据源
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// 报表作用域类型
        /// </summary>
        public PublicEnum.ReortScopeType ScopeType { get; set; }


    }
    /// <summary>
    /// 修改报表
    /// </summary>
    public class ReportInfoEdit : ReportInfoNew
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
    }

    /// <summary>
    /// 报表列表
    /// </summary>
    public class ReportInfoListView
    {
        /// <summary>
        /// 报表信息
        /// </summary>
        public  RPTInfo ReportInfo { get; set; }
        /// <summary>
        /// 报表作用域类型
        /// </summary>

        public string ScopTypeStr { get; set; }
        /// <summary>
        /// 状态
        /// </summary>

        public string StateStr { get; set; }
    }
    #endregion



    #region "报表参数"

    /// <summary>
    /// 新建报表参数
    /// </summary>
    public class ParameterNew
    {
        /// <summary>
        /// 报表ID
        /// </summary>

        public Guid ReportID { get; set; }
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParameterName { get; set; }
        /// <summary>
        /// 参数标题
        /// </summary>
        public string ParameterCaption { get; set; }
        /// <summary>
        /// 参数数据类型
        /// </summary>
        public PublicEnum.ReportParameterType ParameterType { get; set; }
        /// <summary>
        /// List类型数据源
        /// </summary>
        public string TypeListTypeSource { get; set; }
        /// <summary>
        /// List类型值类型
        /// </summary>
        public PublicEnum.ReportParameterType TypeListValueType { get; set; }


    }
    /// <summary>
    /// 参数修改
    /// </summary>
    public class ParemeterEdit : ParameterNew
    {
        /// <summary>
        /// 参数ID
        /// </summary>
        public Guid ID { get; set; }
    }

    /// <summary>
    /// 报表参数列表
    /// </summary>
    public class ParameterList
    {
        /// <summary>
        /// 参数信息
        /// </summary>
        public  RPTParameter ParameterInfo { get; set; }
        /// <summary>
        /// 参数类型
        /// </summary>
        public string ParameterTypeStr { get; set; }
        /// <summary>
        /// List类型值类型
        /// </summary>
        public string TypeListValueTypeStr { get; set; }
    }
    #endregion

}
