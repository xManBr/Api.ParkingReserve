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
    public class VagaController : ControllerBase
    {
        private readonly IVagaService _vagaService;
        private IEstacionamentoService _estacionamentoService;

        public VagaController(IVagaService vagaService, IEstacionamentoService estacionamentoService)
        {
            _vagaService = vagaService;
            _estacionamentoService = estacionamentoService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<Vaga>> Get()
        {
            return _vagaService.Consultar();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<Vaga> Get(string id)
        {
            var est = _vagaService.Consultar(id);
            if (est == null)
            {
                return NotFound($"Vaga id: {id} não encontrada");
            }
            return est;
        }

        [HttpPost]
        [Authorize(Roles = Config.ROLE_ESTACIONAMENTO)]
        public ActionResult<Vaga> Post([FromBody] Vaga vaga)
        {
            if ((vaga.idEstacionamento == null) || (vaga.idEstacionamento == string.Empty))
            {
                return NotFound("É preciso definir a qual estacionamento a vaga pertence.");
            }
            else
            {
                var existeEstacionamnto = _estacionamentoService.Consultar(vaga.idEstacionamento);
                if (existeEstacionamnto == null)
                {
                    return NotFound($"A Vaga está se referindo ao estacionamento id {vaga.idEstacionamento} que não exsite.");
                }
                else
                {
                    _vagaService.Cadastrar(vaga);

                    return CreatedAtAction(nameof(Get), new { id = vaga.idVaga }, vaga);
                }
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Config.ROLE_ESTACIONAMENTO)]
        public ActionResult Put(string id, [FromBody] Vaga vaga)
        {
            var existe = _vagaService.Consultar(id);
            if (existe == null)
            {
                return NotFound($"Vaga id: {id} não encontrada");
            }
            else
            {
                var existeEstacionamnto = _estacionamentoService.Consultar(vaga.idEstacionamento);
                if (existeEstacionamnto == null)
                {
                    return NotFound($"A Vaga está se referindo ao estacionamento id {vaga.idEstacionamento} que não exsite.");
                }
                else
                {
                    var idUsuario = HttpContext.User.Identity.Name;
                    if (existeEstacionamnto.idUsuario != idUsuario)
                    {
                        return NotFound($"A vaga id {id}, está associada ao estacionamento  id: {id} que não pertence ao usuário registrado(logado).");
                    }
                    else
                    {

                        _vagaService.Alterar(id, vaga);

                        return NoContent();
                    }
                }
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Config.ROLE_ESTACIONAMENTO)]
        public ActionResult Delete(string id)
        {
            var vaga = _vagaService.Consultar(id);
            if (vaga == null)
            {
                return NotFound($"Vaga id: {id} não encontrada");
            }
            else
            {
                var existeEstacionamnto = _estacionamentoService.Consultar(vaga.idEstacionamento);
                if (existeEstacionamnto == null)
                {
                    return NotFound($"A Vaga está se referindo ao estacionamento id {vaga.idEstacionamento} que não exsite.");
                }
                else
                {
                    var idUsuario = HttpContext.User.Identity.Name;
                    if (existeEstacionamnto.idUsuario != idUsuario)
                    {
                        return NotFound($"A vaga id {id}, está associada ao estacionamento  id: {id} que não pertence ao usuário registrado(logado).");
                    }
                    else
                    {
                        _vagaService.Deletar(id);
                        return Ok($"Vaga {id} Deletado!");
                    }
                }
            }
        }

        [HttpPut("Habilitar/{id}")]
        [Authorize(Roles = Config.ROLE_ESTACIONAMENTO)]
        public ActionResult Habilitar(string id)
        {
            var vaga = _vagaService.Consultar(id);
            if (vaga == null)
            {
                return NotFound($"Vaga id: {id} não encontrada");
            }
            else
            {
                var existeEstacionamnto = _estacionamentoService.Consultar(vaga.idEstacionamento);
                if (existeEstacionamnto == null)
                {
                    return NotFound($"A Vaga está se referindo ao estacionamento id {vaga.idEstacionamento} que não exsite.");
                }
                else
                {
                    var idUsuario = HttpContext.User.Identity.Name;
                    if (existeEstacionamnto.idUsuario != idUsuario)
                    {
                        return NotFound($"A vaga id {id}, está associada ao estacionamento  id: {id} que não pertence ao usuário registrado(logado).");
                    }
                    else
                    {
                        _vagaService.Habilitar(id);
                        return Ok($"Vaga {id} Habilitada!");
                    }
                }
            }
        }

        [HttpPut("Desabilitar/{id}")]
        [Authorize(Roles = Config.ROLE_ESTACIONAMENTO)]
        public ActionResult Desabilitar(string id)
        {
            var vaga = _vagaService.Consultar(id);
            if (vaga == null)
            {
                return NotFound($"Vaga id: {id} não encontrada");
            }
            else
            {
                var existeEstacionamnto = _estacionamentoService.Consultar(vaga.idEstacionamento);
                if (existeEstacionamnto == null)
                {
                    return NotFound($"A Vaga está se referindo ao estacionamento id {vaga.idEstacionamento} que não exsite.");
                }
                else
                {
                    var idUsuario = HttpContext.User.Identity.Name;
                    if (existeEstacionamnto.idUsuario != idUsuario)
                    {
                        return NotFound($"A vaga id {id}, está associada ao estacionamento  id: {id} que não pertence ao usuário registrado(logado).");
                    }
                    else
                    {
                        _vagaService.Desabilitar(id);
                        return Ok($"Vaga {id} Desabilitado!");
                    }
                }
            }
        }

        [HttpGet("ConsultarVagaSemReserva/{idEstacionamento}")]
        [AllowAnonymous]
        public ActionResult<List<Vaga>> ConsultarVagaSemReserva(string idEstacionamento)
        {
            var vagas = _vagaService.ConsultarVagaSemReserva(idEstacionamento);

            if (vagas.Count == 0)
            {
                return NotFound($"Não há mais vagas disponíveis para reserva no estationamento id : {idEstacionamento}");
            }
            return vagas;
        }
    }
}
