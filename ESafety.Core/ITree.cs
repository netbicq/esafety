using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core
{
    /// <summary>
    /// 树
    /// </summary>
    public interface ITree
    {
        /// <summary>
        /// 获取树
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rps"></param>
        /// <returns></returns>
        IEnumerable<T> GetTree<B,T>(Guid id) where T : TreeBase<ModelBaseTree> where B:ModelBaseTree;
        /// <summary>
        /// 获取子级id集合
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rps"></param>
        /// <returns></returns>
        IEnumerable<Guid> GetChildrenIds<T>(Guid id) where T :ModelBaseTree;
        /// <summary>
        /// 获取所有父级
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="rps"></param>
        /// <returns></returns>
        IEnumerable<T> GetParents<T>(Guid id) where T : ModelBaseTree;
    }
}
