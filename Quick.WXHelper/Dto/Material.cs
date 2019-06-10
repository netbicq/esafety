using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.WXHelper.Dto
{
    /// <summary>
    /// 获取素材列表参数
    /// </summary>
    public class MaterialListPara
    {
        /// <summary>
        /// 素材类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 偏移位置
        /// </summary>
        public int offset { get; set; }
        /// <summary>
        /// 获取数量
        /// </summary>
        public int count { get; set; }
    }

    public class MaterialResultBase
    {
        /// <summary>
        /// 素材总量
        /// </summary>
        public int total_count { get; set; }
        /// <summary>
        /// 本次获取量
        /// </summary>
        public int item_count { get; set; }
         
    }
    public class MaterialTXTIMGResult:MaterialResultBase
    {
       
        /// <summary>
        /// 返回的素材项
        /// </summary>
        public IEnumerable<MaterialTXTIMGBase> item { get; set; }
    }

    public class MaterialIVVResult:MaterialResultBase
    {
        
        /// <summary>
        /// 返回的素材项
        /// </summary>
        public IEnumerable<MaterialIVVBase> item { get; set; }
    }

    /// <summary>
    /// 图文类型素材
    /// </summary>
    public class MaterialTXTIMGBase
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 封面图片ID
        /// </summary>
        public string thumb_media_id { get; set; }
        /// <summary>
        /// 是否显示封面图片
        /// </summary>
        public int show_cover_pic { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        public string digest { get; set; }
        /// <summary>
        /// 图文内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 图文页的url
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 图文的原文地址
        /// </summary>
        public string content_source_url { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public int update_time { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
    }
    /// <summary>
    /// 其它类型素材
    /// </summary>
    public class MaterialIVVBase
    {
        public string media_id { get; set; }

        public string name { get; set; }

        public int update_time { get; set; }

        public string url { get; set; }

    }

}
