using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

namespace ServiceRadiusAdjuster.Configuration
{
    public class OldConfigurationMetaService
    {
        private readonly DirectoryInfo directoryInfo;

        public OldConfigurationMetaService(DirectoryInfo directoryInfo)
        {
            this.directoryInfo = directoryInfo;
        }

        public IEnumerable<OldConfigServiceAndFile> GetOldConfigServicesAndFiles()
        {
            var yamlDeserializer = new Deserializer();

            yield return new OldConfigServiceAndFile(
                new v0.ConfigurationService(yamlDeserializer),
                new FileInfo(Path.Combine(directoryInfo.FullName, ConfigFile.ConfigFile_v0.Name))
            );
            yield return new OldConfigServiceAndFile(
                new v1.ConfigurationService(yamlDeserializer),
                new FileInfo(Path.Combine(directoryInfo.FullName, ConfigFile.ConfigFile_v1.Name))
            );
            yield return new OldConfigServiceAndFile(
                new v2.ConfigurationService(yamlDeserializer),
                new FileInfo(Path.Combine(directoryInfo.FullName, ConfigFile.ConfigFile_v2.Name))
            );
        }

        public Dictionary<string, float> GetOldConfigValuesCombined(IEnumerable<OldConfigServiceAndFile> oldConfigServicesAndFiles)
        {
            var result = new Dictionary<string, float>();

            foreach (var oldConfigurationServiceAndFile in oldConfigServicesAndFiles)
            {
                var getConfigValuesResult = oldConfigurationServiceAndFile.ConfigurationService.GetConfigValues(oldConfigurationServiceAndFile.FileInfo);
                if (getConfigValuesResult.IsFailure)
                {
                    throw new Exception(getConfigValuesResult.Error);
                }

                result.CombineAndUpdate(getConfigValuesResult.Value);
            }

            return result;
        }

        public void BackupOldConfigFiles(IEnumerable<OldConfigServiceAndFile> configurationServiceAndFiles)
        {
            foreach (var configurationServiceAndFile in configurationServiceAndFiles)
            {
                var backupResult = configurationServiceAndFile.ConfigurationService.BackupConfigFileIfItExists(configurationServiceAndFile.FileInfo);
                if (backupResult.IsFailure)
                {
                    throw new Exception(backupResult.Error);
                }
            }
        }
    }
}
