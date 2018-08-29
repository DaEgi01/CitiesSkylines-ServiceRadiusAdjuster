using System;
using System.Collections.Generic;
using System.IO;

namespace ServiceRadiusAdjuster.Configuration
{
    public abstract class OldConfigurationService
    {
        public virtual Result BackupConfigFileIfItExists(FileInfo configFile)
        {
            if (configFile == null) throw new ArgumentNullException(nameof(configFile));

            if (!configFile.Exists)
            {
                return Result.Ok();
            }

            var migratedConfigFileName = configFile.Name.Replace("config", "ServiceRadiusAdjuster") + ".bak";
            var migratedConfigFileFullName = Path.Combine(Path.GetDirectoryName(configFile.FullName), migratedConfigFileName);
            configFile.MoveTo(migratedConfigFileName);

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

        public abstract Result<Dictionary<string, float>> GetConfigValues(FileInfo fileInfo);
    }
}
