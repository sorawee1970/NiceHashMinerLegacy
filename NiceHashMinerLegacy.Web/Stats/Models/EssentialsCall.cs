using System.Collections.Generic;
using Newtonsoft.Json;

namespace NiceHashMinerLegacy.Web.Stats.Models
{
    public class EssentialsCall
    {
        [JsonProperty("params")]
        public List<List<object>> Params;
    }
}
