using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ParkingReserve.Models
{
    [BsonIgnoreExtraElements]
    public class Vaga
    {
        public int idEstacionamento { get; set; }
        public int idVaga { get; set; }
        public string codigo { get; set; }
        public string tipoVaga { get; set; }
        public bool cobertura { get; set; }
        public decimal largura { get; set; }
        public decimal comprimento { get; set; }

    }
}
