using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ParkingReserve.Interfaces
{
    public interface ISecurityService
    {
        string GerarToken(string email, string idUsuario, bool condutor, bool estacionamento, bool administrador);
        string Emcritar(string password);
    }
}
