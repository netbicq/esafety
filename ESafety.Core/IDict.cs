using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.PARA;
using ESafety.Core.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core
{
    /// <summary>
    /// 词典
    /// </summary>
    public interface IDict
    {
        /// <summary>
        /// 获取词典类型集合
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable< DictTypeView>> GetDictTypes();
        /// <summary>
        /// 根据词典类型获取词典列表
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns></returns>

        ActionResult<Pager<Basic_Dict>> GetDictsByType(PagerQuery<Guid> para);

        /// <summary>
        /// 根据id获取词典模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<Basic_Dict> GetDictModel(Guid id);
        /// <summary>
        /// 新建词典
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        ActionResult<bool> AddDict(DictNew dict);
        /// <summary>
        /// 修改词典
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        ActionResult<bool> EditDict(DictEidt dict);
        /// <summary>
        /// 删除词典
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<bool> DelDict(Guid id);
        /// <summary>
        /// 新建词典类型
        /// </summary>
        /// <param name="dicttype"></param>
        /// <returns></returns>
        ActionResult<bool> AddDictType(DictTypeNew dicttype);
        /// <summary>
        /// 删除词典类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<bool> DelDictType(Guid id);

    }
}
