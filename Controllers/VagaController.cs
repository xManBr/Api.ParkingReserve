﻿using Api.ParkingReserve.Interfaces;
using Api.ParkingReserve.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Api.ParkingReserve.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VagaController : ControllerBase
    {
        private readonly IVagaService _vagaService;

        public VagaController(IVagaService vagaService)
        {
            _vagaService = vagaService;
        }

        [HttpGet]
        public ActionResult<List<Vaga>> Get()
        {
            return _vagaService.Consultar();
        }

        [HttpGet("{id}")]
        public ActionResult<Vaga> Get(string id)
        {
            var est = _vagaService.Consultar(id);
            if (est == null)
            {
                return NotFound($"Vaga id: {id} não encontrado");
            }
            return est;
        }

        [HttpPost]
        public ActionResult<Vaga> Post([FromBody] Vaga vaga)
        {
            _vagaService.Cadastrar(vaga);

            return CreatedAtAction(nameof(Get), new { id = vaga.idVaga }, vaga);
        }

        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Vaga vaga)
        {
            var existe = _vagaService.Consultar(id);

            if (existe == null)
            {
                return NotFound($"Vaga id: {id} não encontrado");
            }

            _vagaService.Alterar(id, vaga);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var existe = _vagaService.Consultar(id);

            if (existe == null)
            {
                return NotFound($"Vaga id: {id} não encontrado");
            }

            _vagaService.Deletar(id);

            return Ok($"Vaga {id} Deletado!");
        }

        [HttpPut("Habilitar/{id}")]
        public ActionResult Habilitar(string id)
        {
            var existe = _vagaService.Consultar(id);

            if (existe == null)
            {
                return NotFound($"Vaga id: {id} não encontrado");
            }

            _vagaService.Habilitar(id);

            return Ok($"Vaga {id} Habilitado!");
        }

        [HttpPut("Desabilitar/{id}")]
        public ActionResult Desabilitar(string id)
        {
            var existe = _vagaService.Consultar(id);

            if (existe == null)
            {
                return NotFound($"Vaga id: {id} não encontrado");
            }

            _vagaService.Desabilitar(id);

            return Ok($"Vaga {id} Desabilitado!");
        }

        [HttpGet("ConsultarVagaSemReserva/{idEstacionamento}")]
        public ActionResult<List<Vaga>> ConsultarVagaSemReserva(string idEstacionamento)
        {;
            var vagas = _vagaService.ConsultarVagaSemReserva(idEstacionamento);

            if (vagas.Count == 0)
            {
                return NotFound($"Não há mais vagas disponíveis para reserva no estationamento id : {idEstacionamento}");
            }
            return vagas;
        }


    }
}
