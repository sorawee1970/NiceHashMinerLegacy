using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NiceHashMinerLegacy.Common.Enums;
using NiceHashMinerLegacy.Devices.Algorithms;
using NiceHashMinerLegacy.Web.Stats;
using NiceHashMinerLegacy.Web.Tests.Stats;

namespace NiceHashMinerLegacy.Devices.Tests.Algorithms
{
    [TestClass]
    public class AlgorithmTest
    {
        // All arbitrary
        private const double PowerUsage = 245.80;
        private const AlgorithmType Algo = AlgorithmType.Equihash;
        private const MinerBaseType Miner = MinerBaseType.EWBF;
        private const double Speed = 3654181.233518;

        private static Algorithm _algorithm;
        private static double _kwhInBtc;

        private static readonly Dictionary<AlgorithmType, double> Profits = new Dictionary<AlgorithmType, double>
        {
            {Algo, 0.32548}
        };

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            // Use this to init exchange rates
            ExchangeRateApiTest.Initialize(context);
            // Assume this is returning correct value because it's tested elsewhere
            _kwhInBtc = ExchangeRateApi.GetKwhPriceInBtc();
            _algorithm = new Algorithm(Miner, Algo, "")
            {
                AvaragedSpeed = Speed,
                PowerUsage = PowerUsage
            };
        }

        [TestMethod]
        public void Algorithm_ShouldSubtractPower()
        {
            _algorithm.UpdateCurProfit(Profits);
            // How much is paid by mining
            var expectedRevenue = Profits[Algo] * Speed * 0.000000001;
            // How much is lost to elec prices
            var expectedPowerCost = _kwhInBtc / 1000 * PowerUsage * 24;
            // expected profit = revenue - power costs
            Assert.AreEqual(expectedRevenue - expectedPowerCost, _algorithm.CurrentProfit);
        }
    }
}
