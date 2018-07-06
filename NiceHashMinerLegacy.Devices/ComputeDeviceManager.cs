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
