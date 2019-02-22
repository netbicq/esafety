﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    class FacilitiesManage
    {
    }

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
    }
}