using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.PARA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
   

    public class FacilitiesSortView
    {
        /// <summary>
        /// 设备设施类别ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 上级ID
        /// </summary>
        public Guid ParentID { get; set; }

        public int Level { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string SortName { get; set; }
  
    }

    public class FacilityView
    {
        /// <summary>
        /// 设备设施ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类别ID
        /// </summary>
        public Guid SortID { get; set; }
        /// <summary>
        /// 所属类别名称
        /// </summary>
        public string SortName { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string Principal { get; set; }
        /// <summary>
        /// 负责人电话
        /// </summary>
        public string PrincipalTel { get; set; }
        /// <summary>
        /// 定位
        /// </summary>
        public string Location { get; set; }
    }
    /// <summary>
    /// 设备类别树
    /// </summary>
    public class FacilitiesSortTree : TreeBase<ModelBaseTree>
    {
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
