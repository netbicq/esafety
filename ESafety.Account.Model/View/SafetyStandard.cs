using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    /// <summary>
    /// 执行标准视图
    /// </summary>
    public class SafetyStandardView
    {
        /// <summary>
        /// 执行标准ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string DangerSort { get; set; }
        /// <summary>
        /// 管控措施
        /// </summary>
        public string Controls { get; set; }
        /// <summary>
        /// 风控项类别ID
        /// </summary>
        public Guid DangerSortID { get; set; }
        /// <summary>
        /// 风控项名称
        /// </summary>
        public string DangerName { get; set; }

        /// <summary>
        /// 工程措施
        /// </summary>
        public string Engineering { get; set; }
        /// <summary>
        /// 事故措施
        /// </summary>
        public string Accident { get; set; }
        /// <summary>
        /// 个体措施
        /// </summary>
        public string Individual { get; set; }
    }
    /// <summary>
    /// 执行标准模型
    /// </summary>
    public class SafetyStandardModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 安全标准名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 风控项类别ID
        /// </summary>
        public Guid DangerSortID { get; set; }
        /// <summary>
        /// 管控措施
        /// </summary>
        public string Controls { get; set; }
        /// <summary>
        /// 工程措施
        /// </summary>
        public string Engineering { get; set; }
        /// <summary>
        /// 事故措施
        /// </summary>
        public string Accident { get; set; }
        /// <summary>
        /// 个体措施
        /// </summary>
        public string Individual { get; set; }
    }

}
