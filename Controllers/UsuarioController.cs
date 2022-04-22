using Api.ParkingReserve.Globais;
using Api.ParkingReserve.Interfaces;
using Api.ParkingReserve.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Api.ParkingReserve.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Authorize]
        [Authorize(Roles = Config.ROLE_ADMINISTRADOR)]
        public ActionResult<List<Usuario>> Get()
        {
            return _usuarioService.Consultar();
        }

        [HttpGet("{id}")]
        [Authorize]
        [Authorize(Roles = Config.ROLE_ADMINISTRADOR)]
        public ActionResult<Usuario> Get(string id)
        {
            var est = _usuarioService.Consultar(id);
            if (est == null)
            {
                return NotFound($"Usuario id: {id} não encontrado");
            }
            return est;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<dynamic> Post([FromBody] Usuario usuario)
        {
            _usuarioService.Cadastrar(usuario);

            return _usuarioService.Cadastrar(usuario);
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Put(string id, [FromBody] Usuario usuario)
        {
            var existe = _usuarioService.Consultar(id);

            if (existe == null)
            {
                return NotFound($"Usuario id: {id} não encontrado");
            }

            _usuarioService.Alterar(id, usuario);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Config.ROLE_ADMINISTRADOR)]
        public ActionResult Delete(string id)
        {
            var existe = _usuarioService.Consultar(id);

            if (existe == null)
            {
                return NotFound($"Usuario id: {id} não encontrado");
            }

            _usuarioService.Deletar(id);

            return Ok($"Usuario {id} Deletado!");
        }

        [HttpPut("Habilitar/{id}")]
        [Authorize(Roles = Config.ROLE_ADMINISTRADOR)]
        public ActionResult Habilitar(string id)
        {
            var existe = _usuarioService.Consultar(id);

            if (existe == null)
            {
                return NotFound($"Usuario id: {id} não encontrado");
            }

            _usuarioService.Habilitar(id);

            return Ok($"Usuario {id} Habilitado!");
        }

        [HttpPut("Desabilitar/{id}")]
        [Authorize(Roles = Config.ROLE_ADMINISTRADOR)]
        public ActionResult Desabilitar(string id)
        {
            var existe = _usuarioService.Consultar(id);

            if (existe == null)
            {
                return NotFound($"Usuario id: {id} não encontrado");
            }

            _usuarioService.Desabilitar(id);

            return Ok($"Usuario {id} Desabilitado!");
        }
                    
        [HttpPost("Login")]
        [AllowAnonymous]
        public ActionResult<dynamic> Login([FromBody] Login login)
        {
            var autenticacao = _usuarioService.Login(login.email, login.senha);

            return autenticacao;
        }

    }
}
