using System;
using System.IO;

namespace ServiceRadiusAdjuster.Configuration
{
    public class OldConfigServiceAndFile
    {
        public OldConfigServiceAndFile(OldConfigurationService configurationService, FileInfo fileInfo)
        {
            ConfigurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            FileInfo = fileInfo ?? throw new ArgumentNullException(nameof(fileInfo));
        }

        public OldConfigurationService ConfigurationService { get; }
        public FileInfo FileInfo { get; }
    }
}
