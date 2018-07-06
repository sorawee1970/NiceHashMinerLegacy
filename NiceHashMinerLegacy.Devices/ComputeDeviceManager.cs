using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NiceHashMiner;
using NiceHashMiner.Interfaces;
using NiceHashMinerLegacy.Common;
using NiceHashMinerLegacy.Common.Configs;
using NiceHashMinerLegacy.Common.Enums;
using NiceHashMinerLegacy.Common.Utils;
using NiceHashMinerLegacy.Devices.Device;
using NiceHashMinerLegacy.Devices.Querying;
using NiceHashMinerLegacy.Web.Stats;
using NiceHashMinerLegacy.Web.Stats.Models;

namespace NiceHashMinerLegacy.Devices
{
    /// <summary>
    /// ComputeDeviceManager class is used to query ComputeDevices avaliable on the system.
    /// Query CPUs, GPUs [Nvidia, AMD]
    /// </summary>
    public abstract class ComputeDeviceManager
    {
        public static SystemSpecs SystemSpecs;
        public static void InitFakeDevs()
        {
            var r = new Random();
            for (var i = 0; i < 4; i++)
            {
                Available.Add(new ComputeDevice(r.Next()));
            }
        }

        public static void OnSetDeviceEnabled(object sender, SocketEventArgs e)
        {
            var found = false;
            if (!Available.Devices.Any())
                throw new RpcException("No devices to set", 1);

            foreach (var dev in Available.Devices)
            {
                if (e.Message != "*" && dev.B64Uuid != e.Message) continue;
                found = true;
                dev.Enabled = e.Enabled;
            }

            if (!found)
                throw new RpcException("Device not found", 1);
        }
    }
}
