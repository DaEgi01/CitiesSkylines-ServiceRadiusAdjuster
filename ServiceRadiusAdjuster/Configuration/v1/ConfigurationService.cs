using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

namespace ServiceRadiusAdjuster.Configuration.v1
{
    public class ConfigurationService : OldConfigurationService
    {
        private readonly Deserializer deserializer;

        public ConfigurationService(Deserializer deserializer)
        {
            this.deserializer = deserializer;
        }

        public override Result<Dictionary<string, float>> GetConfigValues(FileInfo configFile)
        {
            var result = new Dictionary<string, float>();

            if (!configFile.Exists)
            {
                return Result.Ok(result);
            }

            using (var streamReader = new StreamReader(configFile.FullName))
            {
                try
                {
                    var dto = deserializer.Deserialize<List<OptionItemDto>>(streamReader);
                    foreach (var optionItem in dto)
                    {
                        result.Add(optionItem.SystemName, optionItem.ServiceRadius);
                    }
                }
                catch (Exception e)
                {
                    return Result.Fail<Dictionary<string, float>>($"Could not deserialize '{configFile.FullName}'. {e.ToString()}");
                }
            }

            return Result.Ok(result);
        }
    }
}
