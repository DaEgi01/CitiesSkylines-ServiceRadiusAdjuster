using System;

namespace ServiceRadiusAdjuster.Model
{
    public class OptionItem : IEquatable<OptionItem>
    {
        public OptionItem(ServiceType serviceType,
            string systemName,
            string displayName,
            int? accumulation,
            int? accumulationDefault,
            float? radius,
            float? radiusDefault,
            bool ignore = false)
        {
            this.ServiceType = serviceType;
            this.SystemName = systemName;
            this.DisplayName = displayName;
            this.Accumulation = accumulation;
            this.AccumulationDefault = accumulationDefault;
            this.Radius = radius;
            this.RadiusDefault = radiusDefault;
            this.Ignore = ignore;
        }

        //TODO primitive obsession
        public ServiceType ServiceType { get; }
        public string SystemName { get; }
        public string DisplayName { get; }
        public int? Accumulation { get; set; }
        public int? AccumulationDefault { get; }
        public float? Radius { get; set; }
        public float? RadiusDefault { get; }
        public bool Ignore { get; set; }

        public override bool Equals(object obj)
        {
            var oi = obj as OptionItem;
            return Equals(oi);
        }

        public bool Equals(OptionItem other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return
            (
                ServiceType == other.ServiceType
                && SystemName == other.SystemName
                && DisplayName == other.DisplayName
                && AccumulationDefault == other.AccumulationDefault
                && RadiusDefault == other.RadiusDefault
                && Ignore == other.Ignore
            );
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 23;
                hash = 17 * ServiceType.GetHashCode();
                hash = 17 * SystemName.GetHashCode();
                hash = 17 * DisplayName.GetHashCode();
                hash = 17 * AccumulationDefault.GetHashCode();
                hash = 17 * RadiusDefault.GetHashCode();
                hash = 17 * Ignore.GetHashCode();
                return hash;
            }
        }
    }
}