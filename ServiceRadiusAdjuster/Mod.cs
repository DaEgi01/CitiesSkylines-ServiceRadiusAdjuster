using ColossalFramework.IO;
using ColossalFramework.UI;
using ICities;
using ServiceRadiusAdjuster.Configuration;
using ServiceRadiusAdjuster.Configuration.v3;
using ServiceRadiusAdjuster.Model;
using ServiceRadiusAdjuster.Service;
using System;
using System.IO;

namespace ServiceRadiusAdjuster
{
    public class Mod : LoadingExtensionBase, IUserMod
    {
        private ModFullTitle modFullTitle;
        private GameEngineService gameEngineService;
        private DirectoryInfo configFilesDirectory;
        private IConfigurationService configurationService;

        private FileInfo currentConfigFile;
        private Profile currentProfile;

        public string Name => "Service Radius Adjuster";
        public string SystemName => "ServiceRadiusAdjuster";

        public string Description => "Adjusts the effect radius of service buildings in your city.";
        public string Version => "1.6.0";

        public void OnEnabled()
        {
            InitializeDependencies();

            if (LoadingManager.exists && LoadingManager.instance.m_loadingComplete)
            {
                InitializeMod();
            }
        }

        public void OnDisabled()
        {
            if (LoadingManager.exists && LoadingManager.instance.m_loadingComplete)
            {
                UninitializeMod();
            }

            UninitializeDependencies();
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            InitializeMod();
        }

        public override void OnLevelUnloading()
        {
            UninitializeMod();
        }

        public void OnSettingsUI(UIHelperBase uIHelperBase)
        {
            var mainGroupUiHelper = uIHelperBase.AddGroup(this.modFullTitle);
            var mainGroupContentPanel = (mainGroupUiHelper as UIHelper).self as UIPanel;
            mainGroupContentPanel.backgroundSprite = string.Empty;

            if (LoadingManager.exists && LoadingManager.instance.m_loadingComplete)
            {
                //TODO move to ui builder
                var batchEditGroup = mainGroupUiHelper.AddGroup("Batch edit");

                var accumulationMultiplier = (UITextField)batchEditGroup.AddTextfield("Accumulation multiplier", string.Empty, (s) => { });
                accumulationMultiplier.numericalOnly = true;
                accumulationMultiplier.allowFloats = true;

                var radiusMultiplier = (UITextField)batchEditGroup.AddTextfield("Radius multiplier", string.Empty, (s) => { });
                radiusMultiplier.numericalOnly = true;
                radiusMultiplier.allowFloats = true;

                batchEditGroup.AddButton("Apply", () => 
                {
                    float? accumulationMultiplierValue = string.IsNullOrEmpty(accumulationMultiplier.text)
                        ? (float?)null
                        : float.Parse(accumulationMultiplier.text);

                    float? radiusMultiplierValue = string.IsNullOrEmpty(radiusMultiplier.text)
                        ? (float?)null
                        : float.Parse(radiusMultiplier.text);

                    this.currentProfile.BatchEdit(accumulationMultiplierValue, radiusMultiplierValue);

                    this.gameEngineService.ApplyToGame(this.currentProfile);
                    this.configurationService.SaveProfile(this.currentProfile)
                        .OnFailure(error => throw new Exception(error));

                    accumulationMultiplier.text = string.Empty;
                    radiusMultiplier.text = string.Empty;
                });

                var manualEditGroup = mainGroupUiHelper.AddGroup("Manually edit");
                manualEditGroup.AddButton("Open file", () =>
                {
                    System.Diagnostics.Process.Start(this.currentConfigFile.FullName);
                });
                manualEditGroup.AddButton("Apply", () =>
                {
                    this.currentProfile = this.configurationService
                        .LoadProfile()
                        .OnFailure(error => throw new Exception(error))
                        .Value
                        .Value;

                    this.gameEngineService.ApplyToGame(this.currentProfile);
                });
            }
            else
            {
                //TODO
            }
        }

        public void InitializeDependencies()
        {
            this.modFullTitle = new ModFullTitle(this.Name, this.Version);
            this.gameEngineService = new GameEngineService();
            this.configFilesDirectory = new DirectoryInfo(DataLocation.localApplicationData);
            this.currentConfigFile = new FileInfo(Path.Combine(this.configFilesDirectory.FullName, "ServiceRadiusAdjuster_v3.xml"));
            this.configurationService = new ConfigurationService(currentConfigFile);
        }

        public void UninitializeDependencies()
        {
            this.modFullTitle = null;
            this.gameEngineService = null;
            this.configFilesDirectory = null;
            this.currentConfigFile = null;
            this.configurationService = null;
        }

        public void InitializeMod()
        {
            var currentProfileMaybe = this.configurationService
                .LoadProfile()
                .OnFailure(error => throw new Exception(error))
                .Value;

            var viewGroupsInGame = gameEngineService
                .GetViewGroupsFromGame()
                .OnFailure(error => throw new Exception(error))
                .Value;

            this.currentProfile = currentProfileMaybe
                .Unwrap(new Profile())
                .Combine(viewGroupsInGame);

            var saveProfileResult = configurationService
                .SaveProfile(this.currentProfile)
                .OnFailure(error => throw new Exception(error));

            this.gameEngineService.ApplyToGame(this.currentProfile);
        }

        public void UninitializeMod()
        {
            //TODO no need actually
        }
    }
}
