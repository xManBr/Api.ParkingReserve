using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ParkingReserve.Models
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string idUsuario { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public string senha { get; set; }
        public string lembreteSenha { get; set; }
        public DateTime dataCadastro { get; set; }
        public bool emailConfirmado { get; set; }
        public bool perfilCondutor { get; set; }
        public bool perfilEstacionamento { get; set; }
        public string situacao { get; set; }
    }
}
