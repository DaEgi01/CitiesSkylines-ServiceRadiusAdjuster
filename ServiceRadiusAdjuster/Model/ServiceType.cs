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
            return new[]
            {
                Building,
                Transport
            };
        }

        public static ServiceType FromName(string name)
        {
            var result = GetAll().SingleOrDefault(s => s.Name == name);
            if (result == null)
            {
                throw new Exception($"Unknown ServiceType '{name}'.");
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ServiceType);
        }

        public bool Equals(ServiceType other)
        {
            return other != null &&
                   Name == other.Name;
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
