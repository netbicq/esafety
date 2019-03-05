using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    public class HealDocmentView
    {
        /// <summary>
        /// 档案ID
        /// </summary>
        public Guid ID { get; set; }
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
        /// 姓名
        /// </summary>
        public string CNName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        ///年龄
        /// </summary>
        public int Age { get; set; }

    }
}
