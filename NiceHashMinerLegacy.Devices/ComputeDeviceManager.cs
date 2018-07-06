using NiceHashMinerLegacy.Common.Interfaces;
using NiceHashMinerLegacy.Devices.Device;
using NiceHashMinerLegacy.Web.Stats;
using NiceHashMinerLegacy.Web.Stats.Models;
using System;
using System.Linq;

namespace NiceHashMinerLegacy.Devices
{
    /// <summary>
    /// ComputeDeviceManager class is used to query ComputeDevices avaliable on the system.
    /// Query CPUs, GPUs [Nvidia, AMD]
    /// </summary>
    public abstract class ComputeDeviceManager
    {
        public static SystemSpecs SystemSpecs;

        public static ICpuAdjuster CpuAdjuster;

        static ComputeDeviceManager()
        {
            NiceHashStats.SetDevicesEnabled = OnSetDeviceEnabled;
        }

        public static void InitFakeDevs()
        {
            var r = new Random();
            for (var i = 0; i < 4; i++)
            {
                Available.Add(new ComputeDevice(r.Next()));
            }
        }

        public static void OnSetDeviceEnabled(string devs, bool enabled)
        {
            var found = false;
            if (!Available.Devices.Any())
                throw new RpcException("No devices to set", 1);

            foreach (var dev in Available.Devices)
            {
                if (devs != "*" && dev.B64Uuid != devs) continue;
                found = true;
                dev.Enabled = enabled;
            }

            if (!found)
                throw new RpcException("Device not found", 1);
        }
    }
}
