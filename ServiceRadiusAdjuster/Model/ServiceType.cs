using System.Collections.Generic;
using System.Linq;

namespace ServiceRadiusAdjuster.Model
{
    public class ServiceType : TypesafeEnum
    {
        private ServiceType(string name) : base(name)
        {
        }

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
                return new ServiceType(name);
            }
            else
            {
                return result;
            }
        }
    }
}
