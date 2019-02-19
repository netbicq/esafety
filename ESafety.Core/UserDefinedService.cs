using ESafety.Core.Model;
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
   
        public UserDefinedService(ORM.IUnitwork work)
        {
            _work = work;
            Unitwork = work;
            _rpsdefined = work.Repository<Model.DB.Basic_UserDefined>();
        }
        /// <summary>
        /// 新建自定义项
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult<bool> AddUserDefined(UserDefinedNew entity)
        {
            var userdefined = new Model.DB.Basic_UserDefined
            { 
                 Caption=entity.Caption,
                 DefinedType=Convert.ToInt32(entity.DefinedType),
                 DictID=entity.DictID,
                 IsEmpty=entity.IsEmpty,
                 IsMulti=entity.IsMulti,
                 VisibleIndex=entity.VisibleIndex,
                 DataType=Convert.ToInt32(entity.DataType)
            };
            _rpsdefined.Add(userdefined);
            _work.Commit();
            return new ActionResult<bool>(true);
        }
        /// <summary>
        /// 修改自定义项
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult<bool> EditUserDefined(UserDefinedEdit entity)
        {
            var userdefined = new Model.DB.Basic_UserDefined
            {
                ID = entity.ID,
                Caption = entity.Caption,
                DataType = Convert.ToInt32(entity.DataType),
                DefinedType = Convert.ToInt32(entity.DefinedType),
                DictID=entity.DictID,
                IsEmpty=entity.IsEmpty,
                IsMulti=entity.IsMulti,
                VisibleIndex=entity.VisibleIndex
            };
            _rpsdefined.Update(userdefined);
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

                var re =userdefined.MAPTO<UserDefinedView>();
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

                var re = from p in userdefineds.ToList()
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
                             DictName = _rpsdict.GetModel(q=>q.ID==p.DictID).DictName
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// 保存自定义项
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public ActionResult<bool> SaveBuisnessValue(BusinessValue values)
        {
            throw new NotImplementedException();
        }
    }
}
