using Api.ParkingReserve.Globais;
using Api.ParkingReserve.Interfaces;
using Api.ParkingReserve.Models;
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
        private readonly ITokenService _tokenService;

        public UsuarioService(IParkingReserveDatabaseSettings settings, IMongoClient mongoClient, ITokenService tokenService)
        {
            var database = mongoClient.GetDatabase(settings.dataBaseName);
            _usuario = database.GetCollection<Usuario>(settings.usuarioCollectionsName);
            _tokenService = tokenService;
        }

        public Usuario Alterar(string id, Usuario usuario)
        {
            var filter = Builders<Usuario>.Filter.Eq("idUsuario", id);

            var update = Builders<Usuario>.Update
                        .Set("email", usuario.email)
                        .Set("telefone", usuario.telefone)
                        .Set("perfilCondutor", usuario.perfilCondutor)
                        .Set("perfilEstacionamento", usuario.perfilEstacionamento);

            _usuario.UpdateOne(filter, update);

            return usuario;
        }

        public Usuario Cadastrar(Usuario usuario)
        {
            usuario.idUsuario = string.Empty;
            usuario.situacao = Config.SITUACAO_USUARIO_HABILITADO;
            _usuario.InsertOne(usuario);

            return usuario;
        }

        public List<Usuario> Consultar()
        {
            return _usuario.Find(e => true).ToList();
        }

        public Usuario Consultar(string id)
        {
            return _usuario.Find(e => e.idUsuario == id).FirstOrDefault();
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

        public string Login(string email, string senha)
        {
            var usuario = _usuario.Find(e => e.email == email).FirstOrDefault();

            if (usuario == null)
            {
                return $"Usuário {email} não nadastrado.";
            }
            if (usuario.situacao == Config.SITUACAO_USUARIO_DESABILITADO)
            {
                return $"Usuário {email} id {usuario.idUsuario} está desabilitado.";
            }
            else
            {
                if (usuario.senha != senha)
                {
                    return "Usuário e senha não Conferem.";
                }
                else
                {
                    return _tokenService.GerarToken(email, usuario.idUsuario, usuario.perfilCondutor, usuario.perfilEstacionamento);
                }
            }
        }
    }
}
