using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using NiceHashMinerLegacy.Common.Utils;

namespace NiceHashMinerLegacy.Windows
{
    public class SystemSpecs : Devices.SystemSpecs
    {
        public SystemSpecs()
        {
            var winQuery = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");

            var searcher = new ManagementObjectSearcher(winQuery);

            foreach (ManagementObject item in searcher.Get())
            {
                if (item["FreePhysicalMemory"] != null)
                    ulong.TryParse(item["FreePhysicalMemory"].ToString(), out FreePhysicalMemory);
                if (item["FreeSpaceInPagingFiles"] != null)
                    ulong.TryParse(item["FreeSpaceInPagingFiles"].ToString(), out FreeSpaceInPagingFiles);
                if (item["FreeVirtualMemory"] != null)
                    ulong.TryParse(item["FreeVirtualMemory"].ToString(), out FreeVirtualMemory);
                if (item["LargeSystemCache"] != null)
                    uint.TryParse(item["LargeSystemCache"].ToString(), out LargeSystemCache);
                if (item["MaxNumberOfProcesses"] != null)
                    uint.TryParse(item["MaxNumberOfProcesses"].ToString(), out MaxNumberOfProcesses);
                if (item["MaxProcessMemorySize"] != null)
                    ulong.TryParse(item["MaxProcessMemorySize"].ToString(), out MaxProcessMemorySize);
                if (item["NumberOfLicensedUsers"] != null)
                    uint.TryParse(item["NumberOfLicensedUsers"].ToString(), out NumberOfLicensedUsers);
                if (item["NumberOfProcesses"] != null)
                    uint.TryParse(item["NumberOfProcesses"].ToString(), out NumberOfProcesses);
                if (item["NumberOfUsers"] != null)
                    uint.TryParse(item["NumberOfUsers"].ToString(), out NumberOfUsers);
                if (item["OperatingSystemSKU"] != null)
                    uint.TryParse(item["OperatingSystemSKU"].ToString(), out OperatingSystemSKU);
                if (item["SizeStoredInPagingFiles"] != null)
                    ulong.TryParse(item["SizeStoredInPagingFiles"].ToString(), out SizeStoredInPagingFiles);
                if (item["SuiteMask"] != null) uint.TryParse(item["SuiteMask"].ToString(), out SuiteMask);
                if (item["TotalSwapSpaceSize"] != null)
                    ulong.TryParse(item["TotalSwapSpaceSize"].ToString(), out TotalSwapSpaceSize);
                if (item["TotalVirtualMemorySize"] != null)
                    ulong.TryParse(item["TotalVirtualMemorySize"].ToString(), out TotalVirtualMemorySize);
                if (item["TotalVisibleMemorySize"] != null)
                    ulong.TryParse(item["TotalVisibleMemorySize"].ToString(), out TotalVisibleMemorySize);
                // log
                Helpers.ConsolePrint("SystemSpecs", $"FreePhysicalMemory = {FreePhysicalMemory}");
                Helpers.ConsolePrint("SystemSpecs", $"FreeSpaceInPagingFiles = {FreeSpaceInPagingFiles}");
                Helpers.ConsolePrint("SystemSpecs", $"FreeVirtualMemory = {FreeVirtualMemory}");
                Helpers.ConsolePrint("SystemSpecs", $"LargeSystemCache = {LargeSystemCache}");
                Helpers.ConsolePrint("SystemSpecs", $"MaxNumberOfProcesses = {MaxNumberOfProcesses}");
                Helpers.ConsolePrint("SystemSpecs", $"MaxProcessMemorySize = {MaxProcessMemorySize}");
                Helpers.ConsolePrint("SystemSpecs", $"NumberOfLicensedUsers = {NumberOfLicensedUsers}");
                Helpers.ConsolePrint("SystemSpecs", $"NumberOfProcesses = {NumberOfProcesses}");
                Helpers.ConsolePrint("SystemSpecs", $"NumberOfUsers = {NumberOfUsers}");
                Helpers.ConsolePrint("SystemSpecs", $"OperatingSystemSKU = {OperatingSystemSKU}");
                Helpers.ConsolePrint("SystemSpecs", $"SizeStoredInPagingFiles = {SizeStoredInPagingFiles}");
                Helpers.ConsolePrint("SystemSpecs", $"SuiteMask = {SuiteMask}");
                Helpers.ConsolePrint("SystemSpecs", $"TotalSwapSpaceSize = {TotalSwapSpaceSize}");
                Helpers.ConsolePrint("SystemSpecs", $"TotalVirtualMemorySize = {TotalVirtualMemorySize}");
                Helpers.ConsolePrint("SystemSpecs", $"TotalVisibleMemorySize = {TotalVisibleMemorySize}");
            }
        }
    }
}
