using ESafety.Core.Model.PARA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    class FacilitiesManage
    {
    }

    public class FacilitiesSortNew
    {
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

    public class FacilityNew
    {
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

        /// <summary>
        /// 自定义类项
        /// </summary>
        public IEnumerable<UserDefinedValue> UserDefineds { get; set; }
    }

    public class FacilityEdit:FacilityNew
    {
        /// <summary>
        /// 设备设施ID
        /// </summary>
        public Guid ID { get; set; }
    }

    public class FacilitiesQuery
    {
        /// <summary>
        /// 设备设施类别ID
        /// </summary>
        public Guid ID { get; set; }
    }

}
