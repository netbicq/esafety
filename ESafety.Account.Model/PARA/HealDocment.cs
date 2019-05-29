using ESafety.Core.Model.PARA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    /// <summary>
    /// 健康档案
    /// 需要电子文档
    /// </summary>
    public class HealDocmentNew
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        public Guid EmployeeID { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime BirthDay { get; set; }
        /// <summary>
        /// 民族
        public string Nation { get; set; }
        /// <summary>
        /// 疾病史
        /// </summary>
        public string IllnessRec { get; set; }
        /// <summary>
        /// 遗传病史
        /// </summary>
        public string HeredityRec { get; set; }
        /// <summary>
        /// 手术史
        /// </summary>
        public string OpreatRec { get; set; }
        /// <summary>
        /// 电子文档
        /// </summary>
        public IEnumerable<AttachFileNew> AttachFiles { get; set; }
        /// <summary>
        /// 自定义类项
        /// </summary>
        public IEnumerable<UserDefinedValue> UserDefineds { get; set; }
    }

    public class HealDocmentEdit: HealDocmentNew
    {
        /// <summary>
        /// 健康档案ID
        /// </summary>
        public Guid ID { get; set; }
    }

    public class HealDocmentQuery
    {
        /// <summary>
        /// 组织架构ID
        /// </summary>
        public Guid OrgID { get; set; }
        /// <summary>
        /// 关键字，姓名
        /// </summary>
        public string Key { get; set; }
    }
}
