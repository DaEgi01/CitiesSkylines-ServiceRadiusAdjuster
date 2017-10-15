namespace ServiceRadiusAdjuster.Model
{
    public sealed class Category : TypesafeEnum
    {
        private readonly string displayName;

        private Category(string name, string displayName) : base(name)
        {
            this.displayName = displayName;
        }

        public string DisplayName => displayName;

        public static readonly Category RoadsMaintenance = new Category("RoadsMaintenance", "Road condition");
        public static readonly Category RoadsIntersection = new Category("RoadsIntersection", "Intersections");
        public static readonly Category Electicity = new Category("ElectricityDefault", "Electricity");
        public static readonly Category WaterServices = new Category("WaterServices", "Water and Sewage");
        public static readonly Category HeatingServices = new Category("WaterHeating", "Heating");
        public static readonly Category Default = new Category("Default", "Default");
        public static readonly Category HealthCare = new Category("HealthcareDefault", "Health Care");
        public static readonly Category FireDeparment = new Category("FireDepartmentFire", "Fire Department");
        public static readonly Category DisasterServices = new Category("FireDepartmentDisaster", "Disaster Services");
        public static readonly Category PoliceDepartment = new Category("PoliceDefault", "Police Department");
        public static readonly Category Education = new Category("EducationDefault", "Education");
        public static readonly Category PublicTransportBus = new Category("PublicTransportBus", "Public Transport - Bus");
        public static readonly Category PublicTransportTram = new Category("PublicTransportTram", "Public Transport - Tram");
        public static readonly Category PublicTransportMetro = new Category("PublicTransportMetro", "Public Transport - Metro");
        public static readonly Category PublicTransportTrain = new Category("PublicTransportTrain", "Public Transport - Train");
        public static readonly Category PublicTransportShip = new Category("PublicTransportShip", "Public Transport - Ship");
        public static readonly Category PublicTransportPlane = new Category("PublicTransportPlane", "Public Transport - Plane");
        public static readonly Category PublicTransportMonorail = new Category("PublicTransportMonorail", "Public Transport - Monorail");
        public static readonly Category PublicTransportCableCar = new Category("PublicTransportPlane", "Public Transport - Plane");
        public static readonly Category PublicTransportTaxi = new Category("PublicTransportTaxi", "Public Transport - Taxi");
        public static readonly Category BeautificationParks = new Category("BeautificationParks", "Beautification - Parks");
        public static readonly Category BeautificationPlazas = new Category("BeautificationPlazas", "Beautification - Plazas");
        public static readonly Category BeautificationOthers = new Category("BeautificationOthers", "Beautification - Others");
        public static readonly Category BeautificationTourismAndLeisure = new Category("BeautificationExpansion1", "Beautification - Tourism & Leisure");
        public static readonly Category BeautificationWinterParksAndPlazas = new Category("BeautificationExpansion2", "Beautification - Winter Parks & Plazas");
        public static readonly Category MonumentLandmarks = new Category("MonumentLandmarks", "Unique Buildings - Landmarks");
        public static readonly Category MonumentTourismAndLeisure = new Category("MonumentExpansion1", "Unique Buildings - Tourism & Leisure");
        public static readonly Category MonumentFootball = new Category("MonumentFootball", "UniqueBuildings - Football");
        public static readonly Category MonumentCategory1 = new Category("MonumentCategory1", "UniqueBuildings - Level 1");
        public static readonly Category MonumentCategory2 = new Category("MonumentCategory2", "UniqueBuildings - Level 2");
        public static readonly Category MonumentCategory3 = new Category("MonumentCategory3", "UniqueBuildings - Level 3");
        public static readonly Category MonumentCategory4 = new Category("MonumentCategory4", "UniqueBuildings - Level 4");
        public static readonly Category MonumentCategory5 = new Category("MonumentCategory5", "UniqueBuildings - Level 5");
        public static readonly Category MonumentCategory6 = new Category("MonumentCategory6", "UniqueBuildings - Level 6");
    }
}
