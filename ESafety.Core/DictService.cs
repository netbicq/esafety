using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.DB.Account;
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
    /// 词典
    /// </summary>
    public class DictService : ServiceBase, IDict
    {
        private IUnitwork _work = null;

        private IRepository<Basic_Dict> rpsDict = null;

        public DictService(IUnitwork work)
        {

            _work = work;
            Unitwork = work;
            rpsDict = _work.Repository<Basic_Dict>();

        }

        /// <summary>
        /// 新建词典
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public ActionResult<bool> AddDict(DictNew dict)
        {
            try
            {
                var check = rpsDict.Any(q => q.ParentID == dict.ParentID && q.DictName == dict.DictName);
                if(check)
                {
                    throw new Exception("已经存在词典：" + dict.DictName);
                }
                if (dict.ParentID == OptionConst.DangerLevel || dict.ParentID == OptionConst.Eval_LECD_C
                     || dict.ParentID == OptionConst.Eval_LECD_E || dict.ParentID == OptionConst.Eval_LECD_L
                     || dict.ParentID == OptionConst.Eval_LSD_L || dict.ParentID == OptionConst.Eval_LSD_S)
                {
                    throw new Exception("系统内置不允许修改!");
                }
                var newdict = dict.MAPTO<Basic_Dict>();

                rpsDict.Add(newdict);
                

                _work.Commit();
                return new ActionResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 新建词典类型
        /// </summary>
        /// <param name="dicttype"></param>
        /// <returns></returns>
        public ActionResult<bool> AddDictType(DictTypeNew dicttype)
        {
            try
            {
                var check = rpsDict.Any(q => q.ParentID == Guid.Empty && q.DictName == dicttype.DictName);
                if (check)
                {
                    throw new Exception("已经存在词典类型：" + dicttype.DictName);
                }
               
                var newtype = dicttype.MAPTO<Basic_Dict>();

                rpsDict.Add(newtype);
                _work.Commit();
                return new ActionResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 删除指定id的词典
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelDict(Guid id)
        {
            try
            {
                var dbdict = rpsDict.GetModel(id);
                if(dbdict==null)
                {
                    throw new Exception("词典未找到");
                }
                if (dbdict.ParentID == OptionConst.DangerLevel || dbdict.ParentID == OptionConst.Eval_LECD_C
                     || dbdict.ParentID == OptionConst.Eval_LECD_E || dbdict.ParentID == OptionConst.Eval_LECD_L
                     || dbdict.ParentID == OptionConst.Eval_LSD_L || dbdict.ParentID == OptionConst.Eval_LSD_S)
                {
                    throw new Exception("系统内置不允许修改!");
                }
                var check = _work.Repository<Doc_Institution>().Any(p => p.TypeID == id);
                if (check)
                {
                    throw new Exception("该词典以用于风险公示下，无法删除!");
                }

                rpsDict.Delete(dbdict);
                _work.Commit();
                return new ActionResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 删除批定id的词典类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<bool> DelDictType(Guid id)
        {
            try
            {
                var dbtype = rpsDict.GetModel(id);
                if (dbtype == null)
                {
                    throw new Exception("词典类型未找到");
                }
                var check = rpsDict.Any(q => q.ParentID == dbtype.ID);
                if(check)
                {
                    throw new Exception("词典类型存在有词典数据，请先删除词典数据");
                }
                if (dbtype.IsSYS)
                {
                    throw new Exception("系统内置词典不允许删除");
                }

                rpsDict.Delete(dbtype);
                _work.Commit();

                return new ActionResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 修改词典
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public ActionResult<bool> EditDict(DictEidt dict)
        {
            try
            {
                var dbdict = rpsDict.GetModel(dict.ID);
                if(dbdict==null)
                {
                    throw new Exception("词典未找到");
                }
                if (dbdict.ParentID == OptionConst.DangerLevel||dbdict.ParentID==OptionConst.Eval_LECD_C
                    ||dbdict.ParentID==OptionConst.Eval_LECD_E||dbdict.ParentID==OptionConst.Eval_LECD_L
                    ||dbdict.ParentID==OptionConst.Eval_LSD_L||dbdict.ParentID==OptionConst.Eval_LSD_S)
                {
                    throw new Exception("系统内置不允许修改!");
                }
                dbdict = dict.CopyTo<Basic_Dict>(dbdict);
               
                rpsDict.Update(dbdict);
                _work.Commit();
                return new ActionResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new ActionResult<bool>(ex);
            }
        }
        /// <summary>
        /// 获取风险等级词典
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<Basic_Dict>> GetDangerLevel()
        {
            try
            {
                var re = getdictsbyParentID(OptionConst.DangerLevel);

                return new ActionResult<IEnumerable<Basic_Dict>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<Basic_Dict>>(ex);
            }
        }

        /// <summary>
        /// 根据id获取词典模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<Basic_Dict> GetDictModel(Guid id)
        {
            try
            {
                var re = rpsDict.GetModel(id);
                if (re == null)
                {
                    throw new Exception("未找到词典");
                }
                return new ActionResult<Basic_Dict>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<Basic_Dict>(ex);
            }
        }
        /// <summary>
        /// 根据词典类型id获取词典列表
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public ActionResult<Pager<Basic_Dict>> GetDictsByType(PagerQuery< Guid> para)
        {
            try
            {
                var retemp = rpsDict.Queryable(q => q.ParentID == para.Query).OrderByDescending(o=>o.MinValue);

                var re = new Pager<Basic_Dict>().GetCurrentPage(retemp, para.PageSize, para.PageIndex);
                
                return new ActionResult<Pager<Basic_Dict>>(re);

            }
            catch (Exception ex)
            {
                return new ActionResult<Pager<Basic_Dict>>(ex);
            }
        }
        /// <summary>
        /// 获取词典类型集合
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable< DictTypeView>> GetDictTypes()
        {
            try
            {
                var retemp = rpsDict.Queryable(q => q.ParentID == Guid.Empty);

                var re = from d in retemp
                         select new DictTypeView
                         {
                             DictName = d.DictName,
                             ID = d.ID,
                             IsSYS = d.IsSYS
                         };

                return new ActionResult<IEnumerable< DictTypeView>>(re);

            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable< DictTypeView>>(ex);
            }
        }

        private IEnumerable<Basic_Dict> getdictsbyParentID(Guid parentid)
        {
            var re = rpsDict.Queryable(q => q.ParentID == parentid);
            return re;
        }
        /// <summary>
        /// 获取资质词典
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<Basic_Dict>> GetDocLicense()
        {
            try
            {
                var re = getdictsbyParentID(OptionConst.DocLicense);
                return new ActionResult<IEnumerable<Basic_Dict>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<Basic_Dict>>(ex);
            }
        }
        /// <summary>
        /// 获取风险公示
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<Basic_Dict>> GetDocRegime()
        {
            try
            {
                var re = getdictsbyParentID(OptionConst.DocRegime);
                return new ActionResult<IEnumerable<Basic_Dict>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<Basic_Dict>>(ex);
            }
        }
        ///// <summary>
        ///// 获取应急预案词典
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult<IEnumerable<Basic_Dict>> GetDocSlution()
        //{
        //    try
        //    {
        //        var re = getdictsbyParentID(OptionConst.DocSlution);
        //        return new ActionResult<IEnumerable<Basic_Dict>>(re);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ActionResult<IEnumerable<Basic_Dict>>(ex);
        //    }
        //}
        /// <summary>
        /// 获取事故后果词典
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<Basic_Dict>> GetEval_SGJG()
        {
            try
            {
                var re = getdictsbyParentID(OptionConst.Eval_SGJG);
                return new ActionResult<IEnumerable<Basic_Dict>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<Basic_Dict>>(ex);
            }
        }
     
        /// <summary>
        /// 获取事故类型词典
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<Basic_Dict>> GetEval_SGLX()
        {
            try
            {
                var re = getdictsbyParentID(OptionConst.Eval_SGLX);
                return new ActionResult<IEnumerable<Basic_Dict>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<Basic_Dict>>(ex);
            }
        }
        /// <summary>
        /// 获取危害因素词典
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<Basic_Dict>> GetEval_WHYS()
        {
            try
            {
                var re = getdictsbyParentID(OptionConst.Eval_WHYS );
                return new ActionResult<IEnumerable<Basic_Dict>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<Basic_Dict>>(ex);
            }
        }
        /// <summary>
        /// 获取影响范围词典
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<Basic_Dict>> GetEval_YXFW()
        {
            try
            {
                var re = getdictsbyParentID(OptionConst.Eval_YXFW);
                return new ActionResult<IEnumerable<Basic_Dict>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<Basic_Dict>>(ex);
            }
        }
        /// <summary>
        /// 获取LECD_L词典
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<Basic_Dict>> GetEval_LECD_L()
        {
            try
            {
                var re = getdictsbyParentID(OptionConst.Eval_LECD_L);
                return new ActionResult<IEnumerable<Basic_Dict>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<Basic_Dict>>(ex);
            }
        }
        /// <summary>
        /// 获取LECD_E词典
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<Basic_Dict>> GetEval_LECD_E()
        {
            try
            {
                var re = getdictsbyParentID(OptionConst.Eval_LECD_E);
                return new ActionResult<IEnumerable<Basic_Dict>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<Basic_Dict>>(ex);
            }
        }
        /// <summary>
        /// 获取LECD_C词典
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<Basic_Dict>> GetEval_LECD_C()
        {
            try
            {
                var re = getdictsbyParentID(OptionConst.Eval_LECD_C);
                return new ActionResult<IEnumerable<Basic_Dict>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<Basic_Dict>>(ex);
            }
        }
        /// <summary>
        /// 获取LSD_L词典
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<Basic_Dict>> GetEval_LSD_L()
        {
            try
            {
                var re = getdictsbyParentID(OptionConst.Eval_LSD_S);
                return new ActionResult<IEnumerable<Basic_Dict>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<Basic_Dict>>(ex);
            }
        }
        /// <summary>
        /// 获取LSD_S词典
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<Basic_Dict>> GetEval_LSD_S()
        {
            try
            {
                var re = getdictsbyParentID(OptionConst.Eval_LSD_S);
                return new ActionResult<IEnumerable<Basic_Dict>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<Basic_Dict>>(ex);
            }
        }
        /// <summary>
        /// 获取隐患等级
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<Basic_Dict>> GetTroubleLevel()
        {
            try
            {
                var re = getdictsbyParentID(OptionConst.TroubleLevel);
                return new ActionResult<IEnumerable<Basic_Dict>>(re);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<Basic_Dict>>(ex);
            }
        }
    }
}
