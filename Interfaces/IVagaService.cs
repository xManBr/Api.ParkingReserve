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
        List<Vaga> ConsultarVagaSemReserva(string idEstacionamento);
        Vaga Consultar(string id);
        Vaga Cadastrar(Vaga vaga);
        Vaga Alterar(string id, Vaga vaga);
        void Deletar(string id);
        void Desabilitar(string id);
        void Habilitar(string id);
    }
}
