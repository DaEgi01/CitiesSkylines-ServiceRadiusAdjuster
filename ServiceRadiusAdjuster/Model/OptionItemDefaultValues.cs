using System.Collections.Generic;

namespace ServiceRadiusAdjuster.Model
{
    public class OptionItemDefaultValues : ValueObject
    {
        public OptionItemDefaultValues(string systemName, int? accumulationDefault, float radiusDefault)
        {
            this.SystemName = systemName;
            this.AccumulationDefault = accumulationDefault;
            this.RadiusDefault = radiusDefault;
        }

        public string SystemName { get; }
        public int? AccumulationDefault { get; }
        public float RadiusDefault { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return SystemName;
            yield return AccumulationDefault;
            yield return RadiusDefault;
        }
    }
}
