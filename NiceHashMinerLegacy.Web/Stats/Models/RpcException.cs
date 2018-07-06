using System;

namespace NiceHashMinerLegacy.Web.Stats.Models
{
    public class RpcException : Exception
    {
        public int Code = 0;

        public RpcException(string message, int code)
            : base(message)
        {
            Code = code;
        }
    }
}
