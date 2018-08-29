using ServiceRadiusAdjuster.Model;

namespace ServiceRadiusAdjuster.Configuration
{
    public interface IConfigurationService
    {
        string Version { get; }
        Result<Maybe<Profile>> LoadProfile();
        Result SaveProfile(Profile profile);
    }
}