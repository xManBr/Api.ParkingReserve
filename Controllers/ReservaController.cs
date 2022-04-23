using Api.ParkingReserve.Interfaces;
using Api.ParkingReserve.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Api.ParkingReserve.Globais;
using Microsoft.AspNetCore.Authorization;

namespace Api.ParkingReserve.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly IReservaService _reservaService;
        private IVagaService _vagaService;
        private IEstacionamentoService _estacionamentoService;

        public ReservaController(IReservaService reservaService, IVagaService vagaService, IEstacionamentoService estacionamentoService)
        {
            _reservaService = reservaService;
            _vagaService = vagaService;
            _estacionamentoService = estacionamentoService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<Reserva>> Get()
        {
            return _reservaService.Consultar();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<Reserva> Get(string id)
        {
            var est = _reservaService.Consultar(id);
            if (est == null)
            {
                return NotFound($"Reserva id: {id} não encontrada");
            }
            return est;
        }

        [HttpPost]
        [Authorize(Roles = Config.ROLE_CONDUTOR)]
        public ActionResult<Reserva> Post([FromBody] Reserva reserva)
        {
            var idUsuarioCorrent = HttpContext.User.Identity.Name;
            reserva.idUsuario = idUsuarioCorrent;
            _reservaService.Cadastrar(reserva);

            return CreatedAtAction(nameof(Get), new { id = reserva.idReserva }, reserva);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Config.ROLE_CONDUTOR)]
        public ActionResult Put(string id, [FromBody] Reserva reserva)
        {
            var existe = _reservaService.Consultar(id);
            if (existe == null)
            {
                return NotFound($"Reserva id: {id} não encontrada");
            }
            else
            {
                var existeEstacionamnto = _estacionamentoService.Consultar(reserva.idEstacionamento);
                if (existeEstacionamnto == null)
                {
                    return NotFound($"A Reserva esta assocada à Vaga id {reserva.idVaga} do ao estacionamento id {reserva.idEstacionamento} que não exsite.");
                }
                else
                {
                    var idUsuarioCorrent = HttpContext.User.Identity.Name;
                    var idUsuario = HttpContext.User.Identity.Name;
                    if ((reserva.idUsuario != idUsuarioCorrent) && (existeEstacionamnto.idUsuario != idUsuario))
                    {
                        return NotFound($"Somente o usuário que efetuou a reserve ou o estacionamento, dono da vaga - podem alterar a reserva.).");
                    }
                    else
                    {
                        _reservaService.Alterar(id, reserva);
                        return NoContent();
                    }
                }
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Config.ROLE_CONDUTOR)]
        public ActionResult Delete(string id)
        {
            var reserva = _reservaService.Consultar(id);
            if (reserva == null)
            {
                return NotFound($"Reserva id: {id} não encontrada");
            }
            else
            {
                var existeEstacionamnto = _estacionamentoService.Consultar(reserva.idEstacionamento);
                if (existeEstacionamnto == null)
                {
                    return NotFound($"A Reserva esta assocada à Vaga id {reserva.idVaga} do ao estacionamento id {reserva.idEstacionamento} que não exsite.");
                }
                else
                {
                    var idUsuarioCorrent = HttpContext.User.Identity.Name;
                    var idUsuario = HttpContext.User.Identity.Name;
                    if ((reserva.idUsuario != idUsuarioCorrent) && (existeEstacionamnto.idUsuario != idUsuario))
                    {
                        return NotFound($"Somente o usuário que efetuou a reserve ou o estacionamento, dono da vaga - podem apagar a reserva.).");
                    }
                    else
                    {
                        _reservaService.Deletar(id);
                        return Ok($"Reserva {id} Apagada!");
                    }
                }
            }
        }

        [HttpPut("Habilitar/{id}")]
        [Authorize(Roles = Config.ROLE_ESTACIONAMENTO)]
        public ActionResult Habilitar(string id)
        {
            var reserva = _reservaService.Consultar(id);
            if (reserva == null)
            {
                return NotFound($"Reserva id: {id} não encontrada");
            }
            else
            {
                var existeEstacionamnto = _estacionamentoService.Consultar(reserva.idEstacionamento);
                if (existeEstacionamnto == null)
                {
                    return NotFound($"A Reserva esta assocada à Vaga id {reserva.idVaga} do ao estacionamento id {reserva.idEstacionamento} que não exsite.");
                }
                else
                {
                    var idUsuarioCorrent = HttpContext.User.Identity.Name;
                    var idUsuario = HttpContext.User.Identity.Name;
                    if ((reserva.idUsuario != idUsuarioCorrent) && (existeEstacionamnto.idUsuario != idUsuario))
                    {
                        return NotFound($"Somente o usuário que efetuou a reserve ou o estacionamento, dono da vaga - podem habilitar a reserva.).");
                    }
                    else
                    {
                        _reservaService.Habilitar(id);
                        return Ok($"Reserva {id} Habilitada!");
                    }
                }
            }

        }

        [HttpPut("Desabilitar/{id}")]
        [Authorize(Roles = Config.ROLE_ESTACIONAMENTO)]
        public ActionResult Desabilitar(string id)
        {
            var reserva = _reservaService.Consultar(id);
            if (reserva == null)
            {
                return NotFound($"Reserva id: {id} não encontrada");
            }
            else
            {
                var existeEstacionamnto = _estacionamentoService.Consultar(reserva.idEstacionamento);
                if (existeEstacionamnto == null)
                {
                    return NotFound($"A Reserva esta assocada à Vaga id {reserva.idVaga} do ao estacionamento id {reserva.idEstacionamento} que não exsite.");
                }
                else
                {
                    var idUsuarioCorrent = HttpContext.User.Identity.Name;
                    var idUsuario = HttpContext.User.Identity.Name;
                    if ((reserva.idUsuario != idUsuarioCorrent) && (existeEstacionamnto.idUsuario != idUsuario))
                    {
                        return NotFound($"Somente o usuário que efetuou a reserve ou o estacionamento, dono da vaga - podem desabilitar a reserva.).");
                    }
                    else
                    {
                        _reservaService.Desabilitar(id);
                        return Ok($"Reserva {id} Desabilitada!");
                    }
                }
            }

        }
    }
}

