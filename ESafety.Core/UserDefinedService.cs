using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.PARA;
using ESafety.Core.Model.View;
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
            //var userdefined = new Model.DB.Basic_UserDefined
            //{ 
            //     Caption=entity.Caption,
            //     DefinedType=Convert.ToInt32(entity.DefinedType),
            //     DictID=entity.DictID,
            //     IsEmpty=entity.IsEmpty,
            //     IsMulti=entity.IsMulti,
            //     VisibleIndex=entity.VisibleIndex,
            //     DataType=Convert.ToInt32(entity.DataType)
            //};
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

            //var userdefined = new Model.DB.Basic_UserDefined
            //{
            //    ID = entity.ID,
            //    Caption = entity.Caption,
            //    DataType = Convert.ToInt32(entity.DataType),
            //    DefinedType = Convert.ToInt32(entity.DefinedType),
            //    DictID=entity.DictID,
            //    IsEmpty=entity.IsEmpty,
            //    IsMulti=entity.IsMulti,
            //    VisibleIndex=entity.VisibleIndex
            //};
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
                re.DictName = _rpsdict.GetModel(p => p.ID == userdefined.DictID).DictName;
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
                var defineds = _rpsdefined.Queryable(q => q.DefinedType == (int)para.DefinedType);

                var businessid = para.BusinessID == null ? Guid.Empty : para.BusinessID;

                var definedids = defineds.Select(s => s.ID);
                //以最新的为准，已经删掉的自定义项值则忽略掉
                var values = _rpsdefinedvalue.Queryable(q => definedids.Contains(q.DefinedID) && q.BusinessID == para.BusinessID).ToList();

                var re = from d in defineds.ToList()
                         let dict = _rpsdict.GetModel(q => q.ID == d.DictID)
                         let dicts = _rpsdict.GetList(q => q.ParentID == dict.ID)
                         let valuemodel =values.FirstOrDefault(q=>q.DefinedID == d.ID)
                         select new UserDefinedForm
                         {
                             Caption = d.Caption,
                             DataType = (PublicEnum.EE_UserDefinedDataType)d.DataType,
                             DataTypeName = Command.GetItems(typeof(PublicEnum.EE_UserDefinedDataType)).FirstOrDefault(q => q.Value == d.DataType).Caption,
                             DefinedType = (PublicEnum.EE_UserDefinedType)d.DefinedType,
                             DefinedTypeName = Command.GetItems(typeof(PublicEnum.EE_UserDefinedType)).FirstOrDefault(q => q.Value == d.DefinedType).Caption,
                             DictID = d.DictID,
                             DictName = dict.DictName,
                             ID = d.ID,
                             IsEmpty = d.IsEmpty,
                             IsMulti = d.IsMulti,
                             VisibleIndex = d.VisibleIndex,
                             DictSelection = dicts,
                             ItemValue =valuemodel ==null?string.Empty:valuemodel.DefinedValue
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
                    newvalues.Add(new Basic_UserDefinedValue
                    {
                        BusinessID = values.BusinessID,
                        DefinedID = v.DefinedID,
                        DefinedType = definedmodel.DataType,
                        DefinedValue = v.DefinedValue,
                        ID = Guid.NewGuid()
                    });
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
