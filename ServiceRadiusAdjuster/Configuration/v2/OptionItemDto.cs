namespace ServiceRadiusAdjuster.Configuration.v2
{
    public class OptionItemDto
    {
        public string SystemName { get; set; }
        public string DisplayName { get; set; }
        public string Resource { get; set; }
        public string ServiceType { get; set; }
        public string AiType { get; set; }
        public int OrderInViewGroup { get; set; }
        public float ServiceRadius { get; set; }
        public float ServiceRadiusDefault { get; set; }
    }
}
