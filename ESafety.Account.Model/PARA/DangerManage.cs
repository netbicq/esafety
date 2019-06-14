using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    public class DangerSortNew
    {
        public Guid ParentID { get; set; }
        public int Level { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string SortName { get; set; }
    }
    public class DangerNew
    {
        /// <summary>
        /// 类别ID
        /// </summary>
        public Guid DangerSortID { get; set; }
        /// <summary>
        /// 风控项
        /// </summary>
        public IEnumerable<DangerItem> Dangers { get; set; }
    }

    public class DangerItem
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 风险级别
        /// </summary>
        public Guid DangerLevel { get; set; }
    }


    public class DangerEdit:DangerNew
    {
        public Guid ID { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 风险级别
        /// </summary>
        public Guid DangerLevel { get; set; }
    }

    public class DangerSafetyStandards
    {
        /// <summary>
        /// 风险点ID
        /// </summary>
        public Guid DangerID { get; set; }
        /// <summary>
        /// 安全标准ID
        /// </summary>
        public Guid SafetyStandardID { get; set; }
    }
    /// <summary>
    /// 风险点类别树形结构
    /// </summary>
    public class DangerSortTree : TreeBase<ModelBaseTree>
    {
        public DangerSortTree()
        {
            Children = new List<ModelBaseTree>();
        }
        /// <summary>
        /// 级次
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string SortName { get; set; }
    }
}
