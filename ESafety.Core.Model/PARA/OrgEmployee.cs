using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model.PARA
{
    /// <summary>
    /// 新建组织架构
    /// </summary>
    public class OrgNew
    {

        public Guid PrentID { get; set; }

        public string OrgName { get; set; }

        public string Principal { get; set; }

        public string PrincipalTel { get; set; }
    }
    /// <summary>
    /// 修改组织架构
    /// </summary>
    public class OrgEdit
    {

        public Guid ID { get; set; }

        public string OrgName { get; set; }

        public string Principal { get; set; }

        public string PrincipalTel { get; set; }

    }


}
