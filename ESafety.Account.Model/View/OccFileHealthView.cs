using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model.View
{
    public class OccFileHealthView
    {
        [KeyAttribute]

        /// <summary>
        /// Guid:
        /// </summary>        				 
        public Guid Id { get; set; }

        /// <summary>
        /// Int32:
        /// </summary>        				 
        public Int32 IsDeal { get; set; }

        /// <summary>
        /// DateTime:
        /// </summary>        				 
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// DateTime:
        /// </summary>        				 
        public DateTime FBornTime { get; set; }

        /// <summary>
        /// Guid:
        /// </summary>        				 
        public Guid FEmpId { get; set; }

        public string FEmpName { get; set; }


        /// <summary>
        /// String:
        /// </summary>        				 
        public String FTypeName { get; set; }

        /// <summary>
        /// String:
        /// </summary>        				 
        public String FGenetic { get; set; }

        /// <summary>
        /// String:
        /// </summary>        				 
        public String FDisease { get; set; }

        /// <summary>
        /// String:
        /// </summary>        				 
        public String FSurgery { get; set; }

        /// <summary>
        /// String:
        /// </summary>        				 
        public String FContent { get; set; }

        public String ZzName { get; set; }

        public Guid ZzId { get; set; }

        public string dtStr { get { return FBornTime.ToString("g"); } }

        public string Sex { get; set; }
    }
}
