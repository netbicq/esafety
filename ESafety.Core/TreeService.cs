using ESafety.Core;
using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.ORM;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core
{
    /// <summary>
    /// 树形结构处理
    /// </summary>
    public class TreeService : ServiceBase, ITree
    {
        private IUnitwork _work = null;
        
        public TreeService(IUnitwork work)
        {
            _work = work;
            Unitwork = work;

        }
        /// <summary>
        /// 获取子级ID集合
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rps"></param>
        /// <returns></returns>
        public IEnumerable<Guid> GetChildrenIds<T>(Guid id) where T:ModelBaseTree
        {
            try
            {

                List<Guid> re = new List<Guid>();
                re.Add(id);
                var rps = _work.Repository<T>();

                var orgall = from org in rps.Queryable(q => q.ParentID == id)
                             select org;

                foreach (var t in orgall)
                {
                    var ids = GetChildrenIds<T>(t.ID);
                    re.AddRange(ids);
                }

                return re;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取父级集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="rps"></param>
        /// <returns></returns>
        public IEnumerable<T> GetParents<T>(Guid id) where T:ModelBaseTree
        {
            List<T> re = new List<T>();

            var rps = _work.Repository<T>();

            var cnode = rps.GetModel(id);
            if (cnode != null)
            {
                var parent = rps.GetModel(q => q.ID == cnode.ParentID) as T;
                if (parent != null)
                {

                    re.Add(parent);
                    re.AddRange(GetParents<T>(parent.ID));
                }
            }

            return re;
        }
        /// <summary>
        /// 获取树形结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <typeparam name="B"></param>
        /// <returns></returns>
        public IEnumerable<T> GetTree<B,T>(Guid id) where T:TreeBase<ModelBaseTree> where B:ModelBaseTree 
        {
            List<T> re = new List<T>();
            var rps = _work.Repository<B>();

            var orgall = (from org in rps.Queryable(q => q.ParentID  == id)
                         select org);

            foreach (var org in orgall)
            {
                var orgtree = org.MAPTO<T>();
                orgtree.Children.AddRange(GetTree<B,T>(org.ID));
                re.Add(orgtree);
            }


            return re;
        }
    }
}
