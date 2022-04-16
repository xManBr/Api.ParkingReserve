using Api.ParkingReserve.Interfaces;
using Api.ParkingReserve.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public ActionResult<List<Reserva>> Get()
        {
            return _reservaService.Consultar();
        }

        [HttpGet("{id}")]
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
        public ActionResult<Reserva> Post([FromBody] Reserva reserva)
        {
            _reservaService.Cadastrar(reserva);

            return CreatedAtAction(nameof(Get), new { id = reserva.idReserva }, reserva);
        }

        [HttpPut("{id}")]
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

