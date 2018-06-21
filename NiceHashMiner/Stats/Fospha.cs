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
                ["i"] = GetIParam(id, DateTime.Now.GetUnixTime()),
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

        internal static string GetIParam(string id, ulong unixTimestamp)
        {
            const int index = 1;
            const int num = 1;

            ulong hash;

            lock (R)
            unchecked
            {
                hash = (ulong) Math.Abs(Math.Floor(R.NextDouble() * Math.Pow(2, 32)));
            }

            var hit = $"{unixTimestamp}.{hash}.{id}";

            return $"{index}.{num}.{hit}";
        }

        internal static string GetDParam(string eventName, string id)
        {
            return "";
        }

        internal static string GenChecksum(string data)
        {
            const ushort largePrime = 65521;
            const uint numShort = 65536;

            var a = 1ul;
            var b = 0ul;

            foreach (var c in data)
            unchecked
            {
                a += c;
                b += a;
            }

            a %= largePrime;
            b %= largePrime;

            var chk = b * numShort + a;
            return chk.ToString("x8");
        }
    }
}
