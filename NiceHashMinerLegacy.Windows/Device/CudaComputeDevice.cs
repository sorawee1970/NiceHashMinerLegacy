using System;
using ManagedCuda.Nvml;
using NiceHashMinerLegacy.Common.Enums;
using NiceHashMinerLegacy.Common.Utils;
using NiceHashMinerLegacy.Devices;

namespace NiceHashMinerLegacy.Windows.Device
{
    public class CudaComputeDevice : Devices.Device.CudaComputeDevice
    {
        private readonly NvPhysicalGpuHandle _nvHandle; // For NVAPI
        private readonly nvmlDevice _nvmlDevice; // For NVML
        private const int GpuCorePState = 0; // memcontroller = 1, videng = 2

        public override float Load
        {
            get
            {
                var load = -1;

                try
                {
                    var rates = new nvmlUtilization();
                    var ret = NvmlNativeMethods.nvmlDeviceGetUtilizationRates(_nvmlDevice, ref rates);
                    if (ret != nvmlReturn.Success)
                        throw new Exception($"NVML get load failed with code: {ret}");

                    load = (int) rates.gpu;
                }
                catch (Exception e)
                {
                    Helpers.ConsolePrint("NVML", e.ToString());
                }

                return load;
            }
        }

        public override float Temp
        {
            get
            {
                var temp = -1f;

                try
                {
                    var utemp = 0u;
                    var ret = NvmlNativeMethods.nvmlDeviceGetTemperature(_nvmlDevice, nvmlTemperatureSensors.Gpu,
                        ref utemp);
                    if (ret != nvmlReturn.Success)
                        throw new Exception($"NVML get temp failed with code: {ret}");

                    temp = utemp;
                }
                catch (Exception e)
                {
                    Helpers.ConsolePrint("NVML", e.ToString());
                }

                return temp;
            }
        }

        public override int FanSpeed
        {
            get
            {
                var fanSpeed = -1;
                if (NVAPI.NvAPI_GPU_GetTachReading != null)
                {
                    var result = NVAPI.NvAPI_GPU_GetTachReading(_nvHandle, out fanSpeed);
                    if (result != NvStatus.OK && result != NvStatus.NOT_SUPPORTED)
                    {
                        // GPUs without fans are not uncommon, so don't treat as error and just return -1
                        Helpers.ConsolePrint("NVAPI", "Tach get failed with status: " + result);
                        return -1;
                    }
                }
                return fanSpeed;
            }
        }

        public override double PowerUsage
        {
            get
            {
                try
                {
                    var power = 0u;
                    var ret = NvmlNativeMethods.nvmlDeviceGetPowerUsage(_nvmlDevice, ref power);
                    if (ret != nvmlReturn.Success)
                        throw new Exception($"NVML power get failed with status: {ret}");

                    return power * 0.001;
                }
                catch (Exception e)
                {
                    Helpers.ConsolePrint("NVML", e.ToString());
                }

                return -1;
            }
        }

        internal CudaComputeDevice(CudaDevice cudaDevice, DeviceGroupType group, int gpuCount,
            NvPhysicalGpuHandle nvHandle, nvmlDevice nvmlHandle)
            : base(cudaDevice, group, gpuCount)
        {

            _nvHandle = nvHandle;
            _nvmlDevice = nvmlHandle;
        }
    }
}
