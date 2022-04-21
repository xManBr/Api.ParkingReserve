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
    public class TokenService : ITokenService
    {
        private readonly IParkingReserveDatabaseSettings _settings;

        public TokenService(IParkingReserveDatabaseSettings settings, IMongoClient mongoClient)
        {
            _settings = settings;
        }
        public string GerarToken(string email, string idUsuario)
        {
            return _settings.fraseSecreta;
        }
    }
}
