using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ParkingReserve.Models
{
    public interface IEstacionamentoService
    {
        List<Estacionamento> Consultar();
        Estacionamento Consultar(string id);
        Estacionamento Cadastrar(Estacionamento est);
        Estacionamento Alterar(string id, Estacionamento est);
        void Deletar(string id);
    }
}
