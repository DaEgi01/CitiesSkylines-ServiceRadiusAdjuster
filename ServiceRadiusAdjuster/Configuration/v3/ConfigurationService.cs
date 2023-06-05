using ServiceRadiusAdjuster.FunctionalCore;
using ServiceRadiusAdjuster.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace ServiceRadiusAdjuster.Configuration.v3
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly XmlSerializer _profileSerializer = new XmlSerializer(typeof(ProfileDto));
        private readonly FileInfo _configFileInfo;
        private readonly ErrorMessageBuilder _errorMessageBuilder;

        public ConfigurationService(FileInfo configFileInfo, ErrorMessageBuilder errorMessageBuilder)
        {
            _configFileInfo = configFileInfo ?? throw new ArgumentNullException(nameof(configFileInfo));
            _errorMessageBuilder = errorMessageBuilder ?? throw new ArgumentNullException(nameof(errorMessageBuilder));
        }

        public string Version => "v3";

        public Result<string, Profile?> LoadProfile()
        {
            try
            {
                if (!File.Exists(_configFileInfo.FullName)) //don't use configFile.Exists here, since that will bug if the file is created on the first run.
                {
                    return Result<string, Profile?>.Ok(null);
                }

                using var streamReader = new StreamReader(_configFileInfo.FullName);
                var profileDto = _profileSerializer.Deserialize(streamReader) as ProfileDto;
                if (profileDto is null)
                {
                    return Result<string, Profile?>.Error(_errorMessageBuilder.Build(nameof(LoadProfile), "Profile could not be created, profileDto is null."));
                }

                if (profileDto.Version != Version)
                {
                    return Result<string, Profile?>.Error(_errorMessageBuilder.Build(nameof(LoadProfile), "Profile could not be created since the versions do not match."));
                }

                var viewGroups = new List<ViewGroup>();
                foreach (var viewGroupsDto in profileDto.ViewGroupDtos)
                {
                    var optionItems = new List<OptionItem>();
                    foreach (var optionItemDto in viewGroupsDto.OptionItemDtos)
                    {
                        ServiceType.FromName(optionItemDto.Type)
                            .OnErrorAndSuccess(
                                error => Debug.Log(error),
                                serviceType => optionItems.Add(OptionItem.From(serviceType, optionItemDto))
                            );
                    }

                    var viewGroup = new ViewGroup(viewGroupsDto.Name, viewGroupsDto.Order, optionItems);
                    viewGroups.Add(viewGroup);
                }

                return Result<string, Profile?>.Ok(new Profile(viewGroups));
            }
            catch (Exception ex)
            {
                return Result<string, Profile?>.Error(_errorMessageBuilder.Build(nameof(LoadProfile), ex));
            }
        }

        public Result<string, Profile> SaveProfile(Profile profile)
        {
            if (profile is null)
            {
                throw new ArgumentNullException(nameof(profile));
            }

            var viewGroupDtos = new List<ViewGroupDto>();
            foreach (var viewGroup in profile.ViewGroups)
            {
                var optionItemDtos = new List<OptionItemDto>();
                foreach (var optionItem in viewGroup.OptionItems)
                {
                    optionItemDtos.Add(
                        new OptionItemDto()
                        {
                            Type = optionItem.ServiceType.Name,
                            SystemName = optionItem.SystemName,
                            DisplayName = optionItem.DisplayName,
                            Accumulation = optionItem.Accumulation,
                            AccumulationDefault = optionItem.AccumulationDefault,
                            Radius = optionItem.Radius,
                            RadiusDefault = optionItem.RadiusDefault,
                            Ignore = optionItem.Ignore
                        }
                    );
                }

                viewGroupDtos.Add(
                    new ViewGroupDto()
                    {
                        Name = viewGroup.Name,
                        Order = viewGroup.Order,
                        OptionItemDtos = optionItemDtos
                    }
                );
            }

            var profileDto = new ProfileDto()
            {
                Version = Version,
                ViewGroupDtos = viewGroupDtos
            };

            try
            {
                using var streamWriter = new StreamWriter(_configFileInfo.FullName, false);
                _profileSerializer.Serialize(streamWriter, profileDto);

                return Result<string, Profile>.Ok(profile);
            }
            catch (Exception ex)
            {
                return Result<string, Profile>.Error(_errorMessageBuilder.Build(nameof(SaveProfile), ex));
            }
        }
    }
}
