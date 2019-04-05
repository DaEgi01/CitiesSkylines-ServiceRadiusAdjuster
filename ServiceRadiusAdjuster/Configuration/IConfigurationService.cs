using ServiceRadiusAdjuster.Model;

namespace ServiceRadiusAdjuster.Configuration
{
    public interface IConfigurationService : IConfigFileVersion
    {
        Result<Maybe<Profile>> LoadProfile();
        Result SaveProfile(Profile profile);
    }
}