using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; 
using System.Data.Entity;
using EntityFramework;
using EntityFramework.Extensions;
using System.Data.Entity.Validation;
using System.Data;
using Dapper;
using ESafety.Core.Model.DB;
using ESafety.Core.Model;

namespace ESafety.ORM
{
    public class Unitwork : IUnitwork
    {


        private Dictionary<string, object> repositorys=null;

        private string errmsg = null;

        private string connstr = null;
        private DbContext _dbcontext = null;

      
        public Unitwork(DbContext context)
        {
            if (context != null)
            {
                _dbcontext =context;// new dbContext(connstr);
            }

        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (disposing)
            {

                if (_dbcontext != null)
                {
                    _dbcontext.Dispose();
                }

            }
        }
        /// <summary>
        /// 创建仓储
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IRepository<T> Repository<T>() where T : ModelBase
        {
            if (repositorys == null)
            {
                repositorys = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;
            if (!repositorys.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryEF<>);
                var repository = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbcontext);
                repositorys.Add(type, repository);
            }
            return (RepositoryEF<T>)repositorys[type];

        }

        /// <summary>
        /// 设置Tantent用户数据库
        /// </summary>
        /// <param name="userdb"></param>
        public void SetUserDB(AppUserDB userdb)
        {
            if (userdb != null)
            {
                if (userdb.DBServer == null)
                {
                    //Data Source=quickcq.com;Initial Catalog=Levy;User ID=sa;Password=1q2w!Q@W0p9o)P(O;MultipleActiveResultSets=True;App=EntityFramework
                    connstr = System.Configuration.ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                }
                else
                {
                    connstr = "Data Source=" + userdb.DBServer + ";Initial Catalog=" +
                        userdb.DBName + ";User ID=" + userdb.DBUid + ";Password=" +
                        userdb.DBPwd + ";MultipleActiveResultSets=True;App=EntityFramework";
                }
            }
            else
            {
                connstr = System.Configuration.ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
               
            }
            _dbcontext.Database.Connection.ConnectionString = connstr; 
        }

        public int Commit()
        {
            try
            {
               
                return _dbcontext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                         errmsg+= string.Format("Property: {0} Error: {1}",
                            validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(errmsg, dbEx);
            }
        }
        /// <summary>
        /// 执行SQL，返回所有集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public DataSet ExecuteSQL(string sql, SqlParameter[] Parameters = null)
        {

            DataSet re = new DataSet();
            SqlConnection conn = _dbcontext.Database.Connection as SqlConnection;
            SqlDataAdapter dap = new SqlDataAdapter();

            
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                 
                cmd.CommandType = CommandType.Text;
                
                if(Parameters!=null)
                {
                    cmd.Parameters.AddRange(Parameters);
                }
                dap.SelectCommand = cmd;
                dap.Fill(re);
                return re;
            }

        }
        /// <summary>
        /// 执行无返回SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Parameters"></param>
        public int ExcuteSQLNoQuery(string sql, SqlParameter[] Parameters = null)
        {
            
            SqlConnection conn = _dbcontext.Database.Connection as SqlConnection;
            
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {

                cmd.CommandType = CommandType.Text;

                if (Parameters != null)
                {
                    cmd.Parameters.AddRange(Parameters);
                }
               return  cmd.ExecuteNonQuery();

            }
        }

        /// <summary>
        /// 返回多结果集
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public dynamic ExecProcedre(string sql,object parameter=null)
        {

            SqlConnection coonn = _dbcontext.Database.Connection as SqlConnection;

            return coonn.QueryMultiple(sql,parameter);
            
        }
        /// <summary>
        /// 返回执行存储过程的单一结果，哪果是多结果请使用参数 OUTPUT
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecProcedreResult(string sql, object parameters=null)
        {
            SqlConnection conn = _dbcontext.Database.Connection as SqlConnection;

            return conn.QueryMultiple(sql, parameters);
        }
        
    }
}
