﻿using ESafety.Core.Model;
using ESafety.Core.Model.DB.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    public class DocCrewPara
    {
        /// <summary>
        /// 根据选项卡id 获取数据
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 模糊查询
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        
        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; }
    }
}