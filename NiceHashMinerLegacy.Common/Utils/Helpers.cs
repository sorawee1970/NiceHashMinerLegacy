using System;
using System.Diagnostics;
using System.Globalization;
using NiceHashMinerLegacy.Common.Configs;
using NiceHashMinerLegacy.Common.Enums;

namespace NiceHashMinerLegacy.Common.Utils
{
    public static class Helpers
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

        public static void ConsolePrint(string grp, string text, params object[] arg)
        {
            ConsolePrint(grp, string.Format(text, arg));
        }

        public static string FormatSpeedOutput(double speed, string separator = " ")
        {
            string ret;

            if (speed < 1000)
                ret = (speed).ToString("F3", CultureInfo.InvariantCulture) + separator;
            else if (speed < 100000)
                ret = (speed * 0.001).ToString("F3", CultureInfo.InvariantCulture) + separator + "k";
            else if (speed < 100000000)
                ret = (speed * 0.000001).ToString("F3", CultureInfo.InvariantCulture) + separator + "M";
            else
                ret = (speed * 0.000000001).ToString("F3", CultureInfo.InvariantCulture) + separator + "G";

            return ret;
        }

        public static string FormatDualSpeedOutput(double primarySpeed, double secondarySpeed = 0, AlgorithmType algo = AlgorithmType.NONE)
        {
            string ret;
            if (secondarySpeed > 0)
            {
                ret = FormatSpeedOutput(primarySpeed, "") + "/" + FormatSpeedOutput(secondarySpeed, "") + " ";
            }
            else
            {
                ret = FormatSpeedOutput(primarySpeed);
            }
            var unit = (algo == AlgorithmType.Equihash) ? "Sol/s " : "H/s ";
            return ret + unit;
        }

        // parsing helpers

        public static int ParseInt(string text)
        {
            return int.TryParse(text, out var tmpVal) ? tmpVal : 0;
        }

        public static long ParseLong(string text)
        {
            return long.TryParse(text, out var tmpVal) ? tmpVal : 0;
        }

        public static double ParseDouble(string text)
        {
            try
            {
                var parseText = text.Replace(',', '.');
                return double.Parse(parseText, CultureInfo.InvariantCulture);
            }
            catch
            {
                return 0;
            }
        }

        public static AlgorithmType DualAlgoFromAlgos(AlgorithmType primary, AlgorithmType secondary)
        {
            if (primary == AlgorithmType.DaggerHashimoto)
            {
                switch (secondary)
                {
                    case AlgorithmType.Decred:
                        return AlgorithmType.DaggerDecred;
                    case AlgorithmType.Lbry:
                        return AlgorithmType.DaggerLbry;
                    case AlgorithmType.Pascal:
                        return AlgorithmType.DaggerPascal;
                    case AlgorithmType.Sia:
                        return AlgorithmType.DaggerSia;
                    case AlgorithmType.Blake2s:
                        return AlgorithmType.DaggerBlake2s;
                    case AlgorithmType.Keccak:
                        return AlgorithmType.DaggerKeccak;
                }
            }

            return primary;
        }
    }
}
