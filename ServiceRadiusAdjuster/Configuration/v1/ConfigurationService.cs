﻿using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

namespace ServiceRadiusAdjuster.Configuration.v1
{
    public class ConfigurationService : OldConfigurationFileService
    {
        private readonly Deserializer deserializer;
        private readonly FileInfo configFileInfo;

        public ConfigurationService(Deserializer deserializer, FileInfo configFileInfo)
            : base("v1", configFileInfo)
        {
            this.deserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
            this.configFileInfo = configFileInfo ?? throw new ArgumentNullException(nameof(configFileInfo));
        }

        public override Result<Dictionary<string, float>> GetConfigValues()
        {
            var result = new Dictionary<string, float>();

            if (!configFileInfo.Exists)
            {
                return Result.Ok(result);
            }

            using (var streamReader = new StreamReader(configFileInfo.FullName))
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
                    return Result.Fail<Dictionary<string, float>>($"Could not deserialize '{configFileInfo.FullName}'. {e.ToString()}");
                }
            }

            return Result.Ok(result);
        }
    }
}
