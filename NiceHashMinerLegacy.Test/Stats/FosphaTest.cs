using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NiceHashMiner.Stats;

namespace NiceHashMinerLegacy.Tests.Stats
{
    [TestClass]
    public class FosphaTest
    {
        private const string I = "1.1.1520438617336.1396536818.3.76631100.1520346127082.55930195";
        private const string TestUser = "3.76631100.1520346127082.55930195";
        private const ulong TestTime = 1520438617336;

        private const string WholeQuery = "v=14.0&i=1.1.1520438617336.1396536818.1382debe&t=toycwpgij&d=c3MxNTIwNDM4NjE3MzMycjMwNS4x*MQ__*ZXZlbnQ_*My43NjYzMTEwMC4xNTIwMzQ2MTI3MDgyLjU1OTMwMTk1*by5w*aHR0cHM6Ly9taW5lcmdvYWxldmVudC5uaWNlaGFzaC5jb20_*eyJ0eiI6LCJsYW5ndWFnZSI6IiIsImVuY29kaW5nIjoiIiwic2NyZWVuQ29sb3JzIjosInNjcmVlblJlc29sdXRpb24iOiIifQ__*eyJ0aXRsZSI6IiBOaWNlSGFzaCBNaW5lciBPbmxpbmUgQWN0aXZpdHkgIn0_*eyJldmVudENhdGVnb3J5IjoiZ29hbCIsImV2ZW50QWN0aW9uIjoiYWNoaWV2ZWQiLCJldmVudExhYmVsIjoibWluZXIgb25saW5lIiwiZXZlbnRDYXRMYWJlbCI6ImdvYWwiLCJldmVudEl0ZW1JZCI6Im1pbmVyIG9ubGluZSJ9&t=0";

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
    }
}
