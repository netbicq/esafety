using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    public class DocTranPeopleView
    {
        [KeyAttribute]

        /// <summary>
        /// Guid:
        /// </summary>        				 
        public Guid Id { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string BName { get; set; }

        /// <summary>
        /// 人员姓名
        /// </summary>
        public string RName { get; set; }
    }
}
