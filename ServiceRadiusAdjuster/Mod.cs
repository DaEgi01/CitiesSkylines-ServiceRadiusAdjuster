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
        private ModFullTitle _modFullTitle;
        private GameEngineService _gameEngineService;
        private DirectoryInfo _configFilesDirectory;
        private FileInfo _currentConfigFile;
        private IConfigurationService _configurationService;

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
            UninitializeDependencies();
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            InitializeMod();
        }

        public void OnSettingsUI(UIHelperBase uIHelperBase)
        {
            var mainGroupUiHelper = uIHelperBase.AddGroup(_modFullTitle);
            var mainGroupContentPanel = (mainGroupUiHelper as UIHelper).self as UIPanel;
            mainGroupContentPanel.backgroundSprite = string.Empty;

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

                var profile = LoadProfileOrDefaultOrThrowException();
                profile.BatchEdit(accumulationMultiplierValue, radiusMultiplierValue);
                ApplyToGameOrThrowException(profile);
                SaveProfileOrThrowException(profile);

                accumulationMultiplier.text = string.Empty;
                radiusMultiplier.text = string.Empty;
            });

            var manualEditGroup = mainGroupUiHelper.AddGroup("Manually edit");
            manualEditGroup.AddButton("Open file", () =>
            {
                System.Diagnostics.Process.Start(_currentConfigFile.FullName);
            });
            manualEditGroup.AddButton("Apply", () =>
            {
                var profile = LoadProfileOrDefaultOrThrowException();
                ApplyToGameOrThrowException(profile);
            });
        }

        public void InitializeDependencies()
        {
            _modFullTitle = new ModFullTitle(Name, Version);
            _gameEngineService = new GameEngineService();
            _configFilesDirectory = new DirectoryInfo(DataLocation.localApplicationData);
            _currentConfigFile = new FileInfo(Path.Combine(_configFilesDirectory.FullName, "ServiceRadiusAdjuster_v3.xml"));
            _configurationService = new ConfigurationService(_currentConfigFile);
        }

        public void UninitializeDependencies()
        {
            _modFullTitle = null;
            _gameEngineService = null;
            _configFilesDirectory = null;
            _currentConfigFile = null;
            _configurationService = null;
        }

        public void InitializeMod()
        {
            var existingOrNewProfile = LoadProfileOrDefaultOrThrowException();
            var updatedProfile = UpdateProfileWithNewItemsOrThrowException(existingOrNewProfile);
            ApplyToGameOrThrowException(updatedProfile);
            SaveProfileOrThrowException(updatedProfile);
        }

        public Profile LoadProfileOrDefaultOrThrowException()
        {
            return _configurationService
                .LoadProfile()
                .OnFailure(error => throw new Exception(error))
                .Value
                .Unwrap(new Profile());
        }

        public Profile UpdateProfileWithNewItemsOrThrowException(Profile profile)
        {
            return profile.Combine(_gameEngineService
                .GetViewGroupsFromGame()
                .OnFailure(error => throw new Exception(error))
                .Value);
        }

        public void ApplyToGameOrThrowException(Profile profile)
        {
            _gameEngineService.ApplyToGame(profile)
                .OnFailure(error => throw new Exception(error));
        }

        public void SaveProfileOrThrowException(Profile profile)
        {
            _configurationService.SaveProfile(profile)
                .OnFailure(error => throw new Exception(error));
        }
    }
}
