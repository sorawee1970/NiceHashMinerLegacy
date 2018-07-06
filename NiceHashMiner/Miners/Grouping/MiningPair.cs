using NiceHashMinerLegacy.Devices.Algorithms;
using NiceHashMinerLegacy.Devices.Device;
using NiceHashMinerLegacy.Windows;

namespace NiceHashMiner.Miners.Grouping
{
    public class MiningPair
    {
        public readonly ComputeDevice Device;
        public readonly Algorithm Algorithm;
        public string CurrentExtraLaunchParameters;

        public MiningPair(ComputeDevice d, Algorithm a)
        {
            Device = d;
            Algorithm = a;
            CurrentExtraLaunchParameters = Algorithm.ExtraLaunchParameters;
        }
    }
}
