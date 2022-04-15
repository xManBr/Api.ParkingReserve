using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.ParkingReserve.Models
{
    [BsonIgnoreExtraElements]
    public class Estacionamento
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string idEstacionamento { get; set; } = string.Empty;
        public string email { get; set; }
        public string telefone { get; set; }
        public string cnpj { get; set; }
        public string nome { get; set; }
        public string cep { get; set; }
        public string idUsuario { get; set; }
        public bool manobrista { get; set; }
        public int pontuacao { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string situacao { get; set; }
        public DateTime dataCadastro { get; set; }
        public DateTime dataCredeciamento { get; set; }
    }
}
