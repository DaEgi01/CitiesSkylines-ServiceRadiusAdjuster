using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceRadiusAdjuster.Model
{
    public sealed class ServiceType
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
    }
}
