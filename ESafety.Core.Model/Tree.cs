using ESafety.Core.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model
{

    /// <summary>
    /// 树形结构基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TreeBase<T>:ModelBaseTree where T:ModelBaseTree
    {
        /// <summary>
        /// 子级
        /// </summary>
        public List<T> Children { get; set; }
    }

    /// <summary>
    /// 组织架构树形结构
    /// </summary> 
    public class OrgTree:TreeBase<ModelBaseTree>
    {

        public OrgTree()
        {
            Children = new List<ModelBaseTree>();
        }
        /// <summary>
        /// 责任人电话
        /// </summary>
        public string PrincipalTel { get; set; }
        /// <summary>
        /// 责任人
        /// </summary>
        public string Principal { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string OrgName { get; set; }
        /// <summary>
        /// 级次
        /// </summary>
        public int Level { get; set; } 
    }
     


   

}
