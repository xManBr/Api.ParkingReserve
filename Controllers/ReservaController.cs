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

        public ReservaController(IReservaService reservaService)
        {
            _reservaService = reservaService;
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
                return NotFound($"Reserva id: {id} não encontrado");
            }
            return est;
        }

        [HttpPost]
        [Authorize(Roles = Config.ROLE_CONDUTOR)]
        public ActionResult<Reserva> Post([FromBody] Reserva reserva)
        {
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
                return NotFound($"Reserva id: {id} não encontrado");
            }

            _reservaService.Alterar(id, reserva);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Config.ROLE_CONDUTOR)]
        public ActionResult Delete(string id)
        {
            var existe = _reservaService.Consultar(id);

            if (existe == null)
            {
                return NotFound($"Reserva id: {id} não encontrada!");
            }

            _reservaService.Deletar(id);

            return Ok($"Reserva {id} Deletada!");
        }

        [HttpPut("Habilitar/{id}")]
        [Authorize(Roles = Config.ROLE_ESTACIONAMENTO)]
        public ActionResult Habilitar(string id)
        {
            var existe = _reservaService.Consultar(id);

            if (existe == null)
            {
                return NotFound($"Reserva id: {id} não encontrada!");
            }

            _reservaService.Habilitar(id);

            return Ok($"Reserva {id} Habilitada!");
        }

        [HttpPut("Desabilitar/{id}")]
        [Authorize(Roles = Config.ROLE_ESTACIONAMENTO)]
        public ActionResult Desabilitar(string id)
        {
            var existe = _reservaService.Consultar(id);

            if (existe == null)
            {
                return NotFound($"Reserva id: {id} não encontrada!");
            }

            _reservaService.Desabilitar(id);

            return Ok($"Reserva {id} Desabilitada!");
        }
    }
}

