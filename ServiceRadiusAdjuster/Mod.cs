using ColossalFramework.IO;
using ICities;
using ServiceRadiusAdjuster.Configuration;
using ServiceRadiusAdjuster.Configuration.v3;
using ServiceRadiusAdjuster.Model;
using ServiceRadiusAdjuster.Service;
using ServiceRadiusAdjuster.View;
using System;
using System.IO;

namespace ServiceRadiusAdjuster
{
    public class Mod : LoadingExtensionBase, IUserMod
    {
        private ModFullTitle modFullTitle;
        private IGameEngineService gameEngineService;
        private OptionsUiBuilder optionsUiBuilder;
        private DirectoryInfo configFilesDirectory;
        private IConfigurationService configurationService;

        public string Name => "Service Radius Adjuster";
        public string SystemName => "ServiceRadiusAdjuster";

        public string Description => "Adjusts the effect radius of service buildings in your city.";
        public string Version => "1.5.0";

        public void OnEnabled()
        {
            this.modFullTitle = new ModFullTitle(this.Name, this.Version);
            this.gameEngineService = new GameEngineService();
            this.optionsUiBuilder = new OptionsUiBuilder();
            this.configFilesDirectory = new DirectoryInfo(DataLocation.localApplicationData);

            var currentConfigFile = new FileInfo(Path.Combine(this.configFilesDirectory.FullName, "ServiceRadiusAdjuster_v3.xml"));
            this.configurationService = new ConfigurationService(currentConfigFile);
        }

        public void OnDisabled()
        {
            this.modFullTitle = null;
            this.gameEngineService = null;
            this.optionsUiBuilder = null;
            this.configFilesDirectory = null;
            this.configurationService = null;
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            this.optionsUiBuilder.ClearExistingUi(helper);

            var currentProfileMaybe = this.configurationService
                .LoadProfile()
                .OnFailure(error => throw new Exception(error))
                .Value;

            if (LoadingManager.exists && LoadingManager.instance.m_loadingComplete)
            {
                var viewGroupsInGame = gameEngineService
                    .GetViewGroupsFromGame()
                    .OnFailure(error => throw new Exception(error))
                    .Value;

                var currentProfile = currentProfileMaybe
                    .Unwrap(new Profile())
                    .Combine(viewGroupsInGame);

                var saveProfileResult = configurationService
                    .SaveProfile(currentProfile)
                    .OnFailure(error => throw new Exception(error));

                this.optionsUiBuilder.BuildGenerateUi(helper, configurationService, gameEngineService, modFullTitle, currentProfile);
                
                this.gameEngineService.ApplyToGame(currentProfile);
            }
            else //in main menu
            {
                if (currentProfileMaybe.HasValue)
                {
                    var currentProfile = currentProfileMaybe.Value;
                    
                    //TODO: find a way to allow a profile and out of game ui
                    //this.optionsUiBuilder.BuildOutOfGameUi(helper, this.configurationService, this.modFullTitle, currentProfile);
                }
                else
                {
                    //this.optionsUiBuilder.BuildNoProfileUi(helper, this.modFullTitle, hasOldProfile);
                }
            }
        }
    }
}
