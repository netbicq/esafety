using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.WXHelper.Dto
{
    public class APIResult<T>
    {

        public APIResult()
        {
            
        }

        public APIResult(T rdata)
        {
            data =rdata;
            state = 200;
            errmsg = "";
        }

        public APIResult(Exception error)
        {
            state = 1000;
            errmsg = error.Message;
            data = default(T);
        }

        public T data { get; set; }

        public int state { get; set; }

        public string errmsg { get; set; }

    }
}
