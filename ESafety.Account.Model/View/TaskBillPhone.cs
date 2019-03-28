using ESafety.Core.Model.DB.Account;
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
    public class TaskSubjectsByTask 
    {
        /// <summary>
        /// 设备类型
        /// </summary>
        public PublicEnum.EE_SubjectType SubjectType { get; set; }
        /// <summary>
        /// 设备类型名称
        /// </summary>
        public string SubjectTypeName { get; set; }
        /// <summary>
        /// 设备集合
        /// </summary>
        public IEnumerable<Basic_Facilities> Facilities { get; set; }

    }

   
}
