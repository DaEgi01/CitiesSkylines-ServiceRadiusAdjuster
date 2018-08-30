using ColossalFramework.IO;
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

        public IEnumerable<OldConfigurationFileService> GetOldConfigServices()
        {
            var yamlDeserializer = new Deserializer();
            yield return new v0.ConfigurationService(yamlDeserializer, new FileInfo(Path.Combine(directoryInfo.FullName, "config.yaml")));
            yield return new v1.ConfigurationService(yamlDeserializer, new FileInfo(Path.Combine(directoryInfo.FullName, "config_v1.yaml")));
            yield return new v2.ConfigurationService(yamlDeserializer, new FileInfo(Path.Combine(directoryInfo.FullName, "config_v2.yaml")));
        }

        public Dictionary<string, float> GetOldConfigValuesCombined(IEnumerable<OldConfigurationFileService> oldConfigServices)
        {
            var result = new Dictionary<string, float>();

            foreach (var oldConfigService in oldConfigServices)
            {
                var getConfigValuesResult = oldConfigService.GetConfigValues();
                if (getConfigValuesResult.IsFailure)
                {
                    throw new Exception(getConfigValuesResult.Error);
                }

                result.CombineAndUpdate(getConfigValuesResult.Value);
            }

            return result;
        }

        public void BackupOldConfigFiles(IEnumerable<OldConfigurationFileService> oldConfigurationServices)
        {
            foreach (var oldConfigurationService in oldConfigurationServices)
            {
                var backupResult = oldConfigurationService.BackupConfigFileIfItExists();
                if (backupResult.IsFailure)
                {
                    throw new Exception(backupResult.Error);
                }
            }
        }
    }
}
