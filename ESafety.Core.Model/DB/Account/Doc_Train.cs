
/*---------------------------------------------------------------- 
// 版权所有。  
// 
// 文件名： Doc_Train
// 文件功能描述： 
// author：DengYinFeng
// 时间：02/27/2019 23:33:07
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
	/// 数据表实体类：Doc_Train 
	/// </summary>
	[Serializable()]
	public class Doc_Train: ModelBase
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
        /// String:培训主题
        /// </summary>        				 
        public String TTheme {get;set;}

        /// <summary>
        /// Int32:培训时长 单位：h
        /// </summary>        				 
        public Int32 TTime {get;set;}

        /// <summary>
        /// DateTime:培训日期
        /// </summary>        				 
        public DateTime TEndTime {get;set;}

        /// <summary>
        /// String:内容
        /// </summary>        				 
        public String TContent {get;set;}

        /// <summary>
        /// String:培训人Id
        /// </summary>        				 
        public Guid TrainerId {get;set;}   
	       
	}
	
}


    