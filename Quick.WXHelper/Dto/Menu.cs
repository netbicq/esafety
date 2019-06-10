using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.WXHelper.Dto
{
    /// <summary>
    /// 菜单基类
    /// </summary>
    public class MenuBase
    {
        /// <summary>
        /// 类型：click  view 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 菜单的catpoal
        /// </summary>
        public string name { get; set; }

        public string url { get; set; }

        public string key { get; set; }


    }
    /// <summary>
    /// 菜单项
    /// </summary>
    public class MenuItem : MenuBase
    {
        public IEnumerable<MenuBase> sub_button { get; set; }
        public MenuItem()
        {
            sub_button = new List<MenuBase>();
        }

    }
    /// <summary>
    /// 创建菜单参数
    /// </summary>
    public class Menu
    {
        public IEnumerable<MenuItem> button { get; set; }
    }
    /// <summary>
    /// 查询菜单返回结构
    /// </summary>
    public class MenuQuery
    {
        public Menu menu { get; set; }
    }
}
