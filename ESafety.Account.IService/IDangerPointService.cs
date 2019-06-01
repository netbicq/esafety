using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.IService
{
    /// <summary>
    /// 风险点接口
    /// </summary>
    public interface IDangerPointService
    {
        /// <summary>
        /// 新建风险点
        /// </summary>
        /// <returns></returns>
        ActionResult<bool> AddDangerPoint(DangerPointNew pointNew);
        /// <summary>
        /// 新建风险点与主体的配置 
        /// </summary>
        /// <param name="relationNew"></param>
        /// <returns></returns>
        ActionResult<bool> AddDangerPointRelation(DangerPointRelationNew relationNew);
        /// <summary>
        /// 修改风险点
        /// </summary>
        /// <param name="pointEdit"></param>
        /// <returns></returns>
        ActionResult<bool> EditDangerPoint(DangerPointEdit pointEdit);
        /// <summary>
        /// 根据ID删除风险点
        /// </summary>
        /// <param name="pointID"></param>
        /// <returns></returns>
        ActionResult<bool> DelDangerPoint(Guid pointID);
        /// <summary>
        /// 删除配置关系
        /// </summary>
        /// <param name="relationID"></param>
        /// <returns></returns>
        ActionResult<bool> DelDangerPointRelation(Guid relationID);
        /// <summary>
        /// 根据ID获取模型
        /// </summary>
        /// <param name="pointID"></param>
        /// <returns></returns>
        ActionResult<DangerPointModel> GetDangerPointModel(Guid pointID);
        /// <summary>
        /// 分页获取风险点
        /// </summary>
        /// <param name="pointName"></param>
        /// <returns></returns>
        ActionResult<Pager<DangerPointView>> GetDangerPointPages(PagerQuery<DangerPointQuery> pointName);
        /// <summary>
        /// 根据风险点ID获取检查主体页
        /// </summary>
        /// <param name="pointID"></param>
        /// <returns></returns>
        ActionResult<Pager<DangerPointRelationView>> GetDangerPointRelationPages(PagerQuery<Guid> pointID);
        /// <summary>
        /// 获取风险点选择器
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<DangerPointSelector>> GetDangerPointSelector();
        /// <summary>
        /// 检查主体选择器
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<DangerPointRelationSelector>> GetDangerPointRelationSelector(DangerPointRelationSelect select);
        /// <summary>
        /// 批量获取二维码
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<QRCoder>> GetQRCoders(IEnumerable<Guid> pointIds);
        /// <summary>
        /// 根据风险点ID,获取危险因素选择器
        /// </summary>
        /// <param name="pointID"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<WXYSSelector>> GetWXYSSelectorByDangerPointId(Guid pointID);
        /// <summary>
        /// APP 端获取风险等级与对应等级的风险点的个数
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<DangerLevel>> GetDangerLevels();
        /// <summary>
        /// APP 根据风险点ID 端获取风险点
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ActionResult<Pager<APPDangerPointView>> GetDangerPointsPage(PagerQuery<Guid> query);

    }
}
