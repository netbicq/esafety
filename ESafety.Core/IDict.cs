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

        /// <summary>
        /// 制度类型
        /// </summary>
        ActionResult<IEnumerable<Basic_Dict>> GetDocRegime();
        /// <summary>
        /// 预案类型
        /// </summary>
        ActionResult<IEnumerable<Basic_Dict>> GetDocSlution();

        /// <summary>
        /// 资质类型
        /// </summary>
        ActionResult<IEnumerable<Basic_Dict>> GetDocLicense();
        /// <summary>
        /// 风险等级
        /// </summary>
        ActionResult<IEnumerable<Basic_Dict>> GetDangerLevel();
        /// <summary>
        /// 隐患等级
        /// </summary>
        ActionResult<IEnumerable<Basic_Dict>> GetTroubleLevel();
        /// <summary>
        /// 危害因素
        /// </summary>
        ActionResult<IEnumerable<Basic_Dict>> GetEval_WHYS();
        /// <summary>
        /// 事故类型
        /// </summary>
        ActionResult<IEnumerable<Basic_Dict>> GetEval_SGLX();
        /// <summary>
        /// 事故后果
        /// </summary>
        ActionResult<IEnumerable<Basic_Dict>> GetEval_SGJG();
        /// <summary>
        /// 影响范围
        /// </summary>
        ActionResult<IEnumerable<Basic_Dict>> GetEval_YXFW();
        /// <summary>
        /// LECD_L
        /// </summary>
        ActionResult<IEnumerable<Basic_Dict>> GetEval_LECD_L();
        /// <summary>
        /// LECD_E
        /// </summary>
        ActionResult<IEnumerable<Basic_Dict>> GetEval_LECD_E();
        /// <summary>
        /// LECD_C
        /// </summary>
        ActionResult<IEnumerable<Basic_Dict>> GetEval_LECD_C();
        /// <summary>
        /// LSD_L
        /// </summary>
        ActionResult<IEnumerable<Basic_Dict>> GetEval_LSD_L();
        /// <summary>
        /// LSD_S
        /// </summary>
        ActionResult<IEnumerable<Basic_Dict>> GetEval_LSD_S();

    }
}
