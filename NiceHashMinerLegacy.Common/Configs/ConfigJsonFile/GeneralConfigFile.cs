using NiceHashMinerLegacy.Common.Configs.Data;

namespace NiceHashMinerLegacy.Common.Configs.ConfigJsonFile
{
    public class GeneralConfigFile : ConfigFile<GeneralConfig>
    {
        public GeneralConfigFile()
            : base(Folders.Config, "General.json", "General_old.json")
        { }
    }
}
