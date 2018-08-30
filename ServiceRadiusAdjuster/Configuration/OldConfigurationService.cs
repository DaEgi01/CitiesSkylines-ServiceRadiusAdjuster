using System;
using System.Collections.Generic;
using System.IO;

namespace ServiceRadiusAdjuster.Configuration
{
    public abstract class OldConfigurationFileService : IConfigFileVersion
    {
        public string Version { get; }
        public FileInfo ConfigFileInfo { get; }

        public OldConfigurationFileService(string version, FileInfo configFile)
        {
            this.Version = version ?? throw new ArgumentNullException(nameof(version));
            this.ConfigFileInfo = configFile ?? throw new ArgumentNullException(nameof(configFile));
        }

        public abstract Result<Dictionary<string, float>> GetConfigValues();

        public virtual Result BackupConfigFileIfItExists()
        {
            if (ConfigFileInfo == null) throw new ArgumentNullException(nameof(ConfigFileInfo));
            if (!ConfigFileInfo.Exists)
            {
                return Result.Ok();
            }

            var migratedConfigFileName = $"ServiceRadiusAdjuster_{Version}.bak";
            var migratedConfigFileFullName = Path.Combine(Path.GetDirectoryName(ConfigFileInfo.FullName), migratedConfigFileName);
            ConfigFileInfo.MoveTo(migratedConfigFileName);

            return Result.Ok();
        }

        public static Result MoveOldConfigurationFilesToNewFolder(DirectoryInfo source, DirectoryInfo target, string searchPattern)
        {
            try
            {
                var oldConfigFiles = source.GetFiles(searchPattern);
                foreach (var file in oldConfigFiles)
                {
                    file.MoveTo(Path.Combine(target.FullName, file.Name));
                }

                return Result.Ok();
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }
    }
}
