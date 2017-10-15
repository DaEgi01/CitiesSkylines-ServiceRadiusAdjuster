namespace ServiceRadiusAdjuster.Configuration.v1
{
    public class OptionItemDto
    {
        public string SystemName { get; set; }
        public string DisplayName { get; set; }
        public string Resource { get; set; }
        public string GameCollectionTypeName { get; set; }
        public string GameCollectionName { get; set; }
        public string AiType { get; set; }
        public string ViewGroup { get; set; }
        public int ViewGroupOrder { get; set; }
        public float ServiceRadius { get; set; }
        public float ServiceRadiusDefault { get; set; }
    }
}
