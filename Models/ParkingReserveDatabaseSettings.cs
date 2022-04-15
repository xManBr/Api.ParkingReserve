using Api.ParkingReserve.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ParkingReserve.Models
{
    public class ParkingReserveDatabaseSettings : IParkingReserveDatabaseSettings
    {
        public string estacionamentoCollectionsName { get; set; }
        public string connectionString { get; set; }
        public string dataBaseName { get; set; }
    }
}
