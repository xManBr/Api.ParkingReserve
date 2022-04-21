using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ParkingReserve.Globais
{
    public class Config
    {

        private const string habilitado = "Habilitado";
        private const string habilitada = "Habilitada";

        private const string desabilitado = "Desabilitado";
        private const string desabilitada = "Desabilitada";

        public static string SITUACAO_RESERVA_DESABILITADA = desabilitada;
        public static string SITUACAO_RESERVA_HABILITADA = habilitada;

        public static string SITUACAO_VAGA_DESABILITADA = desabilitada;
        public static string SITUACAO_VAGA_HABILITADA = habilitada;


        public static string SITUACAO_ESTACIONAMENTO_DESABILITADO = desabilitado;
        public static string SITUACAO_ESTACIONAMENTO_HABILITADO = habilitado;


        public static string SITUACAO_USUARIO_DESABILITADO = desabilitado;
        public static string SITUACAO_USUARIO_HABILITADO = habilitado;
    }
}
