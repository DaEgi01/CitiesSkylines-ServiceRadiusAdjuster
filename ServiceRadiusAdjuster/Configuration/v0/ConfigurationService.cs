using ServiceRadiusAdjuster.Model;
using System.IO;
using YamlDotNet.Serialization;
using System.Collections.Generic;
using System;

namespace ServiceRadiusAdjuster.Configuration.v0
{
    public class ConfigurationService : OldYamlConfigurationService
    {
        private readonly Deserializer deserializer;

        public ConfigurationService(Deserializer deserializer)
        {
            this.deserializer = deserializer;
        }

        public override Result<Dictionary<string, float>> GetConfigValues(FileInfo configFile)
        {
            var result = new Dictionary<string, float>();

            if (!configFile.Exists)
            {
                return Result.Ok<Dictionary<string, float>>(result);
            }

            using (var streamReader = new StreamReader(configFile.FullName))
            {
                try
                {
                    var dto = deserializer.Deserialize<OptionsDto>(streamReader);
                    result.Add(ServiceBuilding.MedicalClinic.Name, dto.MedicalClinicRadius);
                    result.Add(ServiceBuilding.MedicalClinicEurope.Name, dto.MedicalClinicRadius);
                    result.Add(ServiceBuilding.Hospital.Name, dto.HospitalRadius);
                    result.Add(ServiceBuilding.HospitalEurope.Name, dto.HospitalRadius);
                    result.Add(ServiceBuilding.Sauna.Name, dto.SaunaRadius);
                    result.Add(ServiceBuilding.Cemetery.Name, dto.CemeteryRadius);
                    result.Add(ServiceBuilding.Crematory.Name, dto.CrematoryRadius);
                    result.Add(ServiceBuilding.FireHouse.Name, dto.FireHouseRadius);
                    result.Add(ServiceBuilding.FireHouseEurope.Name, dto.FireHouseRadius);
                    result.Add(ServiceBuilding.FireStation.Name, dto.FireStationRadius);
                    result.Add(ServiceBuilding.FireStationEurope.Name, dto.FireStationRadius);
                    result.Add(ServiceBuilding.PoliceStation.Name, dto.PoliceStationRadius);
                    result.Add(ServiceBuilding.PoliceStationEurope.Name, dto.PoliceStationRadius);
                    result.Add(ServiceBuilding.PoliceHeadquarters.Name, dto.PoliceHeadquartersRadius);
                    result.Add(ServiceBuilding.PoliceHeadquartersEurope.Name, dto.PoliceHeadquartersRadius);
                    result.Add(ServiceBuilding.Prison.Name, dto.PrisonRadius);
                    result.Add(ServiceBuilding.ElementarySchool.Name, dto.ElementrySchoolRadius);
                    result.Add(ServiceBuilding.ElementarySchoolEurope.Name, dto.ElementrySchoolRadius);
                    result.Add(ServiceBuilding.HighSchool.Name, dto.HighSchoolRadius);
                    result.Add(ServiceBuilding.HighSchoolEurope.Name, dto.HighSchoolRadius);
                    result.Add(ServiceBuilding.University.Name, dto.UniversityRadius);
                    result.Add(ServiceBuilding.UniversityEurope.Name, dto.UniversityRadius);
                    result.Add(ServiceTransport.Bus.Name, dto.BusStationRadius);
                    result.Add(ServiceTransport.Tram.Name, dto.TramStationRadius);
                    result.Add(ServiceTransport.Metro.Name, dto.MetroStationRadius);
                    result.Add(ServiceTransport.Train.Name, dto.TrainStationRadius);
                    result.Add(ServiceTransport.Ship.Name, dto.HarborRadius);
                    result.Add(ServiceBuilding.CargoTrainTerminal.Name, dto.CargoTrainTerminalRadius);
                    result.Add(ServiceBuilding.CargoHarbor.Name, dto.CargoHarborRadius);
                    result.Add(ServiceBuilding.CargoHub.Name, dto.CargoHubRadius);
                    result.Add(ServiceBuilding.HadronCollider.Name, dto.HadronColliderRadius);
                    result.Add(ServiceBuilding.MedicalCenter.Name, dto.MedicalCenterRadius);
                    result.Add(ServiceBuilding.SpaceElevator.Name, dto.SpaceElevatorRadius);
                }
                catch (Exception e)
                {
                    return Result.Fail<Dictionary<string, float>>($"Could not deserialize '{configFile.FullName}'.");
                }
            }

            return Result.Ok<Dictionary<string, float>>(result);
        }
    }
}
