using System;

namespace NiceHashMiner.Stats.Models
{
    internal class RpcException : Exception
    {
        public int Code = 0;

        public RpcException(string message, int code)
            : base(message)
        {
            Code = code;
        }
    }
}
