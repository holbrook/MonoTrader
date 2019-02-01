using System;
using System.Dynamic;

namespace Tangle.Trading
{
    public class ExecuteStatus
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }

        public ExecuteStatus()
        {
        }

        public ExecuteStatus(int code, string msg, dynamic data=null)
        {
            Code = code;
            Message = msg;
            Data = data;
        }
    }
}
