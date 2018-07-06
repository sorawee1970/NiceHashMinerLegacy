using System;
using NiceHashMiner.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NiceHashMinerLegacy.Common.Utils;
using NiceHashMinerLegacy.Devices;

namespace NiceHashMiner.Miners
{
    public static class MinersManager
    {
        private static MiningSession _curMiningSession;

        public static void StopAllMiners()
        {
            _curMiningSession?.StopAllMiners();
            _curMiningSession = null;
        }

        public static void StopAllMinersNonProfitable()
        {
            _curMiningSession?.StopAllMinersNonProfitable();
        }

        public static string GetActiveMinersGroup()
        {
            // if no session it is idle
            return _curMiningSession != null ? _curMiningSession.GetActiveMinersGroup() : "IDLE";
        }

        public static List<int> GetActiveMinersIndexes()
        {
            return _curMiningSession != null ? _curMiningSession.ActiveDeviceIndexes : new List<int>();
        }

        public static double GetTotalRate()
        {
            return _curMiningSession?.GetTotalRate() ?? 0;
        }

        public static bool StartInitialize(IMainFormRatesComunication mainFormRatesComunication,
            string miningLocation, string worker, string btcAdress)
        {
            _curMiningSession = new MiningSession(Available.Devices,
                mainFormRatesComunication, miningLocation, worker, btcAdress);

            return _curMiningSession.IsMiningEnabled;
        }

        public static bool IsMiningEnabled()
        {
            return _curMiningSession != null && _curMiningSession.IsMiningEnabled;
        }


        /// <summary>
        /// SwichMostProfitable should check the best combination for most profit.
        /// Calculate profit for each supported algorithm per device and group.
        /// </summary>
        /// <param name="niceHashData"></param>
        //[Obsolete("Deprecated in favour of AlgorithmSwitchingManager timer")]
        //public static async Task SwichMostProfitableGroupUpMethod()
        //{
        //    if (_curMiningSession != null) await _curMiningSession.SwichMostProfitableGroupUpMethod();
        //}

        public static async Task MinerStatsCheck()
        {
            if (_curMiningSession != null) await _curMiningSession.MinerStatsCheck();
        }

        public static List<JArray> GetDeviceStatus()
        {
            var devices = Available.Devices;
            var deviceList = new List<JArray>();
            var activeIDs = GetActiveMinersIndexes();
            foreach (var device in devices)
            {
                try
                {
                    var array = new JArray
                    {
                        device.Index,
                        device.Name
                    };
                    var status = Convert.ToInt32(activeIDs.Contains(device.Index)) + ((int)device.DeviceType + 1) * 2;
                    array.Add(status);
                    array.Add((int)Math.Round(device.Load));
                    array.Add((int)Math.Round(device.Temp));
                    array.Add(device.FanSpeed);

                    deviceList.Add(array);
                }
                catch (Exception e) { Helpers.ConsolePrint("SOCKET", e.ToString()); }
            }

            return deviceList;
        }
    }
}
