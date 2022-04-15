using Api.ParkingReserve.Interfaces;
using Api.ParkingReserve.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ParkingReserve.Services
{
    public class EstacionamentoService : IEstacionamentoService
    {
        private readonly IMongoCollection<Estacionamento> _estacionamento;

        public EstacionamentoService(IParkingReserveDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.dataBaseName);
            _estacionamento = database.GetCollection<Estacionamento>(settings.estacionamentoCollectionsName);
        }

        public Estacionamento Alterar(string id, Estacionamento est)
        {
            _estacionamento.ReplaceOne(e=> e.idEstacionamento == id, est );

            return est;
        }

        public Estacionamento Cadastrar(Estacionamento est)
        {
            _estacionamento.InsertOne(est);

            return est;
        }

        public List<Estacionamento> Consultar()
        {
            return _estacionamento.Find(e => true).ToList();
        }

        public Estacionamento Consultar(string id)
        {
            return _estacionamento.Find(e => e.idEstacionamento == id).FirstOrDefault();
        }

        public void Deletar(string id)
        {
            _estacionamento.DeleteOne(e => e.idEstacionamento == id);
        }
    }


}
