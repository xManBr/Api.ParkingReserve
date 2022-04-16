using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ParkingReserve.Models
{
    [BsonIgnoreExtraElements]
    public class ReservaExpressa
    {
        public string cpf { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }    
        public string placa { get; set; }
        public DateTime dataReservada { get; set; }
        public DateTime dataDaReserva { get; set; }
        public bool pagtoAntecipado { get; set; }      
    }
}
