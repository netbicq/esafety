using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.PARA;
using ESafety.Core.Model.View;
using ESafety.ORM;
using ESafety.Unity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core
{
    /// <summary>
    /// 用户自定义项
    /// </summary>
    public class UserDefinedService : ServiceBase, IUserDefined
    {

        private IUnitwork _work = null;

        private IRepository<Model.DB.Basic_UserDefined> _rpsdefined = null;
        private IRepository<Model.DB.Basic_Dict> _rpsdict;
        private IRepository<Basic_UserDefinedValue> _rpsdefinedvalue = null;


        public UserDefinedService(ORM.IUnitwork work)
        {
            _work = work;
            Unitwork = work;
            _rpsdefined = work.Repository<Model.DB.Basic_UserDefined>();
            _rpsdict = work.Repository<Basic_Dict>();
            _rpsdefinedvalue = work.Repository<Basic_UserDefinedValue>();


        }
        /// <summary>
        /// 新建自定义项
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult<bool> AddUserDefined(UserDefinedNew entity)
        {
 
            //必要的逻辑检查

            var datatypecheck = Command.GetItems(typeof(PublicEnum.EE_UserDefinedDataType)).Any(q => q.Value ==(int) entity.DataType);
            if (!datatypecheck)
            {
                throw new Exception("数据类型有误");
            }
            var definedtype = Command.GetItems(typeof(PublicEnum.EE_UserDefinedType)).Any(q => q.Value == (int)entity.DefinedType);
            if (!definedtype)
            {
                throw new Exception("自定义类型有误");
            }
            var check = _rpsdefined.Any(q => q.DefinedType == (int)entity.DefinedType && q.Caption == entity.Caption);
            if (check)
            {
                throw new Exception("已经存在相同的标题 ：" + entity.Caption);
            }

            var dbdefined = entity.MAPTO<Basic_UserDefined>();
            
            _rpsdefined.Add(dbdefined);
            _work.Commit();
            return new ActionResult<bool>(true);
        }

        /// <summary>
        /// 删除自定义项的业务值
        /// 不提交数据库，与业务删除一起提交
        /// </summary>
        /// <param name="buisnessid"></param>
        /// <returns></returns>
        public ActionResult<bool> DeleteBusinessValue(Guid buisnessid)
        {
            try
            {

                _rpsdefinedvalue.Delete(q => q.BusinessID == buisnessid);
                //不提交数据库，与业务删除一起提交
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 删除自定义项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelUserDefined(Guid id)
        {
            try
            {
                var dbfined = _rpsdefined.GetModel(id);
                if (dbfined == null)
                {
                    throw new Exception("未找到自定义项");
                }

                //同时删除自定项的值
                _rpsdefined.Delete(dbfined);
                _rpsdefinedvalue.Delete(q => q.DefinedID == id);

                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }

        /// <summary>
        /// 修改自定义项
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult<bool> EditUserDefined(UserDefinedEdit entity)
        {
            var dbdefined = _rpsdefined.GetModel(q => q.ID == entity.ID);
            if (dbdefined == null)
            {
                throw new Exception("未找到自定义项");
            }
            var check = _rpsdefined.Any(q => q.ID != entity.ID && q.Caption == entity.Caption && q.DefinedType == (int)entity.DefinedType);
            if (check)
            {
                throw new Exception("已经存在相同的标题：" + entity.Caption);
            }
            dbdefined = entity.CopyTo<Basic_UserDefined>(dbdefined);

            
            _rpsdefined.Update(dbdefined);
            _work.Commit();
            return new ActionResult<bool>(true);
        }
        /// <summary>
        /// 获取自定义项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<UserDefinedView> GetUserDefined(Guid id)
        {
            try
            {
                var userdefined = _rpsdefined.GetModel(id);
                var datatypenamelist = Command.GetItems(typeof(PublicEnum.EE_UserDefinedDataType));
                var definedtypenamelist = Command.GetItems(typeof(PublicEnum.EE_UserDefinedType));

                var re = userdefined.MAPTO<UserDefinedView>();
                re.DataTypeName = Command.GetItems(typeof(PublicEnum.EE_UserDefinedDataType)).FirstOrDefault(q => q.Value == userdefined.DataType).Caption;
                re.DefinedTypeName = Command.GetItems(typeof(PublicEnum.EE_UserDefinedType)).FirstOrDefault(q => q.Value == userdefined.DefinedType).Caption;
                var dict = _rpsdict.GetModel(p => p.ID == userdefined.DictID);
                re.DictName = dict == null ? "" : dict.DictName;
                return new ActionResult<UserDefinedView>(re);
            }
            catch (Exception ex)
            {

                return new ActionResult<UserDefinedView>(ex);
            }

        }
        /// <summary>
        /// 获取自定义项支持的数据类型
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<EnumItem>> GetUserdefinedDataType()
        {
            try
            {
                var re = Command.GetItems(typeof(PublicEnum.EE_UserDefinedDataType));
                return new ActionResult<IEnumerable<EnumItem>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<EnumItem>>(ex);
            }
        }

        /// <summary>
        /// 根据自定义类型获取自定义基集合
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<UserDefinedView>> GetUserDefineds(PublicEnum.EE_UserDefinedType type)
        {
            try
            {
                var userdefineds = _rpsdefined.Queryable(p => p.DefinedType == (int)type);
                var datatypenamelist = Command.GetItems(typeof(PublicEnum.EE_UserDefinedDataType));
                var definedtypenamelist = Command.GetItems(typeof(PublicEnum.EE_UserDefinedType));
                var test = userdefineds.ToList();
                var re = from p in userdefineds.ToList()
                         let dict =_rpsdict.GetModel(q=>q.ID == p.DictID)
                         select new UserDefinedView
                         {
                             Caption = p.Caption,
                             DataType = (PublicEnum.EE_UserDefinedDataType)p.DataType,
                             DefinedType = (PublicEnum.EE_UserDefinedType)p.DefinedType,
                             DictID = p.DictID,
                             IsEmpty = p.IsEmpty,
                             IsMulti = p.IsMulti,
                             VisibleIndex = p.VisibleIndex,
                             ID = p.ID,
                             DataTypeName = datatypenamelist.FirstOrDefault(q => q.Value == p.DataType).Caption,
                             DefinedTypeName = definedtypenamelist.FirstOrDefault(q => q.Value == p.DefinedType).Caption,
                             DictName =dict==null?"":dict.DictName
                         };

                return new ActionResult<IEnumerable<UserDefinedView>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<UserDefinedView>>(ex);
            }

        }
        /// <summary>
        /// 获取自定义项类型集合
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<EnumItem>> GetUserDefinedTypes()
        {
            try
            {
                var re = Command.GetItems(typeof(PublicEnum.EE_UserDefinedType));
                return new ActionResult<IEnumerable<EnumItem>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<EnumItem>>(ex);
            }
        }
        /// <summary>
        /// 获取用户自定义项，用于业务表单
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<UserDefinedForm>> GetUserDefineItems(UserDefinedBusiness para)
        {
            try
            {
                //自定义类型当前有效的自定义项
                var defineds = _rpsdefined.Queryable(q => q.DefinedType == (int)para.DefinedType).ToList();

                var businessid = para.BusinessID == null ? Guid.Empty : para.BusinessID;

                var definedids = defineds.Select(s => s.ID);
                //以最新的为准，已经删掉的自定义项值则忽略掉
                var values = _rpsdefinedvalue.Queryable().ToList().Where(q => definedids.Contains(q.DefinedID) && q.BusinessID == para.BusinessID).ToList();
                var dicts = _rpsdict.Queryable().ToList().Where(q=> defineds.Select(s=>s.DictID).Contains(q.ID)).ToList();
                var dictitems = _rpsdict.Queryable().ToList().Where(q => dicts.Select(s => s.ID).Contains(q.ParentID));

                var re = from d in defineds
                         let dict =dicts.FirstOrDefault(q=>q.ID == d.DictID)
                         let dictits = dictitems.Where(q=>q.ParentID == d.DictID)
                         let valuemodel = values.FirstOrDefault(q => q.DefinedID == d.ID)
                         select new UserDefinedForm
                         {
                             Caption = d.Caption,
                             DataType = (PublicEnum.EE_UserDefinedDataType)d.DataType,
                             DataTypeName = Command.GetItems(typeof(PublicEnum.EE_UserDefinedDataType)).FirstOrDefault(q => q.Value == d.DataType).Caption,
                             DefinedType = (PublicEnum.EE_UserDefinedType)d.DefinedType,
                             DefinedTypeName = Command.GetItems(typeof(PublicEnum.EE_UserDefinedType)).FirstOrDefault(q => q.Value == d.DefinedType).Caption,
                             DictID = d.DictID,
                             DictName =dict==null?string.Empty: dict.DictName,
                             ID = d.ID,
                             IsEmpty = d.IsEmpty,
                             IsMulti = d.IsMulti,
                             VisibleIndex = d.VisibleIndex,
                             DictSelection =d.DataType ==(int)PublicEnum.EE_UserDefinedDataType.Dict?dictits:new List<Basic_Dict>(),
                             // ItemValue =valuemodel ==null?string.Empty:valuemodel.DefinedValue
                             ItemValue = 
                             valuemodel == null ? string.Empty:
                             d.IsMulti ?(object)JsonConvert.DeserializeObject<IEnumerable< Guid>>(valuemodel.DefinedValue):
                             d.DataType==(int)PublicEnum.EE_UserDefinedDataType.Bool? (object)bool.Parse(valuemodel.DefinedValue): (object)valuemodel.DefinedValue
                         };
                return new ActionResult<IEnumerable<UserDefinedForm>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<UserDefinedForm>>(ex);
            }
        }

        /// <summary>
        /// 保存自定义项
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public ActionResult<bool> SaveBuisnessValue(UserDefinedBusinessValue values)
        {
            try
            {
                var dbvalues = _rpsdefinedvalue.Queryable(q => q.BusinessID == values.BusinessID);

                List<Basic_UserDefinedValue> newvalues = new List<Basic_UserDefinedValue>();
                foreach (var v in values.Values)
                {
                    var definedmodel = _rpsdefined.GetModel(v.DefinedID);
                    if (definedmodel == null)
                    {
                        throw new Exception("未找到自定义项");
                    }
                    var emptycheck = !definedmodel.IsEmpty && v.DefinedValue == null;
                    if (emptycheck)
                    {
                        throw new Exception("不可空选项必须录入值");
                    }

                    var dbudv = new Basic_UserDefinedValue
                    {
                        BusinessID = values.BusinessID,
                        DefinedID = v.DefinedID,
                        DefinedType = definedmodel.DefinedType,
                        ID = Guid.NewGuid(),
                        
                    };
                    switch ((PublicEnum.EE_UserDefinedDataType)dbudv.DefinedType)
                    {
                        case PublicEnum.EE_UserDefinedDataType.Dict:
                            if (definedmodel.IsMulti)
                            {
                                var rearry = new List<Guid>();
                                var dbvalue  = v.DefinedValue == null ?rearry :JsonConvert.DeserializeObject<List<Guid>>(v.DefinedValue.ToString());

                                var value =JsonConvert.SerializeObject( dbvalue);
                                dbudv.DefinedValue =value;
                            }
                            else
                            {
                                
                                dbudv.DefinedValue =v.DefinedValue ==null?Guid.Empty.ToString(): v.DefinedValue.ToString();
                            }
                            break;
                        case PublicEnum.EE_UserDefinedDataType.Bool:
                            dbudv.DefinedValue = v.DefinedValue == null ? false.ToString() : v.DefinedValue.ToString();
                            break;
                        case PublicEnum.EE_UserDefinedDataType.Number:
                            double temp;
                            if (v.DefinedValue == null)
                            {
                                dbudv.DefinedValue = "0.00";
                            }
                            if(double.TryParse(v.DefinedValue.ToString(),out temp))
                            {
                                dbudv.DefinedValue = temp.ToString();
                            }
                            else
                            {
                                throw new Exception("输入值："+v.DefinedValue+" 数据类型错误，应输入数字类型数据!");
                            }
                            break;
                        case PublicEnum.EE_UserDefinedDataType.Int:
                            int tempnum;
                            if (v.DefinedValue == null)
                            {
                                dbudv.DefinedValue = "0";
                            }
                            if (int.TryParse(v.DefinedValue.ToString(), out tempnum))
                            {
                                dbudv.DefinedValue = tempnum.ToString();
                            }
                            else
                            {
                                throw new Exception("输入值：" + v.DefinedValue + " 数据类型错误，应输入整数类型数据!");
                            }
                            break;
                        case PublicEnum.EE_UserDefinedDataType.Date:
                            DateTime tempdate;
                            if (v.DefinedValue == null)
                            {
                                dbudv.DefinedValue = "";
                            }
                            if (DateTime.TryParse(v.DefinedValue.ToString(), out tempdate))
                            {
                                dbudv.DefinedValue = tempdate.ToString();
                            }
                            else
                            {
                                throw new Exception("输入值：" + v.DefinedValue + " 数据类型错误，应输入日期类型数据!");
                            }
                            break;
                        default:
                            dbudv.DefinedValue=v.DefinedValue==null?string.Empty:v.DefinedValue.ToString();
                            break;
                    }
                    newvalues.Add(dbudv);
                }

                foreach(var dv in dbvalues)
                {
                    _rpsdefinedvalue.Delete(dv);
                }
                foreach(var nv in newvalues)
                {
                    _rpsdefinedvalue.Add(nv);
                }
                //不提交，跟随业务数据一起提交

                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
    }
}
