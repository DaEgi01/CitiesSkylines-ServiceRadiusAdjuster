using ServiceRadiusAdjuster.FunctionalCore;
using ServiceRadiusAdjuster.Model;

namespace ServiceRadiusAdjuster.Configuration;

public interface IConfigurationService : IConfigFileVersion
{
    Result<string, Profile?> LoadProfile();
    Result<string, Profile> SaveProfile(Profile profile);
}