using Newtonsoft.Json;
using NiceHashMinerLegacy.Common.Enums;
using NiceHashMinerLegacy.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMiner.Stats
{
    public static class Fospha
    {
        private const string Endpoint = "https://uktc.fospha.com/nicehash/track.gif";
        private const string V = "14.0";
        private const string F = "toycwpgijh";
        private const string T = "0";

        private static readonly Random R = new Random();

        public static Task LogStartMining(int numDevs)
        {
            return LogEventImpl(Events.MiningStart, "{Version:\"NHML\"}", numDevs.ToString());
        }

        public static Task LogAddWallet(string address)
        {
            return LogEventImpl(Events.AddWallet, address, "");
        }

        public static Task LogEvent(Events name)
        {
            return LogEventImpl(name, "", "");
        }

        private static async Task LogEventImpl(Events name, string eventText, string eventValue)
        {
            var id = "30.1427915758.1531757676537.483558df";  // TODO
            var ts = DateTime.Now.GetUnixTime();

            try
            {
                var pars = new Dictionary<string, string>
                {
                    ["v"] = V,
                    ["i"] = GetIParam(id, ts),
                    ["f"] = F,
                    ["d"] = GetDParam(name, id, ts, eventText, eventValue),
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

                Helpers.ConsolePrint("Fospha", finalReq);

                if (name != Events.MiningStart) return;

                var wr = (HttpWebRequest)WebRequest.Create(finalReq);
                wr.Timeout = 30 * 1000;
                using (var response = wr.GetResponse())
                {
                    var ret = ((HttpWebResponse) response).StatusCode;
                    if (ret != HttpStatusCode.OK)
                        throw new WebException($"Return code: {ret}");
                    if (response.ContentType != "image/gif")
                        throw new WebException("Response not expected content type");
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

        internal static string GetDParam(Events eventName, string id, ulong ts, string eventText, string eventValue)
        {
            var c = new CampaignData
            {
                campaignSource = "dillon",
                campaignMedium = "testing",
                campaignContent = "NHML-Testing"
            };
            var utmSource = $"campaignSource={c.campaignSource}";
            var utmMedium = $"&campaignMedium={c.campaignMedium}";
            var utmContent = $"&campaignContent={c.campaignContent}";

            var trig = "https://minergoalevent.nicehash.com?" + utmSource + utmMedium + utmContent;

            //var eventText = eventName == Events.AddWallet ? ConfigManager.GeneralConfig.BitcoinAddress : "";
            //var eventValue = eventName == Events.MiningStart
            //    ? ComputeDeviceManager.Available.Devices.Count.ToString()
            //    : "";

            var vals = new List<string>();

            // Document ID
            string r;
            lock (R)
            {
                r = (R.Next(1, 10000) / 10d).ToString("F1");
            }

            vals.Add($"js{ts}r{r}");

            // Hit index
            vals.Add("1");

            // Hit type
            vals.Add("event");

            // Client ID
            vals.Add(id);

            // Client ID type
            vals.Add("o.p");

            // Trigger URL
            vals.Add(trig);

            // Software data
            var soft = JsonConvert.SerializeObject(new ClientSoftwareData()) ?? "";
            vals.Add(soft);

            // Page data
            var pd = JsonConvert.SerializeObject(new PageData()) ?? "";
            vals.Add(pd);

            // Event data
            var ed = JsonConvert.SerializeObject(new EventData(eventName.ToString(), eventText, eventValue)) ?? "";
            vals.Add(ed);

            // Campaign data
            var camp = JsonConvert.SerializeObject(c) ?? "";
            vals.Add(camp);

            var base64 = vals.Select(v => Convert.ToBase64String(Encoding.UTF8.GetBytes(v)).Replace('=', '_'));

            return string.Join("*", base64) + "*";
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

        private class CampaignData
        {
            public string campaignSource = "";
            public string campaignMedium = "";
            public string campaignName = "";
            public string campaignKeyword = "";
            public string campaignContent = "";
            public string campaignLp = "";
            public string campaignAdid = "";
        }

        private class ClientSoftwareData
        {
            public string tz = "";
            public string language = "";
            public string encoding = "";
            public string screenColors = "";
            public string screenResolution = "";
        }

        private class PageData
        {
            public string title = "NiceHash Miner Online Activity";
        }

        private class EventData
        {
            public string eventCategory = "goal";
            public string eventCatLabel = "goal";
            public string eventAction = "achieved";
            public string eventLabel;
            public string eventItemId;
            public string eventText;
            public string eventValue;

            public EventData(string name, string text = "", string value = "")
            {
                eventLabel = name;
                eventItemId = name;
                eventText = text;
                eventValue = value;
            }
        }
    }
}
