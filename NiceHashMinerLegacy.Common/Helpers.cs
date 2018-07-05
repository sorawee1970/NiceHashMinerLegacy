using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NiceHashMinerLegacy.Common.Configs;

namespace NiceHashMinerLegacy.Common
{
    class Helpers
    {
        public static void ConsolePrint(string grp, string text)
        {
            // try will prevent an error if something tries to print an invalid character
            try
            {
                // Console.WriteLine does nothing on x64 while debugging with VS, so use Debug. Console.WriteLine works when run from .exe
#if DEBUG
                Debug.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] [" + grp + "] " + text);
#endif
#if !DEBUG
            Console.WriteLine("[" +DateTime.Now.ToLongTimeString() + "] [" + grp + "] " + text);
#endif

                if (ConfigManager.GeneralConfig.LogToFile && Logger.IsInit)
                    Logger.Log.Info("[" + grp + "] " + text);
            }
            catch { }  // Not gonna recursively call here in case something is seriously wrong
        }
    }
}
