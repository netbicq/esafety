using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core;
using ESafety.Core.Model;
using ESafety.Core.Model.DB.Account;
using ESafety.Core.Model.PARA;
using ESafety.ORM;
using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Service
{
    public class OpreationFlowService : ServiceBase, IOpreationFlowService
    {
        private IUnitwork _work = null;
        private IRepository<Basic_Opreation> _rpsopreation = null;
        private IRepository<Basic_OpreationFlow> _rpsof = null;
        private IRepository<Basic_Post> _rpspost = null;

        private IUserDefined usedefinedService = null;
        public OpreationFlowService(IUnitwork work, IUserDefined udf)
        {
            _work = work;
            Unitwork = work;
            _rpsopreation = work.Repository<Basic_Opreation>();
            _rpsof = work.Repository<Basic_OpreationFlow>();
            _rpspost = work.Repository<Basic_Post>();
            usedefinedService = udf;
        }
        /// <summary>
        /// 新建作业模型
        /// </summary>
        /// <param name="opreation"></param>
        /// <returns></returns>
        public ActionResult<bool> AddOpreation(OpreationNew opreation)
        {
            try
            {
                if (opreation == null)
                {
                    throw new Exception("参数有误");
                }
                var check = _rpsopreation.Any(q => q.Name ==opreation.Name);
                if (check)
                {
                    throw new Exception("已经存在相同的操作 ：" +opreation.Name);
                }
                var _opreation = opreation.MAPTO<Basic_Opreation>();
                var definedvalue = new UserDefinedBusinessValue
                {
                    BusinessID =_opreation.ID,
                    Values =opreation.UserDefineds
                };
                var defined = usedefinedService.SaveBuisnessValue(definedvalue);
                if (defined.state != 200)
                {
                    throw new Exception(defined.msg);
                }
                _rpsopreation.Add(_opreation);

                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 新建操作流程
        /// </summary>
        /// <param name="flowNew"></param>
        /// <returns></returns>
        public ActionResult<bool> AddOpreationFlow(OpreationFlowNew flowNew)
        {
            try
            {
                if (flowNew == null)
                {
                    throw new Exception("参数有误");
                }
                var check = _rpsopreation.Any(p=>p.ID==flowNew.OpreationID);
                if (!check)
                {
                    throw new Exception("未找到该操作");
                }
                check = _rpsof.Any(p=>p.PointName==flowNew.PointName);
                if (check)
                {
                    throw new Exception("该节点已存在:"+flowNew.PointName);
                }
                var _opreationflow = flowNew.MAPTO<Basic_OpreationFlow>();
                _rpsof.Add(_opreationflow);

                _work.Commit();
                return new ActionResult<bool>(true);

            }
            catch (Exception ex)
            {

                return new ActionResult<bool>(ex);
            }
        }

        public ActionResult<bool> DelOpreation(Guid id)
        {
            try
            {
                var dbemployee = _rpsopreation.Any(q => q.ID == id);
                if (!dbemployee)
                {
                    throw new Exception("该操作不存在!");
                }

                //作业务检查

                _rpsopreation.Delete(p => p.ID == id);
                //删除自定义项
                usedefinedService.DeleteBusinessValue(id);


                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 删除操作流程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelOpreationFlow(Guid id)
        {
            try
            {
                var dbemployee =_rpsof.Any(q => q.ID == id);
                if (!dbemployee)
                {
                    throw new Exception("该操作流程不存在!");
                }
               _rpsof.Delete(p => p.ID == id);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 修改操作模型
        /// </summary>
        /// <param name="opreation"></param>
        /// <returns></returns>
        public ActionResult<bool> EditOpreation(OpreationEdit opreation)
        {
            try
            {
                var dbopreation = _rpsopreation.GetModel(q => q.ID == opreation.ID);
                if (dbopreation == null)
                {
                    throw new Exception("未找到该操作信息");
                }
                var check = _rpsopreation.Any(p => p.ID != opreation.ID && p.Name == opreation.Name);
                if (check)
                {
                    throw new Exception("已经存在相同的操作：" + opreation.Name);
                }
                dbopreation =opreation.CopyTo<Basic_Opreation>(dbopreation);

                var definevalue = new UserDefinedBusinessValue
                {
                    BusinessID = dbopreation.ID,
                    Values = opreation.UserDefineds
                };
                usedefinedService.SaveBuisnessValue(definevalue);

                _rpsopreation.Update(dbopreation);


                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
      
        public ActionResult<Pager<OpreationView>> GetOpreationPage(PagerQuery<OpreationQuery> para)
        {
            throw new NotImplementedException();
        
        }
        /// <summary>
        /// 获取操作节点列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<OpreationFlowView>> GetOpreationFlows(Guid id)
        {
            try
            {
                var check = _rpsopreation.Any(q => q.ID == id);
                if (!check)
                {
                    throw new Exception("未找到该操作信息");
                }
                var dbof = _rpsof.Queryable(p=>p.OpreationID==id);
                var re = from s in dbof
                         select new OpreationFlowView
                         {
                             ID = s.ID,
                             OpreationID = s.OpreationID,
                             PointIndex = s.PointIndex,
                             PointMemo = s.PointMemo,
                             PointName = s.PointName,
                             PostName =_rpspost.GetModel(p=>p.ID==s.PostID).Name
                         };
                return new ActionResult<IEnumerable<OpreationFlowView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<OpreationFlowView>>(ex);
            }
        }
        /// <summary>
        /// 根据ID 获取操作模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<OpreationView> GetOpreation(Guid id)
        {
            try
            {
                var dbopreation = _rpsopreation.GetModel(id);
                if (dbopreation == null)
                {
                    throw new Exception("未找到该操作模型");
                }
                var re = dbopreation.MAPTO<OpreationView>();
                return new ActionResult<OpreationView>(re);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
