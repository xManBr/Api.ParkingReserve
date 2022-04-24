using Api.ParkingReserve.Globais;
using Api.ParkingReserve.Interfaces;
using Api.ParkingReserve.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ParkingReserve.Services
{
    public class VagaService : IVagaService
    {
        private readonly IMongoCollection<Vaga> _vaga;

        public VagaService(IParkingReserveDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.dataBaseName);
            _vaga = database.GetCollection<Vaga>(settings.vagaCollectionsName);
        }


        public Vaga Alterar(string id, Vaga vaga)
        {
            _vaga.ReplaceOne(e => e.idVaga == id, vaga);

            return vaga;
        }

        public Vaga Cadastrar(Vaga vaga)
        {
            vaga.idVaga = string.Empty;
            vaga.situacao = Config.SITUACAO_VAGA_DESABILITADA;
            vaga.reservaExpressa = null;
            _vaga.InsertOne(vaga);

            return vaga;
        }

        public List<Vaga> Consultar()
        {
            return _vaga.Find(e => true).ToList();
        }

        public Vaga Consultar(string id)
        {
            return _vaga.Find(e => e.idVaga == id).FirstOrDefault();
        }

        public void Deletar(string id)
        {
            _vaga.DeleteOne(e => e.idVaga == id);
        }

        public void Desabilitar(string id)
        {
            this.MudarSituacao(id, Config.SITUACAO_VAGA_DESABILITADA);
        }

        public void Habilitar(string id)
        {
            this.MudarSituacao(id, Config.SITUACAO_VAGA_HABILITADA);
        }

        private void MudarSituacao(string id, string novaSituacao)
        {
            var filter = Builders<Vaga>.Filter.Eq("idVaga", id);

            var update = Builders<Vaga>.Update
                .Set("situacao", novaSituacao);

            _vaga.UpdateOne(filter, update);
        }

        public List<Vaga> ConsultarVagaSemReserva(string idEstacionamento)
        {
            return _vaga.Find(e => (e.idEstacionamento == idEstacionamento) && (e.situacao == Config.SITUACAO_VAGA_HABILITADA) &&  (e.reservaExpressa == null)).ToList();
        }
    }

}

