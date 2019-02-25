using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework;
using System.Data.Entity;
using EntityFramework.Extensions;
using EntityFramework.Future;
using System.Data;
using System.Linq.Expressions;
using System.Data.Entity.Validation;
using ESafety.Core.Model.DB;

namespace ESafety.ORM
{
    public class RepositoryEF<T> : IRepository<T> where T : ModelBase
    {
        private string errmessage = string.Empty;
        private readonly DbContext  _dbcontext ;
        private readonly DbSet<T> _dbSet;

        public RepositoryEF(DbContext db)
        {
            
            this._dbcontext = db;
            this._dbSet = this._dbcontext.Set<T>();
        }

        /// <summary>
        /// 批量新建
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public IEnumerable<T> Add(IEnumerable<T> entitys)
        {
            return _dbSet.AddRange(entitys);  
        }
        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Add(T entity)
        {
            return _dbSet.Add(entity);
        }
        /// <summary>
        /// 是否存在有任何内容
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool Any(Expression<Func<T, bool>> predicate = null)
        {
            return _dbSet.Any(predicate);
        }

        /// <summary>
        /// 是否存在指定ID的实体
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Any(Guid key)
        {
            return _dbSet.Any(q=>q.ID == key);
        }
        /// <summary>
        /// 删除条件内数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Delete(Expression<Func<T, bool>> predicate)
        {

            return _dbSet.Where(predicate).Delete();

        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Delete(T entity)
        {
            return _dbSet.Remove(entity);

        }

        /// <summary>
        /// 获取条件内的实体集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<T> GetList(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return _dbSet.AsEnumerable();
            }
            else
            {
                return _dbSet.Where(predicate);
            }
        }
        /// <summary>
        /// 获取指定ID的实体
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetModel(Guid key)
        {
            return _dbSet.FirstOrDefault(q=>q.ID == key);
        }
        /// <summary>
        /// 获条件内的实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T GetModel(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity"></param>

        public void Update(T entity)
        {
            try
            {
                 
                var entry = _dbcontext.Entry(entity);
                _dbSet.Attach(entity);
                entry.State = EntityState.Modified;               

            }
            catch (DbEntityValidationException dbEx)
            {

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errmessage += string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(errmessage, dbEx);
            }
        }
        /// <summary>
        /// 修改条件内实体
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="updater"></param>
        /// <returns></returns>
        public int Update(Expression<Func<T, bool>> filter, Expression<Func<T, T>> updater)
        {
            return _dbSet.Where(filter).Update(updater);
        }
        /// <summary>
        /// 返回指定条件内的queryable
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<T> Queryable(Expression<Func<T, bool>> predicate=null)
        {
            if(predicate!=null)
            {
                return _dbSet.Where(predicate);
            }
            else
            {
                return _dbSet.AsQueryable();
            }
        }
    }
}
