using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Quick.WXHelper
{
    public class Message
    {

        

        public Message(XElement msg)
        {
            var msgbase = WxMsgService.XMLToObj<MessageBase>(msg);
            switch(msgbase.MsgType)
            {
                case "event":
                    MsgType = "event";
                    var evbase = WxMsgService.XMLToObj<EventMessageBase>(msg);
                    Event = evbase.Event;
                    switch(evbase.Event)
                    {
                        case "subscribe": 
                        case "unsubscribe":

                            break;
                        case "LOCATION"://地理位置
                            LocationEvenMsg = WxMsgService.XMLToObj<LocationEvenMessage>(msg);
                            break;
                    }
                    break;                    
                default:
                    break;
            }
        }
        /// <summary>
        /// 消息类类型
        /// </summary>
        public string MsgType { get; set; }
        /// <summary>
        /// 事件
        /// </summary>
        public string Event { get; set; }
        /// <summary>
        /// 地理位置事件消息
        /// </summary>
        public LocationEvenMessage LocationEvenMsg { get; set; } 
        /// <summary>
        /// 文本消息
        /// </summary>
        public TextMessage TextMessage { get; set; }
    }
}
