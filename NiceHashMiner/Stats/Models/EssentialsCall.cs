using System.Collections.Generic;
using Newtonsoft.Json;

namespace NiceHashMiner.Stats.Models
{
    public class EssentialsCall
    {
        [JsonProperty("params")]
        public List<List<object>> Params;
    }
}
