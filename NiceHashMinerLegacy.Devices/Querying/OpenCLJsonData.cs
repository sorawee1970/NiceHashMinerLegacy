using System.Collections.Generic;

namespace NiceHashMinerLegacy.Devices.Querying
{
    public class OpenCLJsonData
    {
        public string PlatformName = "NONE";
        public int PlatformNum = 0;
        public List<OpenCLDevice> Devices = new List<OpenCLDevice>();
    }
}
