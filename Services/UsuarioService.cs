using Api.ParkingReserve.Globais;
using Api.ParkingReserve.Interfaces;
using Api.ParkingReserve.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ParkingReserve.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMongoCollection<Usuario> _usuario;
        private readonly ISecurityService _securityService;
        private readonly string _emailDosAdministradores;

        public UsuarioService(IParkingReserveDatabaseSettings settings, IMongoClient mongoClient, ISecurityService securityService)
        {
            var database = mongoClient.GetDatabase(settings.dataBaseName);
            _usuario = database.GetCollection<Usuario>(settings.usuarioCollectionsName);
            _securityService = securityService;
            _emailDosAdministradores = settings.emailDosAdministradores;
        }

        public Usuario Alterar(string id, Usuario usuario)
        {
            var filter = Builders<Usuario>.Filter.Eq("idUsuario", id);

            var update = Builders<Usuario>.Update
                        .Set("telefone", usuario.telefone)
                        .Set("perfilCondutor", usuario.perfilCondutor)
                        .Set("perfilEstacionamento", usuario.perfilEstacionamento);

            _usuario.UpdateOne(filter, update);

            return usuario;
        }

        public ActionResult<dynamic> Cadastrar(Usuario usuario)
        {
            var filter = _usuario.Find(x => x.email == usuario.email.Trim()).FirstOrDefault();
            if (filter != null)
            {
                return new { message = $"E-mail: {usuario.email} já foi cadastro no sistema!" };
            }
            else
            {
                usuario.idUsuario = string.Empty;
                usuario.situacao = Config.SITUACAO_USUARIO_HABILITADO;
                usuario.senha = _securityService.Emcritar(usuario.senha);
                var adms = _emailDosAdministradores.Split(' ').ToList();
                if (adms.Contains(usuario.email))
                {
                    usuario.perfilAdministrador = true;
                }
                _usuario.InsertOne(usuario);
                return usuario;
            }
        }

        public List<Usuario> Consultar()
        {
            var usuarios = _usuario.Find(e => true).ToList();

            return usuarios;
        }

        public Usuario Consultar(string id)
        {
            var usuario = _usuario.Find(e => e.idUsuario == id).FirstOrDefault();

            return usuario;
        }

        public void Deletar(string id)
        {
            _usuario.DeleteOne(e => e.idUsuario == id);
        }

        public void Desabilitar(string id)
        {
            this.MudarSituacao(id, Config.SITUACAO_USUARIO_DESABILITADO);
        }

        public void Habilitar(string id)
        {
            this.MudarSituacao(id, Config.SITUACAO_USUARIO_HABILITADO);
        }

        private void MudarSituacao(string id, string novaSituacao)
        {
            var filter = Builders<Usuario>.Filter.Eq("idUsuario", id);

            var update = Builders<Usuario>.Update
                .Set("situacao", novaSituacao);

            _usuario.UpdateOne(filter, update);
        }

        public ActionResult<dynamic> Login(string email, string senha)
        {
            var usuario = _usuario.Find(e => e.email == email).FirstOrDefault();

            if (usuario == null)
            {
                return new { message = $"Usuário {email} não nadastrado." };
            }
            if (usuario.situacao == Config.SITUACAO_USUARIO_DESABILITADO)
            {
                return new { message = $"Usuário {email} id {usuario.idUsuario} está desabilitado - entre em contato com o administrador." };
            }
            else
            {
                if (usuario.senha != _securityService.Emcritar(senha))
                {
                    return new { message = "Usuário e senha não Conferem." };
                }
                else
                {
                    var token = _securityService.GerarToken(email, usuario.idUsuario, usuario.perfilCondutor, usuario.perfilEstacionamento, usuario.perfilAdministrador);
                    usuario.senha = "******";
                    usuario.lembreteSenha = "******";

                    return new Autenticacao
                    {
                        message = "OK",
                        usuario = usuario,
                        token = "Bearer " + token
                    };
                }
            }
        }
    }
}
