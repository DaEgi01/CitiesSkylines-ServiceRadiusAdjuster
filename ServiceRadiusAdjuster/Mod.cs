﻿using ColossalFramework.IO;
using ColossalFramework.UI;
using ICities;
using ServiceRadiusAdjuster.Configuration;
using ServiceRadiusAdjuster.Model;
using ServiceRadiusAdjuster.Presenter;
using ServiceRadiusAdjuster.Service;
using ServiceRadiusAdjuster.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using YamlDotNet.Serialization;

namespace ServiceRadiusAdjuster
{
    public class Mod : IUserMod, ILoadingExtension
    {
        //initialization during 'OnEnabled'
        private IGameEngineService gameEngineService;
        private OptionsUiBuilder optionsUiBuilder;
        private DirectoryInfo configFilesDirectory;
        private IConfigurationService configurationService;

        //initialization during 'OnCreated'
        private ILoading loading;

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
        public string Version => "1.4.0";

        public void OnEnabled()
        {
            this.gameEngineService = new GameEngineService();
            this.optionsUiBuilder = new OptionsUiBuilder();
            this.configFilesDirectory = new DirectoryInfo(DataLocation.localApplicationData);
            var configFileV3 = new FileInfo(Path.Combine(configFilesDirectory.FullName, ConfigFile.ConfigFile_v3.Name));
            this.configurationService = new Configuration.v3.ConfigurationService(configFileV3);

            var oldConfigFilesDirectory = new DirectoryInfo(Path.Combine(DataLocation.modsPath, SystemName));

            OldYamlConfigurationService.MoveOldConfigurationFilesToNewFolder(oldConfigFilesDirectory, configFilesDirectory, "*.yaml");
        }

        public void OnDisabled()
        {
            this.gameEngineService = null;
            this.optionsUiBuilder = null;
            this.configFilesDirectory = null;
            this.configurationService = null;
        }

        public void OnCreated(ILoading loading)
        {
            this.loading = loading;
        }

        public void OnReleased()
        {
            this.loading = null;
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            if (this.gameEngineService.IsInGame() && this.loading.currentMode == AppMode.Game)
            {
                this.helper = helper;

                if (this.loading.loadingComplete) //this is a special case where the mod is reloaded while the game is running (hot loading)
                {
                    this.InitializeMod(helper, this.Name, this.Version, this.configurationService, this.gameEngineService);
                }
                else //this is the regular case, where OnSettingsUI is called before the game is fully loaded.
                {
                    this.helper = helper; //Mod will be initialized during 'OnLevelLoaded' but a reference to the uihelper is needed.
                }
            }
            else
            {
                this.optionsUiBuilder.BuildNoProfileUi(helper, this.Name, this.Version);
            }
        }

        public void OnLevelLoaded(LoadMode mode)
        {
            if (!(mode == LoadMode.LoadGame || mode == LoadMode.LoadScenario || mode == LoadMode.NewGame || mode == LoadMode.NewGameFromScenario))
            {
                return;
            }

            this.InitializeMod(this.helper, this.Name, this.Version, this.configurationService, this.gameEngineService);
        }

        private void InitializeMod(UIHelperBase helper, string modName, string modVersion, IConfigurationService configurationService, IGameEngineService gameEngineService)
        {
            var getViewGroupsResult = gameEngineService.GetViewGroupsFromGame();
            if (getViewGroupsResult.IsFailure)
            {
                throw new Exception(getViewGroupsResult.Error);
            }
            var viewGroupsInGame = getViewGroupsResult.Value;

            var loadProfileResult = this.configurationService.LoadProfile();
            if (loadProfileResult.IsFailure)
            {
                throw new Exception(loadProfileResult.Error);
            }

            Profile currentProfile;
            var loadProfileMaybe = loadProfileResult.Value;
            if (loadProfileMaybe.HasValue)
            {
                currentProfile = loadProfileMaybe.Value.Combine(viewGroupsInGame);
            }
            else
            {
                currentProfile = new Profile(viewGroupsInGame); //generate new profile
            }

            //apply old radius values
            var yamlDeserializer = new Deserializer();

            var configFileV0 = new FileInfo(Path.Combine(this.configFilesDirectory.FullName, ConfigFile.ConfigFile_v0.Name));
            var configurationServiceV0 = new Configuration.v0.ConfigurationService(yamlDeserializer);
            var oldPersistedValuesV0Result = configurationServiceV0.GetConfigValues(configFileV0);
            if (oldPersistedValuesV0Result.IsFailure)
            {
                throw new Exception(oldPersistedValuesV0Result.Error);
            }

            var configFileV1 = new FileInfo(Path.Combine(this.configFilesDirectory.FullName, ConfigFile.ConfigFile_v1.Name));
            var configurationServiceV1 = new Configuration.v1.ConfigurationService(yamlDeserializer);
            var oldPersistedValuesV1Result = configurationServiceV1.GetConfigValues(configFileV1);
            if (oldPersistedValuesV1Result.IsFailure)
            {
                throw new Exception(oldPersistedValuesV1Result.Error);
            }

            var configFileV2 = new FileInfo(Path.Combine(this.configFilesDirectory.FullName, ConfigFile.ConfigFile_v2.Name));
            var configurationServiceV2 = new Configuration.v2.ConfigurationService(yamlDeserializer);
            var oldPersistedValuesV2Result = configurationServiceV2.GetConfigValues(configFileV2);
            if (oldPersistedValuesV2Result.IsFailure)
            {
                throw new Exception(oldPersistedValuesV2Result.Error);
            }

            var combinedOldPersistetValues = oldPersistedValuesV0Result.Value
                .CombineAndUpdate(oldPersistedValuesV1Result.Value)
                .CombineAndUpdate(oldPersistedValuesV2Result.Value);

            currentProfile.ApplyOldValues(combinedOldPersistetValues);

            var saveProfileResult = configurationService.SaveProfile(currentProfile);
            if (saveProfileResult.IsFailure)
            {
                throw new Exception(saveProfileResult.Error);
            }

            var backupConfigFileV0Result = configurationServiceV0.BackupConfigFileIfItExists(configFileV0);
            if (backupConfigFileV0Result.IsFailure)
            {
                throw new Exception(backupConfigFileV0Result.Error);
            }

            var backupConfigFileV1Result = configurationServiceV1.BackupConfigFileIfItExists(configFileV1);
            if (backupConfigFileV1Result.IsFailure)
            {
                throw new Exception(backupConfigFileV1Result.Error);
            }

            var backupConfigFileV2Result = configurationServiceV2.BackupConfigFileIfItExists(configFileV2);
            if (backupConfigFileV2Result.IsFailure)
            {
                throw new Exception(backupConfigFileV2Result.Error);
            }

            if (combinedOldPersistetValues.Count > 0)
            {
                this.ShowMigrationWarningIfInGame();
            }

            this.optionsUiBuilder.BuildProfileUi(helper, modName, modVersion, currentProfile, configurationService, gameEngineService);
            this.optionsUiBuilder.GlobalOptionsPresenter.ApplyAll();
        }

        public void OnLevelUnloading()
        {
            this.helper = null;
        }

        private void ShowMigrationWarningIfInGame()
        {
            if (this.gameEngineService.IsInGame())
            {
                var infoPanel = UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel");
                infoPanel.SetMessage(this.Name, migrationWarning, false);
            }
        }
    }
}