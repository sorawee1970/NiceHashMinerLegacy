using Microsoft.VisualStudio.TestTools.UnitTesting;
using NiceHashMiner.Stats;
using NiceHashMinerLegacy.Common.Enums;

namespace NiceHashMinerLegacy.Tests.Stats
{
    [TestClass]
    public class FosphaTest
    {
        private const string I = "1.1.1520438617336.1396536818.3.76631100.1520346127082.55930195";
        private const string TestUser = "3.76631100.1520346127082.55930195";
        private const ulong TestTime = 1520438617336;

        private const string WholeQuery = "v=14.0&i=1.1.1520438617336.1396536818.1382debe&t=toycwpgij&d=c3MxNTIwNDM4NjE3MzMycjMwNS4x*MQ__*ZXZlbnQ_*My43NjYzMTEwMC4xNTIwMzQ2MTI3MDgyLjU1OTMwMTk1*by5w*aHR0cHM6Ly9taW5lcmdvYWxldmVudC5uaWNlaGFzaC5jb20_*eyJ0eiI6LCJsYW5ndWFnZSI6IiIsImVuY29kaW5nIjoiIiwic2NyZWVuQ29sb3JzIjosInNjcmVlblJlc29sdXRpb24iOiIifQ__*eyJ0aXRsZSI6IiBOaWNlSGFzaCBNaW5lciBPbmxpbmUgQWN0aXZpdHkgIn0_*eyJldmVudENhdGVnb3J5IjoiZ29hbCIsImV2ZW50QWN0aW9uIjoiYWNoaWV2ZWQiLCJldmVudExhYmVsIjoibWluZXIgb25saW5lIiwiZXZlbnRDYXRMYWJlbCI6ImdvYWwiLCJldmVudEl0ZW1JZCI6Im1pbmVyIG9ubGluZSJ9&t=0";

        private const string TestD = "anMxNTIwNDM4NjE3MzMycjUwMC4w*MQ__*ZXZlbnQ_*My43NjYzMTEwMC4xNTIwMzQ2MTI3MDgyLjU1OTMwMTk1*by5w*aHR0cHM6Ly9taW5lcmdvYWxldmVudC5uaWNlaGFzaC5jb20/eyJjYW1wYWlnblNvdXJjZSI6IiIsImNhbXBhaWduTWVkaXVtIjoiIiwiY2FtcGFpZ25OYW1lIjoiIiwiY2FtcGFpZ25LZXl3b3JkIjoiIiwiY2FtcGFpZ25Db250ZW50IjoiIiwiY2FtcGFpZ25McCI6IiIsImNhbXBhaWduQWRpZCI6IiJ9*eyJ0eiI6IiIsImxhbmd1YWdlIjoiIiwiZW5jb2RpbmciOiIiLCJzY3JlZW5Db2xvcnMiOiIiLCJzY3JlZW5SZXNvbHV0aW9uIjoiIn0_*eyJ0aXRsZSI6Ik5pY2VIYXNoIE1pbmVyIE9ubGluZSBBY3Rpdml0eSJ9*eyJldmVudENhdGVnb3J5IjoiZ29hbCIsImV2ZW50Q2F0TGFiZWwiOiJnb2FsIiwiZXZlbnRBY3Rpb24iOiJhY2hpZXZlZCIsImV2ZW50TGFiZWwiOiJMYXVuY2giLCJldmVudEl0ZW1JZCI6IkxhdW5jaCIsImV2ZW50VGV4dCI6IiIsImV2ZW50VmFsdWUiOiIifQ__*eyJjYW1wYWlnblNvdXJjZSI6IiIsImNhbXBhaWduTWVkaXVtIjoiIiwiY2FtcGFpZ25OYW1lIjoiIiwiY2FtcGFpZ25LZXl3b3JkIjoiIiwiY2FtcGFpZ25Db250ZW50IjoiIiwiY2FtcGFpZ25McCI6IiIsImNhbXBhaWduQWRpZCI6IiJ9";

        [TestMethod]
        public void IParamShouldMatch()
        {
            var i = Fospha.GetIParam(TestUser, TestTime);

            var subs = i.Split('.');
            var testSubs = I.Split('.');

            Assert.AreEqual(testSubs.Length, subs.Length);

            for (var k = 0; k < subs.Length; k++)
            {
                // Ignore the random part
                if (k == 3) continue;

                Assert.AreEqual(testSubs[k], subs[k]);
            }
        }

        [TestMethod]
        public void ChecksumShouldMatch()
        {
            const string goodChk = "430cb374";
            var chk = Fospha.GenChecksum(WholeQuery);

            Assert.AreEqual(goodChk, chk);
        }

        [TestMethod]
        public void DParamShouldMatch()
        {
            const ulong time = 1520438617332;

            var ev = Events.Launch;
            var d = Fospha.GetDParam(ev, TestUser, time, "", "");

            var subs = d.Split('*');
            var testSubs = TestD.Split('*');

            Assert.AreEqual(testSubs.Length, subs.Length);

            for (var i = 1; i < subs.Length; i++)
            {
                Assert.AreEqual(testSubs[i], subs[i]);
            }
        }
    }
}
