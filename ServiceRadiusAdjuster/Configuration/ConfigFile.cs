namespace ServiceRadiusAdjuster.Configuration
{
    public sealed class ConfigFile
    {
        public ConfigFile(string name)
        {
            Name = name ?? throw new System.ArgumentNullException(nameof(name));
        }

        public string Name { get; }

        public static readonly ConfigFile ConfigFile_v0 = new ConfigFile("config.yaml");
        public static readonly ConfigFile ConfigFile_v1 = new ConfigFile("config_v1.yaml");
        public static readonly ConfigFile ConfigFile_v2 = new ConfigFile("config_v2.yaml");
        public static readonly ConfigFile ConfigFile_v3 = new ConfigFile("ServiceRadiusAdjuster_v3.xml");

    }
}
