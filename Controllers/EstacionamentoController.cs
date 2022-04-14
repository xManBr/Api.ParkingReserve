using Api.ParkingReserve.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.ParkingReserve.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstacionamentoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EstacionamentoController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("parking-reserve-dev"));

            var list = dbClient.GetDatabase("bertioga-dev").GetCollection<Estacionamento>("Estacionamento").AsQueryable();

            return new JsonResult(list);

        }

        [HttpGet("{id}")]
        public JsonResult Get(string id)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("parking-reserve-dev"));

            var filter = Builders<Estacionamento>.Filter.Eq("idEstacionamento", id);

            var list = dbClient.GetDatabase("bertioga-dev").GetCollection<Estacionamento>("Estacionamento").Find(filter).FirstOrDefault();

            if  (list == null)
            {
                return new JsonResult("ATENÇÃO --> NENHUM REGISTRO ENCONTRADO");
            }
            else
            {
                return new JsonResult(list);
            }            
        }

        [HttpPost]
        public JsonResult Post(Estacionamento estacionamento)
        {
            Guid g = Guid.NewGuid();
            estacionamento.idEstacionamento = g.ToString();

            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("parking-reserve-dev"));
            dbClient.GetDatabase("bertioga-dev").GetCollection<Estacionamento>("Estacionamento").InsertOne(estacionamento);

            return new JsonResult("Inserido com sucesso idEstacionamento " + estacionamento.idEstacionamento);
        }

        [HttpPut]
        public JsonResult Put(Estacionamento e)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("parking-reserve-dev"));

            var filter = Builders<Estacionamento>.Filter.Eq("idEstacionamento", e.idEstacionamento);

            var update = Builders<Estacionamento>.Update
                .Set("email", e.email)
                .Set("telefone", e.telefone)
                .Set("cnpj", e.cnpj)
                .Set("nome", e.nome)
                .Set("cep", e.cep)
                .Set("idUsuario", e.idUsuario)
                .Set("manobrista", e.manobrista)
                .Set("manobrista", e.manobrista)
                .Set("latitude", e.latitude)
                .Set("longitude", e.longitude)
                .Set("situacao", e.situacao)
                .Set("dataCadastro", e.dataCadastro)
                .Set("dataCredeciamento", e.dataCredeciamento);

           var result =  dbClient.GetDatabase("bertioga-dev").GetCollection<Estacionamento>("Estacionamento").UpdateOne(filter, update);

         
            if (result.ModifiedCount > 0)
            {
                return new  JsonResult("Alterado com sucesso idEstacionamento " + e.idEstacionamento);

            }
            else
            {
                return new JsonResult("ATENÇÃO --> NENHUM REGISTRO ALTERADO");
            }

        }

        [HttpDelete("{id}")]
        public JsonResult Delete(string id)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("parking-reserve-dev"));

            var filter = Builders<Estacionamento>.Filter.Eq("idEstacionamento", id);

            var result = dbClient.GetDatabase("bertioga-dev").GetCollection<Estacionamento>("Estacionamento").DeleteOne(filter);

            if (result.DeletedCount > 0)
            {
                return new JsonResult("Deletado com sucesso idEstacionamento " + id);
            }
            else
            {
                return new JsonResult("ATENÇÃO --> NENHUM REGISTRO DELETADO");
            }
        }

    }
}
