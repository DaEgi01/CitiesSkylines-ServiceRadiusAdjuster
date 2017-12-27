using ServiceRadiusAdjuster.Model;
using System.Collections.Generic;

namespace ServiceRadiusAdjuster.Service
{
    public interface IGameEngineService
    {
        Result ApplyToGame(Profile profile);
        Result ApplyToGame(OptionItem optionItem);
        Result<List<ViewGroup>> GetViewGroupsFromGame();
    }
}