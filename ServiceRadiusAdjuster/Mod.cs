using ColossalFramework.IO;
using ColossalFramework.UI;
using ICities;
using ServiceRadiusAdjuster.Configuration;
using ServiceRadiusAdjuster.Configuration.v3;
using ServiceRadiusAdjuster.FunctionalCore;
using ServiceRadiusAdjuster.Model;
using ServiceRadiusAdjuster.Service;
using System;
using System.IO;
using JetBrains.Annotations;

namespace ServiceRadiusAdjuster
{
    [UsedImplicitly]
    public class Mod : LoadingExtensionBase, IUserMod
    {
        private readonly OnTextChanged _doNothingOnTextChanged = _ => {};

        private ErrorMessageBuilder? _errorMessageBuilder;
        private GameEngineService? _gameEngineService;
        private DirectoryInfo? _configFilesDirectory;
        private FileInfo? _currentConfigFile;
        private IConfigurationService? _configurationService;

        public string Name => "Service Radius Adjuster";
        public string SystemName => "ServiceRadiusAdjuster";
        public string Description => "Adjusts the effect radius of service buildings in your city.";
        public string Version => "1.10.0";

        [UsedImplicitly]
        public void OnEnabled()
        {
            InitializeDependencies();

            if (LoadingManager.exists && LoadingManager.instance.m_loadingComplete)
            {
                InitializeMod();
            }
        }

        [UsedImplicitly]
        public void OnDisabled()
        {
            UninitializeDependencies();
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            InitializeMod();
        }

        [UsedImplicitly]
        public void OnSettingsUI(UIHelperBase uIHelperBase)
        {
            var mainGroupUiHelper = uIHelperBase.AddGroup(Name + " v" + Version);
            var mainGroupContentPanel = (mainGroupUiHelper as UIHelper).self as UIPanel;
            mainGroupContentPanel.backgroundSprite = string.Empty;

            var batchEditGroup = mainGroupUiHelper.AddGroup("Batch edit");

            var accumulationMultiplier = (UITextField)batchEditGroup.AddTextfield("Accumulation multiplier", string.Empty, _doNothingOnTextChanged);
            accumulationMultiplier.numericalOnly = true;
            accumulationMultiplier.allowFloats = true;

            var radiusMultiplier = (UITextField)batchEditGroup.AddTextfield("Radius multiplier", string.Empty, _doNothingOnTextChanged);
            radiusMultiplier.numericalOnly = true;
            radiusMultiplier.allowFloats = true;

            batchEditGroup.AddButton("Apply", () =>
            {
                float? accumulationMultiplierValue = string.IsNullOrEmpty(accumulationMultiplier.text)
                    ? null
                    : float.Parse(accumulationMultiplier.text);

                float? radiusMultiplierValue = string.IsNullOrEmpty(radiusMultiplier.text)
                    ? null
                    : float.Parse(radiusMultiplier.text);

                LoadProfileOrDefault()
                    .SelectMany(p => p.BatchEdit(accumulationMultiplierValue, radiusMultiplierValue))
                    .SelectMany(p => _gameEngineService.ApplyToGame(p))
                    .SelectMany(p => _configurationService.SaveProfile(p))
                    .OnError(e => throw new Exception(e));

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
                LoadProfileOrDefault()
                    .SelectMany(p => _gameEngineService.ApplyToGame(p))
                    .OnError(e => new Exception(e));
            });
        }

        public void InitializeDependencies()
        {
            _errorMessageBuilder = new ErrorMessageBuilder();
            _gameEngineService = new GameEngineService();
            _configFilesDirectory = new DirectoryInfo(DataLocation.localApplicationData);
            _currentConfigFile = new FileInfo(Path.Combine(_configFilesDirectory.FullName, "ServiceRadiusAdjuster_v3.xml"));
            _configurationService = new ConfigurationService(_currentConfigFile, _errorMessageBuilder);
        }

        public void UninitializeDependencies()
        {
            _errorMessageBuilder = null;
            _gameEngineService = null;
            _configFilesDirectory = null;
            _currentConfigFile = null;
            _configurationService = null;
        }

        public void InitializeMod()
        {
            LoadProfileOrDefault()
                .SelectMany(p => _gameEngineService.GetViewGroupsFromGame().Select(vg => p.Combine(vg)))
                .SelectMany(p => _gameEngineService.ApplyToGame(p))
                .SelectMany(p => _configurationService.SaveProfile(p))
                .OnError(e => throw new Exception(e));
        }

        public Result<string, Profile> LoadProfileOrDefault()
        {
            return _configurationService
                .LoadProfile()
                .Select(p => p ?? new Profile());
        }
    }
}
