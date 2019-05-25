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
        private readonly string cantApplyValue = "Value can't be applied to the game.";

        public Result<List<ViewGroup>> GetViewGroupsFromGame()
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
                if (bi == null)
                {
                    continue;
                }

                var ai = bi.GetAI();
                if (ai == null)
                {
                    continue;
                }

                switch (ai)
                {
                    case MedicalCenterAI medicalCenterAi: //take care, MedicalCenterAI should come before HospitalAI since it is also a HospitalAI
                        monumentOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), medicalCenterAi.m_healthCareAccumulation, medicalCenterAi.m_healthCareAccumulation, medicalCenterAi.m_healthCareRadius, medicalCenterAi.m_healthCareRadius));
                        break;
                    case SpaceElevatorAI spaceElevatorAi:
                        monumentOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), spaceElevatorAi.m_publicTransportAccumulation, spaceElevatorAi.m_publicTransportAccumulation, spaceElevatorAi.m_publicTransportRadius, spaceElevatorAi.m_publicTransportRadius));
                        break;
                    case HadronColliderAI hadronColliderAi:
                        monumentOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), null, null, hadronColliderAi.m_educationRadius, hadronColliderAi.m_educationRadius));
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
                }
            }

            var loadedTransportInfoCount = PrefabCollection<TransportInfo>.LoadedCount();
            for (uint i = 0; i < loadedTransportInfoCount; i++)
            {
                var ti = PrefabCollection<TransportInfo>.GetLoaded(i);
                if (ti == null)
                {
                    continue;
                }

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
                    if (ai == null)
                    {
                        continue;
                    }

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

            return Result.Ok(viewGroups);

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

        public Result<Maybe<OptionItemDefaultValues>> GetDefaultValues(ServiceType serviceType, string systemName)
        {
            if (serviceType == ServiceType.Building)
            {
                var bi = PrefabCollection<BuildingInfo>.FindLoaded(systemName);
                if (bi == null)
                {
                    return Result.Ok(Maybe<OptionItemDefaultValues>.None);
                }

                var ai = bi.GetAI();
                switch (ai)
                {
                    case MedicalCenterAI medicalCenterAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, medicalCenterAi.m_healthCareAccumulation, medicalCenterAi.m_healthCareRadius)
                        );
                    case SpaceElevatorAI spaceElevatorAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, spaceElevatorAi.m_publicTransportAccumulation, spaceElevatorAi.m_publicTransportRadius)
                        );
                    case HadronColliderAI hadronColliderAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, null, hadronColliderAi.m_educationRadius)
                        );
                    case EdenProjectAI edenProjectAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, edenProjectAi.m_entertainmentAccumulation, edenProjectAi.m_entertainmentRadius)
                        );
                    case UltimateRecyclingPlantAI ultimateRecyclingPlantAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, null, ultimateRecyclingPlantAi.m_collectRadius)
                        );
                    case MaintenanceDepotAI maintenanceDepotAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, null, maintenanceDepotAi.m_maintenanceRadius)
                        );
                    case SnowDumpAI snowDumpAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, null, snowDumpAi.m_collectRadius)
                        );
                    case WaterFacilityAI waterFacilityAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, null, waterFacilityAi.m_vehicleRadius)
                        );
                    case LandfillSiteAI landfillSiteAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, null, landfillSiteAi.m_collectRadius)
                        );
                    case WaterCleanerAI waterCleanerAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, waterCleanerAi.m_cleaningRate, waterCleanerAi.m_maxWaterDistance)
                        );
                    case HospitalAI hospitalAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, hospitalAi.m_healthCareAccumulation, hospitalAi.m_healthCareRadius)
                        );
                    case SaunaAI saunaAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, saunaAi.m_healthCareAccumulation, saunaAi.m_healthCareRadius)
                        );
                    case CemeteryAI cemeteryAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, cemeteryAi.m_deathCareAccumulation, cemeteryAi.m_deathCareRadius)
                        );
                    case FireStationAI fireStationAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, fireStationAi.m_fireDepartmentAccumulation, fireStationAi.m_fireDepartmentRadius)
                        );
                    case FirewatchTowerAI firewatchTowerAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, null, firewatchTowerAi.m_firewatchRadius)
                        );
                    case ShelterAI shelterAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, shelterAi.m_disasterCoverageAccumulation, shelterAi.m_evacuationRange)
                        );
                    case RadioMastAI radioMastAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, null, radioMastAi.m_transmitterPower)
                        );
                    case EarthquakeSensorAI earthquakeSensorAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, null, earthquakeSensorAi.m_detectionRange)
                        );
                    case PoliceStationAI policeStationAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, policeStationAi.m_policeDepartmentAccumulation, policeStationAi.m_policeDepartmentRadius)
                        );
                    case PostOfficeAI postOfficeAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, postOfficeAi.m_serviceAccumulation, postOfficeAi.m_serviceRadius)
                        );
                    case UniqueFacultyAI uniqueFacultyAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, uniqueFacultyAi.m_educationAccumulation, uniqueFacultyAi.m_educationRadius)
                        );
                    case CampusBuildingAI campusBuildingAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, campusBuildingAi.m_educationAccumulation, campusBuildingAi.m_educationRadius)
                        );
                    case SchoolAI schoolAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, schoolAi.m_educationAccumulation, schoolAi.m_educationRadius)
                        );
                    case CargoHarborAI cargoHarborAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, cargoHarborAi.m_cargoTransportAccumulation, cargoHarborAi.m_cargoTransportRadius)
                        );
                    case CargoStationAI cargoStationAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, cargoStationAi.m_cargoTransportAccumulation, cargoStationAi.m_cargoTransportRadius)
                        );
                    case ParkAI parkAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, parkAi.m_entertainmentAccumulation, parkAi.m_entertainmentRadius)
                        );
                    case MuseumAI museumAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, museumAi.m_entertainmentAccumulation, museumAi.m_entertainmentRadius)
                        );
                    case VarsitySportsArenaAI varsitySportsArenaAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, varsitySportsArenaAi.m_entertainmentAccumulation, varsitySportsArenaAi.m_entertainmentRadius)
                        );
                    case MonumentAI monumentAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, monumentAi.m_entertainmentAccumulation, monumentAi.m_entertainmentRadius)
                        );
                    case LibraryAI libraryAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, libraryAi.m_libraryAccumulation, libraryAi.m_libraryRadius)
                        );
                    default:
                        return Result.Fail<Maybe<OptionItemDefaultValues>>(
                            $"Can't handle ai of type '{ai.GetType().FullName}'."
                        );
                }
            }
            else if (serviceType == ServiceType.Transport)
            {
                var ti = PrefabCollection<TransportInfo>.FindLoaded(systemName);
                var ai = (TransportLineAI)ti.m_netInfo.GetAI();

                return Result.Ok<Maybe<OptionItemDefaultValues>>(
                    new OptionItemDefaultValues(systemName, ai.m_publicTransportAccumulation, ai.m_publicTransportRadius)
                );
            }
            else
            {
                return Result.Fail<Maybe<OptionItemDefaultValues>>($"Can't handle service type '{serviceType.Name}'.");
            }
        }

        public Result ApplyToGame(Profile profile)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));

            var errors = new List<string>();

            foreach (var vg in profile.ViewGroups)
            {
                foreach (var oi in vg.OptionItems)
                {
                    var commandResult = ApplyToGame(oi);
                    if (commandResult.IsFailure)
                    {
                        errors.Add(commandResult.Error);
                        continue;
                    }
                }
            }

            if (errors.Any())
            {
                return Result.Fail(string.Join(Environment.NewLine, errors.ToArray()));
            }

            return Result.Ok();
        }

        public Result ApplyToGame(OptionItem optionItem)
        {
            if (optionItem == null) throw new ArgumentNullException(nameof(optionItem));
            if (optionItem.Ignore)
            {
                return Result.Ok();
            }

            if (optionItem.ServiceType == ServiceType.Building)
            {
                var applyToBuildingResult = ApplyToBuilding(optionItem);
                if (applyToBuildingResult.IsSuccess)
                {
                    return Result.Ok();
                }
                else
                {
                    return Result.Fail(cantApplyValue + " " + applyToBuildingResult.Error);
                }
            }
            else if (optionItem.ServiceType == ServiceType.Transport)
            {
                var applyToTransportResult = ApplyToTransport(optionItem);
                if (applyToTransportResult.IsSuccess)
                {
                    return Result.Ok();
                }
                else
                {
                    return Result.Fail(cantApplyValue + " " + applyToTransportResult.Error);
                }
            }
            else
            {
                return Result.Fail($"{cantApplyValue} Service type {optionItem.ServiceType} is unknown.");
            }
        }

        private Result ApplyToBuilding(OptionItem optionItem)
        {
            if (optionItem == null) throw new ArgumentNullException(nameof(optionItem));

            var buildingInfo = PrefabCollection<BuildingInfo>.FindLoaded(optionItem.SystemName);
            if (buildingInfo == null)
            {
                return Result.Ok(); //Building not found but thats ok, because not all savegames have all buildings.
            }

            var ai = buildingInfo.GetAI();
            switch (ai)
            {
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
                default:
                    return Result.Fail("Building AI was not recognized.");
            }

            return Result.Ok();
        }

        private Result ApplyToTransport(OptionItem optionItem)
        {
            var transportInfo = PrefabCollection<TransportInfo>.FindLoaded(optionItem.SystemName);
            if (transportInfo == null)
            {
                return Result.Fail($"No TransportInfo named '{optionItem.SystemName}' was found.");
            }

            var ai = (TransportLineAI)transportInfo.m_netInfo.GetAI();
            ai.m_publicTransportAccumulation = optionItem.Accumulation.Value;
            ai.m_publicTransportRadius = optionItem.Radius.Value;

            return Result.Ok();
        }
    }
}
