using Api.ParkingReserve.Interfaces;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.ParkingReserve.Services
{
    public class TokenService : ITokenService
    {
        private readonly IParkingReserveDatabaseSettings _settings;

        public TokenService(IParkingReserveDatabaseSettings settings, IMongoClient mongoClient)
        {
            _settings = settings;
        }
        public string GerarToken(string email, string idUsuario, bool condutor, bool estacionamento)
        {
            var perfilCondutor = condutor ? "condutor" : "anonimo";
            var perfilEstacionamento = estacionamento ? "estacionamento" : "anonimo";

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.fraseSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Email, email),
                        new Claim(ClaimTypes.Role, perfilCondutor),
                        new Claim(ClaimTypes.Role, perfilEstacionamento),
                        new Claim(ClaimTypes.NameIdentifier, idUsuario)
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(
                      new SymmetricSecurityKey(key),
                      SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
