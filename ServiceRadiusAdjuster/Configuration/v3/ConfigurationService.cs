using System;
using System.Collections.Generic;
using ServiceRadiusAdjuster.Model;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using ServiceRadiusAdjuster.Service;

namespace ServiceRadiusAdjuster.Configuration.v3
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly XmlSerializer profileSerializer = new XmlSerializer(typeof(ProfileDto));
        private readonly FileInfo configFile;

        public ConfigurationService(FileInfo configFile)
        {
            this.configFile = configFile;
        }

        public string Version => "v3";

        public Result<Maybe<Profile>> LoadProfile()
        {
            if (!File.Exists(configFile.FullName)) //don't use configFile.Exists here, since that will bug if the file is created on the first run.
            {
                return Result.Ok(Maybe<Profile>.None);
            }

            using (var streamReader = new StreamReader(configFile.FullName))
            {
                var profileDto = profileSerializer.Deserialize(streamReader) as ProfileDto;
                if (profileDto.Version != this.Version)
                {
                    return Result.Fail<Maybe<Profile>>("Profile can't be created from this file, since the versions do not match.");
                }

                var viewGroups = new List<ViewGroup>();
                foreach (var viewGroupsDto in profileDto.ViewGroupDtos)
                {
                    var optionItems = new List<OptionItem>();
                    foreach (var optionItemDto in viewGroupsDto.OptionItemDtos)
                    {
                        optionItems.Add(new OptionItem(
                            ServiceType.FromName(optionItemDto.Type),
                            optionItemDto.SystemName,
                            optionItemDto.DisplayName,
                            optionItemDto.Accumulation,
                            optionItemDto.AccumulationDefault,
                            optionItemDto.Radius,
                            optionItemDto.RadiusDefault,
                            optionItemDto.Ignore
                        ));
                    }

                    var viewGroup = new ViewGroup(viewGroupsDto.Name, viewGroupsDto.Order, optionItems);
                    viewGroups.Add(viewGroup);
                }

                var profile = new Profile(viewGroups);

                return Result.Ok<Maybe<Profile>>(profile);
            }
        }

        public Result SaveProfile(Profile profile)
        {
            var viewGroupDtos = new List<ViewGroupDto>();
            foreach (var viewGroup in profile.ViewGroups)
            {
                var optionItemDtos = new List<OptionItemDto>();
                foreach (var optionItem in viewGroup.OptionItems)
                {
                    optionItemDtos.Add(new OptionItemDto()
                    {
                        Type = optionItem.ServiceType.Name,
                        SystemName = optionItem.SystemName,
                        DisplayName = optionItem.DisplayName,
                        Accumulation = optionItem.Accumulation,
                        AccumulationDefault = optionItem.AccumulationDefault,
                        Radius = optionItem.Radius,
                        RadiusDefault = optionItem.RadiusDefault,
                        Ignore = optionItem.Ignore
                    });
                }

                viewGroupDtos.Add(new ViewGroupDto()
                {
                    Name = viewGroup.Name,
                    Order = viewGroup.Order,
                    OptionItemDtos = optionItemDtos
                });
            }

            var profileDto = new ProfileDto()
            {
                Version = this.Version,
                ViewGroupDtos = viewGroupDtos
            };

            using (var streamWriter = new StreamWriter(configFile.FullName, false))
            {
                try
                {
                    profileSerializer.Serialize(streamWriter, profileDto);
                }
                catch (Exception ex)
                {
                    return Result.Fail($"Profile can't be saved. {ex.Message}");
                }
            }

            return Result.Ok();
        }
    }
}
