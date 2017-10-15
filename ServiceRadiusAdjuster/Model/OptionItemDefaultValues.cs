using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceRadiusAdjuster.Model
{
    public class OptionItemDefaultValues : ValueObject<OptionItemDefaultValues>
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

        protected override bool EqualsCore(OptionItemDefaultValues other)
        {
            return SystemName == other.SystemName
                && AccumulationDefault == other.AccumulationDefault
                && RadiusDefault == other.RadiusDefault;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                return 17
                    + 23 * SystemName.GetHashCode()
                    + 23 * AccumulationDefault.GetHashCode()
                    + 23 * RadiusDefault.GetHashCode();
            }
        }
    }
}
