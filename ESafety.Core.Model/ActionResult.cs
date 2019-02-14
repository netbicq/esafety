using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Core.Model
{
    /// <summary>
    /// api返回统一数据格式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ActionResult<T>
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ActionResult()
        {

        }
        /// <summary>
        /// 如果是异常，处理详细异常信息
        /// </summary>
        /// <param name="err"></param>
        public ActionResult(Exception err)
        {
            state = 1000;
            string re = err.Message;
            if (err.InnerException != null)
            {
                re += " 详情：" + err.InnerException.Message;
                if (err.InnerException.InnerException != null)
                {
                    re += err.InnerException.InnerException.Message;
                }
            }
            msg = re;
            data = default(T);
        }
        /// <summary>
        /// 返回正常数据
        /// </summary>
        /// <param name="re"></param>
        public ActionResult(T re)
        {
            state = 200;
            msg = "";
            data = re;
        }
        /// <summary>
        /// 状态
        /// </summary>
        public int state { get; set; }
        /// <summary>
        /// 消息，如果有异常，正常为空
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public T data { get; set; }

    }
}
