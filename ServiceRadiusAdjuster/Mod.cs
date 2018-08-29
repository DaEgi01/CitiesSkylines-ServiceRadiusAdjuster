using ColossalFramework.IO;
using ICities;
using ServiceRadiusAdjuster.Configuration;
using ServiceRadiusAdjuster.Model;
using ServiceRadiusAdjuster.Service;
using ServiceRadiusAdjuster.View;
using System;
using System.IO;

namespace ServiceRadiusAdjuster
{
    public class Mod : IUserMod, ILoadingExtension
    {
        //initialization during 'OnEnabled'
        private ModFullTitle modFullTitle;
        private IGameEngineService gameEngineService;
        private OptionsUiBuilder optionsUiBuilder;
        private DirectoryInfo configFilesDirectory;
        private IConfigurationService configurationService;

        private bool onCreatedInvoked = false;

        //initalization during 'OnSettingsUI'
        private UIHelperBase helper;

        //TODO localization
        private string migrationWarning = "A new version of the 'Service Radius Adjuster' is out" +
            " and with it a new file format to save your values." +
            " I tried to migrate most of your settings over to the new file format," +
            " but due to technical reasons, I could not do that for every case." +
            " \n\nIf you have loaded a map with the european theme, " +
            " you have to set the values for the non european buildings again and vice versa." +
            " You will be able to set these values, once you load a map with a non european theme." +
            " \n\nI beg your pardon for this inconvenience, and wish you a lot of fun with Cities: Skylines.";

        public string Name => "Service Radius Adjuster";
        public string SystemName => "ServiceRadiusAdjuster";
        //TODO localization
        public string Description => "Adjusts the effect radius of service buildings in your city.";
        public string Version => "1.5.0";

        public void OnEnabled()
        {
            this.modFullTitle = new ModFullTitle(this.Name, this.Version);
            this.gameEngineService = new GameEngineService();
            this.optionsUiBuilder = new OptionsUiBuilder();
            this.configFilesDirectory = new DirectoryInfo(DataLocation.localApplicationData);
            var configFileV3 = new FileInfo(Path.Combine(configFilesDirectory.FullName, ConfigFile.ConfigFile_v3.Name));
            this.configurationService = new Configuration.v3.ConfigurationService(configFileV3);

            var oldConfigFilesDirectory = new DirectoryInfo(Path.Combine(DataLocation.modsPath, SystemName));
            OldConfigurationService.MoveOldConfigurationFilesToNewFolder(oldConfigFilesDirectory, configFilesDirectory, "*.yaml");
        }

        public void OnDisabled()
        {
            this.modFullTitle = null;
            this.gameEngineService = null;
            this.optionsUiBuilder = null;
            this.configFilesDirectory = null;
            this.configurationService = null;
        }

        public void OnCreated(ILoading loading)
        {
            this.onCreatedInvoked = true;
        }

        public void OnReleased()
        {
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            this.helper = helper; //in the regular case, the mod will be initialized during 'OnLevelLoaded' but a reference to the uihelper is needed.

            if (this.onCreatedInvoked)
            {
                this.optionsUiBuilder.ClearExistingUi(helper);
                this.optionsUiBuilder.BuildHotReloadUnsupportedUi(helper, this.modFullTitle);
                this.onCreatedInvoked = false;
            }
            else
            {
                this.optionsUiBuilder.BuildNoProfileUi(helper, this.modFullTitle);
            }
        }

        public void OnLevelLoaded(LoadMode mode)
        {
            if (!(mode == LoadMode.LoadGame || mode == LoadMode.NewGame || mode == LoadMode.NewGameFromScenario) || this.onCreatedInvoked)
            {
                this.onCreatedInvoked = false;
                return;
            }

            this.InitializeMod(this.helper, this.modFullTitle, this.configurationService, this.gameEngineService);
        }

        public void OnLevelUnloading()
        {
        }

        private void InitializeMod(UIHelperBase helper, ModFullTitle modFullTitle, IConfigurationService configurationService, IGameEngineService gameEngineService)
        {
            this.optionsUiBuilder.ClearExistingUi(helper);

            var viewGroupsInGame = gameEngineService
                .GetViewGroupsFromGame()
                .OnFailure(error => throw new Exception(error))
                .Value;

            var currentProfile = this.configurationService
                .LoadProfile()
                .OnFailure(error => throw new Exception(error))
                .Value
                .Unwrap(
                    configValues => configValues.Combine(viewGroupsInGame), 
                    new Profile(viewGroupsInGame)
                );

            var oldConfigMetaService = new OldConfigurationMetaService(this.configFilesDirectory);
            var oldConfigServicesAndFiles = oldConfigMetaService.GetOldConfigServicesAndFiles();
            var oldConfigValuesCombined = oldConfigMetaService.GetOldConfigValuesCombined(oldConfigServicesAndFiles);
            currentProfile.ApplyOldValues(oldConfigValuesCombined);

            var saveProfileResult = configurationService
                .SaveProfile(currentProfile)
                .OnFailure(error => throw new Exception(error));

            oldConfigMetaService.BackupOldConfigFiles(oldConfigServicesAndFiles);

            if (oldConfigValuesCombined.Count > 0 && this.onCreatedInvoked)
            {
                this.optionsUiBuilder.ShowExceptionPanel(this.Name, this.migrationWarning, false);
            }

            this.optionsUiBuilder.BuildProfileUi(helper, modFullTitle, currentProfile, configurationService, gameEngineService);
            this.optionsUiBuilder.GlobalOptionsPresenter.ApplyAll();
        }
    }
}
