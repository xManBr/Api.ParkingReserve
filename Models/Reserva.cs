using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ParkingReserve.Models
{
    [BsonIgnoreExtraElements]
    public class Reserva 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string idReserva { get; set; } = string.Empty;
        public string idEstacionamento { get; set; }
        public string idVaga { get; set; }
        public string idCondutor { get; set; }
        public string idVeiculo { get; set; }
        public string idUsuario { get; set; }
        public string situacao { get; set; }
        public ReservaExpressa reservaExpressa { get; set; }
    }
}
