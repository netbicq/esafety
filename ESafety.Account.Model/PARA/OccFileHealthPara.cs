using ESafety.Core.Model.DB;
using ESafety.Core.Model.DB.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.PARA
{
    public class OccFileHealthPara
    {
        public string Keyword { get; set; }

        public Guid ZzId { get; set; }

        public Occ_FileHealth tb { get; set; }

    }

    public class AOccFileHealthPara
    {
        public Guid ID { get; set; }

        /// <summary>
        /// DateTime:出生日期
        /// </summary>        				 
        public DateTime FBornTime { get; set; }

        /// <summary>
        /// Guid:人员id
        /// </summary>        				 
        public Guid FEmpId { get; set; }

        /// <summary>
        /// String:民族
        /// </summary>        				 
        public String FTypeName { get; set; }

        /// <summary>
        /// String:遗传史
        /// </summary>        				 
        public String FGenetic { get; set; }

        /// <summary>
        /// String:疾病史
        /// </summary>        				 
        public String FDisease { get; set; }

        /// <summary>
        /// String:手术史
        /// </summary>        				 
        public String FSurgery { get; set; }

        /// <summary>
        /// String:
        /// </summary>        				 
        public String FContent { get; set; }
    }
}
