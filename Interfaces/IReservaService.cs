using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.ParkingReserve.Models;

namespace Api.ParkingReserve.Interfaces
{
    public interface IReservaService
    {
        List<Reserva> Consultar();
        Reserva Consultar(string id);
        Reserva Cadastrar(Reserva est);
        Reserva Alterar(string id, Reserva est);
        void Deletar(string id);
        void Desabilitar(string id);
        void Habilitar(string id);
    }
}
