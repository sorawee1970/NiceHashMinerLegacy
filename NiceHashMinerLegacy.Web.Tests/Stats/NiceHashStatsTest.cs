using Microsoft.VisualStudio.TestTools.UnitTesting;
using NiceHashMinerLegacy.Common.Configs;
using NiceHashMinerLegacy.Devices;
using NiceHashMinerLegacy.Web.Stats;
using NiceHashMinerLegacy.Web.Stats.Models;
using System.Linq;

namespace NiceHashMinerLegacy.Web.Tests.Stats
{
    [TestClass]
    public class NiceHashStatsTest
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            ComputeDeviceManager.InitFakeDevs();
        }

        [TestMethod]
        public void VersionUpdateShouldParse()
        {
            NiceHashStats.ProcessData(TestSocketCalls.Data);

            Assert.AreEqual("1.9.1.2", NiceHashStats.Version);
        }

        [TestMethod]
        [ExpectedException(typeof(RpcException))]
        public void SetInvalidWorkerShouldThrow()
        {
            NiceHashStats.ProcessData(TestSocketCalls.InvalidWorkerSet);
        }

        [TestMethod]
        public void SetValidWorkerShouldPrase()
        {
            NiceHashStats.ProcessData(TestSocketCalls.ValidWorkerSet);
            Assert.AreEqual("main", ConfigManager.GeneralConfig.WorkerName);
        }

        [TestMethod]
        [ExpectedException(typeof(RpcException))]
        public void SetInvalidUserShouldThrow()
        {
            NiceHashStats.ProcessData(TestSocketCalls.InvalidUserSet);
        }

        [TestMethod]
        public void SetValidUserShouldParse()
        {
            NiceHashStats.ProcessData(TestSocketCalls.ValidUserSet);
            Assert.AreEqual("3KpWmp49Cdbswr23KhjagNbwqiwcFh8Br2", ConfigManager.GeneralConfig.BitcoinAddress);
        }

        [TestMethod]
        public void EnableDevicesShouldMatch()
        {
            var devs = Available.Devices;
            // Start all false
            foreach (var dev in devs)
            {
                dev.Enabled = false;
            }

            var first = devs.First();

            NiceHashStats.ProcessData(string.Format(TestSocketCalls.EnableOne, first.B64Uuid));

            foreach (var dev in devs)
            {
                if (dev.B64Uuid != first.B64Uuid)
                    Assert.IsFalse(dev.Enabled);
                else
                    Assert.IsTrue(dev.Enabled);
            }

            NiceHashStats.ProcessData(TestSocketCalls.EnableAll);

            Assert.IsTrue(devs.All(d => d.Enabled));

            var last = devs.Last();

            NiceHashStats.ProcessData(string.Format(TestSocketCalls.DisableOne, last.B64Uuid));

            foreach (var dev in devs)
            {
                if (dev.B64Uuid != last.B64Uuid)
                    Assert.IsTrue(dev.Enabled);
                else
                    Assert.IsFalse(dev.Enabled);
            }

            NiceHashStats.ProcessData(TestSocketCalls.DisableAll);

            Assert.IsFalse(devs.Any(d => d.Enabled));
        }

        [TestMethod]
        [ExpectedException(typeof(RpcException))]
        public void InvalidEnableShouldThrow()
        {
            NiceHashStats.ProcessData(TestSocketCalls.InvalidEnableOne);
        }

        [TestMethod]
        [ExpectedException(typeof(RpcException))]
        public void InvalidDisableShouldThrow()
        {
            NiceHashStats.ProcessData(TestSocketCalls.InvalidDisableOne);
        }
    }
}
