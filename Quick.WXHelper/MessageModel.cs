using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.WXHelper
{

    public class RedPackResponse
    {

        public string result_code { get; set; }

        public string return_msg { get; set; }
    }

    public class WxMessageBase
    {
        /// <summary>
        /// 微信公众号
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 发送账号 openid
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 时间，时间戳
        /// </summary>
        public int CreateTime { get; set; }
        /// <summary>
        /// 消息类型：
        /// text;文本
        /// image;图片
        /// voice;声音
        /// video;视频
        /// shortvideo;短视频
        /// location;位置
        /// link;连接
        /// event;事件
        /// </summary>
        public string MsgType { get; set; }
    }

    /// <summary>
    /// 消息基类
    /// </summary>
    public class MessageBase:WxMessageBase
    {
      
        /// <summary>
        /// 消息ID
        /// </summary>
        public int MsgId { get; set; }

    }
    /// <summary>
    /// 事件消息基类
    /// </summary>
    public class EventMessageBase : WxMessageBase
    {
        /// <summary>
        /// 事件
        /// </summary>
        public string Event { get; set; }
    }
    /// <summary>
    /// 文本消息
    /// </summary>
    public class TextMessage:MessageBase
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
    /// <summary>
    /// 图片消息
    /// </summary>
    public class ImgMessage:MessageBase
    {
        /// <summary>
        /// 图片地址
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 下载地址
        /// </summary>
        public string MediaId { get; set; }
    }
    /// <summary>
    /// 语音消息
    /// </summary>
    public class VoiceMessage : MessageBase
    {
        /// <summary>
        /// 下载地址
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 格式
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// 语音识别后的文本
        /// </summary>
        public string Recognition { get; set; }
    }
    /// <summary>
    /// 视频消息，小视频消息
    /// </summary>
    public class VideoMessage : MessageBase
    {
        /// <summary>
        /// 下载地址
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 微缩图地址，播放地址
        /// </summary>
        public string ThumbMediaId { get; set; }
    }
    /// <summary>
    /// 地理位置推送消息
    /// </summary>
    public class LocationEvenMessage:MessageBase
    {
        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 精度
        /// </summary>
        public double Precision { get; set; }
    }

    /// <summary>
    /// 地理位置消息
    /// </summary>
    public class LocationMessage : MessageBase
    {
        /// <summary>
        /// X坐标
        /// </summary>
        public decimal Location_X { get; set; }
        /// <summary>
        /// Y坐标
        /// </summary>
        public decimal Location_Y { get; set; }
        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public int Scale { get; set; }
        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }
    }

    /// <summary>
    /// 菜单事件消息体
    /// </summary>
    public class MenuEvent:EventMessageBase
    {
        /// <summary>
        /// 菜单Key
        /// </summary>
        public string EventKey { get; set; }
    }
}
