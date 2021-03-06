using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.PARA;
using ESafety.Core.Model.View;
using ESafety.ORM;
using ESafety.Unity;
using Quick.WXHelper;
using Quick.WXHelper.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core
{
    /// <summary>
    /// 审批类
    /// </summary>
    public class FlowService : ServiceBase, IFlow
    {
        private IUnitwork _work = null;

        private IRepository<Flow_Master> rpsFlowMaster = null;
        private IRepository<Flow_Points> rpsPoint = null;
        private IRepository<Flow_PointUsers> rpsPointUser = null;
        private IRepository<Flow_Task> rpsTask = null;
        private IRepository<Model.DB.Flow_Result> rpsResult = null;
        private IRepository<Basic_Employee> rpsEmployee = null;


        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="work"></param>
        public FlowService(IUnitwork work)
        {
            _work = work;
            Unitwork = work;
            rpsPoint = _work.Repository<Flow_Points>();
            rpsPointUser = _work.Repository<Flow_PointUsers>();
            rpsTask = _work.Repository<Flow_Task>();
            rpsResult = _work.Repository<Model.DB.Flow_Result>();
            rpsEmployee = _work.Repository<Basic_Employee>();
            rpsFlowMaster = _work.Repository<Flow_Master>();


        }
        /// <summary>
        /// 新建流程Master
        /// </summary>
        /// <param name="flowMasterNew"></param>
        /// <returns></returns>
        public ActionResult<bool> AddFlowMaster(FlowMasterNew flowMasterNew)
        {
            try
            {
                if (flowMasterNew == null)
                {
                    throw new Exception("参数错误!");
                }
                var check = rpsFlowMaster.Any(p => p.Name == flowMasterNew.Name && p.BusinessType == (int)flowMasterNew.BusinessType);
                if (check)
                {
                    throw new Exception("该业务类型下，已存在名：" + flowMasterNew.Name + "的Master!");
                }
                var flowMaster = flowMasterNew.MAPTO<Flow_Master>();
                rpsFlowMaster.Add(flowMaster);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 新建审批节点
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public ActionResult<bool> AddFlowPoint(Flow_PointsNew point)
        {
            try
            {
                var checkpoint = rpsPoint.Any(q => q.MasterID == point.MasterID && q.PointName == point.PointName);
                if (checkpoint)
                {
                    throw new Exception("同一流程Master下存在节点名称:" + point.PointName + "已经存在");
                }
                var checkindex = rpsPoint.Any(q => q.MasterID == point.MasterID && q.PointIndex == point.PointIndex);
                if (checkindex)
                {
                    throw new Exception("同一流程Master下存在一样的节点顺序：" + point.PointIndex.ToString());
                }

                var dbpoint = point.CopyTo<Flow_Points>(new Flow_Points());
                rpsPoint.Add(dbpoint);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 新建节点审批顺序
        /// </summary>
        /// <param name="pointuser"></param>
        /// <returns></returns>
        public ActionResult<bool> AddPointUser(Flow_PointUsersNew pointuser)
        {
            try
            {
                var point = rpsPoint.GetModel(pointuser.PointID);
                if (point == null)
                {
                    throw new Exception("节点不存在");
                }
                var usercheck = rpsPointUser.Any(q => q.PointID == pointuser.PointID && q.PointUser == pointuser.PointUser);
                if (point.PointType == (int)PublicEnum.EE_FlowPointType.Multi && usercheck)
                {
                    throw new Exception("节点已经存在用户：" + pointuser.PointUser);
                }

                var indexcheck = rpsPointUser.Any(q => q.PointID == pointuser.PointID && q.UserIndex == pointuser.UserIndex);
                if (point.PointType == (int)PublicEnum.EE_FlowPointType.Multi && indexcheck)
                {
                    throw new Exception("节点已经存在相同的顺序");
                }

                usercheck = rpsPointUser.Any(q => q.PointID == pointuser.PointID);
                if (point.PointType == (int)PublicEnum.EE_FlowPointType.Generic && usercheck)
                {
                    throw new Exception("节点为普通审批，已存在审批人!");
                }
                var dbpointUser = pointuser.CopyTo<Flow_PointUsers>(new Flow_PointUsers());

                rpsPointUser.Add(dbpointUser);
                _work.Commit();

                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 审批任务
        /// </summary>
        /// <param name="approve"></param>
        /// <returns></returns>
        public ActionResult<PublicEnum.EE_FlowApproveResult> Approve(Approve approve)
        {
            try
            {
                PublicEnum.EE_FlowApproveResult approveResult;

                var task = rpsTask.GetModel(approve.TaskID);
                if (task == null)
                {
                    throw new Exception("审批任务不存在");
                }
                if (task.TaskUser != AppUser.UserInfo.Login)
                {
                    throw new Exception("任务提交用户不一致");
                }
                var currentpoint = rpsPoint.GetModel(q => q.ID == task.PointID);//当前审批节点
                if (currentpoint == null)
                {
                    throw new Exception("审批节点有误");
                }
                Model.DB.Flow_Result result; //审批结果
                result = new Model.DB.Flow_Result
                {
                    ApplyUser = task.ApplyUser,
                    BusinessID = task.BusinessID,
                    BusinessCode = task.BusinessCode,
                    BusinessDate = task.BusinessDate,
                    BusinessType = task.BusinessType,
                    FlowDate = DateTime.Now,
                    FlowMemo = approve.FlowMemo,
                    FlowResult = (int)approve.FlowResult,
                    FlowUser = task.TaskUser,
                    FlowVersion = task.FlowVersion,
                    ID = Guid.NewGuid(),
                    PointName = currentpoint.PointName,
                    MasterID=task.MasterID
                };
                Flow_Task nexttask;//下一任务

                if (approve.FlowResult == PublicEnum.EE_FlowResult.Pass) //如果拒绝，直接干掉任务，写入审批日志即可 
                {
                    var currenuser = rpsPointUser.GetModel(q => q.PointID == task.PointID && q.PointUser == task.TaskUser); //当前节点
                    if (currenuser == null)
                    {
                        throw new Exception("审批节点用户数据有误");
                    }

                    Flow_PointUsers nexuser;//下一个任务的用户
                    var cpnextuser = rpsPointUser.Queryable(q => q.PointID == currentpoint.ID && q.UserIndex > currenuser.UserIndex).OrderBy(o => o.UserIndex).FirstOrDefault();
                    if (cpnextuser != null) //当前节点存在有下一位用户则发起该用户任务
                    {
                        nexuser = cpnextuser;
                        nexttask = new Flow_Task
                        {
                            ApplyUser = task.ApplyUser,
                            BusinessID = task.BusinessID,
                            BusinessType = task.BusinessType,
                            FlowVersion = task.FlowVersion,
                            BusinessCode = task.BusinessCode,
                            BusinessDate = task.BusinessDate,
                            ID = Guid.NewGuid(),
                            PointID = task.PointID,
                            TaskDate = DateTime.Now,
                            TaskUser = nexuser.PointUser,
                            MasterID = task.MasterID
                        };
                        rpsTask.Add(nexttask);
                        approveResult = PublicEnum.EE_FlowApproveResult.normal;
                        /******************************发送审批流程信息**************************************/
                        var aemp = _work.Repository<Basic_Employee>().GetModel(p => p.Login == nexttask.ApplyUser);
                        var msgToUser = _work.Repository<Auth_User>().GetModel(p => p.Login == nexttask.TaskUser);
                        if (!string.IsNullOrEmpty(msgToUser.openID))
                        {
                            var aorg = _work.Repository<Basic_Org>().GetModel(aemp.OrgID);

                            var msg = nexttask.BusinessType == (int)PublicEnum.EE_BusinessType.Apply ? "作业申请"
                                    : nexttask.BusinessType == (int)PublicEnum.EE_BusinessType.InspectTask ? "巡检任务"
                                    : nexttask.BusinessType == (int)PublicEnum.EE_BusinessType.TempTask ? "临时任务"
                                    : "";
                            var sendData = new Dictionary<string, MessageDataBase>();
                            sendData.Add("first", new MessageDataBase { value = "您有一个待审批事项" });
                            sendData.Add("keyword1", new MessageDataBase { value = nexttask.BusinessCode });
                            sendData.Add("keyword2", new MessageDataBase { value = nexttask.BusinessDate.ToString("yyyy-MM-dddd HH:mm:ss") });
                            sendData.Add("keyword3", new MessageDataBase { value = aemp.CNName });
                            sendData.Add("keyword4", new MessageDataBase { value = aorg.OrgName });
                            sendData.Add("keyword5", new MessageDataBase { value = $"您有一个审批流程类型为{msg}的流程需要审批！" });
                            sendData.Add("remark", new MessageDataBase { value = "ESF微服为安全护航。" });
                            var Msg = new TemplateMessagePara
                            {
                                template_id = "lMtAv69HdQsPg1A-mtQQ4mcoLQ-zvkHs9hBMsXnkwso",
                                touser = msgToUser.openID,
                                data = sendData,
                                url = "http://weixin.bjjtza.com/ApprovalPending"
                            };
                            WxService.SendTemplateMessage(Msg);
                        }
                        /************************************************************************/

                    }
                    else //当前节点没有下一位用户，则找下一节点
                    {
                        var nextpoint = rpsPoint.Queryable(q => q.BusinessType == currentpoint.BusinessType && q.PointIndex > currentpoint.PointIndex).OrderBy(o => o.PointIndex).FirstOrDefault();

                        var isnext = nextpoint != null; //是否存在下一个审批节点
                        if (isnext) //存在下一节点
                        {
                            //根据节点找用户
                            nexuser = rpsPointUser.Queryable(q => q.PointID == nextpoint.ID && (
                           (nextpoint.PointType == (int)PublicEnum.EE_FlowPointType.Generic) ||
                           (nextpoint.PointType == (int)PublicEnum.EE_FlowPointType.Multi && q.UserIndex >= 0))).OrderBy(o => o.UserIndex).FirstOrDefault();
                            if (nexuser == null)
                            {
                                throw new Exception("审批用户数据有误");
                            }
                            nexttask = new Flow_Task
                            {
                                ApplyUser = task.ApplyUser,
                                BusinessID = task.BusinessID,
                                BusinessType = task.BusinessType,
                                FlowVersion = task.FlowVersion,
                                BusinessCode = task.BusinessCode,
                                BusinessDate = task.BusinessDate,
                                ID = Guid.NewGuid(),
                                PointID = nextpoint.ID,
                                TaskDate = DateTime.Now,
                                TaskUser = nexuser.PointUser,
                                MasterID = task.MasterID
                            };
                            /******************************发送审批流程信息**************************************/
                            var aemp = _work.Repository<Basic_Employee>().GetModel(p => p.Login == nexttask.ApplyUser);
                            var msgToUser = _work.Repository<Auth_User>().GetModel(p => p.Login == nexttask.TaskUser);
                            if (!string.IsNullOrEmpty(msgToUser.openID))
                            {
                                var aorg = _work.Repository<Basic_Org>().GetModel(aemp.OrgID);

                                var msg = nexttask.BusinessType == (int)PublicEnum.EE_BusinessType.Apply ? "作业申请"
                                        : nexttask.BusinessType == (int)PublicEnum.EE_BusinessType.InspectTask ? "巡检任务"
                                        : nexttask.BusinessType == (int)PublicEnum.EE_BusinessType.TempTask ? "临时任务"
                                        : "";
                                var sendData = new Dictionary<string, MessageDataBase>();
                                sendData.Add("first", new MessageDataBase { value = "您有一个待审批事项" });
                                sendData.Add("keyword1", new MessageDataBase { value = nexttask.BusinessCode });
                                sendData.Add("keyword2", new MessageDataBase { value = nexttask.BusinessDate.ToString("yyyy-MM-dddd HH:mm:ss") });
                                sendData.Add("keyword3", new MessageDataBase { value = aemp.CNName });
                                sendData.Add("keyword4", new MessageDataBase { value = aorg.OrgName });
                                sendData.Add("keyword5", new MessageDataBase { value = $"您有一个审批流程类型为{msg}的流程需要审批！" });
                                sendData.Add("remark", new MessageDataBase { value = "ESF微服为安全护航。" });
                                var Msg = new TemplateMessagePara
                                {
                                    template_id = "lMtAv69HdQsPg1A-mtQQ4mcoLQ-zvkHs9hBMsXnkwso",
                                    touser = msgToUser.openID,
                                    data = sendData,
                                    url = "http://weixin.bjjtza.com/ApprovalPending"
                                };
                                WxService.SendTemplateMessage(Msg);
                            }

                            /************************************************************************/
                            rpsTask.Add(nexttask);
                            approveResult = PublicEnum.EE_FlowApproveResult.normal;
                        }
                        else//不存在一下节点
                        {
                            result.FlowResult = (int)PublicEnum.EE_FlowResult.Over; //审批结束了
                            approveResult = PublicEnum.EE_FlowApproveResult.over; //返回审批结束的审批结果 


                            //审批的最后节点，直接实例化，不使用注入

                            var bservice = new FlowBusinessService(Unitwork, this);
                            bservice.AppUser = AppUser;
                            bservice.ACOptions = ACOptions;

                            var overresult = bservice.BusinessOver(task.BusinessID, (PublicEnum.EE_BusinessType)task.BusinessType);

                            if (overresult.state != 200)
                            {
                                throw new Exception(overresult.msg);
                            }
                        }
                    }

                }
                else
                {
                    approveResult = PublicEnum.EE_FlowApproveResult.deny;

                }

                rpsResult.Add(result);//当前任务的审批结果

                rpsTask.Delete(task);//删除当前用户的审批任务


                _work.Commit();

                return new ActionResult<PublicEnum.EE_FlowApproveResult>(approveResult);
            }
            catch (Exception ex)
            {
                return new ActionResult<PublicEnum.EE_FlowApproveResult>(ex);
            }
        }


        /// <summary>
        /// 检查业务是否需要审批流程
        /// </summary>
        /// <param name="masterID"></param>
        /// <returns></returns>
        public ActionResult<bool> CheckBusinessFlow(Guid masterID)
        {
            try
            {
                var check = rpsPoint.Any(q => q.MasterID == masterID);
                return new ActionResult<bool>(check);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 删除流程Master
        /// </summary>
        /// <param name="masterID"></param>
        /// <returns></returns>
        public ActionResult<bool> DelFlowMaster(Guid masterID)
        {
            try
            {
                var dbFlowMaster = rpsFlowMaster.GetModel(masterID);
                if (dbFlowMaster == null)
                {
                    throw new Exception("流程Master不存在！");
                }
                var check = rpsPoint.Any(p => p.MasterID == masterID);
                if (check)
                {
                    throw new Exception("流程Master下,存在节点无法删除!");
                }

                rpsFlowMaster.Delete(dbFlowMaster);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 删除审批节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelFlowPoint(Guid id)
        {
            try
            {
                var dbpoint = rpsPoint.GetModel(id);
                if (dbpoint == null)
                {
                    throw new Exception("节点不存在");
                }
                var checkusr = rpsPointUser.Any(q => q.PointID == id);
                if (checkusr)
                {
                    throw new Exception("节点下存在用户，请先删除用户");
                }
                var checktask = rpsTask.Any(q => q.BusinessType == dbpoint.BusinessType);
                if (checktask)
                {
                    throw new Exception("有业务单据正在审批中，不允许删除");
                }
                rpsPoint.Delete(dbpoint);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 删除审批用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelPointUser(Guid id)
        {
            try
            {
                var dbpointuser = rpsPointUser.GetModel(id);
                if (dbpointuser == null)
                {
                    throw new Exception("审批用户不存在");
                }
                var point = rpsPoint.GetModel(dbpointuser.PointID);
                var task = rpsTask.Any(q => q.PointID == point.ID);
                if (task)
                {
                    throw new Exception("审批流程有正在审批的业务单据");
                }

                rpsPointUser.Delete(dbpointuser);
                _work.Commit();
                return new ActionResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 修改流程Master
        /// </summary>
        /// <param name="flowMasterEdit"></param>
        /// <returns></returns>
        public ActionResult<bool> EditFlowMaster(FlowMasterEdit flowMasterEdit)
        {
            try
            {
                var dbFlowMaster = rpsFlowMaster.GetModel(flowMasterEdit.MasterID);
                if (dbFlowMaster == null)
                {
                    throw new Exception("未找到所需修改项!");
                }
                var check = rpsFlowMaster.Any(p => p.Name == flowMasterEdit.Name && p.BusinessType == (int)dbFlowMaster.BusinessType && p.ID != flowMasterEdit.MasterID);
                if (check)
                {
                    throw new Exception("该业务类型下，已存在名：" + flowMasterEdit.Name + "的Master!");
                }
                dbFlowMaster.Name = flowMasterEdit.Name;
                rpsFlowMaster.Update(dbFlowMaster);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 修改审批节点
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public ActionResult<bool> EditFlowPoint(Flow_PointsEdit point)
        {
            try
            {
                var dbpoint = rpsPoint.GetModel(point.ID);
                if (dbpoint == null)
                {
                    throw new Exception("审批节点不存在");
                }
                var taskcheck = rpsTask.Any(q => q.PointID == dbpoint.ID);
                if (taskcheck)
                {
                    throw new Exception("已经存在正在审批的业务单据");
                }
                dbpoint = point.CopyTo<Flow_Points>(dbpoint);



                rpsPoint.Update(dbpoint);
                _work.Commit();

                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 修改审批节点用户
        /// </summary>
        /// <param name="pointuser"></param>
        /// <returns></returns>
        public ActionResult<bool> EditPointUser(Flow_PointUserEdit pointuser)
        {
            try
            {
                var dbpointuser = rpsPointUser.GetModel(q => q.ID == pointuser.ID);
                if (dbpointuser == null)
                {
                    throw new Exception("审批用户不存在");
                }
                var checktask = rpsTask.Any(q => q.PointID == dbpointuser.PointID);
                if (checktask)
                {
                    throw new Exception("有正在审批的业务单据");
                }
                var usercheck = rpsPointUser.Any(q => q.ID != pointuser.ID && q.PointUser == pointuser.PointUser && q.PointID == pointuser.PointID);
                if (checktask)
                {
                    throw new Exception("相同审批节点已经存在用户：" + pointuser.PointUser);
                }
                var point = rpsPoint.GetModel(q => q.ID == pointuser.PointID);

                var indexcheck = rpsPointUser.Any(q => q.ID != pointuser.ID && q.UserIndex == pointuser.UserIndex);
                if (indexcheck && point.PointType == (int)PublicEnum.EE_FlowPointType.Multi)
                {
                    throw new Exception("相同审批节点已经存在用户顺序");
                }

                dbpointuser = pointuser.CopyTo<Flow_PointUsers>(dbpointuser);
                rpsPointUser.Update(dbpointuser);
                _work.Commit();

                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 审批撤回
        /// </summary>
        /// <param name="recall"></param>
        /// <returns></returns>
        public ActionResult<bool> FlowRecall(FlowRecall recall)
        {
            try
            {
                var dbtask = rpsTask.GetModel(q => q.BusinessID == recall.BusinessID && q.FlowVersion == recall.FlowVersion);
                if (dbtask == null)
                {
                    throw new Exception("审批状态不允许撤回");
                }
                var point = rpsPoint.GetModel(q => q.ID == dbtask.PointID);

                var dbresult = new Model.DB.Flow_Result()
                {
                    ID = Guid.NewGuid(),
                    BusinessCode = dbtask.BusinessCode,
                    BusinessDate = DateTime.Now,
                    ApplyUser = AppUser.UserInfo.Login,
                    BusinessID = recall.BusinessID,
                    BusinessType = dbtask.BusinessType,
                    FlowDate = DateTime.Now,
                    FlowMemo = "撤回审批",
                    FlowResult = (int)PublicEnum.EE_FlowResult.BackR,
                    FlowUser = AppUser.UserInfo.Login,
                    FlowVersion = dbtask.FlowVersion,
                    PointName = point.PointName
                };

                rpsTask.Delete(dbtask);
                rpsResult.Add(dbresult);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 获取业务类型集合
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<EnumItem>> GetBusinessTypes()
        {
            try
            {
                var re = Command.GetItems(typeof(PublicEnum.EE_BusinessType));
                return new ActionResult<IEnumerable<EnumItem>>(re);

            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<EnumItem>>(ex);
            }
        }
        /// <summary>
        /// 获取审批日志
        /// </summary>
        /// <param name="businessid"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<FlowLogView>> GetFlowLog(Guid businessid)
        {
            try
            {
                //审批日志
                var logs = rpsResult.Queryable(q => q.BusinessID == businessid).ToList();
                //检查是否有日志
                var task = rpsTask.Queryable(q => q.BusinessID == businessid).ToList();

                var check = logs.Any() || task.Any();

                if (!check)
                {
                    throw new Exception("没有任何审批日志");
                }
                var btype = logs.Any() ? logs[0].BusinessType : task.Any() ? task[0].BusinessType : 0;
                if (btype == 0)
                {
                    throw new Exception("数据有误");
                }
                var masterID = logs[0].MasterID;
                //审批节点
                var points = rpsPoint.Queryable(q => q.BusinessType == btype&&q.MasterID==masterID).OrderBy(o => o.PointIndex).ToList();
                var pointsids = points.Select(s => s.ID);
                //审批用户
                var pointuser = rpsPointUser.Queryable(q => pointsids.Contains(q.PointID));
                //职员
                var empids = logs.Select(s => s.FlowUser);
                var emps = _work.Repository<Basic_Employee>().Queryable(q => empids.Contains(q.Login)).ToList();

                var re = from lg in logs
                         group lg by lg.FlowVersion into g
                         select new FlowLogView
                         {
                             FlowVersion = g.Key,
                             Logs = from p in points
                                    select new FlowLogPoint
                                    {
                                        PointName = p.PointName,
                                        Users = from u in pointuser.Where(q => q.PointID == p.ID).OrderBy(o => o.UserIndex).ToList()
                                                let emp = emps.FirstOrDefault(q => q.Login == u.PointUser)
                                                let loginfo = g.FirstOrDefault(q => q.FlowVersion == g.Key && q.FlowUser == u.PointUser)
                                                select new FlowUser
                                                {
                                                    TaskUser = emp == null ? "" : emp.CNName,
                                                    ResultInfo = loginfo == null ? default(FlowLogResult) : new FlowLogResult
                                                    {
                                                        FlowUser = loginfo.FlowUser,
                                                        FlowDate = loginfo.FlowDate,
                                                        FlowMemo = loginfo.FlowMemo,
                                                        FlowResult = (PublicEnum.EE_FlowResult)loginfo.FlowResult
                                                    }
                                                }
                                    }
                         };


                return new ActionResult<IEnumerable<FlowLogView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<FlowLogView>>(ex);
            }
        }
        /// <summary>
        /// 根据业务类型获取Master选择器
        /// </summary>
        /// <param name="businessType"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<FlowMasterSelector>> GetFlowMasterSelector(PublicEnum.EE_BusinessType businessType)
        {
            try
            {
                var masters = rpsFlowMaster.Queryable(p => p.BusinessType == (int)businessType);
                var re = from m in masters
                         select new FlowMasterSelector
                         {
                             MasterID = m.ID,
                             Name = m.Name
                         };
                return new ActionResult<IEnumerable<FlowMasterSelector>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<FlowMasterSelector>>(ex);
            }
        }

        /// <summary>
        /// 根据业务类型分页获取Master
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult<Pager<FlowMasterView>> GetFlowMastersPage(PagerQuery<int> query)
        {
            try
            {
                var masters = rpsFlowMaster.Queryable(p => p.BusinessType == query.Query || query.Query == 0);
                var retemp = from m in masters.ToList()
                             select new FlowMasterView
                             {
                                 MasterID = m.ID,
                                 BusinessType = m.BusinessType,
                                 Name = m.Name,
                                 TypeName = Command.GetItems(typeof(PublicEnum.EE_BusinessType)).FirstOrDefault(v => v.Value == m.BusinessType).Caption
                             };
                var re = new Pager<FlowMasterView>().GetCurrentPage(retemp, query.PageSize, query.PageIndex);
                return new ActionResult<Pager<FlowMasterView>>(re);

            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<FlowMasterView>>(ex);
            }
        }

        /// <summary>
        /// 获取我的审批
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<Flow_ResultView>> GetMyResult(PagerQuery<string> para)
        {
            try
            {
                var flowresult = rpsResult.Queryable(q => q.FlowUser == AppUser.UserInfo.Login).OrderBy(o => o.FlowDate);
                var slogins = flowresult.Select(s => s.ApplyUser);
                var flogins = flowresult.Select(s => s.FlowUser);
                var emps = _work.Repository<Basic_Employee>().Queryable(q => slogins.Contains(q.Login) || flogins.Contains(q.Login)).ToList();

                var re = from r in flowresult.ToList()
                         let semp = emps.FirstOrDefault(q => q.Login == r.ApplyUser)
                         let femp = emps.FirstOrDefault(q => q.Login == r.FlowUser)
                         select new Flow_ResultView
                         {
                             ApplyUser = r.ApplyUser,
                             FlowUser = r.FlowUser,
                             ApplyUserName = semp.CNName,
                             BusinessCode = r.BusinessCode,
                             BusinessDate = r.BusinessDate,
                             BusinessID = r.BusinessID,
                             BusinessType = (PublicEnum.EE_BusinessType)r.BusinessType,
                             BusinessTypeName = Command.GetItems(typeof(PublicEnum.EE_BusinessType)).FirstOrDefault(q => q.Value == r.BusinessType).Caption,
                             FlowDate = r.FlowDate,
                             FlowResult = (PublicEnum.EE_FlowResult)r.FlowResult,
                             FlowResultName = Command.GetItems(typeof(PublicEnum.EE_FlowResult)).FirstOrDefault(q => q.Value == r.FlowResult).Caption,
                             FlowUserName = femp.CNName,
                             FlowVersion = r.FlowVersion,
                             ID = r.ID,
                             PointName = r.PointName
                         };
                var rel = new Pager<Flow_ResultView>().GetCurrentPage(re, para.PageSize, para.PageIndex);
                return new ActionResult<Pager<Flow_ResultView>>(rel);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<Flow_ResultView>>(ex);
            }
        }
        /// <summary>
        /// 获取我发起的审批
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<Flow_ResultView>> GetMyStart(PagerQuery<string> para)
        {
            try
            {
                var flowresult = rpsResult.Queryable(q => q.ApplyUser == AppUser.UserInfo.Login).OrderBy(o => o.FlowDate);
                var slogins = flowresult.Select(s => s.ApplyUser);
                var flogins = flowresult.Select(s => s.FlowUser);
                var emps = _work.Repository<Basic_Employee>().Queryable(q => slogins.Contains(q.Login) || flogins.Contains(q.Login)).ToList();

                var re = from r in flowresult.ToList()
                         let semp = emps.FirstOrDefault(q => q.Login == r.ApplyUser)
                         let femp = emps.FirstOrDefault(q => q.Login == r.FlowUser)
                         select new Flow_ResultView
                         {
                             ApplyUser = r.ApplyUser,
                             FlowUser = r.FlowUser,
                             ApplyUserName = semp.CNName,
                             BusinessCode = r.BusinessCode,
                             BusinessDate = r.BusinessDate,
                             BusinessID = r.BusinessID,
                             BusinessType = (PublicEnum.EE_BusinessType)r.BusinessType,
                             BusinessTypeName = Command.GetItems(typeof(PublicEnum.EE_BusinessType)).FirstOrDefault(q => q.Value == r.BusinessType).Caption,
                             FlowDate = r.FlowDate,
                             FlowResult = (PublicEnum.EE_FlowResult)r.FlowResult,
                             FlowResultName = Command.GetItems(typeof(PublicEnum.EE_FlowResult)).FirstOrDefault(q => q.Value == r.FlowResult).Caption,
                             FlowUserName = femp.CNName,
                             FlowVersion = r.FlowVersion,
                             ID = r.ID,
                             PointName = r.PointName
                         };
                var rel = new Pager<Flow_ResultView>().GetCurrentPage(re, para.PageSize, para.PageIndex);
                return new ActionResult<Pager<Flow_ResultView>>(rel);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<Flow_ResultView>>(ex);
            }
        }
        /// <summary>
        /// 获取待审批
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<Flow_TaskView>> GetMyTask(PagerQuery<string> para)
        {
            try
            {
                var flowtask = rpsTask.Queryable(q => q.TaskUser == AppUser.UserInfo.Login).ToList();
                var slogins = flowtask.Select(s => s.ApplyUser);
                var flogins = flowtask.Select(s => s.TaskUser);
                var emps = _work.Repository<Basic_Employee>().Queryable(q => slogins.Contains(q.Login) || flogins.Contains(q.Login)).ToList();

                var retmp = from t in flowtask
                            let semp = emps.FirstOrDefault(q => q.Login == t.ApplyUser)
                            let femp = emps.FirstOrDefault(q => q.Login == t.TaskUser)
                            select new Flow_TaskView
                            {
                                TaskUser = t.TaskUser,
                                ApplyUser = t.ApplyUser,
                                ApplyUserName = semp == null ? "" : semp.CNName,
                                BusinessCode = t.BusinessCode,
                                BusinessDate = t.BusinessDate,
                                BusinessID = t.BusinessID,
                                BusinessType = (PublicEnum.EE_BusinessType)t.BusinessType,
                                BusinessTypeName = Command.GetItems(typeof(PublicEnum.EE_BusinessType)).FirstOrDefault(q => q.Value == t.BusinessType).Caption,
                                FlowVersion = t.FlowVersion,
                                ID = t.ID,
                                TaskDate = t.TaskDate
                            };

                var re = new Pager<Flow_TaskView>().GetCurrentPage(retmp, para.PageSize, para.PageIndex);
                return new ActionResult<Pager<Flow_TaskView>>(re);

            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<Flow_TaskView>>(ex);
            }
        }

        /// <summary>
        /// 获取审批节点模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<Flow_PointView> GetPointModel(Guid id)
        {
            try
            {
                var dbpoint = rpsPoint.GetModel(id);

                var re = dbpoint.MAPTO<Flow_PointView>();
                re.BusinessTypeName = Command.GetItems(typeof(PublicEnum.EE_BusinessType)).FirstOrDefault(q => q.Value == dbpoint.BusinessType).Caption;
                re.PointTypeName = Command.GetItems(typeof(PublicEnum.EE_FlowPointType)).FirstOrDefault(q => q.Value == dbpoint.PointType).Caption;

                return new ActionResult<Flow_PointView>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Flow_PointView>(ex);
            }
        }
        /// <summary>
        /// 根据业务类型获取和MasterID，分页获取审批节点集合
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult<Pager<Flow_PointView>> GetPointsByBusinessType(PagerQuery<FlowPointQuery> query)
        {
            try
            {
                var points = rpsPoint.Queryable(q => q.BusinessType == (int)query.Query.BusinessType && q.MasterID == query.Query.MasterID);
                var typenames = Command.GetItems(typeof(PublicEnum.EE_BusinessType));
                var ptypenames = Command.GetItems(typeof(PublicEnum.EE_FlowPointType));

                var retemp = from p in points.ToList()
                             orderby p.PointIndex
                         select new Flow_PointView
                         {
                             BusinessType = (PublicEnum.EE_BusinessType)p.BusinessType,
                             BusinessTypeName = typenames.FirstOrDefault(q => q.Value == p.BusinessType).Caption,
                             ID = p.ID,
                             PointIndex = p.PointIndex,
                             PointName = p.PointName,
                             PointType = (PublicEnum.EE_FlowPointType)p.PointType,
                             PointTypeName = ptypenames.FirstOrDefault(q => q.Value == p.PointType).Caption
                         };
                var re = new Pager<Flow_PointView>().GetCurrentPage(retemp, query.PageSize, query.PageIndex);
                return new ActionResult<Pager<Flow_PointView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<Flow_PointView>>(ex);
            }
        }
        /// <summary>
        /// 根据id获取审批用户模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<Point_UsersView> GetPointUser(Guid id)
        {
            try
            {
                var pointuser = rpsPointUser.GetModel(id);
                if (pointuser == null)
                {
                    throw new Exception("审批用户未找到");

                }
                var re = pointuser.MAPTO<Point_UsersView>();
                var emp = rpsEmployee.GetModel(q => q.Login == re.PointUser);
                re.PointUserName = emp == null ? "" : emp.CNName;

                return new ActionResult<Point_UsersView>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Point_UsersView>(ex);
            }
        }
        /// <summary>
        /// 根据审批节点获取该节点的用户集合
        /// </summary>
        /// <param name="pointid"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<Point_UsersView>> GetPointUsers(Guid pointid)
        {
            try
            {
                var users = rpsPointUser.Queryable(q => q.PointID == pointid);
                var re = from u in users.ToList()
                         let emp = rpsEmployee.GetModel(q => q.Login == u.PointUser)
                         orderby u.UserIndex
                         select new Point_UsersView
                         {
                             ID = u.ID,
                             PointID = u.PointID,
                             PointUser = u.PointUser,
                             UserIndex = u.UserIndex,
                             PointUserName = emp == null ? "" : emp.CNName

                         };
                return new ActionResult<IEnumerable<Point_UsersView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<Point_UsersView>>(ex);
            }
        }
        /// <summary>
        /// 发起任务，是否提交取决于参数
        /// 如果返回 -1则表示未设置审批流程
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public ActionResult<Flow_Task> InitTask(InitTask task)
        {
            try
            {
                var point = rpsPoint.Queryable(q => q.MasterID == task.MasterID).OrderBy(o => o.PointIndex).FirstOrDefault();
                if (point == null)
                {
                    var result = new ActionResult<Flow_Task>();
                    result.data = null;
                    result.state = 200;
                    result.msg = "";
                    return result;
                }
                else
                {
                    var ptask = new Flow_Task //审批用户的任务
                    {
                        ApplyUser = AppUser.UserInfo.Login,
                        BusinessID = task.BusinessID,
                        BusinessDate = task.BusinessDate,
                        BusinessCode = task.BusinessCode,
                        MasterID = task.MasterID,
                        BusinessType = point.BusinessType,
                        PointID = point.ID,
                        TaskDate = DateTime.Now
                    };
                    Flow_PointUsers user;
                    switch ((PublicEnum.EE_FlowPointType)point.PointType)
                    {
                        case PublicEnum.EE_FlowPointType.Generic: //普通节点
                            user = rpsPointUser.GetModel(q => q.PointID == point.ID);
                            if (user == null)
                            {
                                throw new Exception("请配置审批节点的用户!");
                            }
                            ptask.TaskUser = user.PointUser;
                            break;
                        case PublicEnum.EE_FlowPointType.Multi: //会审节点
                            user = rpsPointUser.Queryable(q => q.PointID == point.ID).OrderBy(o => o.UserIndex).FirstOrDefault();
                            if (user == null)
                            {
                                throw new Exception("请配置审批节点的用户!");
                            }
                            ptask.TaskUser = user.PointUser;
                            break;
                        default:
                            break;
                    }

                    /******************************发送审批流程信息**************************************/
                    var aemp = _work.Repository<Basic_Employee>().GetModel(p => p.Login == ptask.ApplyUser);
                    var msgToUser = _work.Repository<Auth_User>().GetModel(p => p.Login == ptask.TaskUser);
                    if (!string.IsNullOrEmpty(msgToUser.openID))
                    {
                        var aorg = _work.Repository<Basic_Org>().GetModel(aemp.OrgID);

                        var msg = ptask.BusinessType == (int)PublicEnum.EE_BusinessType.Apply ? "作业申请"
                                : ptask.BusinessType == (int)PublicEnum.EE_BusinessType.InspectTask ? "巡检任务"
                                : ptask.BusinessType == (int)PublicEnum.EE_BusinessType.TempTask ? "临时任务"
                                : "";
                        var sendData = new Dictionary<string, MessageDataBase>();
                        sendData.Add("first", new MessageDataBase { value = "您有一个待审批事项" });
                        sendData.Add("keyword1", new MessageDataBase { value = ptask.BusinessCode });
                        sendData.Add("keyword2", new MessageDataBase { value = ptask.BusinessDate.ToString("yyyy-MM-dd HH:mm:ss") });
                        sendData.Add("keyword3", new MessageDataBase { value = aemp.CNName });
                        sendData.Add("keyword4", new MessageDataBase { value = aorg.OrgName });
                        sendData.Add("keyword5", new MessageDataBase { value = $"您有一个审批流程类型为{msg}的流程需要审批！" });
                        sendData.Add("remark", new MessageDataBase { value = "ESF微服为安全护航。" });
                        var Msg = new TemplateMessagePara
                        {
                            template_id = "lMtAv69HdQsPg1A-mtQQ4mcoLQ-zvkHs9hBMsXnkwso",
                            touser = msgToUser.openID,
                            data = sendData,
                            url = "http://weixin.bjjtza.com/ApprovalPending"
                        };
                        WxService.SendTemplateMessage(Msg);
                    }
                    /************************************************************************/
                    return new ActionResult<Flow_Task>(ptask);
                }
            }
            catch (Exception ex)
            {

                return new ActionResult<Flow_Task>(ex);
            }
        }
    }
}
