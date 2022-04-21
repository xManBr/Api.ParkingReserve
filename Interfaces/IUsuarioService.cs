﻿using Api.ParkingReserve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ParkingReserve.Interfaces
{
    public interface IUsuarioService
    {
        List<Usuario> Consultar();
        Usuario Consultar(string id);
        Usuario Cadastrar(Usuario usuario);
        Usuario Alterar(string id, Usuario usuario);
        void Deletar(string id);
        void Desabilitar(string id);
        void Habilitar(string id);

        string Login(string email, string senha);

    }
}
