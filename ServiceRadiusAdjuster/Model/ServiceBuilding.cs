using System.Collections.Generic;
using System.Linq;

namespace ServiceRadiusAdjuster.Model
{
    public sealed class ServiceBuilding
    {
        private ServiceBuilding(string name, string displayName)
        {
            this.Name = name ?? throw new System.ArgumentNullException(nameof(name));
            this.DisplayName = displayName ?? throw new System.ArgumentNullException(nameof(displayName));
        }

        public string Name { get; }
        public string DisplayName { get; }

        public static readonly ServiceBuilding MedicalClinic = new ServiceBuilding("Medical Clinic", "Medical Clinic");
        public static readonly ServiceBuilding MedicalClinicEurope = new ServiceBuilding("medicalclinicEU", "Medical Clinic EU");
        public static readonly ServiceBuilding Hospital = new ServiceBuilding("Hospital", "Hospital");
        public static readonly ServiceBuilding HospitalEurope = new ServiceBuilding("hospital_EU", "Hospital EU");
        public static readonly ServiceBuilding Sauna = new ServiceBuilding("Sauna", "Sauna");

        public static readonly ServiceBuilding Cemetery = new ServiceBuilding("Cemetery", "Cemetery");
        public static readonly ServiceBuilding Crematory = new ServiceBuilding("Crematory", "Crematory");

        public static readonly ServiceBuilding FireHouse = new ServiceBuilding("Fire House", "Fire House");
        public static readonly ServiceBuilding FireHouseEurope = new ServiceBuilding("firehouse_EU", "Fire House EU");
        public static readonly ServiceBuilding FireStation = new ServiceBuilding("Fire Station", "Fire Station");
        public static readonly ServiceBuilding FireStationEurope = new ServiceBuilding("Fire_Station_EU", "Fire Station EU");
        public static readonly ServiceBuilding FirewatchTower = new ServiceBuilding("Firewatch Tower", "Firewatch Tower");

        public static readonly ServiceBuilding RadioMastShort = new ServiceBuilding("Radio Mast Short", "Short Radio Mast");
        public static readonly ServiceBuilding RadioMastTall = new ServiceBuilding("Radio Mast Tall", "Tall Radio Mast");

        public static readonly ServiceBuilding PoliceStation = new ServiceBuilding("Police Station", "Police Station");
        public static readonly ServiceBuilding PoliceStationEurope = new ServiceBuilding("police_station_EU", "Police Station EU");
        public static readonly ServiceBuilding PoliceHeadquarters = new ServiceBuilding("Police Headquarters", "Police Headquarters");
        public static readonly ServiceBuilding PoliceHeadquartersEurope = new ServiceBuilding("Police Headquarters EU", "Police Headquarters EU");
        public static readonly ServiceBuilding Prison = new ServiceBuilding("Prison", "Prison");

        public static readonly ServiceBuilding ElementarySchool = new ServiceBuilding("Elementary School", "Elementary School");
        public static readonly ServiceBuilding ElementarySchoolEurope = new ServiceBuilding("Elementary_School_EU", "Elementary School EU");
        public static readonly ServiceBuilding HighSchool = new ServiceBuilding("High School", "High School");
        public static readonly ServiceBuilding HighSchoolEurope = new ServiceBuilding("highschool_EU", "High School EU");
        public static readonly ServiceBuilding University = new ServiceBuilding("University", "University");
        public static readonly ServiceBuilding UniversityEurope = new ServiceBuilding("University_EU", "University EU");

        public static readonly ServiceBuilding CargoTrainTerminal = new ServiceBuilding("Cargo Center", "Cargo Train Terminal");
        public static readonly ServiceBuilding CargoHarbor = new ServiceBuilding("Cargo Harbor", "Cargo Harbor");
        public static readonly ServiceBuilding CargoHub = new ServiceBuilding("Cargo Hub", "Cargo Hub");

        public static readonly ServiceBuilding SmallPark = new ServiceBuilding("Regular Park", "Small Park");
        public static readonly ServiceBuilding ParkWithTrees = new ServiceBuilding("Expensive Park", "Park with Trees");
        public static readonly ServiceBuilding SmallPlayground = new ServiceBuilding("Regular Playground", "Small Playground");
        public static readonly ServiceBuilding LargePlayground = new ServiceBuilding("Expensive Playground", "Large Playground");
        public static readonly ServiceBuilding DogPark = new ServiceBuilding("dog-park-fence", "Dog Park");
        public static readonly ServiceBuilding JapaneseGarden = new ServiceBuilding("JapaneseGarden", "Japanese Garden");
        public static readonly ServiceBuilding BotanicalGarden = new ServiceBuilding("Botanical garden", "Botanical Garden");
        public static readonly ServiceBuilding CarouselPark = new ServiceBuilding("MerryGoRound", "Carousel Park");
        public static readonly ServiceBuilding BouncyCastlePark = new ServiceBuilding("bouncer_castle", "Bouncy Castle Park");
        public static readonly ServiceBuilding PlazaWithTrees = new ServiceBuilding("Regular Plaza", "Plaza with Trees");
        public static readonly ServiceBuilding PlazaWithPicnicTable = new ServiceBuilding("Expensive Plaza", "Plaza with Picnic Table");
        public static readonly ServiceBuilding ParadoxPlaza = new ServiceBuilding("ParadoxPlaza", "Paradox Plaza");
        public static readonly ServiceBuilding BasketballCourt = new ServiceBuilding("Basketball Court", "Basketball Court");
        public static readonly ServiceBuilding TennisCourt = new ServiceBuilding("Tennis_Court_EU", "Tennis Court");

        public static readonly ServiceBuilding FishingPier = new ServiceBuilding("2x8_FishingPier;2x2_winter_fishing_pier", "Fishing Pier");
        public static readonly ServiceBuilding WinterFishingPier = new ServiceBuilding("2x2_winter_fishing_pier", "Winter Fishing Pier");
        public static readonly ServiceBuilding FishingTours = new ServiceBuilding("3x2_Fishing tours", "Fishing Tours");
        public static readonly ServiceBuilding JetSkiRental = new ServiceBuilding("2x2_Jet_ski_rental", "Jet Ski Rental");
        public static readonly ServiceBuilding Marina = new ServiceBuilding("4x4_Marina", "Marina");
        public static readonly ServiceBuilding RestaurantPier = new ServiceBuilding("2x4_RestaurantPier", "Restaurant Pier");
        public static readonly ServiceBuilding BeachVolleyballCourt = new ServiceBuilding("Beachvolley Court", "Beach Volleyball Court");
        public static readonly ServiceBuilding RidingStable = new ServiceBuilding("9x15_RidingStable", "Riding Stable");
        public static readonly ServiceBuilding Skatepark = new ServiceBuilding("Skatepark", "Skatepark");
        public static readonly ServiceBuilding SnowmobileTrack = new ServiceBuilding("Snowmobile Track", "Snowmobile Track");
        public static readonly ServiceBuilding IceHokeyRink = new ServiceBuilding("Ice Hockey Rink", "Ice Hockey Rink");

        public static readonly ServiceBuilding SnowmanPark = new ServiceBuilding("Snowman_Park", "Snowman Park");
        public static readonly ServiceBuilding IceSculpturePark = new ServiceBuilding("Ice Sculpture Park", "Ice Sculpture Park");
        public static readonly ServiceBuilding SleddingHill = new ServiceBuilding("Sled_Hill", "Sledding Hill");
        public static readonly ServiceBuilding CurlingPark = new ServiceBuilding("Curling Park", "Curling Park");
        public static readonly ServiceBuilding SkatingRink = new ServiceBuilding("Skating Rink", "Skating Rink");
        public static readonly ServiceBuilding SkiLodge = new ServiceBuilding("Ski Lodge", "Ski Lodge");
        public static readonly ServiceBuilding CrossCountrySkiingPark = new ServiceBuilding("Cross-Country Skiing", "Cross-Country Skiing Park");
        public static readonly ServiceBuilding FirepitPark = new ServiceBuilding("Public Firepit", "Firepit Park");

        public static readonly ServiceBuilding StatueOfIndustry = new ServiceBuilding("Statue of Industry", "Statue of Industry");
        public static readonly ServiceBuilding StatueOfWealth = new ServiceBuilding("StatueOfWealth", "Statue of Wealth");
        public static readonly ServiceBuilding LazaretPlaza = new ServiceBuilding("Lazaret Plaza", "Lazaret Plaza");
        public static readonly ServiceBuilding StatueOfShopping = new ServiceBuilding("Statue of Shopping", "Statue of Shopping");
        public static readonly ServiceBuilding PlazaOfTheDead = new ServiceBuilding("Plaza of the Dead", "Plaza of the Dead");
        public static readonly ServiceBuilding MeteoritePark = new ServiceBuilding("Meteorite Park", "Meteorite Park");

        public static readonly ServiceBuilding FountainOfLifeAndDeath = new ServiceBuilding("Fountain of LifeDeath", "Fontain of Life and Death");
        public static readonly ServiceBuilding FriendlyNeighborhoodPark = new ServiceBuilding("Friendly Neighborhood", "Friendly Neighborhood Park");
        public static readonly ServiceBuilding TransportTower = new ServiceBuilding("Transport Tower", "Transport Tower");
        public static readonly ServiceBuilding MallOfModeration = new ServiceBuilding("Trash Mall", "Mall of Moderation");
        public static readonly ServiceBuilding PoshMall = new ServiceBuilding("Posh Mall", "Posh Mall");
        public static readonly ServiceBuilding DisasterMemorial = new ServiceBuilding("Disaster Memorial", "Disaster Memorial");

        public static readonly ServiceBuilding ColossalOrderOffices = new ServiceBuilding("Colossal Offices", "Colossal Order Offices");
        public static readonly ServiceBuilding OfficialPark = new ServiceBuilding("Official Park", "Official Park");
        public static readonly ServiceBuilding CourtHouse = new ServiceBuilding("Court House", "Court House");
        public static readonly ServiceBuilding GrandMall = new ServiceBuilding("Grand Mall", "Grand Mall");
        public static readonly ServiceBuilding TaxOffice = new ServiceBuilding("City Hall", "Tax Office");
        public static readonly ServiceBuilding HelicopterPark = new ServiceBuilding("Helicopter Park", "Helicopter Park");

        public static readonly ServiceBuilding BusinessPark = new ServiceBuilding("Business Park", "Business Park");
        public static readonly ServiceBuilding GrandLibrary = new ServiceBuilding("Library", "Grand Library");
        public static readonly ServiceBuilding Observatory = new ServiceBuilding("Observatory", "Observatory");
        public static readonly ServiceBuilding OperaHouse = new ServiceBuilding("Opera House", "Opera House");
        public static readonly ServiceBuilding OpressionOffice = new ServiceBuilding("Opression Office", "Opression Office");
        public static readonly ServiceBuilding PyramidOfSafety = new ServiceBuilding("Pyramid Of Safety", "Pyramid Of Safety");

        public static readonly ServiceBuilding ScienceCenter = new ServiceBuilding("ScienceCenter", "Science Center");
        public static readonly ServiceBuilding ServicingServicesOffices = new ServiceBuilding("Servicing Services", "Servicing Services Offices");
        public static readonly ServiceBuilding Aquarium = new ServiceBuilding("SeaWorld", "Aquarium");
        public static readonly ServiceBuilding ExpoCenter = new ServiceBuilding("ExpoCenter", "Expo Center");
        public static readonly ServiceBuilding HighInterestTower = new ServiceBuilding("High Interest Tower", "High Interest Tower");
        public static readonly ServiceBuilding SphinxOfScenarios = new ServiceBuilding("Sphinx Of Scenarios", "Sphinx of Scenarios");

        public static readonly ServiceBuilding CathedralOfPlenitude = new ServiceBuilding("Cathedral of Plentitude", "Cathedral of Plenitude");
        public static readonly ServiceBuilding Stadium = new ServiceBuilding("Stadium", "Stadium");
        public static readonly ServiceBuilding ModernArtMuseum = new ServiceBuilding("Modern Art Museum", "MAM Modern Art Museum");
        public static readonly ServiceBuilding SeaAndSkyScraper = new ServiceBuilding("SeaAndSky Scraper", "Sea-and-Sky Scraper");
        public static readonly ServiceBuilding TheaterOfWonders = new ServiceBuilding("Theater of Wonders", "Theater of Wonders");
        public static readonly ServiceBuilding SparklyUnicornRainbowPark = new ServiceBuilding("Sparkly Unicorn Rainbow Park", "Sparkly Unicorn Rainbow Park");

        public static readonly ServiceBuilding PandaSanctuary = new ServiceBuilding("Panda Sanctuary", "Panda Sanctuary");
        public static readonly ServiceBuilding OrientalPearlTower = new ServiceBuilding("Oriental Pearl Tower", "Oriental Pearl Tower");
        public static readonly ServiceBuilding TempleComplex = new ServiceBuilding("Temple", "Temple Complex");
        public static readonly ServiceBuilding TrafficPark = new ServiceBuilding("Traffic Park", "Traffic Park");
        public static readonly ServiceBuilding BoatMuseum = new ServiceBuilding("Boat Museum", "Boat Museum");
        public static readonly ServiceBuilding LocomotiveHalls = new ServiceBuilding("Steam Train", "Locomotive Halls");

        public static readonly ServiceBuilding Casino = new ServiceBuilding("Casino", "Casino");
        public static readonly ServiceBuilding DrivingRange = new ServiceBuilding("Driving Range", "Driving Range");
        public static readonly ServiceBuilding FantasticFountain = new ServiceBuilding("Fancy Fountain", "Fantastic Fountain");
        public static readonly ServiceBuilding LuxuryHotel = new ServiceBuilding("LuxuryHotel", "Luxury Hotel");
        public static readonly ServiceBuilding Zoo = new ServiceBuilding("Zoo", "Zoo");

        public static readonly ServiceBuilding FootballStadium = new ServiceBuilding("Football Stadium", "Football Stadium");

        public static readonly ServiceBuilding EdenProject = new ServiceBuilding("Eden Project", "Eden Project");
        public static readonly ServiceBuilding SpaceElevator = new ServiceBuilding("Space Elevator", "Space Elevator");
        public static readonly ServiceBuilding HadronCollider = new ServiceBuilding("Hadron Collider", "Hadron Collider");
        public static readonly ServiceBuilding MedicalCenter = new ServiceBuilding("Medical Center", "Medical Center");

        public static IEnumerable<ServiceBuilding> GetAll()
        {
            return new[]
            {
                MedicalClinic,
                MedicalClinicEurope,
                Hospital,
                HospitalEurope,
                Sauna,
                Cemetery,
                Crematory,
                FireHouse,
                FireHouseEurope,
                FireStation,
                FireStationEurope,
                FirewatchTower,
                RadioMastShort,
                RadioMastTall,
                PoliceStation,
                PoliceStationEurope,
                PoliceHeadquarters,
                PoliceHeadquartersEurope,
                Prison,
                ElementarySchool,
                ElementarySchoolEurope,
                HighSchool,
                HighSchoolEurope,
                University,
                UniversityEurope,
                CargoTrainTerminal,
                CargoHarbor,
                CargoHub,
                SmallPark,
                ParkWithTrees,
                SmallPlayground,
                LargePlayground,
                DogPark,
                JapaneseGarden,
                BotanicalGarden,
                CarouselPark,
                BouncyCastlePark,
                PlazaWithTrees,
                PlazaWithPicnicTable,
                ParadoxPlaza,
                BasketballCourt,
                TennisCourt,
                FishingPier,
                WinterFishingPier,
                FishingTours,
                JetSkiRental,
                Marina,
                RestaurantPier,
                BeachVolleyballCourt,
                RidingStable,
                Skatepark,
                SnowmobileTrack,
                IceHokeyRink,
                SnowmanPark,
                IceSculpturePark,
                SleddingHill,
                CurlingPark,
                SkatingRink,
                SkiLodge,
                CrossCountrySkiingPark,
                FirepitPark,
                StatueOfIndustry,
                StatueOfWealth,
                LazaretPlaza,
                StatueOfShopping,
                PlazaOfTheDead,
                MeteoritePark,
                FountainOfLifeAndDeath,
                FriendlyNeighborhoodPark,
                TransportTower,
                MallOfModeration,
                PoshMall,
                DisasterMemorial,
                ColossalOrderOffices,
                OfficialPark,
                CourtHouse,
                GrandMall,
                TaxOffice,
                HelicopterPark,
                BusinessPark,
                GrandLibrary,
                Observatory,
                OperaHouse,
                OpressionOffice,
                PyramidOfSafety,
                ScienceCenter,
                ServicingServicesOffices,
                Aquarium,
                ExpoCenter,
                HighInterestTower,
                SphinxOfScenarios,
                CathedralOfPlenitude,
                Stadium,
                ModernArtMuseum,
                SeaAndSkyScraper,
                TheaterOfWonders,
                SparklyUnicornRainbowPark,
                PandaSanctuary,
                OrientalPearlTower,
                TempleComplex,
                TrafficPark,
                BoatMuseum,
                LocomotiveHalls,
                Casino,
                DrivingRange,
                FantasticFountain,
                LuxuryHotel,
                Zoo,
                FootballStadium,
                EdenProject,
                SpaceElevator,
                HadronCollider,
                MedicalCenter,
            };
        }

        public static ServiceBuilding FromName(string name)
        {
            var result = GetAll().SingleOrDefault(s => s.Name == name);
            if (result == null)
            {
                return new ServiceBuilding(name, name);
            }

            return result;
        }
    }
}
