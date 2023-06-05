using ServiceRadiusAdjuster.FunctionalCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using ServiceRadiusAdjuster.Configuration.v3;

namespace ServiceRadiusAdjuster.Model
{
    public class OptionItem : IEquatable<OptionItem>
    {
        //TODO localization
        private static readonly string _couldNotParseAccumulationError = "Could not parse the new accumulation value. Please enter a valid number.";
        private static readonly string _couldNotParseRadiusError = "Could not parse the new radius value. Please enter a valid number.";

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
            ServiceType = serviceType;
            SystemName = systemName;
            DisplayName = displayName;
            Accumulation = accumulation;
            AccumulationDefault = accumulationDefault;
            Radius = radius;
            RadiusDefault = radiusDefault;
            Ignore = ignore;
        }

        public static OptionItem From(ServiceType serviceType, OptionItemDto dto)
        {
            return new OptionItem(
                serviceType,
                dto.SystemName,
                dto.DisplayName,
                dto.Accumulation,
                dto.AccumulationDefault,
                dto.Radius,
                dto.RadiusDefault,
                dto.Ignore.GetValueOrDefault(false));
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
            Accumulation = accumulation;
        }

        public Result<string, int> SetAccumulation(string accumulationString)
        {
            var accumulationValid = int.TryParse(accumulationString, out int accumulation);
            if (accumulationValid)
            {
                Accumulation = accumulation;
                return Result<string, int>.Ok(accumulation);
            }
            else
            {
                return Result<string, int>.Error(_couldNotParseAccumulationError);
            }
        }

        public void SetRadius(float radius)
        {
            Radius = radius;
        }

        public Result<string> SetRadius(string radiusString)
        {
            var radiusValid = float.TryParse(radiusString, out float radius);
            if (radiusValid)
            {
                Radius = radius;
                return Result<string>.Ok();
            }
            else
            {
                return Result<string>.Error(_couldNotParseRadiusError);
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as OptionItem);
        }

        public bool Equals(OptionItem? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return ServiceType == other.ServiceType &&
                SystemName == other.SystemName &&
                DisplayName == other.DisplayName &&
                Accumulation == other.Accumulation &&
                AccumulationDefault == other.AccumulationDefault &&
                Radius == other.Radius &&
                RadiusDefault == other.RadiusDefault &&
                Ignore == other.Ignore;
        }

        public override int GetHashCode()
        {
            var hashCode = -173902468;
            hashCode = hashCode * -1521134295 + EqualityComparer<ServiceType>.Default.GetHashCode(ServiceType);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SystemName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DisplayName);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(Accumulation);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(AccumulationDefault);
            hashCode = hashCode * -1521134295 + EqualityComparer<float?>.Default.GetHashCode(Radius);
            hashCode = hashCode * -1521134295 + EqualityComparer<float?>.Default.GetHashCode(RadiusDefault);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(Ignore);
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
