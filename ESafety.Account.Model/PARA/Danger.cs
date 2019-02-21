﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    public class Danger
    {

    }

    public class DangerSortNew
    {
        public Guid ParetID { get; set; }
        public int Level { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string SortName { get; set; }
    }
    public class DangerNew
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
        /// 类别ID
        /// </summary>
        public Guid DangerSortID { get; set; }
    }

    public class DangerEdit:DangerNew
    {
        public Guid Id { get; set; }
    }
}
