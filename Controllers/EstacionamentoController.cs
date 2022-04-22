using Api.ParkingReserve.Globais;
using Api.ParkingReserve.Interfaces;
using Api.ParkingReserve.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.ParkingReserve.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstacionamentoController : ControllerBase
    {
        private readonly IEstacionamentoService _estacionamentoService;

        public EstacionamentoController(IEstacionamentoService estacionamentoService)
        {
            _estacionamentoService = estacionamentoService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<Estacionamento>> Get()
        {
            return _estacionamentoService.Consultar();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<Estacionamento> Get(string id)
        {
            var est = _estacionamentoService.Consultar(id);
            if (est == null)
            {
                return NotFound($"Estacionamento id: {id} não encontrado");
            }
            return est;
        }

        [HttpPost]
        [Authorize(Roles = Config.ROLE_ESTACIONAMENTO)]
        public ActionResult<Estacionamento> Post([FromBody] Estacionamento estacionamento)
        {
            estacionamento.idUsuario = HttpContext.User.Identity.Name;
            _estacionamentoService.Cadastrar(estacionamento);

            return CreatedAtAction(nameof(Get), new { id = estacionamento.idEstacionamento }, estacionamento);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Config.ROLE_ESTACIONAMENTO)]
        public ActionResult Put(string id, [FromBody] Estacionamento estacionamento)
        {
            var idUsuarioCorrent = HttpContext.User.Identity.Name;
            if (estacionamento.idUsuario != idUsuarioCorrent)
            {
                return NotFound($"O Estacionamento id: {id} não pertence ao usuário registrado(logado).");
            }
            else
            {
                var existe = _estacionamentoService.Consultar(id);

                if (existe == null)
                {
                    return NotFound($"Estacionamento id: {id} não encontrado");
                }

                _estacionamentoService.Alterar(id, estacionamento);

                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Config.ROLE_ESTACIONAMENTO)]
        public ActionResult Delete(string id)
        {
            var existe = _estacionamentoService.Consultar(id);

            if (existe == null)
            {
                return NotFound($"Estacionamento id: {id} não encontrado");
            }

            var idUsuarioCorrent = HttpContext.User.Identity.Name;
            if (existe.idUsuario != idUsuarioCorrent)
            {
                return NotFound($"O Estacionamento id: {id} não pertence ao usuário registrado(logado).");
            }
            else
            {
                _estacionamentoService.Deletar(id);

                return Ok($"Estacionamento {id} Deletado!");
            }
        }

        [HttpPut("Habilitar/{id}")]
        [Authorize(Roles = Config.ROLE_ESTACIONAMENTO)]
        public ActionResult Habilitar(string id)
        {
            var existe = _estacionamentoService.Consultar(id);

            if (existe == null)
            {
                return NotFound($"Estacionamento id: {id} não encontrado");
            }

            var idUsuarioCorrent = HttpContext.User.Identity.Name;
            if (existe.idUsuario != idUsuarioCorrent)
            {
                return NotFound($"O Estacionamento id: {id} não pertence ao usuário registrado(logado).");
            }
            else
            {
                _estacionamentoService.Habilitar(id);

                return Ok($"Estacionamento {id} Habilitado!");
            }
        }

        [HttpPut("Desabilitar/{id}")]
        [Authorize(Roles = Config.ROLE_ESTACIONAMENTO)]
        public ActionResult Desabilitar(string id)
        {
            var existe = _estacionamentoService.Consultar(id);

            if (existe == null)
            {
                return NotFound($"Estacionamento id: {id} não encontrado");
            }
            var idUsuarioCorrent = HttpContext.User.Identity.Name;
            if (existe.idUsuario != idUsuarioCorrent)
            {
                return NotFound($"O Estacionamento id: {id} não pertence ao usuário registrado(logado).");
            }
            else
            {
                _estacionamentoService.Desabilitar(id);

                return Ok($"Estacionamento {id} Desabilitado!");
            }
        }
    }
}
