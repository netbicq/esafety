﻿using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.View
{
   
    /// <summary>
    /// 审批节点
    /// </summary>
    public class Flow_PointView
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 业务单据类型
        /// </summary>
        public PublicEnum.EE_BusinessType BusinessType { get; set; }
        /// <summary>
        /// 节点名称  
        /// </summary>
        public string PointName { get; set; }
        /// <summary>
        /// 节点类型
        /// </summary>
        public PublicEnum.EE_FlowPointType PointType { get; set; }
        /// <summary>
        /// 节点顺序
        /// </summary>
        public int PointIndex { get; set; }
        /// <summary>
        /// 业务单据类型
        /// </summary>
        public string BusinessTypeName { get; set; }
        /// <summary>
        /// 审批节点类型
        /// </summary>
        public string PointTypeName { get; set; }


    }
    /// <summary>
    /// 审批用户
    /// </summary>
    public class Point_UsersView
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 审批节点ID
        /// </summary>
        public Guid PointID { get; set; }
        /// <summary>
        /// 审批用户
        /// </summary>
        public string PointUser { get; set; }
        /// <summary>
        /// 用户顺序
        /// </summary>
        public int UserIndex { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string PointUserName { get; set; }
    }
    /// <summary>
    /// 审批任务
    /// </summary>
    public class Flow_TaskView
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 业务单据id
        /// </summary>
        public Guid BusinessID { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public PublicEnum.EE_BusinessType BusinessType { get; set; }
        /// <summary>
        /// 业务类型名
        /// </summary>
        public string BusinessTypeName { get; set; }

        /// <summary>
        /// 任务用户
        /// </summary>
        public string TaskUser { get; set; }
        /// <summary>
        /// 发起人
        /// </summary>
        public string ApplyUser { get; set; }
        /// <summary>
        /// 发起人
        /// </summary>
        public string ApplyUserName { get; set; }
        /// <summary>
        /// 任务日期
        /// </summary>
        public DateTime TaskDate { get; set; }
        /// <summary>
        /// 审批版本
        /// </summary>
        public long FlowVersion { get; set; }
        /// <summary>
        /// 业务模型
        /// </summary>
        public dynamic BusinessObj { get; set; }


    }
    /// <summary>
    /// 审批结果
    /// </summary>
    public class Flow_Result
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 审批版本
        /// </summary>
        public long FlowVersion { get; set; }
        /// <summary>
        /// 审批用户
        /// </summary>
        public string FlowUser { get; set; }
        /// <summary>
        /// 审批用户
        /// </summary>
        public string FlowUserName { get; set; }
        /// <summary>
        /// 审批结果
        /// </summary>
        public PublicEnum.EE_FlowResult FlowResult { get; set; }
        /// <summary>
        /// 审批结果
        /// </summary>
        public string FlowResultName { get; set; }
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime FlowDate { get; set; }
        /// <summary>
        /// 发起人
        /// </summary>
        public string ApplyUser { get; set; }
        /// <summary>
        /// 发起人
        /// </summary>
        public string ApplyUserName { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string PointName { get; set; }
        /// <summary>
        /// 业务单据id
        /// </summary>
        public Guid BusinessID { get; set; }
        /// <summary>
        /// 业务单据类型
        /// </summary>
        public PublicEnum.EE_BusinessType BusinessType { get; set; }
        /// <summary>
        /// 业务单据类型
        /// </summary>
        public string BusinessTypeName { get; set; }


    }

}
