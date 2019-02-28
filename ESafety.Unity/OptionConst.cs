using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Unity
{
    /// <summary>
    /// 账套选项常数
    /// </summary>
    public static class OptionConst
    {
         
        /// <summary>
        /// 检查身份证选项 bool
        /// </summary>
        public const string OC_CONST_IsCheckCard = "IsCheckCard";



        #region "词典常量"
        /// <summary>
        /// 制度类型
        /// </summary>
        public static Guid DocRegime { get { return Guid.Parse("5fc3fc8d-c00e-401a-a844-fd841386038b"); } }
        /// <summary>
        /// 预案类型
        /// </summary>
        public static Guid DocSlution { get { return Guid.Parse("5fc3fc8d-c00e-401a-a844-fd841386039b"); } }

        /// <summary>
        /// 资质类型
        /// </summary>
        public static Guid DocLicense { get { return Guid.Parse("5fc3fc8d-c00e-401a-a844-fd841386010b"); } }
        /// <summary>
        /// 风险等级
        /// </summary>
        public static Guid DangerLevel { get { return Guid.Parse("5fc3fc8d-c00e-401a-a844-fd841386031b"); } }
        /// <summary>
        /// 危害因素
        /// </summary>
        public static Guid Eval_WHYS { get { return Guid.Parse("5fc3fc8d-c00e-401a-a844-fd841386033b"); } }
        /// <summary>
        /// 事故类型
        /// </summary>
        public static Guid Eval_SGLX { get { return Guid.Parse("5fc3fc8d-c00e-401a-a844-fd841386034b"); } }
        /// <summary>
        /// 事故后果
        /// </summary>
        public static Guid Eval_SGJG { get { return Guid.Parse("5fc3fc8d-c00e-401a-a844-fd841386035b"); } }
        /// <summary>
        /// 影响范围
        /// </summary>
        public static Guid Eval_YXFW { get { return Guid.Parse("5fc3fc8d-c00e-401a-a844-fd841386036b"); } }
        /// <summary>
        /// 事故可能性
        /// </summary>
        public static Guid Eval_SGKLX { get { return Guid.Parse("5fc3fc8d-c00e-401a-a844-fd841386037b"); } }


        #endregion

    }
}
