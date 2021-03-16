using System;

namespace ServiceRadiusAdjuster.Model
{
    public class ModFullTitle
    {
        public ModFullTitle(string modName, string modVersion)
        {
            ModName = modName ?? throw new ArgumentNullException(nameof(modName));
            ModVersion = modVersion ?? throw new ArgumentNullException(nameof(modVersion));
        }

        public string ModName { get; }
        public string ModVersion { get; }

        public static implicit operator string(ModFullTitle modFullTitle)
        {
            return modFullTitle.ModName + " v" + modFullTitle.ModVersion;
        }
    }
}
