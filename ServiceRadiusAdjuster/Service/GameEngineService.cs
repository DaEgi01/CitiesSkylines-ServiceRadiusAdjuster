using ServiceRadiusAdjuster.Model;
using System;
using System.Collections.Generic;

namespace ServiceRadiusAdjuster.Service
{
    public class GameEngineService : IGameEngineService
    {
        public GameEngineService()
        {
        }

        public bool IsInGame()
        {
            return PrefabCollection<BuildingInfo>.LoadedCount() > 0;
        }

        public Result<List<ViewGroup>> GetViewGroupsFromGame()
        {
            if (!this.IsInGame())
            {
                return Result.Fail<List<ViewGroup>>($"{nameof(GetViewGroupsFromGame)} can't be executed, since we are not in game.");
            }

            var result = new List<ViewGroup>();

            var healthCareOptionItems = new List<OptionItem>();
            var deathCareOptionItems = new List<OptionItem>();
            var fireDepartmentOptionItems = new List<OptionItem>();
            var disasterServicesOptionItems = new List<OptionItem>();
            var policeDepartmentOptionItems = new List<OptionItem>();
            var educationOptionItems = new List<OptionItem>();
            var publicTransportOptionItems = new List<OptionItem>();
            var cargoTransportOptionItems = new List<OptionItem>();
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
                var ai = bi.GetAI();
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
                    case HospitalAI hospitalAi:
                        healthCareOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), hospitalAi.m_healthCareAccumulation, hospitalAi.m_healthCareAccumulation, hospitalAi.m_healthCareRadius, hospitalAi.m_healthCareRadius));
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
                    case RadioMastAI radioMastAi:
                        disasterServicesOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), null, null, radioMastAi.m_transmitterPower, radioMastAi.m_transmitterPower));
                        break;
                    case PoliceStationAI policeStationAi:
                        policeDepartmentOptionItems.Add(new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), policeStationAi.m_policeDepartmentAccumulation, policeStationAi.m_policeDepartmentAccumulation, policeStationAi.m_policeDepartmentRadius, policeStationAi.m_policeDepartmentRadius));
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
                    case MonumentAI monumentAi:
                        var monumentOptionItem = new OptionItem(ServiceType.Building, bi.name, bi.GetUncheckedLocalizedTitle(), monumentAi.m_entertainmentAccumulation, monumentAi.m_entertainmentAccumulation, monumentAi.m_entertainmentRadius, monumentAi.m_entertainmentRadius);
                        var monumentList = GetAppropriateMonumentList(bi.category);
                        monumentList.Add(monumentOptionItem);
                        break;
                }
            }

            var loadedTransportInfoCount = PrefabCollection<TransportInfo>.LoadedCount();
            for (uint i = 0; i < loadedTransportInfoCount; i++)
            {
                var ti = PrefabCollection<TransportInfo>.GetLoaded(i);

                if (ti.category == Category.PublicTransportBus.Name
                    || ti.category == Category.PublicTransportCableCar.Name
                    || ti.category == Category.PublicTransportMetro.Name
                    || ti.category == Category.PublicTransportMonorail.Name
                    || ti.category == Category.PublicTransportPlane.Name
                    || ti.category == Category.PublicTransportShip.Name
                    || ti.category == Category.PublicTransportTrain.Name
                    || ti.category == Category.PublicTransportTram.Name)
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
                new ViewGroup("Health Care", 100, healthCareOptionItems),
                new ViewGroup("Dealth Care", 200, deathCareOptionItems),
                new ViewGroup("Fire Department", 300, fireDepartmentOptionItems),
                new ViewGroup("Disaster Services", 400, disasterServicesOptionItems),
                new ViewGroup("Police Department", 500, policeDepartmentOptionItems),
                new ViewGroup("Education", 600, educationOptionItems),
                new ViewGroup("Public Transport", 700, publicTransportOptionItems),
                new ViewGroup("Cargo Transport", 800, cargoTransportOptionItems),
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
                    case HospitalAI hospitalAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, hospitalAi.m_healthCareAccumulation, hospitalAi.m_healthCareRadius)
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
                    case RadioMastAI radioMastAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, null, radioMastAi.m_transmitterPower)
                        );
                    case PoliceStationAI policeStationAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, policeStationAi.m_policeDepartmentAccumulation, policeStationAi.m_policeDepartmentRadius)
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
                    case MonumentAI monumentAi:
                        return Result.Ok<Maybe<OptionItemDefaultValues>>(
                            new OptionItemDefaultValues(systemName, monumentAi.m_entertainmentAccumulation, monumentAi.m_entertainmentRadius)
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

            foreach (var vg in profile.ViewGroups)
            {
                foreach (var oi in vg.OptionItems)
                {
                    var commandResult = ApplyToGame(oi);
                    if (commandResult.IsFailure)
                    {
                        return commandResult;
                    }
                }
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
                return ApplyToBuilding(optionItem);
            }
            else if (optionItem.ServiceType == ServiceType.Transport)
            {
                return ApplyToTransport(optionItem);
            }
            else
            {
                return Result.Fail($"Service type {optionItem.ServiceType} is unknown.");
            }
        }

        private Result ApplyToBuilding(OptionItem optionItem)
        {
            if (optionItem == null) throw new ArgumentNullException(nameof(optionItem));

            var buildingInfo = PrefabCollection<BuildingInfo>.FindLoaded(optionItem.SystemName);
            if (buildingInfo == null)
            {
                return Result.Fail($"Building '{optionItem.SystemName}' not found.");
            }

            var ai = buildingInfo.GetAI();
            switch (ai)
            {
                case CargoHarborAI cargoHarborAI:
                    cargoHarborAI.m_cargoTransportAccumulation = optionItem.Accumulation.Value;
                    cargoHarborAI.m_cargoTransportRadius = optionItem.Radius.Value;
                    break;
                case CargoStationAI cargoStationAI:
                    cargoStationAI.m_cargoTransportAccumulation = optionItem.Accumulation.Value;
                    cargoStationAI.m_cargoTransportRadius = optionItem.Radius.Value;
                    break;
                case CemeteryAI cemeteryAI:
                    cemeteryAI.m_deathCareAccumulation = optionItem.Accumulation.Value;
                    cemeteryAI.m_deathCareRadius = optionItem.Radius.Value;
                    break;
                case EdenProjectAI edenProjectAI:
                    edenProjectAI.m_entertainmentAccumulation = optionItem.Accumulation.Value;
                    edenProjectAI.m_entertainmentRadius = optionItem.Radius.Value;
                    break;
                case FireStationAI fireStationAI:
                    fireStationAI.m_fireDepartmentAccumulation = optionItem.Accumulation.Value;
                    fireStationAI.m_fireDepartmentRadius = optionItem.Radius.Value;
                    break;
                case FirewatchTowerAI firewatchTowerAI:
                    firewatchTowerAI.m_firewatchRadius = optionItem.Radius.Value;
                    break;
                case HadronColliderAI hadronColliderAI:
                    hadronColliderAI.m_educationRadius = optionItem.Radius.Value;
                    break;
                case MedicalCenterAI medicalCenterAI:
                    medicalCenterAI.m_healthCareAccumulation = optionItem.Accumulation.Value;
                    medicalCenterAI.m_healthCareRadius = optionItem.Radius.Value;
                    break;
                case HospitalAI hospitalAI:
                    hospitalAI.m_healthCareAccumulation = optionItem.Accumulation.Value;
                    hospitalAI.m_healthCareRadius = optionItem.Radius.Value;
                    break;
                case ParkAI parkAI:
                    parkAI.m_entertainmentAccumulation = optionItem.Accumulation.Value;
                    parkAI.m_entertainmentRadius = optionItem.Radius.Value;
                    break;
                case MonumentAI monumentAi:
                    monumentAi.m_entertainmentAccumulation = optionItem.Accumulation.Value;
                    monumentAi.m_entertainmentRadius = optionItem.Radius.Value;
                    break;
                case PoliceStationAI policeStationAI:
                    policeStationAI.m_policeDepartmentAccumulation = optionItem.Accumulation.Value;
                    policeStationAI.m_policeDepartmentRadius = optionItem.Radius.Value;
                    break;
                case RadioMastAI radioMastAI:
                    radioMastAI.m_transmitterPower = Convert.ToInt32(optionItem.Radius);
                    break;
                case SaunaAI saunaAI:
                    saunaAI.m_healthCareAccumulation = optionItem.Accumulation.Value;
                    saunaAI.m_healthCareRadius = optionItem.Radius.Value;
                    break;
                case SchoolAI schoolAI:
                    schoolAI.m_educationAccumulation = optionItem.Accumulation.Value;
                    schoolAI.m_educationRadius = optionItem.Radius.Value;
                    break;
                case SpaceElevatorAI spaceElevatorAI:
                    spaceElevatorAI.m_publicTransportAccumulation = optionItem.Accumulation.Value;
                    spaceElevatorAI.m_publicTransportRadius = optionItem.Radius.Value;
                    break;
                default:
                    return Result.Fail("Building AI was not recognized.");
                    break;
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

        public OptionItem LoadFromGame(ServiceType serviceType, string systemName)
        {
            if (serviceType == ServiceType.Building)
            {
                return LoadBuilding(systemName);
            }
            else if (serviceType == ServiceType.Transport)
            {
                return LoadTransport(systemName);
            }
            else
            {
                throw new Exception("Collection type name is unknown.");
            }
        }

        private OptionItem LoadBuilding(string systemName)
        {
            var bi = PrefabCollection<BuildingInfo>.FindLoaded(systemName);
            var ai = bi.GetAI();

            if (ai is MedicalCenterAI medicalCenterAi)
            {
                return new OptionItem(ServiceType.Building, systemName, bi.GetUncheckedLocalizedTitle(), medicalCenterAi.m_healthCareAccumulation, medicalCenterAi.m_healthCareAccumulation, medicalCenterAi.m_healthCareRadius, medicalCenterAi.m_healthCareRadius);
            }
            else if (ai is SpaceElevatorAI spaceElevatorAi)
            {
                return new OptionItem(ServiceType.Building, systemName, bi.GetUncheckedLocalizedTitle(), spaceElevatorAi.m_publicTransportAccumulation, spaceElevatorAi.m_publicTransportAccumulation, spaceElevatorAi.m_publicTransportRadius, spaceElevatorAi.m_publicTransportRadius);
            }
            else if (ai is HadronColliderAI hadronColliderAi)
            {
                return new OptionItem(ServiceType.Building, systemName, bi.GetUncheckedLocalizedTitle(), null, null, hadronColliderAi.m_educationRadius, hadronColliderAi.m_educationRadius);
            }
            else if (ai is EdenProjectAI edenProjectAi) //take care, should come before ParkAI since it is also a ParkAI
            {
                return new OptionItem(ServiceType.Building, systemName, bi.GetUncheckedLocalizedTitle(), edenProjectAi.m_entertainmentAccumulation, edenProjectAi.m_entertainmentAccumulation, edenProjectAi.m_entertainmentRadius, edenProjectAi.m_entertainmentRadius);
            }
            else if (ai is HospitalAI hospitalAi)
            {
                return new OptionItem(ServiceType.Building, systemName, bi.GetUncheckedLocalizedTitle(), hospitalAi.m_healthCareAccumulation, hospitalAi.m_healthCareAccumulation, hospitalAi.m_healthCareRadius, hospitalAi.m_healthCareRadius);
            }
            else if (ai is CemeteryAI cemeteryAi)
            {
                return new OptionItem(ServiceType.Building, systemName, bi.GetUncheckedLocalizedTitle(), cemeteryAi.m_deathCareAccumulation, cemeteryAi.m_deathCareAccumulation, cemeteryAi.m_deathCareRadius, cemeteryAi.m_deathCareRadius);
            }
            else if (ai is FireStationAI fireStationAi)
            {
                return new OptionItem(ServiceType.Building, systemName, bi.GetUncheckedLocalizedTitle(), fireStationAi.m_fireDepartmentAccumulation, fireStationAi.m_fireDepartmentAccumulation, fireStationAi.m_fireDepartmentRadius, fireStationAi.m_fireDepartmentRadius);
            }
            else if (ai is FirewatchTowerAI firewatchTowerAi)
            {
                return new OptionItem(ServiceType.Building, systemName, bi.GetUncheckedLocalizedTitle(), null, null, firewatchTowerAi.m_firewatchRadius, firewatchTowerAi.m_firewatchRadius);
            }
            else if (ai is RadioMastAI radioMastAi)
            {
                return new OptionItem(ServiceType.Building, systemName, bi.GetUncheckedLocalizedTitle(), null, null, radioMastAi.m_transmitterPower, radioMastAi.m_transmitterPower);
            }
            else if (ai is PoliceStationAI policeStationAi)
            {
                return new OptionItem(ServiceType.Building, systemName, bi.GetUncheckedLocalizedTitle(), policeStationAi.m_policeDepartmentAccumulation, policeStationAi.m_policeDepartmentAccumulation, policeStationAi.m_policeDepartmentRadius, policeStationAi.m_policeDepartmentRadius);
            }
            else if (ai is SchoolAI schoolAi)
            {
                return new OptionItem(ServiceType.Building, systemName, bi.GetUncheckedLocalizedTitle(), schoolAi.m_educationAccumulation, schoolAi.m_educationAccumulation, schoolAi.m_educationRadius, schoolAi.m_educationRadius);
            }
            else if (ai is CargoHarborAI cargoHarborAi) //take care, should come before CargoStationAI
            {
                return new OptionItem(ServiceType.Building, systemName, bi.GetUncheckedLocalizedTitle(), cargoHarborAi.m_cargoTransportAccumulation, cargoHarborAi.m_cargoTransportAccumulation, cargoHarborAi.m_cargoTransportRadius, cargoHarborAi.m_cargoTransportRadius);
            }
            else if (ai is CargoStationAI cargoStationAi)
            {
                return new OptionItem(ServiceType.Building, systemName, bi.GetUncheckedLocalizedTitle(), cargoStationAi.m_cargoTransportAccumulation, cargoStationAi.m_cargoTransportAccumulation, cargoStationAi.m_cargoTransportRadius, cargoStationAi.m_cargoTransportRadius);
            }
            else if (ai is ParkAI parkAi)
            {
                return new OptionItem(ServiceType.Building, systemName, bi.GetUncheckedLocalizedTitle(), parkAi.m_entertainmentAccumulation, parkAi.m_entertainmentAccumulation, parkAi.m_entertainmentRadius, parkAi.m_entertainmentRadius);
            }
            else if (ai is MonumentAI monumentAi)
            {
                return new OptionItem(ServiceType.Building, systemName, bi.GetUncheckedLocalizedTitle(), monumentAi.m_entertainmentAccumulation, monumentAi.m_entertainmentAccumulation, monumentAi.m_entertainmentRadius, monumentAi.m_entertainmentRadius);
            }
            else
            {
                throw new Exception($"Can't handle ai of type '{ai.GetType().FullName}'.");
            }
        }

        private OptionItem LoadTransport(string systemName)
        {
            var ti = PrefabCollection<TransportInfo>.FindLoaded(systemName);
            var ai = (TransportLineAI)ti.m_netInfo.GetAI();
            return new OptionItem(ServiceType.Transport, ti.name, ti.GetUncheckedLocalizedTitle(), ai.m_publicTransportAccumulation, ai.m_publicTransportAccumulation, ai.m_publicTransportRadius, ai.m_publicTransportRadius);
        }
    }
}
