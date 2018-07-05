using System;
using System.Collections.Generic;
using System.Text;
using NiceHashMinerLegacy.Common.Configs.Data;

namespace NiceHashMinerLegacy.Common.Interfaces
{
    public interface IDevice
    {
        string Uuid { get; }

        DeviceBenchmarkConfig GetAlgorithmDeviceConfig();
        ComputeDeviceConfig GetComputeDeviceConfig();
        void SetFromComputeDeviceConfig(ComputeDeviceConfig conf);
        void SetAlgorithmDeviceConfig(DeviceBenchmarkConfig conf);
        bool IsAlgorithmSettingsInitialized();
    }
}
