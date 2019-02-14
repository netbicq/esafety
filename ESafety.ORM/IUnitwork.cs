﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using ESafety.Core.Model.DB;

namespace ESafety.ORM
{
    public interface IUnitwork:IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IRepository<T> Repository<T>() where T :class;
        

        void SetUserDB(Core.Model.AppUserDB userdb);
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        DataSet ExecuteSQL(string sql,SqlParameter[] Parameters=null);
        /// <summary>
        /// 只执行SQL语句，返回受影响的数据行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Parameters"></param>
        int ExcuteSQLNoQuery(string sql, SqlParameter[] Parameters = null);
        /// <summary>
        /// 执行存储过程，返回结果集
        /// </summary>
        /// <param name="ProcedreName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        dynamic ExecProcedre(string ProcedreName,object parameter=null);
        /// <summary>
        /// 执行返回值的存储过程
        /// </summary>
        /// <param name="ProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object ExecProcedreResult(string sql, object parameters=null);

        int Commit();
    }
    
}
