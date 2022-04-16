using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.ParkingReserve.Models;

namespace Api.ParkingReserve.Interfaces
{
    public interface IVagaService
    {
        List<Vaga> Consultar();
        Vaga Consultar(string id);
        Vaga Cadastrar(Vaga est);
        Vaga Alterar(string id, Vaga est);
        void Deletar(string id);
        void Desabilitar(string id);
        void Habilitar(string id);
    }
}
