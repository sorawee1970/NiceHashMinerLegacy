using NiceHashMinerLegacy.Common;
using NiceHashMinerLegacy.Common.Enums;
using NiceHashMinerLegacy.Devices.Algorithms;

namespace NiceHashMinerLegacy.Devices.Device
{
    public abstract class CpuComputeDevice : ComputeDevice
    {
        public CpuComputeDevice(int id, string group, string name, int threads, ulong affinityMask, int cpuCount)
            : base(id,
                name,
                true,
                DeviceGroupType.CPU,
                false,
                DeviceType.CPU,
                string.Format(International.GetText("ComputeDevice_Short_Name_CPU"), cpuCount),
                0)
        {
            Threads = threads;
            AffinityMask = affinityMask;
            Uuid = GetUuid(ID, GroupNames.GetGroupName(DeviceGroupType, ID), Name, DeviceGroupType);
            AlgorithmSettings = GroupAlgorithms.CreateForDeviceList(this);
            Index = ID; // Don't increment for CPU
        }
    }
}
