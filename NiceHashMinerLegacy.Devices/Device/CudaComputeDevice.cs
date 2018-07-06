using NiceHashMinerLegacy.Common;
using NiceHashMinerLegacy.Common.Enums;
using NiceHashMinerLegacy.Devices.Algorithms;

namespace NiceHashMinerLegacy.Devices.Device
{
    public abstract class CudaComputeDevice : ComputeDevice
    {
        protected int SMMajor;
        protected int SMMinor;

        public CudaComputeDevice(CudaDevice cudaDevice, DeviceGroupType group, int gpuCount)
            : base((int) cudaDevice.DeviceID,
                cudaDevice.GetName(),
                true,
                group,
                cudaDevice.IsEtherumCapable(),
                DeviceType.NVIDIA,
                string.Format(International.GetText("ComputeDevice_Short_Name_NVIDIA_GPU"), gpuCount),
                cudaDevice.DeviceGlobalMemory)
        {
            BusID = cudaDevice.pciBusID;
            SMMajor = cudaDevice.SM_major;
            SMMinor = cudaDevice.SM_minor;
            Uuid = cudaDevice.UUID;
            AlgorithmSettings = GroupAlgorithms.CreateForDeviceList(this);
            Index = ID + Available.AvailCpus; // increment by CPU count
        }
    }
}
