using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ParkingReserve.Models
{
    public class Autenticacao : UsuarioBase
    {
        public string message { get; set; }
        public Usuario usuario { get; set; }
        public string token { get; set; }
    }
}
