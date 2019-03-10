using System;
using System.Collections.Generic;

namespace ServiceRadiusAdjuster.Model
{
    public class OptionItem : IEquatable<OptionItem>
    {
        //TODO localization
        private readonly string couldNotParseAccumulationError = "Could not parse the new accumulation value. Please enter a valid number.";
        private readonly string couldNotParseRadiusError = "Could not parse the new radius value. Please enter a valid number.";

        public OptionItem(
            ServiceType serviceType,
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
            return this.Equals(obj as OptionItem);
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

            return this.ServiceType == other.ServiceType &&
                this.SystemName == other.SystemName &&
                this.DisplayName == other.DisplayName &&
                this.Accumulation == other.Accumulation &&
                this.AccumulationDefault == other.AccumulationDefault &&
                this.Radius == other.Radius &&
                this.RadiusDefault == other.RadiusDefault &&
                this.Ignore == other.Ignore;
        }

        public override int GetHashCode()
        {
            var hashCode = -173902468;
            hashCode = hashCode * -1521134295 + EqualityComparer<ServiceType>.Default.GetHashCode(this.ServiceType);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.SystemName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.DisplayName);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.Accumulation);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.AccumulationDefault);
            hashCode = hashCode * -1521134295 + EqualityComparer<float?>.Default.GetHashCode(this.Radius);
            hashCode = hashCode * -1521134295 + EqualityComparer<float?>.Default.GetHashCode(this.RadiusDefault);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.Ignore);
            return hashCode;
        }

        public static bool operator ==(OptionItem item1, OptionItem item2)
        {
            return EqualityComparer<OptionItem>.Default.Equals(item1, item2);
        }

        public static bool operator !=(OptionItem item1, OptionItem item2)
        {
            return !(item1 == item2);
        }
    }
}