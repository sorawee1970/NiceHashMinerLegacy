using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NiceHashMinerLegacy.Common.Enums;
using NiceHashMiner.Miners.Parsing;
using NiceHashMiner.Algorithms;

namespace NiceHashMiner.Miners
{
    public class MkxMiner : Miner
    {
        public MkxMiner() : base("MkxMiner")
        {
            IsApiReadException = true;
        }
        public override Task<ApiData> GetSummaryAsync()
        {
            return Task.FromResult(GetExceptionSummary());
        }

        public override void Start(string url, string btcAdress, string worker)
        {
            LastCommandLine = CreateLaunchCommand(url, btcAdress, worker);
            ProcessHandle = _Start();
        }

        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
        {
            throw new NotImplementedException();
        }

        protected override void BenchmarkOutputErrorDataReceivedImpl(string outdata)
        {
            throw new NotImplementedException();
        }

        protected override bool BenchmarkParseLine(string outdata)
        {
            throw new NotImplementedException();
        }

        protected override int GetMaxCooldownTimeInMilliseconds()
        {
            return 5 * 60 * 1000;  // Doesn't matter since API read exc
        }

        protected override void _Stop(MinerStopType willswitch)
        {
            Stop_cpu_ccminer_sgminer_nheqminer(willswitch);
        }

        private string CreateLaunchCommand(string url, string btcAddress, string worker)
        {
            var devices = string.Join(",", MiningSetup.MiningPairs.Select(p => p.Device.ID));
            return $" {ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.AMD)}" +
                   $" -o {url} -u {btcAddress}.{worker} -p x -d {devices}";
        }
    }
}
