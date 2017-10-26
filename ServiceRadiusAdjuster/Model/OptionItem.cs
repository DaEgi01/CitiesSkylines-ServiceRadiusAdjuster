using System;

namespace ServiceRadiusAdjuster.Model
{
    public class OptionItem : IEquatable<OptionItem>
    {
        //TODO localization
        private readonly string couldNotParseAccumulationError = "Could not parse the new accumulation value. Please enter a valid number.";
        private readonly string couldNotParseRadiusError = "Could not parse the new radius value. Please enter a valid number.";

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
        public int? Accumulation { get; private set; }
        public int? AccumulationDefault { get; }
        public float? Radius { get; private set; }
        public float? RadiusDefault { get; }
        public bool Ignore { get; set; }

        public void SetAccumulation(int accumulation)
        {
            this.Accumulation = accumulation;
        }

        public Result SetAccumulation(string accumulationString)
        {
            var accumulationValid = int.TryParse(accumulationString, out int accumulation);
            if (accumulationValid)
            {
                this.Accumulation = accumulation;
                return Result.Ok();
            }
            else
            {
                return Result.Fail(couldNotParseAccumulationError);
            }
        }

        public void SetRadius(float radius)
        {
            this.Radius = radius;
        }

        public Result SetRadius(string radiusString)
        {
            var radiusValid = float.TryParse(radiusString, out float radius);
            if (radiusValid)
            {
                this.Radius = radius;
                return Result.Ok();
            }
            else
            {
                return Result.Fail(couldNotParseRadiusError);
            }
        }

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