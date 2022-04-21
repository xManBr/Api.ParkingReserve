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
        Reserva Cadastrar(Reserva rserva);
        Reserva Alterar(string id, Reserva reserva);
        void Deletar(string id);
        void Desabilitar(string id);
        void Habilitar(string id);
    }
}
