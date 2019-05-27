using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    class DangerSet
    {
    }

    public class DangerRelationNew
    {
        /// <summary>
        /// 主题ID
        /// </summary>
        public Guid SubjectID { get; set; }
        //风险点ID
        public IEnumerable<Guid> DangerID { get; set; }
    }

    public class DangerRelationQuery
    {
        public Guid SubjectID { get; set; }


    }

}
