﻿using ESafety.Core.Model.DB.Account;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    /// <summary>
    /// 来自任务的设备集合
    /// </summary>
    public class TaskBillModel
    {
        /// <summary>
        /// 单据ID
        /// </summary>
        public Guid BillID { get; set; }
        /// <summary>
        /// 任务名
        /// </summary>
        public string TaskName { set; get; }
        /// <summary>
        /// 职员名称
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 预计完成时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 风险点名
        /// </summary>
        public string DangerName { get; set; }

        /// <summary>
        /// 检查主体总数
        /// </summary>
        public int SubCount { get; set; }
        /// <summary>
        /// 已检查主体数
        /// </summary>
        public int SubCheckedCount { get; set; }

    }

    public class TaskSubjectView
    {
        /// <summary>
        /// 单据ID
        /// </summary>
        public Guid BillID { get; set; }
        /// <summary>
        /// 主体ID
        /// </summary>
        public Guid SubID { get; set; }
        /// <summary>
        /// 主体名
        /// </summary>
        public string SubName { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string Principal { get; set; }
        /// <summary>
        /// 负责人电话
        /// </summary>
        public string PrincipalTel { get; set; }
        /// <summary>
        /// 风险等级
        /// </summary>
        public string DangerLevel { get; set; }
        /// <summary>
        /// 主体类型
        /// </summary>
        public PublicEnum.EE_SubjectType SubType { get; set; }
        /// <summary>
        /// 主体类型名称
        /// </summary>
        public string SubTypeName { get; set; }
    }

    public class TaskSubjectOverView: TaskSubjectView
    {
        public Guid SubResultID { get; set; }
    }

    public class SubResultView
    {
        /// <summary>
        /// 检查时间
        /// </summary>
        public DateTime ResultTime { get; set; }
        /// <summary>
        /// 检查结果
        /// </summary>
        public PublicEnum.EE_TaskResultType TaskResult { get; set; }
        /// <summary>
        /// 检查描述
        /// </summary>
        public string TaskResultMemo { get; set; }
    }

}
