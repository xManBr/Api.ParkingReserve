using Api.ParkingReserve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ParkingReserve.Interfaces
{
    public interface IEstacionamentoService
    {
        List<Estacionamento> Consultar();
        Estacionamento Consultar(string id);
        Estacionamento Cadastrar(Estacionamento est);
        Estacionamento Alterar(string id, Estacionamento est);
        void Deletar(string id);
        void Desabilitar(string id);
        void Habilitar(string id);
    }
}
