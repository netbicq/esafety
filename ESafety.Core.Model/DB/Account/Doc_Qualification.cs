
/*---------------------------------------------------------------- 
// 版权所有。  
// 
// 文件名： Doc_Qualification
// 文件功能描述： 
// author：DengYinFeng
// 时间：02/27/2019 20:49:50
// 创建标识： 
// 
// 修改标识： 
//  
// 修改描述：此代码由T4模板自动生成
//			 对此文件的更改可能会导致不正确的行为，并且如果
//			 重新生成代码，这些更改将会丢失。
//----------------------------------------------------------------*/

using System;
using System.ComponentModel.DataAnnotations;
namespace ESafety.Core.Model.DB.Account
{
	/// <summary>
	/// 数据表实体类：Doc_Qualification 
	/// </summary>
	[Serializable()]
	public class Doc_Qualification: ModelBase
	{    
	    	     
	    /// <summary>
	    /// Int32:
	    /// </summary>        				 
	    public Int32 IsDeal {get;set;}   
	    	     
	    /// <summary>
	    /// DateTime:
	    /// </summary>        				 
	    public DateTime CreateTime {get;set;}

        /// <summary>
        /// String:资质名称
        /// </summary>        				 
        public String QName {get;set;}

        /// <summary>
        /// DateTime:有效期【结束时间】
        /// </summary>        				 
        public DateTime QEndTime {get;set;}

        /// <summary>
        /// DateTime:审核日期
        /// </summary>        				 
        public DateTime QAudit {get;set;}

        /// <summary>
        /// String:
        /// </summary>        				 
        public String QInstitutions {get;set;}

        /// <summary>
        /// Guid:机构id
        /// </summary>        				 
        public Guid QInsId {get;set;}

        /// <summary>
        /// String:
        /// </summary>        				 
        public String QPeople {get;set;}

        /// <summary>
        /// Guid:持有人id
        /// </summary>        				 
        public Guid QPeopleId {get;set;}


        /// <summary>
        /// Guid:类别Id
        /// </summary>        				 
        public Guid QTypeId {get;set;}   
	       
	}
	
}


    