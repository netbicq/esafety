﻿using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{

    public class TaskBillQuery
    {
        /// <summary>
        /// 任务状态
        /// </summary>
        public int TaskState { get; set; }
        /// <summary>
        /// 执行岗位
        /// </summary>
        public Guid PostID { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string Key { get; set; }
    }

    public class TaskBillSubjectsQuery
    {
        /// <summary>
        /// 任务单据ID
        /// </summary>
        public Guid BillID { get; set; }
    }

    /// <summary>
    /// 任务单评测参数模型
    /// </summary>
    public class TaskBillEval
    {
        /// <summary>
        /// 任务单ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 危害因素
        /// </summary>
        public Guid Eval_WHYS { get; set; }
        /// <summary>
        /// 事故类型
        /// </summary>
        public Guid Eval_SGLX { get; set; }
        /// <summary>
        /// 事故结果
        /// </summary>
        public Guid Eval_SGJG { get; set; }
        /// <summary>
        /// 影响范围
        /// </summary>
        public Guid Eval_YXFW { get; set; }
        /// <summary>
        /// 评测方法
        /// </summary>
        public PublicEnum.EE_EvaluateMethod Eval_Method { get; set; }
        /// <summary>
        /// 隐患等级
        /// </summary>
        public PublicEnum.EE_TroubleLevel TroubleLevel { get; set; }
        /// <summary>
        /// 是否管控
        /// </summary>
        public bool IsControl { get; set; }
    }
     
}
