using Api.ParkingReserve.Interfaces;
using Api.ParkingReserve.Models;
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
        public ActionResult<List<Usuario>> Get()
        {
            return _usuarioService.Consultar();
        }

        [HttpGet("{id}")]
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
        public ActionResult<Usuario> Post([FromBody] Usuario usuario)
        {
            _usuarioService.Cadastrar(usuario);

            return CreatedAtAction(nameof(Get), new { id = usuario.idUsuario }, usuario);
        }

        [HttpPut("{id}")]
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
        public ActionResult<dynamic> Login([FromBody] Login login)
        {
            var autenticacao = _usuarioService.Login(login.email, login.senha);

            return autenticacao;
        }

    }
}
