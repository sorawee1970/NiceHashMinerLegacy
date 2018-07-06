using System;
using System.Diagnostics;
using NiceHashMinerLegacy.Common.Utils;

namespace NiceHashMinerLegacy.Windows.Device
{
    public class CpuComputeDevice : Devices.Device.CpuComputeDevice
    {
        private readonly PerformanceCounter _cpuCounter;

        public override float Load
        {
            get
            {
                try
                {
                    if (_cpuCounter != null) return _cpuCounter.NextValue();
                }
                catch (Exception e) { Helpers.ConsolePrint("CPUDIAG", e.ToString()); }
                return -1;
            }
        }

        internal CpuComputeDevice(int id, string group, string name, int threads, ulong affinityMask, int cpuCount)
            : base(id,
                group,
                name,
                threads,
                affinityMask,
                cpuCount)
        {
            _cpuCounter = new PerformanceCounter
            {
                CategoryName = "Processor",
                CounterName = "% Processor Time",
                InstanceName = "_Total"
            };
        }
    }
}
