using ServiceRadiusAdjuster.FunctionalCore;
using ServiceRadiusAdjuster.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceRadiusAdjuster.Service
{
    public class GameEngineService
    {
        //TODO localization
        //TODO find out ui category of CampusBuildingAI
        //TODO find out ui category of VarsitySportsArenaAI
        //TODO find out ui category of MuseumAI
        //TODO find out ui category of LibraryAI
        private readonly string _cantApplyValue = "Value can't be applied to the game.";
        private readonly ErrorMessageBuilder _errorMessageBuilder;

        public GameEngineService(ErrorMessageBuilder errorMessageBuilder)
        {
            _errorMessageBuilder = errorMessageBuilder ?? throw new ArgumentNullException(nameof(errorMessageBuilder));
        }

        public Result<string, List<ViewGroup>> GetViewGroupsFromGame()
        {
            var result = new List<ViewGroup>();

            var roadConditionOptionItems = new List<OptionItem>();
            var waterUtilityOptionItems = new List<OptionItem>();
            var garbageUtilityOptionItems = new List<OptionItem>();
            var healthCareOptionItems = new List<OptionItem>();
            var deathCareOptionItems = new List<OptionItem>();
            var fireDepartmentOptionItems = new List<OptionItem>();
            var disasterServicesOptionItems = new List<OptionItem>();
            var policeDepartmentOptionItems = new List<OptionItem>();
            var educationOptionItems = new List<OptionItem>();
            var publicTransportOptionItems = new List<OptionItem>();
            var cargoTransportOptionItems = new List<OptionItem>();
            var postServiceOptionItems = new List<OptionItem>();
            var entertainmentParksOptionItems = new List<OptionItem>();
            var entertainmentPlazasOptionItems = new List<OptionItem>();
            var entertainmentOtherParksOptionItems = new List<OptionItem>();
            var entertainmentTourismAndLeisureOptionItems = new List<OptionItem>();
            var entertainmentWinterParksAndPlazasOptionItems = new List<OptionItem>();
            var uniqueBuildingsLevel1OptionItems = new List<OptionItem>();
            var uniqueBuildingsLevel2OptionItems = new List<OptionItem>();
            var uniqueBuildingsLevel3OptionItems = new List<OptionItem>();
            var uniqueBuildingsLevel4OptionItems = new List<OptionItem>();
            var uniqueBuildingsLevel5OptionItems = new List<OptionItem>();
            var uniqueBuildingsLevel6OptionItems = new List<OptionItem>();
            var uniqueBuildingsLandmarkOptionItems = new List<OptionItem>();
            var uniqueBuildingsTourismAndLeisureOptionItems = new List<OptionItem>();
            var uniqueBuildingsFootballStadiumOptionItems = new List<OptionItem>();
            var monumentOptionItems = new List<OptionItem>();

            var loadedBuildingInfoCount = PrefabCollection<BuildingInfo>.LoadedCount();
            for (uint i = 0; i < loadedBuildingInfoCount; i++)
            {
                var bi = PrefabCollection<BuildingInfo>.GetLoaded(i);
                if (bi is null)
                    continue;

                var ai = bi.GetAI();
                if (ai is null)
                    continue;

                switch (ai)
                {
                    case AirportAuxBuildingAI airportAuxBuildingAi: //take care, AirportAuxBuildingAI should come before AirportBuildingAI since it is also a AirportBuildingAI
                        publicTransportOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), airportAuxBuildingAi.m_attractivenessAccumulation, airportAuxBuildingAi.m_attractivenessAccumulation, airportAuxBuildingAi.m_attractivenessRadius, airportAuxBuildingAi.m_attractivenessRadius));
                        break;
                    case AirportEntranceAI airportEntranceAi: //take care, AirportEntranceAI should come before AirportBuildingAI since it is also a AirportBuildingAI
                        publicTransportOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), airportEntranceAi.m_attractivenessAccumulation, airportEntranceAi.m_attractivenessAccumulation, airportEntranceAi.m_attractivenessRadius, airportEntranceAi.m_attractivenessRadius));
                        break;
                    case AirportBuildingAI airportBuildingAi:
                        publicTransportOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), airportBuildingAi.m_attractivenessAccumulation, airportBuildingAi.m_attractivenessAccumulation, airportBuildingAi.m_attractivenessRadius, airportBuildingAi.m_attractivenessRadius));
                        break;
                    case AirportCargoGateAI airportCargoGateAi:
                        cargoTransportOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), airportCargoGateAi.m_cargoTransportAccumulation, airportCargoGateAi.m_cargoTransportAccumulation, airportCargoGateAi.m_cargoTransportRadius, airportCargoGateAi.m_cargoTransportRadius));
                        break;
                    case AirlineHeadquartersAI airlineHeadquartersAi: //take care, AirlineHeadquartersAI should come before MonumentAI since it is also a MonumentAI
                        publicTransportOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), airlineHeadquartersAi.m_entertainmentAccumulation, airlineHeadquartersAi.m_entertainmentAccumulation, airlineHeadquartersAi.m_entertainmentRadius, airlineHeadquartersAi.m_entertainmentRadius));
                        break;
                    case MedicalCenterAI medicalCenterAi: //take care, MedicalCenterAI should come before HospitalAI since it is also a HospitalAI
                        monumentOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), medicalCenterAi.m_healthCareAccumulation, medicalCenterAi.m_healthCareAccumulation, medicalCenterAi.m_healthCareRadius, medicalCenterAi.m_healthCareRadius));
                        break;
                    case SpaceElevatorAI spaceElevatorAi:
                        monumentOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), spaceElevatorAi.m_publicTransportAccumulation, spaceElevatorAi.m_publicTransportAccumulation, spaceElevatorAi.m_publicTransportRadius, spaceElevatorAi.m_publicTransportRadius));
                        break;
                    case HadronColliderAI hadronColliderAi:
                        monumentOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), null, null, hadronColliderAi.m_educationRadius, hadronColliderAi.m_educationRadius));
                        break;
                    case IceCreamStandAI iceCreamStandAi:
                        entertainmentParksOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), iceCreamStandAi.m_entertainmentAccumulation, iceCreamStandAi.m_entertainmentAccumulation, iceCreamStandAi.m_entertainmentRadius, iceCreamStandAi.m_entertainmentRadius));
                        break;
                    case EdenProjectAI edenProjectAi: //take care, EdenProjectAI should come before ParkAI since it is also a ParkAI
                        monumentOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), edenProjectAi.m_entertainmentAccumulation, edenProjectAi.m_entertainmentAccumulation, edenProjectAi.m_entertainmentRadius, edenProjectAi.m_entertainmentRadius));
                        break;
                    case UltimateRecyclingPlantAI ultimateRecyclingPlantAi:
                        monumentOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), null, null, ultimateRecyclingPlantAi.m_collectRadius, ultimateRecyclingPlantAi.m_collectRadius));
                        break;
                    case MaintenanceDepotAI maintenanceDepotAi:
                        roadConditionOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), null, null, maintenanceDepotAi.m_maintenanceRadius, maintenanceDepotAi.m_maintenanceRadius));
                        break;
                    case SnowDumpAI snowDumpAi:
                        roadConditionOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), null, null, snowDumpAi.m_collectRadius, snowDumpAi.m_collectRadius));
                        break;
                    case WaterFacilityAI waterFacilityAi when waterFacilityAi.m_pumpingVehicles > 0:
                        waterUtilityOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), null, null, waterFacilityAi.m_vehicleRadius, waterFacilityAi.m_vehicleRadius));
                        break;
                    case LandfillSiteAI landfillSiteAi when landfillSiteAi.m_garbageTruckCount > 0:
                        garbageUtilityOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), null, null, landfillSiteAi.m_collectRadius, landfillSiteAi.m_collectRadius));
                        break;
                    case WaterCleanerAI waterCleanerAi:
                        garbageUtilityOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), waterCleanerAi.m_cleaningRate, waterCleanerAi.m_cleaningRate, waterCleanerAi.m_maxWaterDistance, waterCleanerAi.m_maxWaterDistance));
                        break;
                    case HospitalAI hospitalAi:
                        healthCareOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), hospitalAi.m_healthCareAccumulation, hospitalAi.m_healthCareAccumulation, hospitalAi.m_healthCareRadius, hospitalAi.m_healthCareRadius));
                        break;
                    case SaunaAI saunaAi:
                        healthCareOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), saunaAi.m_healthCareAccumulation, saunaAi.m_healthCareAccumulation, saunaAi.m_healthCareRadius, saunaAi.m_healthCareRadius));
                        break;
                    case CemeteryAI cemeteryAi:
                        deathCareOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), cemeteryAi.m_deathCareAccumulation, cemeteryAi.m_deathCareAccumulation, cemeteryAi.m_deathCareRadius, cemeteryAi.m_deathCareRadius));
                        break;
                    case FireStationAI fireStationAi:
                        fireDepartmentOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), fireStationAi.m_fireDepartmentAccumulation, fireStationAi.m_fireDepartmentAccumulation, fireStationAi.m_fireDepartmentRadius, fireStationAi.m_fireDepartmentRadius));
                        break;
                    case FirewatchTowerAI firewatchTowerAi:
                        fireDepartmentOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), null, null, firewatchTowerAi.m_firewatchRadius, firewatchTowerAi.m_firewatchRadius));
                        break;
                    case ShelterAI shelterAi:
                        disasterServicesOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), shelterAi.m_disasterCoverageAccumulation, shelterAi.m_disasterCoverageAccumulation, shelterAi.m_evacuationRange, shelterAi.m_evacuationRange));
                        break;
                    case RadioMastAI radioMastAi:
                        disasterServicesOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), null, null, radioMastAi.m_transmitterPower, radioMastAi.m_transmitterPower));
                        break;
                    case EarthquakeSensorAI earthquakeSensorAi:
                        disasterServicesOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), null, null, earthquakeSensorAi.m_detectionRange, earthquakeSensorAi.m_detectionRange));
                        break;
                    case PoliceStationAI policeStationAi:
                        policeDepartmentOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), policeStationAi.m_policeDepartmentAccumulation, policeStationAi.m_policeDepartmentAccumulation, policeStationAi.m_policeDepartmentRadius, policeStationAi.m_policeDepartmentRadius));
                        break;
                    case UniqueFacultyAI uniqueFacultyAi:
                        educationOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), uniqueFacultyAi.m_educationAccumulation, uniqueFacultyAi.m_educationAccumulation, uniqueFacultyAi.m_educationRadius, uniqueFacultyAi.m_educationRadius));
                        break;
                    case CampusBuildingAI campusBuildingAi:
                        educationOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), campusBuildingAi.m_educationAccumulation, campusBuildingAi.m_educationAccumulation, campusBuildingAi.m_educationRadius, campusBuildingAi.m_educationRadius));
                        break;
                    case SchoolAI schoolAi:
                        educationOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), schoolAi.m_educationAccumulation, schoolAi.m_educationAccumulation, schoolAi.m_educationRadius, schoolAi.m_educationRadius));
                        break;
                    case CargoHarborAI cargoHarborAi: //take care, CargoHarborAI should come before CargoStationAI
                        cargoTransportOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), cargoHarborAi.m_cargoTransportAccumulation, cargoHarborAi.m_cargoTransportAccumulation, cargoHarborAi.m_cargoTransportRadius, cargoHarborAi.m_cargoTransportRadius));
                        break;
                    case CargoStationAI cargoStationAi:
                        cargoTransportOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), cargoStationAi.m_cargoTransportAccumulation, cargoStationAi.m_cargoTransportAccumulation, cargoStationAi.m_cargoTransportRadius, cargoStationAi.m_cargoTransportRadius));
                        break;
                    case ParkAI parkAi:
                        var parkOptionItem = new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), parkAi.m_entertainmentAccumulation, parkAi.m_entertainmentAccumulation, parkAi.m_entertainmentRadius, parkAi.m_entertainmentRadius);
                        var parkList = GetAppropriateParkList(bi.category);
                        parkList.Add(parkOptionItem);
                        break;
                    case MuseumAI museumAi:
                        educationOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), museumAi.m_entertainmentAccumulation, museumAi.m_entertainmentAccumulation, museumAi.m_entertainmentRadius, museumAi.m_entertainmentRadius));
                        break;
                    case VarsitySportsArenaAI varsitySportsArenaAi:
                        var varsitySportsArenaÎtem = new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), varsitySportsArenaAi.m_entertainmentAccumulation, varsitySportsArenaAi.m_entertainmentAccumulation, varsitySportsArenaAi.m_entertainmentRadius, varsitySportsArenaAi.m_entertainmentRadius);
                        var varsitySportsArenaList = GetAppropriateMonumentList(bi.category);
                        varsitySportsArenaList.Add(varsitySportsArenaÎtem);
                        break;
                    case MonumentAI monumentAi:
                        var monumentOptionItem = new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), monumentAi.m_entertainmentAccumulation, monumentAi.m_entertainmentAccumulation, monumentAi.m_entertainmentRadius, monumentAi.m_entertainmentRadius);
                        var monumentList = GetAppropriateMonumentList(bi.category);
                        monumentList.Add(monumentOptionItem);
                        break;
                    case PostOfficeAI postOfficeAi:
                        postServiceOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), postOfficeAi.m_serviceAccumulation, postOfficeAi.m_serviceAccumulation, postOfficeAi.m_serviceRadius, postOfficeAi.m_serviceRadius));
                        break;
                    case LibraryAI libraryAi:
                        educationOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), libraryAi.m_libraryAccumulation, libraryAi.m_libraryAccumulation, libraryAi.m_libraryRadius, libraryAi.m_libraryRadius));
                        break;
                    case ChildcareAI childcareAI:
                        healthCareOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), childcareAI.m_healthCareAccumulation, childcareAI.m_healthCareAccumulation, childcareAI.m_healthCareRadius, childcareAI.m_healthCareRadius));
                        break;
                    case EldercareAI eldercareAI:
                        healthCareOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), eldercareAI.m_healthCareAccumulation, eldercareAI.m_healthCareAccumulation, eldercareAI.m_healthCareRadius, eldercareAI.m_healthCareRadius));
                        break;
                    case MarketAI marketAI:
                        healthCareOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), marketAI.m_healthCareAccumulation, marketAI.m_healthCareAccumulation, marketAI.m_healthCareRadius, marketAI.m_healthCareRadius));
                        break;
                }
            }

            var loadedTransportInfoCount = PrefabCollection<TransportInfo>.LoadedCount();
            for (uint i = 0; i < loadedTransportInfoCount; i++)
            {
                var ti = PrefabCollection<TransportInfo>.GetLoaded(i);
                if (ti is null)
                    continue;

                if (ti.category == Category.PublicTransportBus.Name
                    || ti.category == Category.PublicTransportCableCar.Name
                    || ti.category == Category.PublicTransportMetro.Name
                    || ti.category == Category.PublicTransportMonorail.Name
                    || ti.category == Category.PublicTransportPlane.Name
                    || ti.category == Category.PublicTransportShip.Name
                    || ti.category == Category.PublicTransportTrain.Name
                    || ti.category == Category.PublicTransportTram.Name
                )
                {
                    var ai = ti.m_netInfo.GetAI() as TransportLineAI;
                    if (ai is null)
                        continue;

                    publicTransportOptionItems.Add(new OptionItem(ServiceType.Transport, ti.name, ti.GetUncheckedLocalizedTitle(), ai.m_publicTransportAccumulation, ai.m_publicTransportAccumulation, ai.m_publicTransportRadius, ai.m_publicTransportRadius));
                }
            }

            var viewGroups = new List<ViewGroup>()
            {
                new ViewGroup("Road Maintenance", 25, roadConditionOptionItems),
                new ViewGroup("Water Utilities", 50, waterUtilityOptionItems),
                new ViewGroup("Garbage Utilities", 75, garbageUtilityOptionItems),
                new ViewGroup("Health Care", 100, healthCareOptionItems),
                new ViewGroup("Dealth Care", 200, deathCareOptionItems),
                new ViewGroup("Fire Department", 300, fireDepartmentOptionItems),
                new ViewGroup("Disaster Services", 400, disasterServicesOptionItems),
                new ViewGroup("Police Department", 500, policeDepartmentOptionItems),
                new ViewGroup("Education", 600, educationOptionItems),
                new ViewGroup("Public Transport", 700, publicTransportOptionItems),
                new ViewGroup("Cargo Transport", 800, cargoTransportOptionItems),
                new ViewGroup("Post Service", 850, postServiceOptionItems),
                new ViewGroup("Entertainment - Parks", 900, entertainmentParksOptionItems),
                new ViewGroup("Entertainment - Plazas", 1000, entertainmentPlazasOptionItems),
                new ViewGroup("Entertainment - Other Parks", 1100, entertainmentOtherParksOptionItems),
                new ViewGroup("Entertainment - Tourism & Leisure", 1200, entertainmentTourismAndLeisureOptionItems),
                new ViewGroup("Entertainment - Winter Parks & Plazas", 1300, entertainmentWinterParksAndPlazasOptionItems),
                new ViewGroup("Unique Buildings - Level 1", 1400, uniqueBuildingsLevel1OptionItems),
                new ViewGroup("Unique Buildings - Level 2", 1500, uniqueBuildingsLevel2OptionItems),
                new ViewGroup("Unique Buildings - Level 3", 1600, uniqueBuildingsLevel3OptionItems),
                new ViewGroup("Unique Buildings - Level 4", 1700, uniqueBuildingsLevel4OptionItems),
                new ViewGroup("Unique Buildings - Level 5", 1800, uniqueBuildingsLevel5OptionItems),
                new ViewGroup("Unique Buildings - Level 6", 1900, uniqueBuildingsLevel6OptionItems),
                new ViewGroup("Unique Buildings - Landmarks", 2000, uniqueBuildingsLandmarkOptionItems),
                new ViewGroup("Unique Buildings - Tourism & Leisure", 2100, uniqueBuildingsTourismAndLeisureOptionItems),
                new ViewGroup("Unique Buildings - Football Stadium", 2200, uniqueBuildingsFootballStadiumOptionItems),
                new ViewGroup("Monuments", 2300, monumentOptionItems)
            };

            return Result<string, List<ViewGroup>>.Ok(viewGroups);

            List<OptionItem> GetAppropriateParkList(string category)
            {
                if (category == Category.BeautificationPlazas.Name)
                {
                    return entertainmentPlazasOptionItems;
                }
                else if (category == Category.BeautificationOthers.Name)
                {
                    return entertainmentOtherParksOptionItems;
                }
                else if (category == Category.BeautificationTourismAndLeisure.Name)
                {
                    return entertainmentTourismAndLeisureOptionItems;
                }
                else if (category == Category.BeautificationWinterParksAndPlazas.Name)
                {
                    return entertainmentWinterParksAndPlazasOptionItems;
                }
                else //put every other category into default parks
                {
                    return entertainmentParksOptionItems;
                }
            }

            List<OptionItem> GetAppropriateMonumentList(string category)
            {
                if (category == Category.MonumentCategory2.Name)
                {
                    return uniqueBuildingsLevel2OptionItems;
                }
                else if (category == Category.MonumentCategory3.Name)
                {
                    return uniqueBuildingsLevel3OptionItems;
                }
                else if (category == Category.MonumentCategory4.Name)
                {
                    return uniqueBuildingsLevel4OptionItems;
                }
                else if (category == Category.MonumentCategory5.Name)
                {
                    return uniqueBuildingsLevel5OptionItems;
                }
                else if (category == Category.MonumentCategory6.Name)
                {
                    return uniqueBuildingsLevel6OptionItems;
                }
                else if (category == Category.MonumentLandmarks.Name)
                {
                    return uniqueBuildingsLandmarkOptionItems;
                }
                else if (category == Category.MonumentTourismAndLeisure.Name)
                {
                    return uniqueBuildingsTourismAndLeisureOptionItems;
                }
                else if (category == Category.MonumentFootball.Name)
                {
                    return uniqueBuildingsFootballStadiumOptionItems;
                }
                else //put every other category into monument level 1
                {
                    return uniqueBuildingsLevel1OptionItems;
                }
            }
        }

        public Result<string, Profile> ApplyToGame(Profile profile)
        {
            if (profile is null)
                throw new ArgumentNullException(nameof(profile));

            var errors = new List<string>();

            foreach (var vg in profile.ViewGroups)
            {
                foreach (var oi in vg.OptionItems)
                {
                    ApplyToGame(oi).OnError(e => errors.Add(e));
                }
            }

            if (errors.Any())
            {
                return Result<string, Profile>.Error(string.Join(Environment.NewLine, errors.ToArray()));
            }

            return Result<string, Profile>.Ok(profile);
        }

        public Result<string> ApplyToGame(OptionItem optionItem)
        {
            if (optionItem is null) 
                throw new ArgumentNullException(nameof(optionItem));

            if (optionItem.Ignore)
                return Result<string>.Ok();

            if (optionItem.ServiceType == ServiceType.Building)
            {
                ApplyToBuilding(optionItem);
                return Result<string>.Ok();
            }
            else if (optionItem.ServiceType == ServiceType.Transport)
            {
                ApplyToTransport(optionItem);
                return Result<string>.Ok();
            }
            else
            {
                return Result<string>.Error($"{_cantApplyValue} Service type {optionItem.ServiceType} is unknown.");
            }
        }

        private void ApplyToBuilding(OptionItem optionItem)
        {
            if (optionItem is null)
                throw new ArgumentNullException(nameof(optionItem));

            var buildingInfo = PrefabCollection<BuildingInfo>.FindLoaded(optionItem.SystemName);
            if (buildingInfo is null)
                return;

            var ai = buildingInfo.GetAI();
            switch (ai)
            {
                case AirportAuxBuildingAI airportAuxBuildingAi:
                    airportAuxBuildingAi.m_attractivenessAccumulation = optionItem.Accumulation.Value;
                    airportAuxBuildingAi.m_attractivenessRadius = optionItem.Radius.Value;
                    break;
                case AirportEntranceAI airportEntranceAi:
                    airportEntranceAi.m_attractivenessAccumulation = optionItem.Accumulation.Value;
                    airportEntranceAi.m_attractivenessRadius = optionItem.Radius.Value;
                    break;
                case AirportBuildingAI airportBuildingAi:
                    airportBuildingAi.m_attractivenessAccumulation = optionItem.Accumulation.Value;
                    airportBuildingAi.m_attractivenessRadius = optionItem.Radius.Value;
                    break;
                case AirportCargoGateAI airportCargoGateAi:
                    airportCargoGateAi.m_cargoTransportAccumulation = optionItem.Accumulation.Value;
                    airportCargoGateAi.m_cargoTransportRadius = optionItem.Radius.Value;
                    break;
                case AirlineHeadquartersAI airlineHeadquartersAi:
                    airlineHeadquartersAi.m_entertainmentAccumulation = optionItem.Accumulation.Value;
                    airlineHeadquartersAi.m_entertainmentRadius = optionItem.Radius.Value;
                    break;
                case MedicalCenterAI medicalCenterAi:
                    medicalCenterAi.m_healthCareAccumulation = optionItem.Accumulation.Value;
                    medicalCenterAi.m_healthCareRadius = optionItem.Radius.Value;
                    break;
                case SpaceElevatorAI spaceElevatorAi:
                    spaceElevatorAi.m_publicTransportAccumulation = optionItem.Accumulation.Value;
                    spaceElevatorAi.m_publicTransportRadius = optionItem.Radius.Value;
                    break;
                case HadronColliderAI hadronColliderAi:
                    hadronColliderAi.m_educationRadius = optionItem.Radius.Value;
                    break;
                case EdenProjectAI edenProjectAi:
                    edenProjectAi.m_entertainmentAccumulation = optionItem.Accumulation.Value;
                    edenProjectAi.m_entertainmentRadius = optionItem.Radius.Value;
                    break;
                case UltimateRecyclingPlantAI ultimateRecyclingPlantAi:
                    ultimateRecyclingPlantAi.m_collectRadius = optionItem.Radius.Value;
                    break;
                case LandfillSiteAI landfillSiteAi:
                    landfillSiteAi.m_collectRadius = optionItem.Radius.Value;
                    break;
                case CargoHarborAI cargoHarborAi:
                    cargoHarborAi.m_cargoTransportAccumulation = optionItem.Accumulation.Value;
                    cargoHarborAi.m_cargoTransportRadius = optionItem.Radius.Value;
                    break;
                case CargoStationAI cargoStationAi:
                    cargoStationAi.m_cargoTransportAccumulation = optionItem.Accumulation.Value;
                    cargoStationAi.m_cargoTransportRadius = optionItem.Radius.Value;
                    break;
                case CemeteryAI cemeteryAi:
                    cemeteryAi.m_deathCareAccumulation = optionItem.Accumulation.Value;
                    cemeteryAi.m_deathCareRadius = optionItem.Radius.Value;
                    break;
                case FireStationAI fireStationAi:
                    fireStationAi.m_fireDepartmentAccumulation = optionItem.Accumulation.Value;
                    fireStationAi.m_fireDepartmentRadius = optionItem.Radius.Value;
                    break;
                case FirewatchTowerAI firewatchTowerAi:
                    firewatchTowerAi.m_firewatchRadius = optionItem.Radius.Value;
                    break;
                case EarthquakeSensorAI earthquakeSensorAi:
                    earthquakeSensorAi.m_detectionRange = optionItem.Radius.Value;
                    break;
                case HospitalAI hospitalAi:
                    hospitalAi.m_healthCareAccumulation = optionItem.Accumulation.Value;
                    hospitalAi.m_healthCareRadius = optionItem.Radius.Value;
                    break;
                case IceCreamStandAI iceCreamStandAi:
                    iceCreamStandAi.m_entertainmentAccumulation = optionItem.Accumulation.Value;
                    iceCreamStandAi.m_entertainmentRadius = optionItem.Radius.Value;
                    break;
                case ParkAI parkAi:
                    parkAi.m_entertainmentAccumulation = optionItem.Accumulation.Value;
                    parkAi.m_entertainmentRadius = optionItem.Radius.Value;
                    break;
                case MaintenanceDepotAI maintenanceDepotAi:
                    maintenanceDepotAi.m_maintenanceRadius = optionItem.Radius.Value;
                    break;
                case MuseumAI museumAi:
                    museumAi.m_entertainmentAccumulation = optionItem.Accumulation.Value;
                    museumAi.m_entertainmentRadius = optionItem.Radius.Value;
                    break;
                case VarsitySportsArenaAI varsitySportsArenaAi:
                    varsitySportsArenaAi.m_entertainmentAccumulation = optionItem.Accumulation.Value;
                    varsitySportsArenaAi.m_entertainmentRadius = optionItem.Radius.Value;
                    break;
                case MonumentAI monumentAi:
                    monumentAi.m_entertainmentAccumulation = optionItem.Accumulation.Value;
                    monumentAi.m_entertainmentRadius = optionItem.Radius.Value;
                    break;
                case PoliceStationAI policeStationAi:
                    policeStationAi.m_policeDepartmentAccumulation = optionItem.Accumulation.Value;
                    policeStationAi.m_policeDepartmentRadius = optionItem.Radius.Value;
                    break;
                case PostOfficeAI postOfficeAi:
                    postOfficeAi.m_serviceAccumulation = optionItem.Accumulation.Value;
                    postOfficeAi.m_serviceRadius = optionItem.Radius.Value;
                    break;
                case RadioMastAI radioMastAi:
                    radioMastAi.m_transmitterPower = (int)optionItem.Radius.Value;
                    break;
                case SaunaAI saunaAi:
                    saunaAi.m_healthCareAccumulation = optionItem.Accumulation.Value;
                    saunaAi.m_healthCareRadius = optionItem.Radius.Value;
                    break;
                case UniqueFacultyAI uniqueFacultyAi:
                    uniqueFacultyAi.m_educationAccumulation = optionItem.Accumulation.Value;
                    uniqueFacultyAi.m_educationRadius = optionItem.Radius.Value;
                    break;
                case CampusBuildingAI campusBuildingAi:
                    campusBuildingAi.m_educationAccumulation = optionItem.Accumulation.Value;
                    campusBuildingAi.m_educationRadius = optionItem.Radius.Value;
                    break;
                case SchoolAI schoolAi:
                    schoolAi.m_educationAccumulation = optionItem.Accumulation.Value;
                    schoolAi.m_educationRadius = optionItem.Radius.Value;
                    break;
                case ShelterAI shelterAi:
                    shelterAi.m_disasterCoverageAccumulation = optionItem.Accumulation.Value;
                    shelterAi.m_evacuationRange = optionItem.Radius.Value;
                    break;
                case SnowDumpAI snowDumpAi:
                    snowDumpAi.m_collectRadius = optionItem.Radius.Value;
                    break;
                case WaterCleanerAI waterCleanerAi:
                    waterCleanerAi.m_cleaningRate = optionItem.Accumulation.Value;
                    waterCleanerAi.m_maxWaterDistance = optionItem.Radius.Value;
                    break;
                case WaterFacilityAI waterFacilityAi:
                    waterFacilityAi.m_vehicleRadius = optionItem.Radius.Value;
                    break;
                case LibraryAI libraryAi:
                    libraryAi.m_libraryAccumulation = optionItem.Accumulation.Value;
                    libraryAi.m_libraryRadius = optionItem.Radius.Value;
                    break;
                case ChildcareAI childcareAi:
                    childcareAi.m_healthCareAccumulation = optionItem.Accumulation.Value;
                    childcareAi.m_healthCareRadius = optionItem.Radius.Value;
                    break;
                case EldercareAI eldercareAi:
                    eldercareAi.m_healthCareAccumulation = optionItem.Accumulation.Value;
                    eldercareAi.m_healthCareRadius = optionItem.Radius.Value;
                    break;
                case MarketAI marketAi:
                    marketAi.m_healthCareAccumulation = optionItem.Accumulation.Value;
                    marketAi.m_healthCareRadius = optionItem.Radius.Value;
                    break;
                default:
                    return; //Unknown AI is ok, just not supported.
            }
        }

        private void ApplyToTransport(OptionItem optionItem)
        {
            var transportInfo = PrefabCollection<TransportInfo>.FindLoaded(optionItem.SystemName);
            if (transportInfo is null)
                return;

            var ai = (TransportLineAI)transportInfo.m_netInfo.GetAI();
            ai.m_publicTransportAccumulation = optionItem.Accumulation.Value;
            ai.m_publicTransportRadius = optionItem.Radius.Value;

            return;
        }
    }
}
