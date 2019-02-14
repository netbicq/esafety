using ESafety.Core.Model.DB.Platform;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.ReportResult
{
    /// <summary>
    /// 报表List类型参数数据源
    /// </summary>
    public class ParameterListSource
    {
        /// <summary>
        /// ID
        /// </summary>
        public object ID { get; set; }
        /// <summary>
        /// 显示内容
        /// </summary>
        public string Name { get; set; }

    }
    /// <summary>
    /// 报表参数
    /// </summary>
    public class Parameter
    {

        /// <summary>
        /// 参数ID
        /// </summary>
        public Guid ParameterID { get; set; }
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParameterName { get; set; }
        /// <summary>
        /// 参数类型
        /// </summary>
        public PublicEnum.ReportParameterType ParameterType { get; set; }
        /// <summary>
        /// 参数标题
        /// </summary>
        public string ParameterCaption { get; set; }

        /// <summary>
        /// 如果参数为Combox的话，则会返回此值，否则为空
        /// </summary>
        public IEnumerable<ParameterListSource> DataSource { get; set; }

    }
    /// <summary>
    /// 执行报表前获取的报表信息和报表所需参数
    /// </summary>
    public class ExcuteReportView
    {
        /// <summary>
        /// 报表信息
        /// </summary>
        public  RPTInfo ReportInfo { get; set; }
        /// <summary>
        /// 报表所需参数集合
        /// </summary>
        public IEnumerable<Parameter> Parameters { get; set; }
    }

    /// <summary>
    /// 执行报表的参数
    /// </summary>
    public class ExcuteParameter
    {
        /// <summary>
        /// 参数ID
        /// </summary>
        public Guid ParameterID { get; set; }
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParameterName { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        public object ParameterValue { get; set; }
        /// <summary>
        /// 参数类型
        /// </summary>
        public PublicEnum.ReportParameterType ParameterType { get; set; }
    }
    /// <summary>
    /// 执行报表提交的参数
    /// </summary>
    public class ExcuteReprot
    {
        /// <summary>
        /// 报表ID
        /// </summary>
        public Guid ReprotID { get; set; }
        /// <summary>
        /// 执行的表的参数列表，需要提交各参数的值
        /// </summary>
        public IEnumerable<ExcuteParameter> Parameters { get; set; }

    }


    /// <summary>
    /// 报表返回的列
    /// </summary>
    public class ReportResultColumns
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 列标题
        /// </summary>
        public string ColumnCaption { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Visiable { get; set; }

    }
    /// <summary>
    /// 报表分表
    /// </summary>
    public class ReportResultChildeTable
    {
        /// <summary>
        /// 分表信息
        /// </summary>
        public  RPTChildrenTable TableInfo { get; set; }
        /// <summary>
        /// 分表列集合
        /// </summary>
        public IEnumerable<ReportResultColumns> TableColumns { get; set; }
        /// <summary>
        /// 分表数据
        /// </summary>
        public IEnumerable<dynamic> TableData { get; set; }


    }
    /// <summary>
    /// 执行报表返回的结果
    /// </summary>
    public class ReportResult
    {
        /// <summary>
        /// 报表信息
        /// </summary>
        public  RPTInfo ReportInfo { get; set; }
        /// <summary>
        /// 报表列
        /// </summary>
        public IEnumerable<ReportResultColumns> ReportColumns { get; set; }
        /// <summary>
        /// 报表数据
        /// </summary>
        public Pager<dynamic> ReprotData { get; set; }

        /// <summary>
        /// 报表分表集合
        /// </summary>
        public IEnumerable<ReportResultChildeTable> ChildeTables { get; set; }
    }

}
