using ServiceRadiusAdjuster.FunctionalCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceRadiusAdjuster.Model
{
    public sealed class ServiceType : IEquatable<ServiceType>
    {
        private ServiceType(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }

        public static readonly ServiceType Building = new ServiceType("Building");
        public static readonly ServiceType Transport = new ServiceType("Transport");

        public static IEnumerable<ServiceType> GetAll()
        {
            yield return Building;
            yield return Transport;
        }

        public static Result<string, ServiceType> FromName(string name)
        {
            var result = GetAll().SingleOrDefault(s => s.Name == name);
            if (result is null)
            {
                return Result<string, ServiceType>.Error($"Unknown ServiceType '{name}'.");
            }

            return Result<string, ServiceType>.Ok(result);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ServiceType);
        }

        public bool Equals(ServiceType? other)
        {
            return other is not null
                && Name == other.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public static bool operator ==(ServiceType type1, ServiceType type2)
        {
            return EqualityComparer<ServiceType>.Default.Equals(type1, type2);
        }

        public static bool operator !=(ServiceType type1, ServiceType type2)
        {
            return !(type1 == type2);
        }
    }
}
