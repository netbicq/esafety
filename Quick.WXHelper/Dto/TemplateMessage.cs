using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.WXHelper.Dto
{

    /// <summary>
    /// 发送模板消息
    /// </summary>
    public class TemplateMessagePara
    {
        /// <summary>
        /// 用户openid
        /// </summary>
        public string touser { get; set; }
        /// <summary>
        /// 模板ID
        /// </summary>
        public string template_id { get; set; }
        /// <summary>
        /// 详情跳转地址
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 关联小程序
        /// </summary>
        public MessageMini miniprogram { get; set; }
        /// <summary>
        /// 业务数据
        /// </summary>
        public IDictionary<string, MessageDataBase> data { get; set; }

    }
    /// <summary>
    /// 模板消息的小程序数据
    /// </summary>
    public class MessageMini
    {
        /// <summary>
        /// 小程序appid
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 小程序具体页面路径
        /// </summary>
        public string pagepath { get; set; }
    }
    public class MessageDataBase
    {
        /// <summary>
        /// 业务数据
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 业务数据颜色
        /// </summary>
        public string color { get { return "#FF3030"; } }
    }

    public class TemplateMessage
    {
        public List<TemplateMessageItem> template_list { get; set; }
    }

    /// <summary>
    /// 模板消息Model
    /// </summary>
    public class TemplateMessageItem
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public string template_id { get; set; }
        /// <summary>
        /// 标题 
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 一级行业
        /// </summary>
        public string primary_industry { get; set; }
        /// <summary>
        /// 二级行业
        /// </summary>
        public string deputy_industry { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 示例
        /// </summary>
        public string example { get; set; }
    }
}
