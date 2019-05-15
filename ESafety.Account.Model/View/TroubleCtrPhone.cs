﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    /// <summary>
    /// APP管控详情数据
    /// </summary>
    public class APPTroubleCtrView
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid KeyID { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string Principal { get; set; }
        /// <summary>
        ///隐患管控执行人
        /// </summary>
        public string Executor { get; set; }
        /// <summary>
        /// 隐患管控验收人
        /// </summary>
        public string Acceptor { get; set; }
        /// <summary>
        /// 主体名
        /// </summary>
        public string SubName { get; set; }
        /// <summary>
        /// 风控项
        /// </summary>
        public string DangerName { get; set; }
        /// <summary>
        /// 管控项状态
        /// </summary>
        public Unity.PublicEnum.EE_TroubleState State { get; set; }
        /// <summary>
        /// 预计完成时间
        /// </summary>
        public DateTime? EstimatedDate { get; set; }
        /// <summary>
        /// 隐患级别
        /// </summary>
        public Unity.PublicEnum.EE_TroubleLevel TroubleLevel { get; set; }
        /// <summary>
        /// 风险等级ID
        /// </summary>
        public Guid CDangerLevel { get; set; }
        /// <summary>
        /// 风险等级
        /// </summary>
        public string CDangerLevelName { get; set; }
        /// <summary>
        /// 管控目标
        /// </summary>
        public string CtrTarget { get; set; }
        /// <summary>
        /// 问题详情
        /// </summary>
        public string TroubleDetails { get; set; }
        /// <summary>
        /// 风险点
        /// </summary>
        public string DangerPoint { get; set; }
        /// <summary>
        /// 当前人 1负责人 2执行人 3验收人 
        /// </summary>
        public int Cuser { get; set; }
    }



}
