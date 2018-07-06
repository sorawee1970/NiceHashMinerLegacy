using System.Collections.Generic;
using Newtonsoft.Json;

namespace NiceHashMinerLegacy.Web.Stats.Models
{
    internal class ExecutedCall
    {
        public readonly string method = "executed";
        [JsonProperty("params")]
        public List<object> Params = new List<object> { 0 };  // TODO ID?

        public ExecutedCall(int code, string message)
        {
            Params.Add(code);
            if (message != null)
            {
                Params.Add(message);
            }
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
