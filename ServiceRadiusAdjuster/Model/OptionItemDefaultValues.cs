namespace ServiceRadiusAdjuster.Model
{
    public readonly struct OptionItemDefaultValues
    {
        public OptionItemDefaultValues(string systemName, int? accumulationDefault, float radiusDefault)
        {
            SystemName = systemName;
            AccumulationDefault = accumulationDefault;
            RadiusDefault = radiusDefault;
        }

        public string SystemName { get; }
        public int? AccumulationDefault { get; }
        public float RadiusDefault { get; }
    }
}
