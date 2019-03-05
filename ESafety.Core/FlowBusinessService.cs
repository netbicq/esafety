using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.PARA;
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
    /// 业务审批基类
    /// </summary>
    public abstract class FlowBusinessService : ServiceBase, IFlowBusiness
    {

        private IUnitwork _work = null;
        private IFlow srvFlow = null;

        public FlowBusinessService(IUnitwork work, IFlow flow)
        {
            _work = work;
            Unitwork = work;
            srvFlow = flow;
        }
        /// <summary>
        /// 业务审核
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<bool> BusinessAprove(BusinessAprovePara para)
        {
            try
            {

                var flowtaskcheck = _work.Repository<Flow_Task>().Any(q => q.BusinessID == para.BusinessID);//有无审批任务正在进行
                if (flowtaskcheck)
                {
                    throw new Exception("审批流程尚未完成");
                }

                //检查审批结果
               
                //是否需要审批
                var isflowcheck = srvFlow.CheckBusinessFlow(para.BusinessType);
                if (isflowcheck.state != 200)
                {
                    throw new Exception(isflowcheck.msg);
                }
                if (isflowcheck.data) //如果要审批则检查
                {
                    //取最大的审批版本
                    var maxversion = _work.Repository<Flow_Result>().Queryable(q => q.BusinessID == para.BusinessID).Max(m => m.FlowVersion);
                    var flowovercheck = _work.Repository<Flow_Result>().Any(q => q.BusinessID == para.BusinessID && q.FlowVersion == maxversion && q.FlowResult == (int)PublicEnum.EE_FlowResult.Over);
                    if (!flowovercheck)//最新审批版本如果没有找到over则表示审批流程未全部通过
                    {
                        throw new Exception("审批结果不支持对业务审核操作");
                    }
                }

                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 发起业务审批，返回业务审批的状态
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Flow_Task> StartFlow(BusinessAprovePara para)
        {
            try
            {
                var initpara = new InitTask
                {
                    BusinessID = para.BusinessID,
                    BusinessType = para.BusinessType
                };

                var flowtask = srvFlow.InitTask(initpara);
                if (flowtask.state != 200)
                {
                    throw new Exception(flowtask.msg);
                }
                return flowtask;

            }
            catch (Exception ex)
            {
                return new ActionResult<Flow_Task>(ex);
            }
        }
        /// <summary>
        /// 业务单据发起审批流程
        /// </summary>
        /// <param name="businessid"></param>
        /// <returns></returns>
        public virtual  ActionResult<bool> FlowStart<T>(T rps, Guid businessid) where T:IRepository<ModelBase>
        {
            try
            {

            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 审核业务单据
        /// </summary>
        /// <param name="businessid"></param>
        /// <returns></returns>
        public virtual ActionResult<bool> ApproveBill<T> (T rps, Guid businessid) where T:IRepository<ModelBase>
        {
            try
            {

            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }


    }
}
