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
    public class ReservaService : IReservaService
    {
        private readonly IMongoCollection<Reserva> _reserva;
        private readonly IVagaService _vagaService;

        public ReservaService(IParkingReserveDatabaseSettings settings, IMongoClient mongoClient, IVagaService vagaService)
        {
            var database = mongoClient.GetDatabase(settings.dataBaseName);
            _reserva = database.GetCollection<Reserva>(settings.reservaCollectionsName);
            _vagaService = vagaService;
        }


        public Reserva Alterar(string id, Reserva reserva)
        {
            _reserva.ReplaceOne(e => e.idReserva == id, reserva);

            var vaga = _vagaService.Consultar(reserva.idVaga);
            if (vaga != null)
            {
                vaga.reservaExpressa = reserva.reservaExpressa;
                _vagaService.Alterar(reserva.idVaga, vaga);
            }

            return reserva;
        }

        public Reserva Cadastrar(Reserva reserva)
        {
            reserva.situacao = Config.SITUACAO_RESERVA_HABILITADA;
            _reserva.InsertOne(reserva);

            var vaga = _vagaService.Consultar(reserva.idVaga);
            if (vaga != null)
            {
                vaga.reservaExpressa = reserva.reservaExpressa;
                _vagaService.Alterar(reserva.idVaga, vaga);
            }

            return reserva;
        }

        public List<Reserva> Consultar()
        {
            return _reserva.Find(e => true).ToList();
        }

        public Reserva Consultar(string id)
        {
            return _reserva.Find(e => e.idReserva == id).FirstOrDefault();
        }

        public void Deletar(string id)
        {
            var reserva = _reserva.Find(e => e.idReserva == id).FirstOrDefault();

            var vaga = _vagaService.Consultar(reserva.idVaga);
            if (vaga != null)
            {
                vaga.reservaExpressa = null;
                _vagaService.Alterar(reserva.idVaga, vaga);
            }

            _reserva.DeleteOne(e => e.idReserva == id);

        }

        public void Desabilitar(string id)
        {
            this.MudarSituacao(id, Config.SITUACAO_RESERVA_DESABILITADA);
        }

        public void Habilitar(string id)
        {
            this.MudarSituacao(id, Config.SITUACAO_RESERVA_HABILITADA);
        }

        private void MudarSituacao(string id, string novaSituacao)
        {
            var filter = Builders<Reserva>.Filter.Eq("idReserva", id);

            var update = Builders<Reserva>.Update
                .Set("situacao", novaSituacao);

            _reserva.UpdateOne(filter, update);
        }
    }

}
