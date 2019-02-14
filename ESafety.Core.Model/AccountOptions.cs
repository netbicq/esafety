using ESafety.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model
{
    /// <summary>
    /// 账套选项
    /// </summary>
    public static class AccountOptions
    {

        /// <summary>
        /// 获取选项集合
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<OptionsItemView> GetOptions()
        {
            IList<OptionsItemView> opts = new List<OptionsItemView>();
            opts.Add(new OptionsItemView
            {
                OptionKey = OptionConst.OC_CONST_IsCheckCard,
                OptionType = PublicEnum.AccountOptionItemType.Bool,
                OptionCaption = "检查身份证",
                Remark = "设置为启用，录入职员信息时会检查其身份证相关信息的合法性，否则不检查"
            });
            
            return opts;

        }

    }
    /// <summary>
    /// 设置企业的选项值 
    /// </summary>
    public class SetOptoin
    {
        /// <summary>
        /// 账套ID
        /// </summary>
        public Guid AccountID { get; set; }
        /// <summary>
        /// 选项集合
        /// </summary>
        public IEnumerable<OptionItemSet> Options { get; set; }
    }
    /// <summary>
    /// 返回的选项模型
    /// </summary>
    public class OptionsItemView
    {
        /// <summary>
        /// 选项Key
        /// </summary>
        public string OptionKey { get; set; }
        /// <summary>
        /// 选项标题
        /// </summary>
        public string OptionCaption { get; set; }
        /// <summary>
        /// 选项类型
        /// </summary>
        public PublicEnum.AccountOptionItemType OptionType { get; set; }
        /// <summary>
        /// 枚举类型
        /// </summary>
        public string EnumType { get; set; }
        /// <summary>
        /// 枚举的选项
        /// </summary>
        public IEnumerable<EnumItem> EnumOptions { get; set; }
        /// <summary>
        /// 选项值
        /// </summary>
        public string ItemValue { get; set; }
        /// <summary>
        /// 多值类型的值，其它类型此属性为null
        /// </summary>
        public MultiValue MultiValue { get; set; }
        /// <summary>
        /// 集合类型的值，其它类型此属性为null
        /// </summary>
        public IEnumerable<string> ListValue { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
    
}
