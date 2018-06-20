using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NiceHashMiner.Miners;
using NiceHashMinerLegacy.Extensions;

namespace NiceHashMiner.Stats
{
    public static class Fospha
    {
        private const string Endpoint = "https://uktc.fospha.com/nicehash/track.gif";
        private const string V = "14.0";
        private const string F = "toycwpgij";
        private const string T = "0";

        private static readonly Random R = new Random();

        public static async Task LogEvent(string name)
        {
            var id = "";  // TODO

            var pars = new Dictionary<string, string>
            {
                ["v"] = V,
                ["i"] = GetIParam(id),
                ["f"] = F,
                ["d"] = GetDParam(name, id),
                ["t"] = T
            };
            
            var req = "";
            foreach (var kvp in pars)
            {
                req += $"{kvp.Key}={kvp.Value}&";
            }
            
            var checksum = GenChecksum(req);
            req += $"c={checksum}";
            var finalReq = $"{Endpoint}?{req}";

            try
            {
                var wr = (HttpWebRequest) WebRequest.Create(finalReq);
                wr.Timeout = 30 * 1000;
                using (var response = await wr.GetResponseAsync())
                {
                    Helpers.ConsolePrint("Fospha", response.ContentType);
                }
            }
            catch (Exception e)
            {
                Helpers.ConsolePrint("Fospha", e.ToString());
            }
        }

        private static string GetIParam(string id)
        {
            const int index = 1;
            const int num = 1;

            var unixNow = DateTime.Now.GetUnixTime();

            ulong hash;

            lock (R)
            unchecked
            {
                hash = (ulong) Math.Abs(Math.Floor(R.NextDouble() * Math.Pow(2, 32)));
            }

            var hit = $"{unixNow}.{hash}.{id}";

            return $"{index}.{num}.{hit}";
        }

        private static string GetDParam(string eventName, string id)
        {
            return "";
        }

        private static string GenChecksum(string data)
        {
            return "";
        }
    }
}
