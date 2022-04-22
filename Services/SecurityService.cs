using Api.ParkingReserve.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.ParkingReserve.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IParkingReserveDatabaseSettings _settings;

        public SecurityService(IParkingReserveDatabaseSettings settings, IMongoClient mongoClient)
        {
            _settings = settings;
        }
        public string GerarToken(string email, string idUsuario, bool condutor, bool estacionamento)
        {
            var clams = new List<Claim>();
            clams.Add(new Claim(ClaimTypes.Email, email));
            clams.Add(new Claim(ClaimTypes.NameIdentifier, idUsuario));
            if (condutor)
            {
                clams.Add(new Claim(ClaimTypes.Role, "condutor"));
            }
            if (estacionamento)
            {
                clams.Add(new Claim(ClaimTypes.Role, "estacionamento"));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.fraseSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(clams),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(
                      new SymmetricSecurityKey(key),
                      SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string Emcritar(string password)
        {
            var key = Encoding.ASCII.GetBytes(_settings.fraseSecreta);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: key,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
