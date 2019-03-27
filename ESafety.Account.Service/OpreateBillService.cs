using ESafety.Account.IService;
using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core;
using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.DB.Account;
using ESafety.Core.Model.PARA;
using ESafety.ORM;
using ESafety.Unity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Service
{
    /// <summary>
    /// 作业申请单
    /// </summary>
    public class OpreateBillService : FlowBusinessService, IOpreateBill
    {

        private IUnitwork _work = null;

        private IRepository<Bll_OpreationBill> rpsOpreateBill = null;
        private IRepository<Bll_OpreateionBillFlow> rpsBillFlow = null;


        public OpreateBillService(IUnitwork work, IFlow flow) : base(work, flow)
        {

            _work = work;
            rpsOpreateBill = work.Repository<Bll_OpreationBill>();
            rpsBillFlow = work.Repository<Bll_OpreateionBillFlow>();
            Unitwork = work;

        }
        /// <summary>
        /// 新建作业申请单
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        public ActionResult<bool> AddNew(OperateBillNew bill)
        {
            try
            {
                var opreationmodel = _work.Repository<Core.Model.DB.Account.Basic_Opreation>().GetModel(bill.OpreationID);
                if (opreationmodel == null)
                {
                    throw new Exception("作业流程未找到");
                }

                var flows = _work.Repository<Core.Model.DB.Account.Basic_OpreationFlow>().Queryable(q => q.OpreationID == opreationmodel.ID).OrderBy(o => o.PointIndex);


                var billdb = bill.MAPTO<Core.Model.DB.Account.Bll_OpreationBill>();
                billdb.BillCode = Command.CreateCode();
                billdb.FlowsJson = JsonConvert.SerializeObject(flows);
                billdb.OpreationJSON = JsonConvert.SerializeObject(opreationmodel);
                billdb.State = (int)PublicEnum.BillFlowState.normal;
                billdb.CreateMan = AppUser.EmployeeInfo.CNName;

                _work.Repository<Core.Model.DB.Account.Bll_OpreationBill>().Add(billdb);
                _work.Commit();

                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 修改作业申请单
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        public ActionResult<bool> EditBill(OpreateBillEdit bill)
        {
            try
            {
                var dbbill = rpsOpreateBill.GetModel(bill.ID);
                if (dbbill == null)
                {
                    throw new Exception("作业申请单不存在");
                }
                if (dbbill.State != (int)PublicEnum.BillFlowState.normal)
                {
                    throw new Exception("作业申请单状态不允许修改");
                }
                var eddbbill = bill.CopyTo<Core.Model.DB.Account.Bll_OpreationBill>(dbbill);

                rpsOpreateBill.Update(eddbbill);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        public override ActionResult<bool> StartBillFlow(Guid businessid)
        {
            try
            {
                var businessmodel = rpsOpreateBill.GetModel(businessid);
                if (businessmodel == null)
                {
                    throw new Exception("任务不存在");
                }
                if (businessmodel.State >= (int)PublicEnum.BillFlowState.pending)
                {
                    throw new Exception("状态不支持");
                }

                var flowtask = base.StartFlow(new BusinessAprovePara
                {
                    BusinessID = businessmodel.ID,
                    BusinessType = PublicEnum.EE_BusinessType.InspectTask
                });
                if (flowtask.state != 200)
                {
                    throw new Exception(flowtask.msg);
                }
                var taskmodel = flowtask.data;
                if (taskmodel == null)//未定义审批流程
                {
                    //没有流程，直接为审批通过
                    businessmodel.State = (int)PublicEnum.BillFlowState.approved;

                    rpsOpreateBill.Update(businessmodel);
                    _work.Commit();
                }
                else
                {
                    //更新业务单据状态
                    businessmodel.State = (int)PublicEnum.BillFlowState.pending;
                    rpsOpreateBill.Update(businessmodel);

                    //写入审批流程起始任务
                    taskmodel.BusinessCode = businessmodel.BillCode;
                    taskmodel.BusinessDate = businessmodel.CreateDate;

                    _work.Repository<Flow_Task>().Add(taskmodel);

                    _work.Commit();

                }


                return new ActionResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        public override ActionResult<bool> Approve(Guid businessid)
        {
            try
            {
                var businessmodel = rpsOpreateBill.GetModel(businessid);
                if (businessmodel == null)
                {
                    throw new Exception("业务单据不存在");
                }
                if (businessmodel.State != (int)PublicEnum.BillFlowState.approved)
                {
                    throw new Exception("业务单据状态不支持");
                }

                //检查审批流程状态
                var flowcheck = base.BusinessAprove(new BusinessAprovePara
                {
                    BusinessID = businessid,
                    BusinessType = PublicEnum.EE_BusinessType.InspectTask
                });
                if (flowcheck.state != 200)
                {
                    throw new Exception(flowcheck.msg);
                }
                if (flowcheck.data)
                {
                    businessmodel.State = (int)PublicEnum.BillFlowState.audited;
                    rpsOpreateBill.Update(businessmodel);
                    _work.Commit();
                    return new ActionResult<bool>(true);
                }
                else
                {
                    throw new Exception("审批结果检查未通过");
                }
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 获取作业单模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<OpreateBillModel> GetModel(Guid id)
        {
            try
            {
                var dbmodel = rpsOpreateBill.GetModel(id);
                if (dbmodel == null)
                {
                    throw new Exception("作业单据不存在");
                }
                var re = dbmodel.MAPTO<OpreateBillModel>();
                var opreation = _work.Repository<Basic_Opreation>().GetModel(dbmodel.OpreationID);
                re.OpreationName = opreation == null ? "" : opreation.Name;
                re.StateName = Command.GetItems(typeof(PublicEnum.BillFlowState)).FirstOrDefault(q => q.Value == dbmodel.State).Caption;
                var emp = _work.Repository<Basic_Employee>().GetModel(p => p.ID == dbmodel.PrincipalEmployeeID);
                re.OrgID =emp==null?Guid.Empty:emp.OrgID;
                return new ActionResult<OpreateBillModel>(re);

            }
            catch (Exception ex)
            {
                return new ActionResult<OpreateBillModel>(ex);
            }
        }
        /// <summary>
        /// 获取作业单表列表
        /// </summary>
        /// <param name="Para"></param>
        /// <returns></returns>
        public ActionResult<Pager<OpreateBillModel>> GetList(PagerQuery<string> Para)
        {
            try
            {
                var bills = rpsOpreateBill.Queryable(q => q.BillName.Contains(Para.KeyWord) || Para.KeyWord == "");
                var opretions = _work.Repository<Basic_Opreation>().Queryable(q => bills.Select(s => s.OpreationID).Contains(q.ID));
                var emps = _work.Repository<Basic_Employee>().Queryable(q => bills.Select(s => s.PrincipalEmployeeID).Contains(q.ID));

                var retemp = from bill in bills
                             let opreation = opretions.FirstOrDefault(q => q.ID == bill.OpreationID)
                             let emp = emps.FirstOrDefault(q => q.ID == bill.PrincipalEmployeeID)
                             select new OpreateBillModel
                             {
                                 BillCode = bill.BillCode,
                                 OpreationID = bill.OpreationID,
                                 ID = bill.ID,
                                 Description=bill.Description,
                                 OrgID=emp.OrgID,
                                 BillLong = bill.BillLong,
                                 BillName = bill.BillName,
                                 CreateDate = bill.CreateDate,
                                 CreateMan = bill.CreateMan,
                                 EndTime = bill.EndTime,
                                 OpreationName = opreation == null ? "" : opreation.Name,
                                 PrincipalEmployeeID = bill.PrincipalEmployeeID,
                                 StartTime = bill.StartTime,
                                 State = (PublicEnum.BillFlowState)bill.State,
                                 StateName =
                                  bill.State == (int)PublicEnum.BillFlowState.approved ? "审批通过" :
                                  bill.State == (int)PublicEnum.BillFlowState.audited ? "已审核" :
                                  bill.State == (int)PublicEnum.BillFlowState.cancel ? "已作废" :
                                  bill.State == (int)PublicEnum.BillFlowState.check ? "已验收" :
                                  bill.State == (int)PublicEnum.BillFlowState.deny ? "已拒绝" :
                                  bill.State == (int)PublicEnum.BillFlowState.normal ? "待审批" :
                                  bill.State == (int)PublicEnum.BillFlowState.pending ? "审批中" :
                                  bill.State == (int)PublicEnum.BillFlowState.recalled ? "已撤回" : "未知",
                                 PrincipalEmployeeName = emp == null ? "" : emp.CNName
                             };

                var re = new Pager<OpreateBillModel>().GetCurrentPage(retemp, Para.PageSize, Para.PageIndex);
                return new ActionResult<Pager<OpreateBillModel>>(re);

            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<OpreateBillModel>>(ex);
            }
        }
        /// <summary>
        /// 删除指定id的作业单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelBill(Guid id)
        {
            try
            {
                var dbmodel = rpsOpreateBill.GetModel(id);
                if (dbmodel == null)
                {
                    throw new Exception("作业单据不存在");
                }
                if (dbmodel.State != (int)PublicEnum.BillFlowState.normal)
                {
                    throw new Exception("单据状态不允许操作");
                }

                rpsOpreateBill.Delete(dbmodel);
                _work.Repository<Flow_Result>().Delete(q => q.BusinessID == dbmodel.ID);

                _work.Commit();

                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 处理作业单流程
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        public ActionResult<bool> FlowResult(OpreateBillFlowResult flow)
        {
            try
            {
                if (flow == null)
                {
                    throw new Exception("参数有误");
                }
                if ((int)flow.FlowResult == 0)
                {
                    throw new Exception("处理结果数据有误");
                }
                var oflow = _work.Repository<Basic_OpreationFlow>().GetModel(q => q.ID == flow.OpreationFlowID);
                if (oflow == null)
                {
                    throw new Exception("作业流程节点不存在");
                }
                var opreationflow = _work.Repository<Basic_Opreation>().GetModel(oflow.OpreationID);
                if (opreationflow == null)
                {
                    throw new Exception("作业流程不存在");
                }

                if (flow.FlowResult == PublicEnum.OpreateFlowResult.reback && !opreationflow.IsBackReturn)
                {
                    throw new Exception("作业流和不支持回退");
                }
                var checkflow = rpsBillFlow.Any(q => q.FlowResult == (int)flow.FlowResult && q.OpreationFlowID == flow.OpreationFlowID);
                if (checkflow)
                {
                    throw new Exception("该作业单已经提交了该节点的处理结果");
                }
                var billmodel = rpsOpreateBill.GetModel(flow.BillID);
                if (billmodel == null)
                {
                    throw new Exception("业务单据不存在");
                }
                if (billmodel.State != (int)PublicEnum.BillFlowState.audited)
                {
                    throw new Exception("作业单状态不允许");
                }

                //所有节点集合，
                IEnumerable<Basic_OpreationFlow> points = JsonConvert.DeserializeObject<IEnumerable<Basic_OpreationFlow>>(billmodel.FlowsJson);

                bool writbill = false;

                switch (flow.FlowResult)
                {
                    case PublicEnum.OpreateFlowResult.stop: //终止
                        billmodel.State = (int)PublicEnum.BillFlowState.stop;
                        writbill = true;
                        break;
                    case PublicEnum.OpreateFlowResult.over: //完成，如果是最后一步则修改单据状态
                        var lastpointid = points.OrderByDescending(o => o.PointIndex).FirstOrDefault().ID;
                        if (flow.OpreationFlowID == lastpointid)
                        {
                            billmodel.State = (int)PublicEnum.BillFlowState.Over;
                            writbill = true;
                        }
                        break;
                    case PublicEnum.OpreateFlowResult.reback://退回到最后一步时则退回完成
                        var relastpointid = points.OrderBy(o => o.PointIndex).FirstOrDefault().ID;
                        if (flow.OpreationFlowID == relastpointid)
                        {
                            billmodel.State = (int)PublicEnum.BillFlowState.Reback;
                            writbill = true;
                        }
                        break;
                    default:
                        break;

                }

                var dbflow = flow.MAPTO<Bll_OpreateionBillFlow>();
                dbflow.BillID = billmodel.ID;
                dbflow.FlowEmployeeID = AppUser.EmployeeInfo.ID;
                dbflow.FlowTime = DateTime.Now;
                dbflow.ID = Guid.NewGuid();

                rpsBillFlow.Add(dbflow);
                if (writbill)
                {
                    rpsOpreateBill.Update(billmodel);
                }
                _work.Commit();

                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 获取带处理节点的单据模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<OpreateBillFlowModel> GetBillFlowModel(Guid id)
        {
            try
            {
                var billmodel = _work.Repository<Bll_OpreationBill>().GetModel(id);
                if (billmodel == null)
                {
                    throw new Exception("作业单据不存在");
                }
                var remodel = billmodel.MAPTO<OpreateBillFlowModel>();

                var points = JsonConvert.DeserializeObject<IEnumerable<Basic_OpreationFlow>>(billmodel.FlowsJson).OrderBy(o => o.PointIndex);

                var opreationmodel = JsonConvert.DeserializeObject<Basic_Opreation>(billmodel.OpreationJSON);
                var emp = _work.Repository<Basic_Employee>().GetModel(billmodel.PrincipalEmployeeID);

                //返回值的属性赋值
                remodel.OpreationName = opreationmodel.Name;
                remodel.PrincipalEmployeeName = emp == null ? "" : emp.CNName;
                remodel.StateName = Command.GetItems(typeof(PublicEnum.BillFlowState)).FirstOrDefault(q => q.Value == billmodel.State).Caption;

                //节点处理
                var flows = _work.Repository<Bll_OpreateionBillFlow>().Queryable(q => q.BillID == billmodel.ID);

                var postids = _work.Repository<Core.Model.DB.Account.Basic_PostEmployees>().Queryable(q => q.EmployeeID == AppUser.EmployeeInfo.ID).Select(s=>s.PostID).ToList();

                var reflows = from f in points
                              let uppoint = points.FirstOrDefault(q => q.ID == points.Where(p => p.PointIndex < f.PointIndex).OrderByDescending(o => o.PointIndex).FirstOrDefault().ID)
                              let nextpoint =points.FirstOrDefault(q=>q.ID == points.Where(p=>p.PointIndex >f.PointIndex ).OrderBy(o=>o.PointIndex).FirstOrDefault().ID)
                              select new OpreateBillFlow
                              {
                                  OpreationFlowID = f.ID,
                                  OpreationID = opreationmodel.ID,
                                  PointIndex = f.PointIndex,
                                  PointName = f.PointName,
                                  PostID = f.PostID,
                                  PostName = f.PointName,
                                  FlowUEModel = new OpreateFlowUEModel
                                  {
                                      FinishEnable =
                                         //单据状态为结束装态，处理按钮不可有
                                         (billmodel.State == (int)PublicEnum.BillFlowState.stop ||
                                         billmodel.State == (int)PublicEnum.BillFlowState.Reback ||
                                         billmodel.State == (int)PublicEnum.BillFlowState.Over) ? false
                                         ://如果本节点已经存在了记录，按钮不允许处理
                                         flows.Any(q => q.OpreationFlowID == f.ID) ? false
                                         : //如果存在上级节点，且上级节点没有完成记录则不可用
                                         uppoint != null && !flows.Any(q => q.OpreationFlowID == uppoint.ID) ? false
                                         ://如果当前人员不在节点的岗位，不可用
                                         !postids.Contains(f.PostID)?false
                                         :
                                         true,
                                      StopEnable =//单据状态为结束装态，处理按钮不可有
                                         (billmodel.State == (int)PublicEnum.BillFlowState.stop ||
                                         billmodel.State == (int)PublicEnum.BillFlowState.Reback ||
                                         billmodel.State == (int)PublicEnum.BillFlowState.Over) ? false
                                         ://如果存在上一级且上级没有完成记录数据处理 不可用
                                         uppoint != null && !flows.Any(q => q.OpreationFlowID == uppoint.ID && q.FlowResult ==(int)PublicEnum.OpreateFlowResult.over) ? false
                                         ://如果存在终止记录数据处理，不可用
                                         flows.Any(q=>q.OpreationFlowID == f.ID && q.FlowResult ==(int)PublicEnum.OpreateFlowResult.stop)?false
                                         ://如果当前人员不在节点的岗位，不可用
                                         !postids.Contains(f.PostID) ? false
                                         : true,
                                      ReBackEnable =//单据状态为结束装态，处理按钮不可有
                                         (billmodel.State == (int)PublicEnum.BillFlowState.stop ||
                                         billmodel.State == (int)PublicEnum.BillFlowState.Reback ||
                                         billmodel.State == (int)PublicEnum.BillFlowState.Over) ? false
                                         ://如果存在上级且上级没有完成记录数据处理 不可用
                                         uppoint !=null && !flows.Any(q=>q.OpreationFlowID == uppoint.ID)?false
                                         ://如果当前人员不在节点的岗位，不可用
                                         !postids.Contains(f.PostID) ? false
                                         : true,
                                      LeftLine =
                                         flows.Any(q => q.OpreationFlowID == f.ID) ? true
                                         : false,
                                      RightLien =
                                         flows.Any(q => q.OpreationFlowID == f.ID &&
                                         q.FlowResult == (int)PublicEnum.OpreateFlowResult.reback) ? true : false

                                  }
                              };


                return new ActionResult<OpreateBillFlowModel>(remodel);
            }
            catch (Exception ex)
            {
                return new ActionResult<OpreateBillFlowModel>(ex);
            }
        }
    }
}
