using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.ParkingReserve.Models;

namespace Api.ParkingReserve.Interfaces
{
    public interface IParkingReserveDatabaseSettings
    {
        string connectionString { get; set; }
        string dataBaseName { get; set; }
        string fraseSecreta { get; set; }
        string emailDosAdministradores { get; set; }

        string estacionamentoCollectionsName { get; set;}
        string vagaCollectionsName { get; set; }
        string reservaCollectionsName { get; set; }
        string usuarioCollectionsName { get; set; }

    }
}
