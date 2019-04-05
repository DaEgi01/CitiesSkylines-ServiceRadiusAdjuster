using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceRadiusAdjuster.Model
{
    public sealed class ServiceTransport
    {
        private ServiceTransport(string name, string stationName)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            StationName = stationName ?? throw new ArgumentNullException(nameof(stationName));
        }

        public string Name { get; }
        public string StationName { get; }

        public static readonly ServiceTransport Bus = new ServiceTransport("Bus", "Bus Station");
        public static readonly ServiceTransport Tram = new ServiceTransport("Tram", "Tram Station");
        public static readonly ServiceTransport Train = new ServiceTransport("Train", "Train Station");
        public static readonly ServiceTransport Metro = new ServiceTransport("Metro", "Metro Station");
        public static readonly ServiceTransport Airplane = new ServiceTransport("Airplane", "Airport");
        public static readonly ServiceTransport Ship = new ServiceTransport("Ship", "Harbor");
        public static readonly ServiceTransport Taxi = new ServiceTransport("Taxi", "Tax Stand");
        //TODO public static readonly ServiceTransport EvacuationBus = new ServiceTransport("Evacuation Bus", "?"); //TODO: find out if it would do anything
        public static readonly ServiceTransport Ferry = new ServiceTransport("Ferry", "Ferry Stop");
        public static readonly ServiceTransport CableCar = new ServiceTransport("CableCar", "Cable Car Stop");
        public static readonly ServiceTransport Monorail = new ServiceTransport("Monorail", "Monorail Station");
        public static readonly ServiceTransport Blimp = new ServiceTransport("Blimp", "Blimp Stop");

        public static IEnumerable<ServiceTransport> GetAll()
        {
            return new[]
            {
                Bus,
                Tram,
                Train,
                Metro,
                Airplane,
                Ship,
                Taxi,
                Ferry,
                CableCar,
                Monorail,
                Blimp
            };
        }

        public static ServiceTransport FromName(string name)
        {
            var result = GetAll().SingleOrDefault(s => s.Name == name);
            if (result == null)
            {
                return new ServiceTransport(name, name);
            }

            return result;
        }
    }
}
