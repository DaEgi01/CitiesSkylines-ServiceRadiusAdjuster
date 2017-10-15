using ServiceRadiusAdjuster.Model;
using System.Collections.Generic;

namespace ServiceRadiusAdjuster.Service
{
    public interface IGameEngineService
    {
        bool IsInGame();
        Result ApplyToGame(Profile profile);
        Result ApplyToGame(OptionItem optionItem);
        OptionItem LoadFromGame(ServiceType serviceType, string systemName);
        Result<List<ViewGroup>> GetViewGroupsFromGame();
    }
}