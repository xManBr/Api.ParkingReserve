using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ParkingReserve.Models
{
    public interface IParkingReserveDatabaseSettings
    {
        string estacionamentoCollectionsName { get; set;}
        string connectionString { get; set; }
        string dataBaseName { get; set; }
    }
}
