
/*---------------------------------------------------------------- 
// 版权所有。  
// 
// 文件名： Occ_Medical
// 文件功能描述： 
// author：DengYinFeng
// 时间：02/28/2019 11:46:53
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
	/// 数据表实体类：Occ_Medical 
	/// </summary>
	[Serializable()]
	public class Occ_Medical: ModelBase
	{    
		[KeyAttribute]
	    	     
	    /// <summary>
	    /// Guid:
	    /// </summary>        				 
	    public Guid Id {get;set;}   
	    	     
	    /// <summary>
	    /// Int32:
	    /// </summary>        				 
	    public Int32 IsDeal {get;set;}   
	    	     
	    /// <summary>
	    /// DateTime:
	    /// </summary>        				 
	    public DateTime CreateTime {get;set;}   
	    	     
	    /// <summary>
	    /// Guid:
	    /// </summary>        				 
	    public Guid MEmpId {get;set;}   
	    	     
	    /// <summary>
	    /// Int32:
	    /// </summary>        				 
	    public Int32 MAge {get;set;}   
	    	     
	    /// <summary>
	    /// DateTime:
	    /// </summary>        				 
	    public DateTime MTime {get;set;}   
	    	     
	    /// <summary>
	    /// String:
	    /// </summary>        				 
	    public String MContent {get;set;}   
	       
	}
	
}


    