using System.Collections.Generic;
using System.Linq;
using NiceHashMinerLegacy.Common.Enums;
using NiceHashMinerLegacy.Devices.Device;

namespace NiceHashMinerLegacy.Devices
{
    public static class Available
    {
        public static bool HasNvidia = false;
        public static bool HasAmd = false;
        public static bool HasCpu = false;
        public static int CpusCount = 0;

        public static int AvailCpus
        {
            get { return Devices.Count(d => d.DeviceType == DeviceType.CPU); }
        }

        public static int AvailNVGpus
        {
            get { return Devices.Count(d => d.DeviceType == DeviceType.NVIDIA); }
        }

        public static int AvailAmdGpus
        {
            get { return Devices.Count(d => d.DeviceType == DeviceType.AMD); }
        }

        public static int AvailGpUs => AvailAmdGpus + AvailNVGpus;
        public static int AmdOpenCLPlatformNum = -1;
        public static bool IsHyperThreadingEnabled = false;

        public static ulong NvidiaRamSum = 0;
        public static ulong AmdRamSum = 0;

        private static readonly List<ComputeDevice> _devices = new List<ComputeDevice>();
        public static IReadOnlyList<ComputeDevice> Devices => _devices.AsReadOnly();

        // methods
        public static ComputeDevice GetDeviceWithUuid(string uuid)
        {
            return Devices.FirstOrDefault(dev => uuid == dev.Uuid);
        }

        public static List<ComputeDevice> GetSameDevicesTypeAsDeviceWithUuid(string uuid)
        {
            var compareDev = GetDeviceWithUuid(uuid);
            return (from dev in Devices
                    where uuid != dev.Uuid && compareDev.DeviceType == dev.DeviceType
                    select GetDeviceWithUuid(dev.Uuid)).ToList();
        }

        public static ComputeDevice GetCurrentlySelectedComputeDevice(int index, bool unique)
        {
            return Devices[index];
        }

        public static int GetCountForType(DeviceType type)
        {
            return Devices.Count(device => device.DeviceType == type);
        }

        public static void Add(ComputeDevice dev)
        {
            _devices.Add(dev);
        }

        public static void DisableCpuGroup()
        {
            foreach (var device in Devices)
            {
                if (device.DeviceType == DeviceType.CPU)
                {
                    device.Enabled = false;
                }
            }
        }

        public static bool ContainsAmdGpus
        {
            get { return Devices.Any(device => device.DeviceType == DeviceType.AMD); }
        }

        public static bool ContainsGpus
        {
            get
            {
                return Devices.Any(device =>
                    device.DeviceType == DeviceType.NVIDIA || device.DeviceType == DeviceType.AMD);
            }
        }

        public static void UncheckedCpu()
        {
            // Auto uncheck CPU if any GPU is found
            if (ContainsGpus) DisableCpuGroup();
        }
    }
}
